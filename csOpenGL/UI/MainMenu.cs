﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class MainMenu : UI
    {

        public MainMenu() : base()
        {
            CreateMainMenu();
        }

        public void CreateMainMenu() {
            // Final button
            buttons.Add(new DrawnButton("Play", 1920 / 2 - 100, 1080 / 2 - 360, 200, 100, () => { new OneMinuteUI(); Globals.leaderUI = new LeaderUI(); Globals.suppliesUI = new SuppliesUI(); Globals.scientistUI = new ScientistUI(); }, 0.5f, 0.5f, 0.5f));

            // Start 1 minute section
            buttons.Add(new DrawnButton("Play", 1920 / 2 - 100, 1080 / 2 - 60, 200, 100, () => { Globals.currentState = GameState.MAPVIEW; Globals.activeButtons = new List<DrawnButton>(); }, 0.5f, 0.5f, 0.5f));

            // Load tutorial
            buttons.Add(new DrawnButton("Tutorial", 1920 / 2 - 100, 1080 / 2 + 60, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));

            // Show Settings
            buttons.Add(new DrawnButton("Settings", 1920 / 2 - 100, 1080 / 2 + 180, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));

            // Alt F4
            buttons.Add(new DrawnButton("Quit", 1920 / 2 - 100, 1080 / 2 + 300, 200, 100, () => { Window.window.Exit(); }, 0.5f, 0.5f, 0.5f));
        }
        
    }
}
