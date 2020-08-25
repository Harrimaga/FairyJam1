using FairyJam.Orbitals;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secretary;
using FairyJam.Technology;

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
        private Hotkey Q = new Hotkey(false).AddKey(Key.Q);

        public List<DrawnButton> buttons = new List<DrawnButton>();


        public Game(Window window)
        {
            this.window = window;
            this.timer = new Timer(60000);
            OnLoad();
            ReadFiles();
        }

        public void OnLoad()
        {
            Globals.map = new Map(20, 20);
            Globals.map.Generate();

            buttons.Add(new DrawnButton("test", 0, 0, 200, 100, () => { Window.window.ToggleShader(Shaders.basic); }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("test2", 0, 105, 200, 100, () => { Window.window.ToggleShader(Shaders.blur); }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("Bloom", 0, 210, 200, 100, () => { Window.window.ToggleShader(Shaders.bloom); }, 0.5f, 0.5f, 0.5f));
            Globals.currentState = GameState.MAPVIEW;

            //buttons.Add(new DrawnButton("Play", 1920 / 2 - 100, 1080 / 2 - 60, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            //buttons.Add(new DrawnButton("Tutorial", 1920 / 2 - 100, 1080 / 2 + 60, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            //buttons.Add(new DrawnButton("Settings", 1920 / 2 - 100, 1080 / 2 + 180, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            //buttons.Add(new DrawnButton("Quit", 1920 / 2 - 100, 1080 / 2 + 300, 200, 100, () => { window.Exit(); }, 0.5f, 0.5f, 0.5f));
        }

        private void ReadFiles()
        {
            try
            {
                string[] traitLines = FileHandler.Read("People/traits.txt");
                Globals.possibleTraits = ParseTraits(traitLines);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                string[] nameLines = FileHandler.Read("People/namelist.txt");
                Globals.nameLists = ParseNameLists(nameLines);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Person[] possiblePeople = new Leader[25];
            Namelist namelist = Globals.nameLists[0]; // @TODO For now just the first on we find, later on allow for selection?
            for(int i = 0; i < 25; i++)
            {
                namelist.Next();
                Trait trait = Globals.possibleTraits[Globals.random.Next(Globals.possibleTraits.Length)]; // Gets a random existing trait
                List<Trait> traitsToAdd = new List<Trait> { trait }; 
                possiblePeople[i] = new Leader(100, namelist.GivenName, namelist.FamilyName, Enums.LeaderTitle.Admiral, traitsToAdd, true);
            }
            int c = possiblePeople.Length;

        }

        private Trait[] ParseTraits(string[] lines)
        {
            List<Trait> list = new List<Trait>();
            Trait trait = new Trait();

            foreach (string line in lines)
            {
                if (line.Trim() == "{")
                {
                    trait = new Trait();
                } else if (line.Trim() == "}")
                {
                    list.Add(trait);
                }
                else
                {
                    string[] words = line.Split('=');
                    switch (words[0].Trim())
                    {
                        case "name":
                            trait.Name = words[1].Trim();
                            break;
                        case "population_growth":
                            Globals.logger.Log("Trait action `" + words[0] + "` is not yet implemented", LogLevel.INFO);
                            break;
                        default:
                            Globals.logger.Log("Trait action `" + words[0] + "` was unknown", LogLevel.WARNING);
                            break;
                    }
                }
            }

            return list.ToArray();
        }

        private Namelist[] ParseNameLists(string[] lines)
        {
            List<Namelist> list = new List<Namelist>();
            Namelist namelist = null;
            bool inList = false;
            bool inListIsFamilyName = false;
            List<string> names = new List<string>();

            foreach(string line in lines)
            {
                if (inList)
                {
                    if (line.Contains('}'))
                    {
                        if (inListIsFamilyName)
                        {
                            namelist.FamilyNames = names.ToArray();
                        }
                        else
                        {
                            namelist.GivenNames = names.ToArray();
                        }
                        names = new List<string>();
                        inList = false;
                    }
                    else
                    {
                        names.Add(line.Trim());
                    }
                } else if (namelist == null && line.Contains('=')) // This is a namelist declaration and we aren't making a namelist already
                {
                    string name = line.Split('=')[0].Trim();
                    namelist = new Namelist(name);
                } else if(namelist != null && line.Contains('}')) // We are making a namelist and we found the closing argument
                {
                    list.Add(namelist);
                    namelist = null;
                } else if(namelist != null && line.Contains('{')) // We are making a namelist and we found an opening argument
                {
                    inList = true;
                    string nameListType = line.Split('=')[0].Trim();
                    inListIsFamilyName = (nameListType == "family");
                }
            }

            return list.ToArray();
        }

        private void ParseTechnologies(string[] lines)
        {
            TechTree tree = null;
            bool startedTier = false;
            bool startedTechnology = false;
            Enums.TechType type = Enums.TechType.Technology;
            foreach(string line in lines)
            {
                bool opening = line.Contains('{');
                bool closing = line.Contains('}');

                if(tree == null && opening) // There is no tree being built and this line contains {
                {
                    string treeName = line.Split('=')[0].Trim();
                    // Get which techtree this is
                    switch (treeName)
                    {
                        case "diplomatic":
                            type = Enums.TechType.Diplomatic;
                            break;
                        case "economic":
                            type = Enums.TechType.Economic;
                            break;
                        case "military":
                            type = Enums.TechType.Military;
                            break;
                        default:
                            type = Enums.TechType.Technology;
                            break;
                    }
                    // Create the tree object
                    tree = new TechTree(type);
                } else if (tree != null && opening && !startedTier) // There is a tree being built, this line contains { and we haven't started a tier yet
                {
                    startedTier = true;
                    tree.NewTier();
                } else if (tree != null && opening && startedTier && !startedTechnology) // This is a new technology
                {
                    startedTechnology = true;
                }
            }
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
            if (Q.IsDown() && Globals.currentState == GameState.SYSTEMVIEW)
            {
                Globals.currentState = GameState.MAPVIEW;
                Window.camX = Globals.mapCamX;
                Window.camY = Globals.mapCamY;
            }

            if (Globals.currentState == GameState.SYSTEMVIEW)
            {
                Globals.currentSystem.Update();
            }
        }

        public static void switchViewToSystem(PlanetarySystem ps)
        {
            Globals.mapCamX = Window.camX;
            Globals.mapCamY = Window.camY;
            Window.camX = 0;
            Window.camY = 0;
            Globals.currentState = GameState.SYSTEMVIEW;
            Globals.currentSystem = ps;
        }

        public void Draw()
        {
            //Do all you draw calls here
            if (Globals.currentState == GameState.MAPVIEW)
            {
                Globals.map.Draw();
            }
            if (Globals.currentState == GameState.SYSTEMVIEW)
            {
                Globals.currentSystem.Draw();
            }


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