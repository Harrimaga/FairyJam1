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

        public LeaderUI() {
            GenerateLeaders();
            foreach(Leader l in possibleLeaders)
            {
                scrolledButtons.Add(new LeaderEntry(l));
            }
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
                possibleLeaders.Add(new Leader(100, namelist.GivenName, namelist.FamilyName, Enums.LeaderTitle.Admiral, traitsToAdd, false));
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
        }

    }

}
