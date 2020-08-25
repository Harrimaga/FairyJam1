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

        public LeaderUI() {
            GenerateLeaders();
            foreach(Leader l in possibleLeaders)
            {
                scrolledButtons.Add(new LeaderEntry(l));
            }
            selectedLeader = possibleLeaders[0];
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
                }
                possibleLeaders.Add(new Leader(100, namelist.GivenName, namelist.FamilyName, Enums.LeaderTitle.Admiral, traitsToAdd, false));
            }
        }

        public override void Draw()
        {
            base.Draw();
            // Draw Leader information and stats

            // Draw Name
            Window.window.DrawText(selectedLeader.ToString(), 400, 50);
            
            // Draw Traits

            // Draw Description

            // Draw random background information that just wastes time
        }

        public override void SelectFromList(int i)
        {
            if(i < possibleLeaders.Count)
            {
                selectedLeader = possibleLeaders[i];
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
            Window.window.DrawText(leader.ToString(), 100, 60 + 25 * y, Globals.buttonFont);
            int x = 0;
            foreach(Trait t in leader.Traits)
            {
                t.sprite.Draw(32*x, 60 + 25 * y);
                x++;
            }
        }

    }

}
