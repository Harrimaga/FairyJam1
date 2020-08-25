using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{

    class Nation
    {

        public double Materials { get; set; }
        public double Fuel { get; set; }
        public double Food { get; set; }
        public double Money { get; set; }
        public double Happiness { get; set; }
        public double TechPoints { get; set; }
        public double Population { get; set; }

        public List<Leader> leaders { get;}
        public List<Scientist> scientists { get;}

        public Nation()
        {
            Materials = 5;
            Fuel = 5;
            Food = 5;
            Money = 5;
            Happiness = 5;
            TechPoints = 5;
            Population = 5;
            leaders = new List<Leader>();
            scientists = new List<Scientist>();

        }

        public void AddLeader(Leader l)
        {
            leaders.Add(l);
        }

        public void RemoveLeader(Leader l)
        {
            leaders.Remove(l);
        }

        public void AddScientist(Scientist s)
        {
            scientists.Add(s);
        }

        public void RemoveScientist(Scientist s)
        {
            scientists.Remove(s);
        }

        public void Update()
        {
            for(int i = leaders.Count() - 1; i >= 0; i--)
            {
                Leader l = leaders[i];
                l.Update(() => leaders.Remove(l));
            }
            for(int i = scientists.Count() - 1; i >= 0; i--)
            {
                Scientist l = scientists[i];
                l.Update(() => scientists.Remove(l));
            }
        }

    }

}
