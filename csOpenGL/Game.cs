using FairyJam.Orbitals;
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
        private Timer timer;
        private Hotkey left = new Hotkey(true).AddKey(Key.A).AddKey(Key.Left);
        private Hotkey right = new Hotkey(true).AddKey(Key.D).AddKey(Key.Right);
        private Hotkey up = new Hotkey(true).AddKey(Key.W).AddKey(Key.Up);
        private Hotkey down = new Hotkey(true).AddKey(Key.S).AddKey(Key.Down);

        public List<DrawnButton> buttons = new List<DrawnButton>();

        private PlanetarySystem ps = new PlanetarySystem();

        public Game(Window window)
        {
            this.window = window;
            this.timer = new Timer(60000);
            OnLoad();
            ReadFiles();
        }

        public void OnLoad()
        {
            Globals.map = new Map(100, 100);

            buttons.Add(new DrawnButton("test", 0, 0, 200, 100, () => { Window.window.ToggleShader(Shaders.basic); }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("test2", 0, 105, 200, 100, () => { Window.window.ToggleShader(Shaders.blur); }, 0.5f, 0.5f, 0.5f));
            Globals.currentState = GameState.MAINMENU;

            //buttons.Add(new DrawnButton("Play", 1920 / 2 - 100, 1080 / 2 - 60, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            //buttons.Add(new DrawnButton("Tutorial", 1920 / 2 - 100, 1080 / 2 + 60, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            //buttons.Add(new DrawnButton("Settings", 1920 / 2 - 100, 1080 / 2 + 180, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            //buttons.Add(new DrawnButton("Quit", 1920 / 2 - 100, 1080 / 2 + 300, 200, 100, () => { window.Exit(); }, 0.5f, 0.5f, 0.5f));

            ps.Generate(Globals.random.Next(1,10));
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

            Person[] possiblePeople = new Leader[25];
            for(int i = 0; i < 25; i++)
            {
                Trait trait = Globals.possibleTraits[Globals.random.Next(Globals.possibleTraits.Length)];
                List<Trait> traitsToAdd = new List<Trait>();
                traitsToAdd.Add(trait);
                possiblePeople[i] = new Leader(100, "Yu Ri", "Kwon", Enums.LeaderTitle.Admiral, traitsToAdd, true);
            }
            int t = possiblePeople.Length;

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
                    switch (words[0].Trim())
                    {
                        case "name":
                            trait.Name = words[1].Trim();
                            break;
                        case "population_growth":
                            Globals.logger.Log("Trait action `" + words[0] + "` is not yet implemented", LogLevel.DEBUG);
                            break;
                        default:
                            Globals.logger.Log("Trait action `"+ words[0] +"` was unknown", LogLevel.WARNING);
                            break;
                    }
                }
            }

            return list.ToArray();
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

            ps.Update();
        }

        public void Draw()
        {
            //Do all you draw calls here
            Globals.map.Draw();


            foreach (DrawnButton button in buttons)
            {
                button.Draw();
            }

            ps.Draw();
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
