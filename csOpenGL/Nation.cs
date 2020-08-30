using FairyJam.Equipment.SpecialEquipment;
using FairyJam.Equipment.Weapons;
using FairyJam.Orbitals;
using FairyJam.Ships;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{

    public class Nation
    {

        public double Materials { get; set; }
        public double Fuel { get; set; }
        public double Food { get; set; }
        public double Money { get; set; }
        public double Happiness { get; set; }
        public double TechPoints { get; set; }
        public double Population { get; set; }
        public double[] resourceChanges { get; set; }
        public List<Leader> leaders { get;}
        public List<Scientist> scientists { get;}
        public List<SupplyCompany> supplyCompanies { get;}
        public string Name { get; }
        public System.Drawing.Color Color { get; }
        public List<Fleet> fleets;
        public List<Ship> unlockedHulls, presets;
        public List<Weapon> unlockedWeapons;
        public List<SpecialEquipment> unlockedSpecials;
        public double[] modifiers = new double[Enums.modAmount];


        public Nation(string name)
        {
            Materials = 1000;
            Fuel = 1000;
            Food = 10;
            Money = 1000;
            Happiness = 0;
            TechPoints = 0;
            Population = 10;
            Color = Color.FromArgb(255, Globals.random.Next(256), Globals.random.Next(256), Globals.random.Next(256));
            leaders = new List<Leader>();
            scientists = new List<Scientist>();
            supplyCompanies = new List<SupplyCompany>();
            fleets = new List<Fleet>();
            presets = new List<Ship>();
            unlockedHulls = new List<Ship>()
            {
                new Military(this, "MilitaryMk0", 10, 10, 10, 0, 0, 100, 1)
            };
            unlockedWeapons = new List<Weapon>()
            {
                new BasicGun(),
                new BasicPlasmaGun()
            };
            unlockedSpecials = new List<SpecialEquipment>()
            {
                new Cloak()
            };
            resourceChanges = new double[7];
            Name = name;
        }

        public void addToMod(double a, Enums.Modifier mod) 
        {
            modifiers[(int)mod] += a;
        }

        public void multiplyToMod(double a, Enums.Modifier mod) 
        {
            modifiers[(int)mod] *= a;
        }

        public double getMod(Enums.Modifier mod) 
        {
            return modifiers[(int)mod];
        }


        public double PopCap() 
        {
            double res = 0;
            for(int i=0;i<Globals.map.mapWidth;i++)
            {
                for(int j=0;j<Globals.map.mapHeight;j++)
                {
                    if(Globals.map.grid[i,j].ps != null && Globals.map.grid[i,j].ps.Owner == this) 
                    {
                        foreach(Planet p in Globals.map.grid[i,j].ps.planets) 
                        {
                            res += p.PopCap;
                        }
                    }
                }
            }
            return res;
        }

        public void UpdateResources()
        {
            resourceChanges = new double[7]
            {
                Money,
                Materials,
                Food,
                Fuel,
                Population,
                Happiness,
                TechPoints
            };
        }

        public void SetTurnResources()
        {
            resourceChanges = new double[7]
            {
                -resourceChanges[0] + Money,
                -resourceChanges[1] + Materials,
                -resourceChanges[2] + Food,
                -resourceChanges[3] + Fuel,
                -resourceChanges[4] + Population,
                -resourceChanges[5] + Happiness,
                -resourceChanges[6] + TechPoints
            };
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

        public void AddFleet(Fleet f) 
        {
            fleets.Add(f);
        }

        public void RemoveFleet(Fleet f) 
        {
            fleets.Remove(f);
        }

        public void AddSupplyCompany(SupplyCompany s)
        {
            supplyCompanies.Add(s);
            Materials += s.Materials;
            Fuel += s.Fuel;
            Food += s.Food;
            Money += s.Money;
            Population += s.Population;
        }

        public void RemoveSupplyCompany(SupplyCompany s)
        {
            supplyCompanies.Remove(s);
            Materials -= s.Materials;
            Fuel -= s.Fuel;
            Food -= s.Food;
            Money -= s.Money;
            Population -= s.Population;
        }

        public void UpdateHappiness(double i)
        {
            Happiness += i;
            if (Happiness < -100)
            {
                Happiness = -100;
            }
            else if (Happiness > 100)
            {
                Happiness = 100;
            }
        }

        public void Turn()
        {
            // Enemy fleet growth
            if (this != Globals.PlayerNation)
            {
                foreach (Fleet fleet in fleets)
                {
                    if (Globals.random.Next(100) < Balance.HostileFleetGrowthChance)
                    {
                        for (int i = Globals.random.Next(1, 4); i > 0; i--)
                        {
                            Ship s = new Military(this);
                            for (int x = Globals.random.Next(1, 5); x > 0; x--)
                            {
                                s.AddWeapon(new BasicGun());
                            }
                            fleet.AddToFleet(s);
                        }
                    }
                }
            }

            for(int i=0;i<Enums.modAmount;i++)
            {
                modifiers[i] = 1;
            }
            for(int i = leaders.Count() - 1; i >= 0; i--)
            {
                Leader l = leaders[i];
                l.Turn(() => {}, this);//leaders.Remove(l)
            }
            for(int i = scientists.Count() - 1; i >= 0; i--)
            {
                Scientist l = scientists[i];
                l.Turn(() => {}, this); //scientists.Remove(l)
            }

            double popCap = PopCap();

            // Convert people into money
            Money += Population * Balance.MoneyPerPop;

            // Update Population
            // If not enough food
            if (Population * Balance.FoodPerPop > Food)
            {
                UpdateHappiness(10*(Food - Population * Balance.FoodPerPop) / (popCap * Balance.FoodPerPop));
                Population--;
                Population *= 0.9;
                if(Population < 0) 
                {
                    Population = 0;
                }
                Food = 0;
            }
            // If enough
            else 
            {
                Food -= Population*Balance.FoodPerPop;
                UpdateHappiness(1);
                Population *= (getMod(Enums.Modifier.PopulationGrowth) + Happiness * Balance.HappinessPopGrowth);
            }

            // Check if pop is over cap
            if (Population > popCap)
            {
                Population = popCap;
            }

            // Techpoints gain
            TechPoints += Balance.TechpointsPerTurn * getMod(Enums.Modifier.TechGrowth);

            
            for(int i = fleets.Count() - 1; i >= 0; i--)
            {
                Fleet l = fleets[i];
                l.Turn();
            }
        }
    }
}
