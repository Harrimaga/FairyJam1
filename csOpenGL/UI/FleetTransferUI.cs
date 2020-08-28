using FairyJam.Orbitals;
using FairyJam.Ships;
using OpenTK.Graphics.ES10;
using OpenTK.Input;
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
        private const int buttonAmount = 10;

        private Ship selectedShip;
        
        public FleetTransferUI(UI prev, Fleet f, PlanetarySystem ps)
        {
            this.f = f;
            this.prev = prev;
            this.ps = ps;
            selectedShip = null;
            buttons.Add(new DrawnButton("Back", 1920/2-195, 1080/2+245, 390, 50, () => { prev.reSelect(); if (f.ships.Count == 0) { f.owner.fleets.Remove(f); ps.fleets.Remove(f); } }, 0, 0.5f, 0.5f, true));

            for (int i = 0; i < buttonAmount; i++)
            {
                int k = i;
                buttons.Add(new DrawnButton("", 1920 / 2 - 195, 1080 / 2 - 155 + 20 * i, 190, 20, () => { OnSelectRemove(f.ships[k + scrollA]); }, 1, 1, 1, false,  () => { if(f.ships.Count <= k+scrollA) {return;}OnSelectRemove(f.ships[k + scrollA], true); }));
            }

            for (int j = 0; j < buttonAmount; j++)
            {
                int k = j;
                buttons.Add(new DrawnButton("", 1920 / 2 + 5, 1080 / 2 - 155 + 20 * j, 190, 20, () => { OnSelectAdd(ps.ships[k + scrollU]); }, 1, 1, 1, false, () => { if(ps.ships.Count <= k+scrollU) {return;} OnSelectAdd(ps.ships[k + scrollU], true); }));
            }
        }

        private void OnSelectAdd(Ship ship, bool b = false)
        {
            if (selectedShip == ship || b)
            {
                // Move to other side
                f.AddToFleet(ship);
                ps.ships.Remove(ship);
            }
            else
            {
                // Highlight
                selectedShip = ship;
            }
        }

        private void OnSelectRemove(Ship ship, bool b = false)
        {
            if (selectedShip == ship || b)
            {
                // Move to other side
                ps.ships.Add(ship);
                f.RemoveFromFleet(ship);
            }
            else
            {
                // Highlight
                selectedShip = ship;
            }
        }

        public override void Draw()
        {
            backGround.DrawLate(1920 / 2 - 200, 1080 / 2 - 300, false, 0, 1, 1, 1, 0.8f);

            // Selected Ship Stats:
            string text;
            if (selectedShip != null)
            {
                text = selectedShip.Name;
                // Type
                // Hull
                // Speed
                // Evasiveness
                // M: Weapon damage
                // M: Weapon Types
                // F: Capacity
                // F: Capacity Type (Population / Resources)
                Window.window.DrawText("Type: " + selectedShip.Type.ToString(), 1920 / 2 - 195, 1080 / 2 - 275, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Hull: " + selectedShip.HealthPoints + "/" + selectedShip.MaxHealth, 1920 / 2 - 195, 1080 / 2 - 255, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Speed: " + selectedShip.Speed, 1920 / 2 - 195, 1080 / 2 - 235, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Evasion: " + selectedShip.Evasiveness, 1920 / 2 - 195, 1080 / 2 - 215, 0, 0, 0, 1, true, Globals.buttonFont);

                int offset = 0;
                double damage = 0;
                foreach (Weapon weapon in selectedShip.WeaponList)
                {
                    damage += (weapon.MaxDamage + weapon.MinDamage) / 2;
                }
                Window.window.DrawText("Dmg: " + damage, 1920 / 2 + 5, 1080 / 2 - 275, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Types: " + selectedShip.WeaponTypes(), 1920 / 2 + 5, 1080 / 2 - 255, 0, 0, 0, 1, true, Globals.buttonFont);
                offset = 2;
                switch (selectedShip.Type)
                {
                    case ShipType.FREIGHTER:
                        Window.window.DrawText("Cap: " + selectedShip.MaxResourceLoad, 1920 / 2 + 5, 1080 / 2 - 275, 0, 0, 0, 1, true, Globals.buttonFont);
                        offset = 1;
                        break;
                    case ShipType.TRANSPORTATION:
                        Window.window.DrawText("Cap: " + selectedShip.MaxPeopleLoad, 1920 / 2 + 5, 1080 / 2 - 275, 0, 0, 0, 1, true, Globals.buttonFont);
                        offset = 1;
                        break;
                }

                if (selectedShip.Special != null)
                {
                    Window.window.DrawText("Special: \n" + selectedShip.Special.Name, 1920 / 2 + 5, 1080 / 2 - 275 + offset * 20, 0, 0, 0, 1, true, Globals.buttonFont);
                }
            }
            else
            {
                text = "Select a ship";
            }
            Window.window.DrawTextCentered(text, 1920 / 2, 1080 / 2 - 295, 0, 0, 0, 1, true, Globals.buttonFont);

            // Unassigned ships in Fleet:

            Window.window.DrawTextCentered("Fleet:", 1920 / 2 - 100, 1080 / 2 - 175, 0, 0, 0, 1, true, Globals.buttonFont);

            for (int i = scrollA; i < (buttonAmount + scrollA < f.ships.Count ? buttonAmount + scrollA : f.ships.Count); i++)
            {
                Window.window.DrawText(f.ships[i].Name, 1920 / 2 - 195, 1080 / 2 - 155 + (i - scrollA) * 20, f.ships[i] == selectedShip ? 1 : 0, 0, 0, 1, true, Globals.buttonFont);
            }

            // Assigned ships in Fleet:

            Window.window.DrawTextCentered("Ships:", 1920 / 2 + 100, 1080 / 2 - 175, 0, 0, 0, 1, true, Globals.buttonFont);

            for (int i = scrollU; i < (buttonAmount + scrollU < ps.ships.Count ? buttonAmount + scrollU : ps.ships.Count); i++)
            {
                Window.window.DrawText(ps.ships[i].Name, 1920 / 2 + 5, 1080 / 2 - 155 + (i - scrollU) * 20, ps.ships[i] == selectedShip ? 1 : 0, 0, 0, 1, true, Globals.buttonFont);
            }


            Window.window.DrawTextCentered(f.Name, 1920 / 2, 1080 / 2 + 70, 0, 0, 0, 1, true, Globals.buttonFont);
            // Fleet Stats
            if (f.ships.Count > 0)
            {
                Window.window.DrawText("Ships: " + f.ships.Count, 1920 / 2 - 195, 1080 / 2 + 90, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Speed: " + f.GetSpeed(), 1920 / 2 - 195, 1080 / 2 + 110, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Total Hull: " + f.GetCurrentHull() + "/" + f.GetMaxHull(), 1920 / 2 - 195, 1080 / 2 + 130, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Total Damage: " + f.DamageTotal(), 1920 / 2 - 195, 1080 / 2 + 150, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Evasiveness: " + f.GetEvasiveness().ToString("N2"), 1920 / 2 - 195, 1080 / 2 + 170, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Transport Cap: " + f.GetTransportCap(), 1920 / 2 + 5, 1080 / 2 + 90, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Resource Cap: " + f.GetResourceCap(), 1920 / 2 + 5, 1080 / 2 + 110, 0, 0, 0, 1, true, Globals.buttonFont);
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
                if (scrollA > assigned.Count - buttonAmount)
                {
                    scrollA = assigned.Count - buttonAmount;
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
                if (scrollU > unassigned.Count - buttonAmount)
                {
                    scrollU = unassigned.Count - buttonAmount;
                }
                if (scrollU < 0)
                {
                    scrollU = 0;
                }
            }
        }
        
        public override bool MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if(!Globals.checkCol(mx, my, 0, 0, 1920 / 2 - 200, 1080 / 2 - 300, 400, 600)) 
            {
                return false;
            }
            return true;
        }

    }
}
