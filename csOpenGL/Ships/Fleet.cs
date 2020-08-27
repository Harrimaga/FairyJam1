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
        private PlanetarySystem destination, next;
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

        public void SetDestination(PlanetarySystem ps)
        {
            destination = ps;

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
    }
}
