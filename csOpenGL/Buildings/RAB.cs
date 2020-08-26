using FairyJam.Orbitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Buildings
{
    public class RAB : Building
    {

        private double[] gains;
        
        /// Food / Materials / Fuel
        public RAB(string name, double[] gains) : base(name)
        {
            this.gains = gains;
        }

        public override void Turn(Orbital o)
        {
            // TODO: Efficiency
            o.Owner.Food += gains[0] > o.materialsAvailable[0] ? o.materialsAvailable[0] : gains[0];
            o.Owner.Materials += gains[1] > o.materialsAvailable[1] ? o.materialsAvailable[1] : gains[1];
            o.Owner.Fuel += gains[2] > o.materialsAvailable[2] ? o.materialsAvailable[2] : gains[2];

            o.materialsAvailable[0] -= gains[0] > o.materialsAvailable[0] ? o.materialsAvailable[0] : gains[0];
            o.materialsAvailable[1] -= gains[1] > o.materialsAvailable[1] ? o.materialsAvailable[1] : gains[1];
            o.materialsAvailable[2] -= gains[2] > o.materialsAvailable[2] ? o.materialsAvailable[2] : gains[2];
        }
    }
}
