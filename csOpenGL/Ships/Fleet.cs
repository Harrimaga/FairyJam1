using FairyJam.Orbitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    public class Fleet
    {
        public List<Ship> ships;
        private PlanetarySystem destination, next, origin;
        private int turnsTillDestination;
        public Nation owner;
        public string Name { get; set; }

        public Fleet(Nation owner, List<Ship> ships)
        {
            this.ships = ships;
            this.owner = owner;
            Name = "Fleet " + Globals.random.Next(100, 999);
        }

        public void AddToFleet(Ship s)
        {
            ships.Add(s);
        }

        public void RemoveFromFleet(Ship s)
        {
            ships.Remove(s);
        }

        public void SetDestination(PlanetarySystem destination, PlanetarySystem origin)
        {
            this.destination = destination;
            this.origin = origin;

            // TODO: Calculate Turn Time
            turnsTillDestination = 2;
        }

        public int GetSpeed()
        {
            int lowest = int.MaxValue;
            foreach (Ship ship in ships)
            {
                if (ship.Speed < lowest)
                {
                    lowest = ship.Speed;
                }
            }
            return lowest;
        }

        public double DamageTotal() 
        {
            double damage = 0;
            foreach(Ship s in ships) 
            {
                foreach (Weapon weapon in s.WeaponList)
                {
                    damage += (weapon.MaxDamage + weapon.MinDamage) / 2;
                }
            }
            return damage;
        }

        public double GetEvasiveness()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.Evasiveness;
            }
            return e / ships.Count;
        }

        public double GetMaxHull()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.MaxHealth;
            }
            return e;
        }

        public double GetCurrentHull()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.HealthPoints;
            }
            return e;
        }

        public double GetTransportCap()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.MaxPeopleLoad;
            }
            return e;
        }

        public double GetResourceCap()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.MaxResourceLoad;
            }
            return e;
        }

        public void FightFleet(Fleet otherFleet)
        {
            //Copy of all fleet ships
            List<Ship> yourFleet = ships;
            List<Ship> enemyfleet = otherFleet.ships; 

            //Iterate through both as long 1 list is not empty
            while(yourFleet.Count > 0 && enemyfleet.Count > 0)
            {
                int total = yourFleet.Count + enemyfleet.Count;
                int nr = Globals.random.Next(0, total);
                Ship selectedShip;
                bool mine;
                if (nr > yourFleet.Count)
                {
                    selectedShip = yourFleet[nr];
                    mine = true;
                }
                else
                {
                    selectedShip = enemyfleet[nr - yourFleet.Count];
                    mine = false;
                }

                foreach (Weapon weapon in selectedShip.WeaponList)
                {
                    Ship targetShip;
                    if (mine)
                    {
                        if (otherFleet.ships.Count == 0)
                            break;
                        targetShip = otherFleet.ships[Globals.random.Next(0, otherFleet.ships.Count)];
                    }
                    else
                    {
                        if (ships.Count == 0)
                            break;
                        targetShip = ships[Globals.random.Next(0, ships.Count)];
                    }

                    selectedShip.AttackShip(targetShip, weapon);

                    if(targetShip.HealthPoints <= 0)
                        if(mine)
                        {
                            otherFleet.ships.Remove(targetShip);
                            enemyfleet.Remove(targetShip);
                        }
                        else
                        {

                            ships.Remove(targetShip);
                            yourFleet.Remove(targetShip);
                        } 
                }
            }

            //Iterate through yourfleet if it is not empty yet and enemy fleet still has ships left
            //YOUR fleet still has ships left to attack
            while (yourFleet.Count > 0 && otherFleet.ships.Count > 0)
            {
                Ship selectedShip = yourFleet[Globals.random.Next(0, yourFleet.Count)];

                foreach (Weapon weapon in selectedShip.WeaponList)
                {
                    Ship targetShip = otherFleet.ships[Globals.random.Next(0, otherFleet.ships.Count)];
                    selectedShip.AttackShip(targetShip, weapon);

                    if (targetShip.HealthPoints <= 0)
                    {
                        otherFleet.ships.Remove(targetShip);
                        enemyfleet.Remove(targetShip);
                    }
                }
            }

            //Iterate through enemyfleet if it is not empty yet and your fleet still has ships left
            //ENEMY fleet still has ships left to attack
            while (enemyfleet.Count > 0 && ships.Count > 0)
            {
                Ship selectedShip = enemyfleet[Globals.random.Next(0, enemyfleet.Count)];

                foreach (Weapon weapon in selectedShip.WeaponList)
                {
                    Ship targetShip = ships[Globals.random.Next(0, otherFleet.ships.Count)];
                    selectedShip.AttackShip(targetShip, weapon);

                    if (targetShip.HealthPoints <= 0)
                    {
                        ships.Remove(targetShip);
                        yourFleet.Remove(targetShip);
                    }
                }
            }
            
        }

    }
}
