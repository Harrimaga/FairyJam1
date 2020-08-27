using FairyJam.Buildings;
using FairyJam.Orbitals;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class PlanetUI : UI
    {

        private Sprite backGround = new Sprite(400, 601, 0, Textures.Get(1));
        private Orbital o;

        private DrawnButton rab, shipyard, housing, toResource, toShipManagement, toShipBuilding;

        public PlanetUI(Orbital o) : base()
        {
            this.o = o;
            rab = new DrawnButton("", 1920 / 2 - 195, 1080 / 2 - 50, 127, 100, () => { o.Build(new RAB("", new double[3] { 10, 10, 10 })); }, 0, 1, 0, true);
            shipyard = new DrawnButton("", 1920 / 2 - 62, 1080 / 2 - 50, 126, 100, () => { o.Build(new Shipyard("")); }, 0, 1, 0, true);
            housing = new DrawnButton("", 1920 / 2 + 69, 1080 / 2 - 50, 126, 100, () => { o.Build(new Housing("")); }, 0, 1, 0, true);

            toResource = new DrawnButton("Resource Overview", 1920 / 2 - 195, 1080 / 2 + 55, 390, 77, () => { }, 0, 0, 1, true);
            toShipManagement = new DrawnButton("Ship Management", 1920 / 2 - 195, 1080 / 2 + 137, 390, 77, () => { }, 0, 0, 1, true);
            toShipBuilding = new DrawnButton("Ship Building", 1920 / 2 - 195, 1080 / 2 + 219, 390, 77, () => { }, 0, 0, 1, true);

            buttons.Add(rab);
            buttons.Add(shipyard);
            buttons.Add(housing);
            buttons.Add(toResource);
            buttons.Add(toShipManagement);
            buttons.Add(toShipBuilding);
        }

        public override void Draw() 
        {
            backGround.DrawLate(1920 / 2 - 200, 1080 / 2 - 300, false, 0, 1, 1, 1, 0.8f);
            Window.window.DrawTextCentered(o.Name, 1920 / 2, 1080 / 2 - 300, 0, 0, 0, 1, true);
            int offset = 0;
            if (o.materialsAvailable[0] > 0)
            {
                Window.window.DrawText("Food: " + Math.Truncate(o.materialsAvailable[0] * 100) / 100, 1920 / 2 - 195, 1080 / 2 - 200, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText((Math.Truncate(o.GetEfficiency(0) * 10000) / 100).ToString("N2") + "%", 1920 / 2 + 135, 1080 / 2 - 200, 0, 0, 0, 1, true, Globals.buttonFont);
                offset += 50;
            }
                
            if (o.materialsAvailable[1] > 0)
            {
                Window.window.DrawText("Materials: " + Math.Truncate(o.materialsAvailable[1] * 100) / 100, 1920 / 2 - 195, 1080 / 2 - 200 + offset, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText((Math.Truncate(o.GetEfficiency(1) * 10000) / 100).ToString("N2") + "%", 1920 / 2 + 135, 1080 / 2 - 200 + offset, 0, 0, 0, 1, true, Globals.buttonFont);
                offset += 50;
            }
                
            if (o.materialsAvailable[2] > 0)
            {
                Window.window.DrawText("Fuel: " + Math.Truncate(o.materialsAvailable[2] * 100) / 100, 1920 / 2 - 195, 1080 / 2 - 200 + offset, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText((Math.Truncate(o.GetEfficiency(2) * 10000) / 100).ToString("N2") + "%", 1920 / 2 + 135, 1080 / 2 - 200 + offset, 0, 0, 0, 1, true, Globals.buttonFont);
            }


            // RAB building button:

            foreach (DrawnButton button in buttons)
            {
                button.Draw();
            }
            Window.window.DrawText(o.GetBuildingCount(0).ToString(), 1920 / 2 - 190, 1080 / 2 + 25, true, Globals.buttonFont);
            Window.window.DrawText(o.GetBuildingCount(1).ToString(), 1920 / 2 - 57, 1080 / 2 + 25, true, Globals.buttonFont);
            Window.window.DrawText(o.GetBuildingCount(2).ToString(), 1920 / 2 + 74, 1080 / 2 + 25, true, Globals.buttonFont);
        }

        public override void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if(!Globals.checkCol(mx, my, 0, 0, 1920 / 2 - 200, 1080 / 2 - 300, 400, 600)) 
            {
                Globals.currentUI = null;
                Globals.activeButtons = new List<DrawnButton>();
            }
        }
    }
}
