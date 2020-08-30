using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class ScientistUI : OneMinuteUI
    {

        private List<Scientist> possibleScientists;
        private Scientist selectedScientist;
        private DrawnButton hireButton;

        public ScientistUI()
        {
            GenerateScientists();
            foreach (Scientist l in possibleScientists)
            {
                scrolledButtons.Add(new ScientistEntry(l));
            }
            selectedScientist = possibleScientists[0];
            hireButton = new DrawnButton(selectedScientist.Hired ? "FIRE" : "HIRE", 1600, 850, 320, 100, () => { HireFire(); }, 0.5f, 0.5f, 0.5f);
            buttons.Add(hireButton);
        }

        // Generate Scientist List
        public void GenerateScientists()
        {
            possibleScientists = new List<Scientist>();
            Namelist namelist = Globals.nameLists[GenerationSettings.ScientistNameGroup]; // @TODO For now just the first on we find, later on allow for selection?
            for (int i = 0; i < 250; i++)
            {
                namelist.Next();
                Trait trait = Globals.possibleTraitsScientist[Globals.random.Next(Globals.possibleTraitsScientist.Length)]; // Gets a random existing trait
                List<Trait> traitsToAdd = new List<Trait> { trait };
                if (Globals.random.Next(0, 100) > 50)
                {
                    trait = Globals.possibleTraitsScientist[Globals.random.Next(Globals.possibleTraitsScientist.Length)];
                    traitsToAdd.Add(trait);

                    if (Globals.random.Next(0, 100) > 50)
                    {
                        trait = Globals.possibleTraitsScientist[Globals.random.Next(Globals.possibleTraitsScientist.Length)];
                        traitsToAdd.Add(trait);
                    }
                }
                possibleScientists.Add(new Scientist(100, namelist.GivenName, namelist.FamilyName, Enums.ScientistTitle.Feiv, traitsToAdd, false));
            }
        }

        public override void Draw()
        {
            base.Draw();
            // Draw Scientist information and stats

            // Draw Name
            Window.window.DrawText(selectedScientist.ToString(), 600, 50);

            // Draw Traits
            for (int i = 0; i < selectedScientist.Traits.Count; i++)
            {
                Window.window.DrawText(selectedScientist.Traits[i].ToString(), 600, 100 + 80 * i, false, Globals.buttonFont);
                Window.window.DrawText(selectedScientist.Traits[i].Description, 650, 130 + 80 * i, false, Globals.buttonFont);
            }

            // Draw Description

            // Draw random background information that just wastes time
        }

        public override void SelectFromList(int i)
        {
            if (i < possibleScientists.Count)
            {
                if (selectedScientist == possibleScientists[i])
                {
                    hireButton.OnClick();
                }
                else
                {
                    selectedScientist = possibleScientists[i];
                    hireButton.Text = selectedScientist.Hired ? "FIRE" : "HIRE";
                }
            }
        }

        public void HireFire()
        {
            if (!selectedScientist.Hired && Globals.PlayerNation.scientists.Count >= 10)
            {
                return;
            }
            selectedScientist.Hired = !selectedScientist.Hired;
            hireButton.Text = selectedScientist.Hired ? "FIRE" : "HIRE";
            if (selectedScientist.Hired)
            {
                Globals.PlayerNation.AddScientist(selectedScientist);
            }
            else
            {
                Globals.PlayerNation.RemoveScientist(selectedScientist);
            }
        }

    }

    class ScientistEntry : ListEntry
    {

        public Scientist scientist;

        public ScientistEntry(Scientist scientist)
        {
            this.scientist = scientist;
        }

        public override void Draw(int y)
        {
            if (scientist.Hired)
            {
                Window.window.DrawText(scientist.ToString(), 100, 60 + 25 * y, 1, 0, 0, 1, true, Globals.buttonFont);
            }
            else
            {
                Window.window.DrawText(scientist.ToString(), 100, 60 + 25 * y, true, Globals.buttonFont);
            }
            int x = 0;
            foreach (Trait t in scientist.Traits)
            {
                t.sprite.Draw(2 + 32 * x, 60 + 25 * y, false);
                x++;
            }
        }

    }
}
