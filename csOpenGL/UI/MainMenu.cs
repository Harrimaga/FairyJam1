using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    public class MainMenu
    {
        private List<DrawnButton> buttons;

       public void CreateMainMenu() {
            buttons.Add(new DrawnButton("Play", 1920 / 2 - 100, 1080 / 2 - 60, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("Tutorial", 1920 / 2 - 100, 1080 / 2 + 60, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("Settings", 1920 / 2 - 100, 1080 / 2 + 180, 200, 100, () => { }, 0.5f, 0.5f, 0.5f));
            buttons.Add(new DrawnButton("Quit", 1920 / 2 - 100, 1080 / 2 + 300, 200, 100, () => { Window.window.Exit(); }, 0.5f, 0.5f, 0.5f));
       }

        public void LoadMenu()
        {
            Globals.activeButtons = buttons;
        }
    }
}
