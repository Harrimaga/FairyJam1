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

    }
}
