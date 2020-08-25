using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class LeaderUI : OneMinuteUI
    {
        private List<Leader> possibleLeaders;
        private Leader selectedLeader;
        private DrawnButton hireButton;

        public LeaderUI() {
            GenerateLeaders();
            foreach(Leader l in possibleLeaders)
            {
                scrolledButtons.Add(new LeaderEntry(l));
            }
            selectedLeader = possibleLeaders[0];
            hireButton = new DrawnButton(selectedLeader.Hired ? "FIRE" : "HIRE", 1600, 800, 320, 100, () => { HireFire(); }, 0.5f, 0.5f, 0.5f);
            buttons.Add(hireButton);
        }

        // Generate Leader List
        public void GenerateLeaders()
        {
            possibleLeaders = new List<Leader>();
            Namelist namelist = Globals.nameLists[1]; // @TODO For now just the first on we find, later on allow for selection?
            for (int i = 0; i < 250; i++)
            {
                namelist.Next();
                Trait trait = Globals.possibleTraits[Globals.random.Next(Globals.possibleTraits.Length)]; // Gets a random existing trait
                List<Trait> traitsToAdd = new List<Trait> { trait };
                if (Globals.random.Next(0,100) > 50)
                {
                    trait = Globals.possibleTraits[Globals.random.Next(Globals.possibleTraits.Length)];
                    traitsToAdd.Add(trait);

                    if (Globals.random.Next(0, 100) > 50)
                    {
                        trait = Globals.possibleTraits[Globals.random.Next(Globals.possibleTraits.Length)];
                        traitsToAdd.Add(trait);
                    }
                }
                possibleLeaders.Add(new Leader(100, namelist.GivenName, namelist.FamilyName, Enums.LeaderTitle.Admiral, traitsToAdd, false));
            }
        }

        public override void Draw()
        {
            base.Draw();
            // Draw Leader information and stats

            // Draw Name
            Window.window.DrawText(selectedLeader.ToString(), 600, 50);
            
            // Draw Traits
            for(int i = 0; i < selectedLeader.Traits.Count; i++)
            {
                Window.window.DrawText(selectedLeader.Traits[i].ToString(), 600, 100 + 80 * i, Globals.buttonFont);
                Window.window.DrawText(selectedLeader.Traits[i].Description, 650, 130 + 80 * i, Globals.buttonFont);
            }

            // Draw Description

            // Draw random background information that just wastes time
        }

        public override void SelectFromList(int i)
        {
            if(i < possibleLeaders.Count)
            {
                if (selectedLeader == possibleLeaders[i])
                {
                    hireButton.OnClick();
                }
                else
                {
                    selectedLeader = possibleLeaders[i];
                    hireButton.Text = selectedLeader.Hired ? "FIRE" : "HIRE";
                }
            }
        }

        public void HireFire()
        {
            if(!selectedLeader.Hired && Globals.PlayerNation.leaders.Count >= 10)
            {
                return;
            }
            selectedLeader.Hired = !selectedLeader.Hired;
            hireButton.Text = selectedLeader.Hired ? "FIRE" : "HIRE";
            if (selectedLeader.Hired)
            {
                Globals.PlayerNation.AddLeader(selectedLeader);
            }
            else
            {
                Globals.PlayerNation.RemoveLeader(selectedLeader);
            }
        }

    }

    class LeaderEntry : ListEntry
    {

        public Leader leader;

        public LeaderEntry(Leader leader)
        {
            this.leader = leader;
        }

        public override void Draw(int y)
        {
            if (leader.Hired)
            {
                Window.window.DrawText(leader.ToString(), 100, 60 + 25 * y, 1, 0, 0, 1, Globals.buttonFont);
            }
            else
            {
                Window.window.DrawText(leader.ToString(), 100, 60 + 25 * y, Globals.buttonFont);
            }
            int x = 0;
            foreach(Trait t in leader.Traits)
            {
                t.sprite.Draw(2+ 32*x, 60 + 25 * y);
                x++;
            }
        }

    }

}
