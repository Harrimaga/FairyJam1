using FairyJam.Orbitals;
using FairyJam.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class FleetTransferUI : UI
    {
        private Sprite backGround = new Sprite(400, 600, 0, Textures.Get(1));
        private UI prev;
        private Fleet f;
        private PlanetarySystem ps;
        private int scrollA = 0, scrollU = 0;
        private const int buttonAmount = 15;
        
        public FleetTransferUI(UI prev, Fleet f, PlanetarySystem ps)
        {
            this.f = f;
            this.prev = prev;
            this.ps = ps;
            buttons.Add(new DrawnButton("Back", 1920/2-195, 1080/2+245, 390, 50, () => prev.reSelect(), 0, 0.5f, 0.5f, true));
        }

        public override void Draw()
        {
            backGround.DrawLate(1920 / 2 - 200, 1080 / 2 - 300, false, 0, 1, 1, 1, 0.8f);

            // Unassigned ships in Fleet:
            for (int i = scrollU; i < (buttonAmount + scrollA < f.ships.Count ? buttonAmount + scrollA : f.ships.Count); i++)
            {
                Window.window.DrawText(f.ships[i].Name, 1920 / 2 - 195, 1080 / 2 - 155 + (i - scrollA) * 20, 0, 0, 0, 1, true, Globals.buttonFont);
            }

            // Assigned ships in Fleet:
            for (int i = scrollU; i < (buttonAmount + scrollU < ps.ships.Count ? buttonAmount + scrollU : ps.ships.Count); i++)
            {
                Window.window.DrawText(ps.ships[i].Name, 1920 / 2 + 5, 1080 / 2 - 155 + (i - scrollU) * 20, 0, 0, 0, 1, true, Globals.buttonFont);
            }
        }

        public void Scroll(int val)
        {
            // If in owned fleets
            List<Ship> unassigned = ps.ships;
            List<Ship> assigned = f.ships;
            if (Globals.checkCol(Window.window.mouseX, Window.window.mouseY, 0, 0, 1920 / 2 - 195, 1080 / 2 - 155, 190, 20 * buttonAmount))
            {
                scrollA += val;
                if (scrollA > unassigned.Count - buttonAmount)
                {
                    scrollA = unassigned.Count - buttonAmount;
                }
                if (scrollA < 0)
                {
                    scrollA = 0;
                }
            }

            // If in enemy fleets
            if (Globals.checkCol(Window.window.mouseX, Window.window.mouseY, 0, 0, 1920 / 2 + 5, 1080 / 2 - 155, 190, 20 * buttonAmount))
            {
                scrollU += val;
                if (scrollU > assigned.Count - buttonAmount)
                {
                    scrollU = assigned.Count - buttonAmount;
                }
                if (scrollU < 0)
                {
                    scrollU = 0;
                }
            }
        }
    }
}
