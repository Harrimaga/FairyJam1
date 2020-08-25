using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Technology
{
    class TechTree
    {
        public Enums.TechType Type { get; set; }
        private List<Tier> Tiers { get; set; }

        public TechTree(Enums.TechType type)
        {
            Type  = type;
            Tiers = new List<Tier>();
        }

        public void AddTech(Technology technology)
        {
            Tiers[Tiers.Count - 1].Technologies.Add(technology);
        }

        public void NewTier()
        {
            Tiers.Add(new Tier());
        }

        private class Tier
        {
            public List<Technology> Technologies { get; set; }

            public Tier()
            {
                Technologies = new List<Technology>();
            }
        }
    }
}
