using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secretary;

namespace FairyJam
{
    class Game
    {

        public Window window;
        private Hotkey left = new Hotkey(true).AddKey(Key.A).AddKey(Key.Left);
        private Hotkey right = new Hotkey(true).AddKey(Key.D).AddKey(Key.Right);
        private Hotkey up = new Hotkey(true).AddKey(Key.W).AddKey(Key.Up);
        private Hotkey down = new Hotkey(true).AddKey(Key.S).AddKey(Key.Down);

        public List<DrawnButton> buttons = new List<DrawnButton>();

        public Game(Window window)
        {
            this.window = window;
            OnLoad();
            ReadFiles();
        }

        public void OnLoad()
        {
            Globals.map = new Map(100, 100);

            buttons.Add(new DrawnButton("test", 0, 0, 200, 100, () => { Window.window.ToggleShader(Shaders.basic); }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("test2", 0, 105, 200, 100, () => { Window.window.ToggleShader(Shaders.blur); }, 0.5f, 0.5f, 0.5f));
        }

        private void ReadFiles()
        {
            try
            {
                string[] traitLines = FileHandler.Read("People/traits.txt");
                Globals.possibleTraits = ParseTraits(traitLines);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
        }

        private Trait[] ParseTraits(string[] lines)
        {
            List<Trait> list = new List<Trait>();
            Trait trait = new Trait();

            foreach (string line in lines)
            {
                line.Trim();

                if (line == "{")
                {
                    trait = new Trait();
                } else if (line == "}")
                {
                    list.Add(trait);
                }else
                {
                    string[] words = line.Split('=');
                    foreach(string word in words)
                    {
                        word.Trim();
                    }
                    switch (words[0])
                    {
                        case "population_growth":
                            Console.WriteLine("Not yet implemented");
                            break;
                        default:
                            Globals.logger.Log("Trait action was unknown", LogLevel.WARNING);
                            break;
                    }
                }
            }

            return list.ToArray();
        }

        public void Update(double delta)
        {
            Globals.DeltaTime = delta;

            //Updating logic
            if (left.IsDown()) Window.camX -= (float)(10 * delta);
            if (right.IsDown()) Window.camX += (float)(10 * delta);
            if (up.IsDown()) Window.camY -= (float)(10 * delta);
            if (down.IsDown()) Window.camY += (float)(10 * delta);
        }

        public void Draw()
        {
            //Do all you draw calls here
            Globals.map.Draw();


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
