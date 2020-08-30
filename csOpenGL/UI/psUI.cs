using FairyJam.Orbitals;
using FairyJam.Ships;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class psUI : UI
    {
        private Sprite backGround = new Sprite(400, 600, 0, Textures.Get(1));
        private PlanetarySystem ps;

        private DrawnButton createFleet, sendFleet;
        private const int buttonAmount = 15;
        private int scrollFriendly = 0, scrollEnemy = 0;
        private List<Fleet> selected;

        public psUI(PlanetarySystem ps)
        {
            this.ps = ps;
            selected = new List<Fleet>();
            if (ps.Owner == Globals.PlayerNation)
            {
                createFleet = new DrawnButton("Create Fleet", 1920 / 2 - 195, 1080 / 2 + 195, 190, 100, () => { ps.fleets.Add(new Fleet(ps.Owner, new List<Ship>())); new FleetTransferUI(this, ps.fleets.Last(), ps); }, 0, 0.5f, 0.5f, true);
                buttons.Add(createFleet);  
            }

            sendFleet = new DrawnButton("Send Fleet", 1920 / 2 + 5, 1080 / 2 + 195, 190, 100, () => { if (selected.Count > 0) { new FleetDestinationUI(selected, ps); } }, 0, 0.5f, 0.5f, true);
            buttons.Add(sendFleet);

            for (int j=0;j<buttonAmount;j++)
            {
                int k = j;
                buttons.Add(new DrawnButton("", 1920/2 - 195, 1080/2 - 155 + 20 * j, 190, 20, () => { List<Fleet> fl = ps.GetFleets(Globals.PlayerNation)[0]; if (fl.Count <= k + scrollFriendly) { return; } SelectFleet(fl[k + scrollFriendly]); }, 1, 1, 1, false, () => { List<Fleet> fl = ps.GetFleets(Globals.PlayerNation)[0]; if(fl.Count <= k + scrollFriendly) {return; } new FleetTransferUI(this, fl[k + scrollFriendly], ps); }));
            }
        }

        public void SelectFleet(Fleet f)
        {
            if (selected.Contains(f))
            {
                selected.Remove(f);
            }
            else
            {
                selected.Add(f);
            }
        }

        public override void Draw()
        {
            backGround.DrawLate(1920 / 2 - 200, 1080 / 2 - 300, false, 0, 1, 1, 1, 0.8f);

            // PS Name:
            Window.window.DrawTextCentered(ps.Name(), 1920 / 2, 1080 / 2 - 295, 0, 0, 0, 1, true, Globals.buttonFont);

            // PS Info:
            Window.window.DrawText("Owner: " + (ps.Owner == null ? "Nobody" : ps.Owner.Name), 1920 / 2 - 195, 1080 / 2 - 265, 0, 0, 0, 1, true, Globals.buttonFont);
            Window.window.DrawText("Planets: " + ps.GetCount(0), 1920 / 2 - 195, 1080 / 2 - 245, 0, 0, 0, 1, true, Globals.buttonFont);
            Window.window.DrawText("Astroids: " + ps.GetCount(2), 1920 / 2 - 195, 1080 / 2 - 225, 0, 0, 0, 1, true, Globals.buttonFont);
            Window.window.DrawText("Moons: " + ps.GetCount(1), 1920 / 2 - 195, 1080 / 2 - 205, 0, 0, 0, 1, true, Globals.buttonFont);
            Window.window.DrawText("Unassigned Ships: " + ps.ships.Count, 1920 / 2 - 195, 1080 / 2 - 185, 0, 0, 0, 1, true, Globals.buttonFont);

            // Owned Fleets in System:
            List<Fleet>[] fleets = ps.GetFleets(Globals.PlayerNation);

            for (int i = scrollFriendly; i < (buttonAmount + scrollFriendly < fleets[0].Count ? buttonAmount + scrollFriendly : fleets[0].Count); i++)
            {
                Window.window.DrawText(fleets[0][i].Name, 1920 / 2 - 195, 1080 / 2 - 155 + (i - scrollFriendly) * 20, selected.Contains(fleets[0][i]) ? 1 : 0, 0, 0, 1, true, Globals.buttonFont);
            }

            // Enemy Fleets in System:
            for (int i = scrollEnemy; i < (buttonAmount + scrollEnemy < fleets[1].Count ? buttonAmount + scrollEnemy : fleets[1].Count); i++)
            {
                Window.window.DrawText(fleets[1][i].Name, 1920 / 2 + 5, 1080 / 2 - 155 + (i - scrollEnemy) * 20, 0, 0, 0, 1, true, Globals.buttonFont);
            }
        }

        public override bool MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if(!Globals.checkCol(mx, my, 0, 0, 1920 / 2 - 200, 1080 / 2 - 300, 400, 600)) 
            {
                Globals.currentUI = null;
                Globals.activeButtons = new List<DrawnButton>();
                return false;
            }
            return true;
        }

        public void Scroll(int val)
        {
            // If in owned fleets
            List<Fleet>[] fleets = ps.GetFleets(Globals.PlayerNation);
            if (Globals.checkCol(Window.window.mouseX, Window.window.mouseY, 0, 0, 1920 / 2 - 195, 1080 / 2 - 155, 190, 20 * buttonAmount))
            {
                scrollFriendly += val;
                if (scrollFriendly > fleets[0].Count - buttonAmount)
                {
                    scrollFriendly = fleets[0].Count - buttonAmount;
                }
                if (scrollFriendly < 0)
                {
                    scrollFriendly = 0;
                }
            }

            // If in enemy fleets
            if (Globals.checkCol(Window.window.mouseX, Window.window.mouseY, 0, 0, 1920 / 2 + 5, 1080 / 2 - 155, 190, 20 * buttonAmount))
            {
                scrollEnemy += val;
                if (scrollEnemy > fleets[1].Count - buttonAmount)
                {
                    scrollEnemy = fleets[1].Count - buttonAmount;
                }
                if (scrollEnemy < 0)
                {
                    scrollEnemy = 0;
                }
            }
        }
    }
}
