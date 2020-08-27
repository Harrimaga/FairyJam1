using FairyJam.Orbitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class PlanetUI : UI
    {

        private Sprite backGround = new Sprite(400, 600, 0, Textures.Get(1));
        private Orbital o;

        public PlanetUI(Orbital o) : base()
        {
            this.o = o;
        }

        public override void Draw() 
        {
            backGround.DrawLate(1920 / 2 - 200, 1080 / 2 - 300, false, 0, 1, 1, 1, 0.8f);
            Window.window.DrawTextCentered(o.Name, 1920 / 2, 1080 / 2 - 300, 0, 0, 0, 1, true);
            int offset = 0;
            if (o.materialsAvailable[0] > 0)
            {
                Window.window.DrawText("Food: " + Math.Truncate(o.materialsAvailable[0] * 100) / 100, 1920 / 2 - 200, 1080 / 2 - 200, 0, 0, 0, 1, true, Globals.buttonFont);
                offset += 50;
            }
                
            if (o.materialsAvailable[1] > 0)
            {
                Window.window.DrawText("Materials: " + Math.Truncate(o.materialsAvailable[1] * 100) / 100, 1920 / 2 - 200, 1080 / 2 - 200 + offset, 0, 0, 0, 1, true, Globals.buttonFont);
                offset += 50;
            }
                
            if (o.materialsAvailable[2] > 0)
            {
                Window.window.DrawText("Fuel: " + Math.Truncate(o.materialsAvailable[2] * 100) / 100, 1920 / 2 - 200, 1080 / 2 - 200 + offset, 0, 0, 0, 1, true, Globals.buttonFont);
            }
        }

    }
}
