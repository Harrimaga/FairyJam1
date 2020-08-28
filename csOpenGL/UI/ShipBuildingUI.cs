using FairyJam.Equipment.SpecialEquipment;
using FairyJam.Ships;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class ShipBuildingUI : UI
    {

        private Sprite backGround = new Sprite(1720, 880, 0, Textures.Get(1));
        public UI prev;
        public string name;
        public int scrollHull, scrollWeapon,scrollPreset, scrollSpecial, buttonAmount, weaponSlotsLeft, specialSlotsLeft;
        public Ship selectedHull;
        public Weapon selectedWeapon;
        public SpecialEquipment special;
        public SpecialEquipment selectedSpecial;
        public List<Weapon> selectedWeapons;
        public DrawnButton nameBox, quantity;
        public double materialCost = 0, moneyCost = 0;

        public ShipBuildingUI(UI prev) : base()
        {
            this.prev = prev;
            name = "";
            buttonAmount = 10;
            buttons.Add(new DrawnButton("Back", 1920 - 300, 1080 - 150, 200, 50, () => { prev.reSelect(); }, 0, 0.5f, 0.5f, true));
            buttons.Add(new DrawnButton("Build!", 1920 - 600, 1080 - 150, 200, 50, () => { BuildShip(); }, 0, 0.5f, 0.5f, true));
            buttons.Add(new DrawnButton("Add Preset", 1920 - 600, 225, 200, 50, () => { if (selectedHull == null) { return; } Globals.PlayerNation.presets.Add(selectedHull.CopyHull());Globals.PlayerNation.presets.Last().Name = name;  Globals.PlayerNation.presets.Last().WeaponList = new List<Weapon>(selectedWeapons); }, 0, 0.5f, 0.5f, true));
            nameBox = new DrawnButton("Type Name", 105, 155, 200, 50, () => { nameBox.Text = ""; Globals.num = false;Globals.typing = nameBox; }, 0, 0.5f, 0.5f, true);
            quantity = new DrawnButton("Quantity", 310, 155, 200, 50, () => { quantity.Text = ""; Globals.num = true;Globals.typing = quantity; }, 0, 0.5f, 0.5f, true);
            buttons.Add(nameBox);
            buttons.Add(quantity);
            
            for (int i = 0; i < buttonAmount; i++)
            {
                int k = i;
                buttons.Add(new DrawnButton("", 105, 280 + k * 20, 190, 20, () => { if(Globals.PlayerNation.unlockedHulls.Count <= k+scrollHull) {return;}selectedWeapons = new List<Weapon>(); selectedHull = Globals.PlayerNation.unlockedHulls[k + scrollHull]; weaponSlotsLeft = Globals.PlayerNation.unlockedHulls[k + scrollHull].MaxSlots; specialSlotsLeft = Globals.PlayerNation.unlockedHulls[k + scrollHull].SpecialSlots;  selectedHull.WeaponList = selectedWeapons;}, 1, 1, 1, false));
            }

            for (int i = 0; i < buttonAmount; i++)
            {
                int k = i;
                buttons.Add(new DrawnButton("", 310, 280 + k * 20, 190, 20, () => { WeaponClick(k);}, 1, 1, 1, false, () => WeaponRightClick(k)));
            }
            for (int i = 0; i < buttonAmount; i++)
            {
                int k = i;
                buttons.Add(new DrawnButton("", 517, 280 + k * 20, 190, 20, () => { SpecialClick(k); }, 1, 1, 1, false, () => { SpecialRightClick(k); } ));
            }



            for (int i = 0; i < buttonAmount; i++)
            {
                int k = i;
                buttons.Add(new DrawnButton("", 1920-600, 280 + k * 20, 190, 20, () => { if(Globals.PlayerNation.presets.Count <= k+scrollPreset) {return;} selectedHull = Globals.PlayerNation.presets[k+scrollPreset].CopyHull(); selectedWeapons = new List<Weapon>(Globals.PlayerNation.presets[k+scrollPreset].WeaponList);selectedHull.WeaponList = selectedWeapons;weaponSlotsLeft = selectedHull.MaxSlots; foreach(Weapon w in selectedWeapons) {weaponSlotsLeft -= w.Slotsize;} nameBox.Text = selectedHull.Name; }, 1, 1, 1, false, () => {if(Globals.PlayerNation.presets.Count <= k+scrollPreset) {return;}Globals.PlayerNation.presets.Remove(Globals.PlayerNation.presets[k+scrollPreset]);}));
            }

        }

        public void BuildShip()
        {
            if(!int.TryParse(quantity.Text, out int q) || selectedHull == null) return;
            if(moneyCost > Globals.PlayerNation.Money || materialCost > Globals.PlayerNation.Materials) return;
            for(int i=0;i<q;i++)
            {
                Ship s = selectedHull.CopyHull();
                s.WeaponList = new List<Weapon>(selectedWeapons);
                Globals.currentSystem.ships.Add(s);
                s.Name = name;
            }
            Globals.PlayerNation.Money -= moneyCost;
            Globals.PlayerNation.Materials -= materialCost;
            selectedHull = null;
            selectedWeapons = new List<Weapon>();
            selectedWeapon = null;
            special = null;
            selectedSpecial = null;
        }

        public void SpecialClick(int k)
        {
            if (selectedHull == null || Globals.PlayerNation.unlockedSpecials.Count <= k + scrollSpecial)
            {
                return;
            }

            if (special != Globals.PlayerNation.unlockedSpecials[ k + scrollSpecial])
            {
                special = Globals.PlayerNation.unlockedSpecials[k + scrollSpecial];
                return;
            }

            if (specialSlotsLeft == 0)
            {
                return;
            }
            selectedSpecial = special;
            specialSlotsLeft -= 1;
        }

        public void SpecialRightClick(int k)
        {
            if (selectedHull == null || Globals.PlayerNation.unlockedSpecials.Count <= k + scrollSpecial)
            {
                return;
            }
            if (selectedSpecial == Globals.PlayerNation.unlockedSpecials[k + scrollSpecial])
            {
                selectedSpecial = null;
                specialSlotsLeft += 1;
            }
        }

        public void WeaponClick(int k)
        {
            if (selectedHull == null || Globals.PlayerNation.unlockedWeapons.Count <= k + scrollWeapon) 
            {
                return;
            }
            if (selectedWeapon != Globals.PlayerNation.unlockedWeapons[k + scrollWeapon])
            {
                selectedWeapon = Globals.PlayerNation.unlockedWeapons[k + scrollWeapon];
                return;
            }
            if (weaponSlotsLeft - Globals.PlayerNation.unlockedWeapons[k + scrollWeapon].Slotsize < 0) 
            { 
                return; 
            }
            selectedWeapons.Add(Globals.PlayerNation.unlockedWeapons[k + scrollWeapon]); weaponSlotsLeft -= Globals.PlayerNation.unlockedWeapons[k + scrollWeapon].Slotsize;
        }

        public void WeaponRightClick(int k)
        {
            if (selectedHull == null || Globals.PlayerNation.unlockedWeapons.Count <= k + scrollWeapon)
            {
                return;
            }
            if (selectedWeapons.Contains(Globals.PlayerNation.unlockedWeapons[k + scrollWeapon]))
            {
                weaponSlotsLeft += Globals.PlayerNation.unlockedWeapons[k + scrollWeapon].Slotsize;
                selectedWeapons.Remove(Globals.PlayerNation.unlockedWeapons[k + scrollWeapon]);
            }
        }

        public override void Draw()
        {
            backGround.DrawLate(100, 100, false, 0, 1, 1, 1, 1);
            name = nameBox.Text;

            List<Ship> hulls = Globals.PlayerNation.unlockedHulls;
            Window.window.DrawText("Hull:", 105, 255, 0, 0, 0, 1, true, Globals.buttonFont);

            for (int i = scrollHull; i < (buttonAmount + scrollHull < hulls.Count ? buttonAmount + scrollHull : hulls.Count); i++)
            {
                Window.window.DrawText(hulls[i].Name, 105, 280 + (i - scrollHull) * 20, hulls[i] == selectedHull ? 1 : 0, 0, 0, 1, true, Globals.buttonFont);
            }

            if (selectedHull != null)
            {
                List<Weapon> weapons = Globals.PlayerNation.unlockedWeapons;
                Window.window.DrawText("Weapons: (" + (selectedHull.MaxSlots - weaponSlotsLeft) + "/" + selectedHull.MaxSlots + ")", 310, 255, 0, 0, 0, 1, true, Globals.buttonFont);

                for (int i = scrollWeapon; i < (buttonAmount + scrollWeapon < weapons.Count ? buttonAmount + scrollWeapon : weapons.Count); i++)
                {
                    Window.window.DrawText(weapons[i].Name, 312, 280 + (i - scrollWeapon) * 20, weapons[i] == selectedWeapon ? 1 : 0, 0, 0, 1, true, Globals.buttonFont);
                }
            }

            if (selectedHull != null)
            {
                List<SpecialEquipment> specials = Globals.PlayerNation.unlockedSpecials;
                Window.window.DrawText("Specials: (" + (selectedHull.SpecialSlots - specialSlotsLeft) + "/" + selectedHull.SpecialSlots + ")", 517, 255, 0, 0, 0, 1, true, Globals.buttonFont);

                for (int i = scrollSpecial; i < (buttonAmount + scrollSpecial < specials.Count ? buttonAmount + scrollSpecial : specials.Count); i++)
                {
                    Window.window.DrawText(specials[i].Name, 519, 280 + (i - scrollSpecial) * 20, specials[i] == special ? 1 : 0, 0, 0, 1, true, Globals.buttonFont);
                }
            }
            for (int i = scrollPreset; i < (buttonAmount + scrollPreset < Globals.PlayerNation.presets.Count ? buttonAmount + scrollPreset : Globals.PlayerNation.presets.Count); i++)
            {
                Window.window.DrawText(Globals.PlayerNation.presets[i].Name, 1920-600, 280 + (i - scrollPreset) * 20, 0, 0, 0, 1, true, Globals.buttonFont);
            }

            // Hull stats
            if (selectedHull != null)
            {
                Window.window.DrawText("Type: " + selectedHull.Type.ToString(), 105, 680, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Hull: " + selectedHull.MaxHealth, 105, 700, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Speed: " + selectedHull.Speed, 105, 720, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Evasion: " + selectedHull.Evasiveness, 105, 740, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Cost: \n" + selectedHull.materialCost + " Materials\n" + selectedHull.moneyCost + " Money", 105, 760, 0, 0, 0, 1, true, Globals.buttonFont);
            }

            // Weapon stats
            if (selectedWeapon != null)
            {
                // Damage
                // Type
                // Slots
                // Cost
                Window.window.DrawText(selectedWeapon.Name, 310, 680, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Damage: " + selectedWeapon.MinDamage + " - " + selectedWeapon.MaxDamage, 310, 700, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Type: " + selectedWeapon.Type.ToString(), 310, 720, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Size: " + selectedWeapon.Slotsize, 310, 740, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Cost: " + selectedWeapon.Cost + " Money", 310, 760, 0, 0, 0, 1, true, Globals.buttonFont);
            }

            // Special stats
            if (special != null)
            {
                Window.window.DrawText(special.Name, 517, 680, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Cost: " + special.Cost + " Money", 517, 760, 0, 0, 0, 1, true, Globals.buttonFont);
            }

            // Ship stats
            if (selectedHull != null)
            {
                // Type
                // Hull
                // Speed
                // Evasiveness
                // M: Weapon damage
                // M: Weapon Types
                // F: Capacity
                // F: Capacity Type (Population / Resources)
                Window.window.DrawText("Type: " + selectedHull.Type.ToString(), 1620 - 50, 1080 / 2 - 275, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Hull: " + selectedHull.MaxHealth, 1620 - 50, 1080 / 2 - 255, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Speed: " + selectedHull.Speed, 1620 - 50, 1080 / 2 - 235, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Evasion: " + selectedHull.Evasiveness, 1620 - 50, 1080 / 2 - 215, 0, 0, 0, 1, true, Globals.buttonFont);

                int offset = 0;

                double damage = 0;
                Dictionary<Weapon, int> weaponCount = new Dictionary<Weapon, int>();
                foreach (Weapon weapon in selectedHull.WeaponList)
                {
                    damage += (weapon.MaxDamage + weapon.MinDamage) / 2;
                    if (weaponCount.ContainsKey(weapon))
                    {
                        weaponCount[weapon] += 1;
                    }
                    else
                    {
                        weaponCount.Add(weapon, 1);
                        
                    }
                }
                Window.window.DrawText("Dmg: " + damage, 1620 - 50, 1080 / 2 - 195, 0, 0, 0, 1, true, Globals.buttonFont);
                Window.window.DrawText("Types: " + selectedHull.WeaponTypes(), 1620 - 50, 1080 / 2 - 175, 0, 0, 0, 1, true, Globals.buttonFont);

                foreach (var entry in weaponCount)
                {
                    Window.window.DrawText(" - " + entry.Key.Name + " " + entry.Value + "x", 1620 - 50, 1080 / 2 - 155 + offset * 20, 0, 0, 0, 1, true, Globals.buttonFont);
                    offset += 1;
                }

                switch (selectedHull.Type)
                {
                    case ShipType.FREIGHTER:
                        Window.window.DrawText("Cap: " + selectedHull.MaxResourceLoad, 1620 - 50, 1080 / 2 - 155 + offset * 20, 0, 0, 0, 1, true, Globals.buttonFont);
                        offset += 1;
                        break;
                    case ShipType.TRANSPORTATION:
                        Window.window.DrawText("Cap: " + selectedHull.MaxPeopleLoad, 1620 - 50, 1080 / 2 - 155 + offset * 20, 0, 0, 0, 1, true, Globals.buttonFont);
                        offset += 1;
                        break;
                }

                if (selectedSpecial != null)
                {
                    Window.window.DrawText("Special: \n" + selectedSpecial.Name, 1620 - 50, 1080 / 2 - 155 + offset * 20, 0, 0, 0, 1, true, Globals.buttonFont);
                    offset += 2;
                }

                materialCost = selectedHull.materialCost;
                moneyCost = selectedHull.moneyCost;

                if (selectedSpecial != null)
                {
                    moneyCost += selectedSpecial.Cost;
                }
                
                foreach (Weapon weapon in selectedWeapons)
                {
                    moneyCost += weapon.Cost;
                }
                Window.window.DrawText("Cost per ship:\n" + materialCost + " Materials\n" + moneyCost + " Money", 1620 - 50, 1080 / 2 - 135 + offset * 20, 0, 0, 0, 1, true, Globals.buttonFont);

                if (int.TryParse(quantity.Text, out int q))
                {
                    moneyCost *= q;
                    materialCost *= q;
                }

                Window.window.DrawText("Total Cost:\n" + materialCost + " Materials\n" + moneyCost + " Money", 1620 - 50, 1080 / 2 - 55 + offset * 20, 0, 0, 0, 1, true, Globals.buttonFont);

            }

        }
        // Unlocked Hulls


        // Weapons for slots
        // Special equipment if available

        // Stats


        public void Scroll(int val)
        {
            // Hulls
            if (Globals.checkCol(Window.window.mouseX, Window.window.mouseY, 0, 0, 105, 280, 190, 20 * buttonAmount))
            {
                scrollHull += val;
                if (scrollHull > Globals.PlayerNation.unlockedHulls.Count - buttonAmount)
                {
                    scrollHull = Globals.PlayerNation.unlockedHulls.Count - buttonAmount;
                }
                if (scrollHull < 0)
                {
                    scrollHull = 0;
                }
            }

            // Weapons
            if (Globals.checkCol(Window.window.mouseX, Window.window.mouseY, 0, 0, 312, 280, 190, 20 * buttonAmount))
            {
                scrollWeapon += val;
                if (scrollWeapon > Globals.PlayerNation.unlockedWeapons.Count - buttonAmount)
                {
                    scrollWeapon = Globals.PlayerNation.unlockedWeapons.Count - buttonAmount;
                }
                if (scrollWeapon < 0)
                {
                    scrollWeapon = 0;
                }
            }
            // specials
            if (Globals.checkCol(Window.window.mouseX, Window.window.mouseY, 0, 0, 519, 280, 190, 20 * buttonAmount))
            {
                scrollSpecial += val;
                if (scrollSpecial > Globals.PlayerNation.unlockedSpecials.Count - buttonAmount)
                {
                    scrollSpecial = Globals.PlayerNation.unlockedSpecials.Count - buttonAmount;
                }
                if (scrollSpecial < 0)
                {
                    scrollSpecial = 0;
                }
            }
            // Preset
            if (Globals.checkCol(Window.window.mouseX, Window.window.mouseY, 0, 0, 1920-600, 280, 190, 20 * buttonAmount))
            {
                scrollPreset += val;
                if (scrollPreset > Globals.PlayerNation.presets.Count - buttonAmount)
                {
                    scrollPreset = Globals.PlayerNation.presets.Count - buttonAmount;
                }
                if (scrollPreset < 0)
                {
                    scrollPreset = 0;
                }
            }
        }


        
        public override bool MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if(!Globals.checkCol(mx, my, 0, 0, 100, 100, 1720, 880)) 
            {
                Globals.currentUI = null;
                Globals.activeButtons = new List<DrawnButton>();
                return false;
            }
            return true;
        }


    }
}
