using FairyJam.Orbitals;
using FairyJam.UI;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secretary;
using System.Drawing.Design;
using System.Globalization;

namespace FairyJam
{
    class Game
    {

        public Window window;
        private Hotkey left = new Hotkey(true).AddKey(Key.A).AddKey(Key.Left);
        private Hotkey right = new Hotkey(true).AddKey(Key.D).AddKey(Key.Right);
        private Hotkey up = new Hotkey(true).AddKey(Key.W).AddKey(Key.Up);
        private Hotkey down = new Hotkey(true).AddKey(Key.S).AddKey(Key.Down);
        private Hotkey Q = new Hotkey(false).AddKey(Key.Q);
        private Hotkey pause = new Hotkey(false).AddKey(Key.Space);
        MainMenu um = new MainMenu();

        bool TimerEnabled = true;

        public Game(Window window)
        {
            this.window = window;
            Globals.timer = new Timer(60000);
            Globals.paused = false;
            Globals.typing = null;
            OnLoad();
            ReadFiles();
        }

        public void OnLoad()
        {
            Globals.map = new Map(20, 20);
            //buttons.Add(new DrawnButton("test", 0, 0, 200, 100, () => { Window.window.ToggleShader(Shaders.basic); }, 0.5f, 0.5f, 0.5f));
            //buttons.Add(new DrawnButton("test2", 0, 105, 200, 100, () => { Window.window.ToggleShader(Shaders.blur); }, 0.5f, 0.5f, 0.5f));
            //buttons.Add(new DrawnButton("Bloom", 0, 210, 200, 100, () => { Window.window.ToggleShader(Shaders.bloom); }, 0.5f, 0.5f, 0.5f));
            Globals.currentState = GameState.MAINMENU;
            Globals.PlayerNation = new Nation("Player");
            Globals.SpacePirates = new Nation("Space Pirates");
            Globals.players = new List<Nation>()
            {
                Globals.PlayerNation,
                Globals.SpacePirates
            };
            Globals.map.Generate();

            UI.MainHUD.Init();

            Globals.eventHandler = new Events.EventHandler();

        }

        public void Update(double delta)
        {
            Globals.DeltaTime = delta;

            if (Globals.currentState == GameState.MINUTE)
            {
                Globals.timer.UpdateTimer();
                if (Globals.timer.Expired() && TimerEnabled)
                {
                    Globals.currentState = GameState.MAPVIEW;
                    Globals.currentUI = null;
                    Globals.eventHandler.events.Clear();
                    Globals.activeButtons = new List<DrawnButton>();

                    // Randomly select starting planet

                    bool success = false;
                    while (!success)
                    {
                        int rndX = Globals.random.Next(Globals.map.mapWidth);
                        int rndY = Globals.random.Next(Globals.map.mapHeight);

                        Tile tile = Globals.map.grid[rndX,rndY];
                        if (tile.ps != null)
                        {
                            success = true;
                            tile.ps.Owner = Globals.PlayerNation;
                            tile.ps.fleets = new List<Ships.Fleet>();

                            int drawX = tile.x * Globals.TileWidth - 2 * tile.x + Globals.TileWidth/2 + tile.psOffsetX;
                            int drawY = tile.y * Globals.TileHeight + Globals.TileWidth / 2 + tile.psOffsetY;
                            if (tile.y % 2 == 1)
                            {
                                drawX += Globals.TileWidth / 2;
                            }

                            Window.camX = drawX - 960;
                            Window.camY = drawY - 540;
                        }
                    }
                }
            }
            if (Globals.currentState == GameState.SYSTEMVIEW && !Globals.paused)
            {
                Globals.currentSystem.Update();
            }
            
            if (Globals.typing != null)
            {
                Hotkey.Type();
                return;
            }

            //if (timer.Expired())
            //{
            //    window.Exit();
            //}

            //Updating logic
            if (left.IsDown()) Window.camX -= (float)(25 * delta);
            if (right.IsDown()) Window.camX += (float)(25 * delta);
            if (up.IsDown()) Window.camY -= (float)(25 * delta);
            if (down.IsDown()) Window.camY += (float)(25 * delta);
            if (pause.IsDown()) Globals.paused = !Globals.paused;

            if(Q.IsDown() && Globals.currentState == GameState.MAPVIEW)
            {
                um.reSelect();
                Globals.currentState = GameState.MAINMENU;
            }
            if (Q.IsDown() && Globals.currentState == GameState.SYSTEMVIEW)
            {
                Globals.currentState = GameState.MAPVIEW;
                Window.camX = Globals.mapCamX;
                Window.camY = Globals.mapCamY;
                Globals.currentUI = null;
                Globals.activeButtons = new List<DrawnButton>();
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
            Globals.currentUI = null;
            ps.Draw();
            ps.Update();
        }

        public void Draw()
        {
            if(Globals.currentState == GameState.WON) 
            {
                Window.window.DrawTextCentered("You won!", 960, 540, 1, 1, 1, 1);
                return;
            }

            if (Globals.currentState == GameState.LOST)
            {
                Window.window.DrawTextCentered("You lost!", 960, 540, 1, 1, 1, 1);
                return;
            }

            //Do all you draw calls here
            if (Globals.currentState == GameState.MAPVIEW)
            {
                Globals.map.Draw();
            }
            else if (Globals.currentState == GameState.SYSTEMVIEW)
            {
                Globals.currentSystem.Draw();
            }
            if (Globals.currentUI != null)
            {
                Globals.currentUI.Draw();
            }

            foreach (DrawnButton button in Globals.activeButtons)
            {
                button.Draw();
            }

            Globals.eventHandler.Draw();

            if (Globals.currentState == GameState.MAPVIEW || Globals.currentState == GameState.SYSTEMVIEW)
            {
                MainHUD.Draw();
            }
        }

        public void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            
            if(Globals.typing != null && !Globals.typing.IsInButton(mx, my)) 
            {
                Globals.typing = null;
            }
            
            if (Globals.currentState == GameState.SYSTEMVIEW || Globals.currentState == GameState.MAPVIEW) MainHUD.MouseDown(e, mx, my);
            {
                for (int i = Globals.activeButtons.Count - 1; i >= 0; i--)
                {
                    DrawnButton button = Globals.activeButtons[i];
                    if (button.IsInButton(mx, my))
                    {
                        if (e.Button == MouseButton.Left)
                        {
                            button.OnClick();
                        }
                        else if (e.Button == MouseButton.Right)
                        {
                            button.OnRightClick();
                        }
                        break;
                    }
                }
            }
            if (Globals.currentUI != null)
            {
                if(Globals.currentUI.MouseDown(e, mx, my)) 
                {
                    return;
                }
            }
            if (Globals.currentState == GameState.SYSTEMVIEW)
            {
                Globals.currentSystem.MouseDown(e, mx, my);
            }
            if (Globals.currentState == GameState.MAPVIEW)
            {
                Globals.map.MouseDown(e, mx, my);
            }

            Globals.eventHandler.MouseDown(e, mx, my);

        }

        public void MouseUp(MouseButtonEventArgs e, int mx, int my)
        {

        }

        public void MouseWheelScrollUp()
        {
            if (Globals.currentState == GameState.MINUTE)
            {
                if (Globals.currentUI == Globals.leaderUI)
                {
                    Globals.leaderUI.Scroll(-2);
                }
                else if (Globals.currentUI == Globals.scientistUI)
                {
                    Globals.scientistUI.Scroll(-2);
                }
                else if (Globals.currentUI == Globals.suppliesUI)
                {
                    Globals.suppliesUI.Scroll(-2);
                }
            }

            if (Globals.currentUI is psUI)
            {
                psUI ui = (psUI)Globals.currentUI;

                ui.Scroll(-2);
            }

            if (Globals.currentUI is FleetTransferUI)
            {
                FleetTransferUI ui = (FleetTransferUI)Globals.currentUI;

                ui.Scroll(-2);
            }
            if (Globals.currentUI is ShipBuildingUI)
            {
                ShipBuildingUI ui = (ShipBuildingUI)Globals.currentUI;

                ui.Scroll(-2);
            }
        }

        public void MouseWheelScrollDown()
        {
            if (Globals.currentState == GameState.MINUTE)
            {
                if (Globals.currentUI == Globals.leaderUI)
                {
                    Globals.leaderUI.Scroll(2);
                }
                else if (Globals.currentUI == Globals.scientistUI)
                {
                    Globals.scientistUI.Scroll(2);
                }
                else if (Globals.currentUI == Globals.suppliesUI)
                {
                    Globals.suppliesUI.Scroll(2);
                }
            }

            if (Globals.currentUI is psUI)
            {
                psUI ui = (psUI)Globals.currentUI;

                ui.Scroll(2);
            }

            if (Globals.currentUI is FleetTransferUI)
            {
                FleetTransferUI ui = (FleetTransferUI)Globals.currentUI;

                ui.Scroll(2);
            }
            if (Globals.currentUI is ShipBuildingUI)
            {
                ShipBuildingUI ui = (ShipBuildingUI)Globals.currentUI;

                ui.Scroll(2);
            }
        }

        private void ReadFiles()
        {
            try
            {
                string[] traitLines = FileHandler.Read("People/traitsLeaders.txt");
                Globals.possibleTraitsLeader = ParseTraits(traitLines);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                string[] traitLines = FileHandler.Read("People/traitsScientists.txt");
                Globals.possibleTraitsScientist = ParseTraits(traitLines);
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
                }
                else if (line.Trim() == "}")
                {
                    list.Add(trait);
                }
                else
                {
                    string[] words = line.Split('=');
                    double num;
                    switch (words[0].Trim())
                    {
                        case "name":
                            trait.Name = words[1].Trim();
                            break;
                        case "description":
                            trait.Description = words[1].Trim();
                            break;
                        case "population_growth":
                            num = double.Parse(words[1], CultureInfo.InvariantCulture);
                            trait.Actions.Add((Nation n) => { n.addToMod(num, Enums.Modifier.PopulationGrowth);});
                            break;
                        case "material_gain":
                            num = double.Parse(words[1], CultureInfo.InvariantCulture);
                            trait.Actions.Add((Nation n) => { n.Materials += num;});
                            break;
                        case "happiness_gain":
                            num = double.Parse(words[1], CultureInfo.InvariantCulture);
                            trait.Actions.Add((Nation n) => { n.Happiness += num;});
                            break;
                        case "fuel_efficiency":
                            num = double.Parse(words[1], CultureInfo.InvariantCulture);
                            trait.Actions.Add((Nation n) => { n.multiplyToMod(num, Enums.Modifier.FuelEfficiency);});
                            break;
                        case "techpoint_gain":
                            num = double.Parse(words[1], CultureInfo.InvariantCulture);
                            trait.Actions.Add((Nation n) => { n.addToMod(num, Enums.Modifier.TechGrowth);});
                            break;
                        case "sprite":
                            trait.sprite = new Sprite(25, 25, 0, Textures.Get(int.Parse(words[1].Trim())));
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

            foreach (string line in lines)
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
                }
                else if (namelist == null && line.Contains('=')) // This is a namelist declaration and we aren't making a namelist already
                {
                    string name = line.Split('=')[0].Trim();
                    namelist = new Namelist(name);
                }
                else if (namelist != null && line.Contains('}')) // We are making a namelist and we found the closing argument
                {
                    list.Add(namelist);
                    namelist = null;
                }
                else if (namelist != null && line.Contains('{')) // We are making a namelist and we found an opening argument
                {
                    inList = true;
                    string nameListType = line.Split('=')[0].Trim();
                    inListIsFamilyName = (nameListType == "family");
                }
            }

            return list.ToArray();
        }


    }
}