using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class Game
    {

        public Window window;
        private Timer timer;
        private Hotkey left = new Hotkey(true).AddKey(Key.A).AddKey(Key.Left);
        private Hotkey right = new Hotkey(true).AddKey(Key.D).AddKey(Key.Right);
        private Hotkey up = new Hotkey(true).AddKey(Key.W).AddKey(Key.Up);
        private Hotkey down = new Hotkey(true).AddKey(Key.S).AddKey(Key.Down);

        private Sprite s = new Sprite(1920, 1080, 0, Textures.Get(Textures.test));
        public List<DrawnButton> buttons = new List<DrawnButton>();

        public Game(Window window)
        {
            this.window = window;
            this.timer = new Timer(60000);
            OnLoad();
        }

        public void OnLoad()
        {
            buttons.Add(new DrawnButton("test", 0, 0, 200, 100, () => { Window.window.ToggleShader(Shaders.basic); }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("test2", 0, 105, 200, 100, () => { Window.window.ToggleShader(Shaders.blur); }, 0.5f, 0.5f, 0.5f));
            Globals.currentState = GameState.MAINMENU;

            buttons.Add(new DrawnButton("Play", 1920 / 2 - 100, 1080 / 2 - 60, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("Tutorial", 1920 / 2 - 100, 1080 / 2 + 60, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("Settings", 1920 / 2 - 100, 1080 / 2 + 180, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("Quit", 1920 / 2 - 100, 1080 / 2 + 300, 200, 100, () => { window.Exit(); }, 0.5f, 0.5f, 0.5f));
        }

        public void Update(double delta)
        {
            Globals.DeltaTime = delta;
            timer.UpdateTimer();

            //if (timer.Expired())
            //{
            //    window.Exit();
            //}

            //Updating logic
            if (left.IsDown()) Window.camX -= (float)(10 * delta);
            if (right.IsDown()) Window.camX += (float)(10 * delta);
            if (up.IsDown()) Window.camY -= (float)(10 * delta);
            if (down.IsDown()) Window.camY += (float)(10 * delta);
        }

        public void Draw()
        {
            //Do all you draw calls here
            s.Draw(0, 0);
            foreach (DrawnButton button in buttons)
            {
                button.Draw();
            }
        }

        public void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if (e.Button == MouseButton.Left)
            {
                for (int i = buttons.Count - 1; i >= 0; i--)
                {
                    DrawnButton button = buttons[i];
                    if (button.IsInButton(mx, my))
                    {
                        button.OnClick();
                        break;
                    }
                }
            }
        }

        public void MouseUp(MouseButtonEventArgs e, int mx, int my)
        {

        }
        
    }
}
