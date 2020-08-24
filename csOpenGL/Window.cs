using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using QuickFont;
using SharpFont;

namespace FairyJam
{

    class DrawList
    {

        private SData[] data;
        private int size = 0, max = 10;

        public DrawList()
        {
            data = new SData[max];
        }

        public void Add(SData s)
        {
            if(size == max)
            {
                SData[] n = new SData[max * 2];
                for(int i = 0; i < max; i++)
                {
                    n[i] = data[i];
                }
                data = n;
                max *= 2;
            }
            data[size] = s;
            size++;
        }

        public SData[] getData()
        {
            return data;
        }

        public void Clear()
        {
            size = 0;
        }

        public int Count()
        {
            return size;
        }

    }

    class Window : GameWindow
    {
        public static DrawList sd, lateDraw = new DrawList();
        public static float screenScaleX, screenScaleY, camX = 0, camY = 0;
        public static Window window;

        private MainShader mainShader;
        private List<Shader> shaders = new List<Shader>();
        private List<bool> enabledShaders = new List<bool>();
        private int numOfShaders = 0, frameBuffer0 = -1, frameBuffer1 = -1;
        private long frameBufferTexture0 = -1, frameBufferTexture1 = -1;
        private bool noobMode = false;

        QFont font;
        QFontDrawing textDrawing;
        private double delta;
        private Game game;
        public int mouseX, mouseY;

        public Window(int w, int h, string title) : base(w, h, GraphicsMode.Default, title)
        {
            Window.sd = new DrawList();
            Window.window = this;
        }

        protected override void OnLoad(EventArgs e)
        {
            //basic window stuff
            WindowBorder = WindowBorder.Hidden;
            WindowState = WindowState.Fullscreen;

            GL.ClearColor(0.05f, 0.05f, 0.05f, 1.0f);
            Globals.Width = Width;
            Globals.Height = Height;

            //loading the Shaders
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            string s = GL.GetString(StringName.Vendor);
            noobMode = GL.GetString(StringName.Vendor).Equals("Intel");
            mainShader = new MainShader(!noobMode);
            AddShader(mainShader);
            CreatePostShaders();


            //Creating frame buffers
            frameBuffer0 = createFrameBuffer(FramebufferAttachment.ColorAttachment0, ref frameBufferTexture0);
            frameBuffer1 = createFrameBuffer(FramebufferAttachment.ColorAttachment1, ref frameBufferTexture1);
            GL.DrawBuffers(2, new DrawBuffersEnum[]{ DrawBuffersEnum.ColorAttachment0, DrawBuffersEnum.ColorAttachment1 });

            //Font
            font = new QFont("Fonts/arial.ttf", 36*Height/1080.0f, new QuickFont.Configuration.QFontBuilderConfiguration(true));
            textDrawing = new QFontDrawing();
            Matrix4 m = Matrix4.Identity;
            m.M11 /= (float)(1920.0/2);
            m.M22 /= (float)(1080.0/2);
            textDrawing.ProjectionMatrix = m;

            //Textures
            Textures.Load();

            game = new Game(this);
            base.OnLoad(e);
        }

        private int createFrameBuffer(FramebufferAttachment fba, ref long rtHandler)
        {
            int fb = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fb);

            int rt = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, rt);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba32f, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, new IntPtr());

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, 0x2601);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, 0x2601);

            GL.FramebufferTexture(FramebufferTarget.Framebuffer, fba, rt, 0);

            rtHandler = GL.Arb.GetImageHandle(rt, 0, false, 0, (PixelFormat)0x8814);

            return fb;
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            Window.screenScaleX = Width / 1920.0f;
            Window.screenScaleY = Height / 1080.0f;
            Globals.Width = Width;
            Globals.Height = Height;
            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            delta = e.Time*60;
            Update();
            foreach (Texture t in Textures.list)
            {
                GL.Arb.MakeImageHandleResident(t.Handle, All.ReadOnly);
            }
            
            GL.Viewport(0, 0, Width, Height);

            if (noobMode)
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                mainShader.late = false;
                mainShader.Use(0);
                mainShader.late = true;
                mainShader.Use(0, false);
            }
            else
            {
                int shadersDone = 0;
                long prev = -1;
                for (int i = 0; i < shaders.Count; i++)
                {
                    if (enabledShaders[i])
                    {
                        shadersDone++;
                        int fb;
                        long rt;
                        if (shadersDone % 2 == 0)
                        {
                            fb = frameBuffer1;
                            rt = frameBufferTexture1;
                        }
                        else
                        {
                            fb = frameBuffer0;
                            rt = frameBufferTexture0;
                        }
                        GL.BindFramebuffer(FramebufferTarget.Framebuffer, fb);
                        shaders[i].Use(prev);
                        prev = rt;
                    }
                }

                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                mainShader.RunLate(prev);
            }
            textDrawing.RefreshBuffers();
            textDrawing.Draw();

            Context.SwapBuffers();

            Window.sd.Clear();
            Window.lateDraw.Clear();
            foreach (Texture t in Textures.list)
            {
                GL.Arb.MakeImageHandleNonResident(t.Handle);
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            game.MouseDown(e, mouseX, mouseY);
            
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            game.MouseUp(e, mouseX, mouseY);
            if (Globals.currentState == GameState.MAPVIEW)
            {
                Globals.map.MouseDown(e, mouseX, mouseY);
            }
            
            base.OnMouseDown(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if(e.Delta > 0)
            {
                game.MouseWheelScrollUp();
            }
            else if(e.Delta < 0)
            {
                game.MouseWheelScrollDown();
            }
        }

        private void Update()
        {
            //input
            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            Hotkey.Update(input);
            mouseX = window.mouseX;
            mouseY = window.mouseY;
            //other shit
            textDrawing.DrawingPrimitives.Clear();
            game.Update(delta);
            game.Draw();
            SoundManager.Update();
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            mouseX = (int)(e.X / screenScaleX);
            mouseY = (int)(e.Y / screenScaleY);
            base.OnMouseMove(e);
        }

        public void DrawText(string text, int x, int y, QFont f = null)
        {
            if(f == null)
            {
                f = font;
            }
            textDrawing.Print(f, text, new Vector3(x-960, 540-y, 0), QFontAlignment.Left);
        }

        public void DrawText(string text, int x, int y, float r, float g, float b, float a, QFont f = null)
        {
            if (f == null)
            {
                f = font;
            }
            QFontRenderOptions opt = new QFontRenderOptions() { Colour = Color.FromArgb(new Color4(r, g, b, a).ToArgb()) };
            textDrawing.Print(f, text, new Vector3(x - 960, 540 - y, 0), QFontAlignment.Left, opt);
        }

        public void DrawTextCentered(string text, int x, int y, QFont f = null)
        {
            if (f == null)
            {
                f = font;
            }
            textDrawing.Print(f, text, new Vector3(x - 960, 540 - y, 0), QFontAlignment.Centre);
        }

        public void DrawTextCentered(string text, int x, int y, float r, float g, float b, float a, QFont f = null)
        {
            if (f == null)
            {
                f = font;
            }
            QFontRenderOptions opt = new QFontRenderOptions() { Colour = Color.FromArgb(new Color4(r, g, b, a).ToArgb()) };
            textDrawing.Print(f, text, new Vector3(x - 960, 540 - y, 0), QFontAlignment.Centre, opt);
        }

        public void AddShader(Shader s, bool enabled = true)
        {
            shaders.Add(s);
            enabledShaders.Add(enabled);
            numOfShaders += enabled ? 1 : 0;
        }

        public void SetShaderEnabled(int shaderNum, bool enabled)
        {
            if (shaderNum >= enabledShaders.Count) return;
            numOfShaders -= enabledShaders[shaderNum] ? 1 : 0;
            numOfShaders += enabled ? 1 : 0;
            enabledShaders[shaderNum] = enabled;
        }

        public void ToggleShader(int shaderNum)
        {
            if (shaderNum >= enabledShaders.Count) return;
            bool enabled = !enabledShaders[shaderNum];
            numOfShaders -= enabledShaders[shaderNum] ? 1 : 0;
            numOfShaders += enabled ? 1 : 0;
            enabledShaders[shaderNum] = enabled;
        }

        private void CreatePostShaders()
        {
            //AddShader(new BasicPostShader());
            //AddShader(new BlurShader());
            //AddShader(new BloomShader());
        }

    }
}
