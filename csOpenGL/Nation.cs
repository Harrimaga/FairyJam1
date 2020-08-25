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
            Materials = 10;
            Fuel = 10;
            Food = 10;
            Money = 10;
            Happiness = 10;
            TechPoints = 10;
            Population = 10;
            leaders = new List<Leader>();
            scientists = new List<Scientist>();

        }

        public double AddSuppliesPointBuy(int num, double amount) {
            switch(num) {
            case 0:
                if(Materials + amount < 10) {
                    return 10 - Materials;
                }
                Materials += amount;
                break;
            case 1:
                if(Food + amount < 10) {
                    return 10 - Food;
                }
                Food += amount;
                break;
            case 2:
                if(Fuel + amount < 10) {
                    return 10 - Fuel;
                }
                Fuel += amount;
                break;
            case 3:
                if(Population + amount < 10) {
                    return 10 - Population;
                }
                Population += amount;
                break;
            case 4:
                if(Money + amount < 10) {
                    return 10 - Money;
                }
                Money += amount;
                break;
            }
            return amount;
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
