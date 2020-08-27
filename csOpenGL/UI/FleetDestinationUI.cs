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
    class FleetDestinationUI : UI
    {
        private List<Fleet> fleets;
        private PlanetarySystem ps;
        public FleetDestinationUI(List<Fleet> fleets, PlanetarySystem ps)
        {
            this.fleets = fleets;
            this.ps = ps;
        }

        public override void Draw() 
        {
            Window.window.DrawTextCentered("Click on a planet to travel to", 1920/2, 100, 1, 1, 1, 1, true);
        }

        public override bool MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            List<DrawnButton> buttons = Globals.map.GetButtons();
            for (int i = buttons.Count - 1; i >= 0; i--)
            {
                DrawnButton button = buttons[i];
                if (button != null && button.IsInButton(mx, my))
                {
                    if (e.Button == MouseButton.Left)
                    {
                        foreach (Fleet fleet in fleets)
                        {
                            fleet.SetDestination(Globals.map.GetSystem(button), ps);
                        }
                        Globals.currentUI = null;
                        Globals.activeButtons = new List<DrawnButton>();
                    }
                    break;
                }
            }
            return true;
        }

    }
}
