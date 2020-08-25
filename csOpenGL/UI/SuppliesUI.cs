using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class SuppliesUI : OneMinuteUI
    {
        private List<SupplyCompany> possibleSupplyCompanies;
        private SupplyCompany selectedSupplyCompany;
        private DrawnButton hireButton;

        public SuppliesUI()
        {
            GenerateSupplyCompanys();
            foreach (SupplyCompany l in possibleSupplyCompanies)
            {
                scrolledButtons.Add(new SupplyCompanyEntry(l));
            }
            selectedSupplyCompany = possibleSupplyCompanies[0];
            hireButton = new DrawnButton(selectedSupplyCompany.Hired ? "FIRE" : "HIRE", 1600, 800, 320, 100, () => { HireFire(); }, 0.5f, 0.5f, 0.5f);
            buttons.Add(hireButton);
        }

        // Generate SupplyCompany List
        public void GenerateSupplyCompanys()
        {
            possibleSupplyCompanies = new List<SupplyCompany>();
            Namelist namelist = Globals.nameLists[0]; // @TODO For now just the first on we find, later on allow for selection?
            for (int i = 0; i < 250; i++)
            {
                namelist.Next();
                Trait trait = Globals.possibleTraits[Globals.random.Next(Globals.possibleTraits.Length)]; // Gets a random existing trait
                List<Trait> traitsToAdd = new List<Trait> { trait };
                if (Globals.random.Next(0, 100) > 50)
                {
                    trait = Globals.possibleTraits[Globals.random.Next(Globals.possibleTraits.Length)];
                    traitsToAdd.Add(trait);

                    if (Globals.random.Next(0, 100) > 50)
                    {
                        trait = Globals.possibleTraits[Globals.random.Next(Globals.possibleTraits.Length)];
                        traitsToAdd.Add(trait);
                    }
                }
                possibleSupplyCompanies.Add(new SupplyCompany());
            }
        }

        public override void Draw()
        {
            base.Draw();
            // Draw SupplyCompany information and stats

            // Draw Name
            Window.window.DrawText(selectedSupplyCompany.ToString(), 600, 50);

            // Draw Supplies
            string[] supplies = selectedSupplyCompany.GetSupplies();

            int t = 0;
            for (int i = 0; i < supplies.Length; i++)
            {
                if(supplies[i].Equals("0")) continue;
                Window.window.DrawText(selectedSupplyCompany.SupplyName(i) + ": " + supplies[i], 600, 100 + 80 * t);
                t++;
            }

            // Draw Traits
            //for (int i = 0; i < selectedSupplyCompany.Traits.Count; i++)
            //{
            //    Window.window.DrawText(selectedSupplyCompany.Traits[i].ToString(), 600, 100 + 80 * i, Globals.buttonFont);
            //    Window.window.DrawText(selectedSupplyCompany.Traits[i].Description, 650, 130 + 80 * i, Globals.buttonFont);
            //}

            // Draw Description

            // Draw random background information that just wastes time
        }

        public override void SelectFromList(int i)
        {
            if (i < possibleSupplyCompanies.Count)
            {
                if (selectedSupplyCompany == possibleSupplyCompanies[i])
                {
                    hireButton.OnClick();
                }
                else
                {
                    selectedSupplyCompany = possibleSupplyCompanies[i];
                    hireButton.Text = selectedSupplyCompany.Hired ? "FIRE" : "HIRE";
                }
            }
        }

        public void HireFire()
        {
            if (!selectedSupplyCompany.Hired && Globals.PlayerNation.supplyCompanies.Count >= 10)
            {
                return;
            }
            selectedSupplyCompany.Hired = !selectedSupplyCompany.Hired;
            hireButton.Text = selectedSupplyCompany.Hired ? "FIRE" : "HIRE";
            if (selectedSupplyCompany.Hired)
            {
                Globals.PlayerNation.AddSupplyCompany(selectedSupplyCompany);
            }
            else
            {
                Globals.PlayerNation.RemoveSupplyCompany(selectedSupplyCompany);
            }
        }

    }

    class SupplyCompanyEntry : ListEntry
    {

        public SupplyCompany supplyCompany;
        private Sprite[] sprites;

        public SupplyCompanyEntry(SupplyCompany supplyCompany)
        {
            this.supplyCompany = supplyCompany;
            sprites = new Sprite[5]{
                new Sprite(25, 25, 0, Textures.Get(Textures.materials)),
                new Sprite(25, 25, 0, Textures.Get(Textures.food)),
                new Sprite(25, 25, 0, Textures.Get(Textures.fuel)),
                new Sprite(25, 25, 0, Textures.Get(Textures.population)),
                new Sprite(25, 25, 0, Textures.Get(Textures.money))
            };
        }

        public override void Draw(int y)
        {
            if (supplyCompany.Hired)
            {
                Window.window.DrawText(supplyCompany.ToString(), 100, 60 + 25 * y, 1, 0, 0, 1, true, Globals.buttonFont);
            }
            else
            {
                Window.window.DrawText(supplyCompany.ToString(), 100, 60 + 25 * y, true, Globals.buttonFont);
            }
            int x = 0;

            // Draw Supplies
            string[] supplies = supplyCompany.GetSupplies();

            int t = 0;
            for (int i = 0; i < supplies.Length; i++)
            {
                if (supplies[i].Equals("0")) continue;
                sprites[i].Draw(2 + 32 * t, 60 + 25 * y);
                t++;
            }

            //int x = 0;
            //foreach (Trait t in scientist.Traits)
            //{
            //    t.sprite.Draw(2 + 32 * x, 60 + 25 * y);
            //    x++;
            //}

        }

    }
}
