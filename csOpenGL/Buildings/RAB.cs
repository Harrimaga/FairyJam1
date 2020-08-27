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
            double gains0 = gains[0] * o.GetEfficiency(0) > o.materialsAvailable[0] ? o.materialsAvailable[0] : gains[0] * o.GetEfficiency(0);
            double gains1 = gains[1] * o.GetEfficiency(1) > o.materialsAvailable[1] ? o.materialsAvailable[1] : gains[1] * o.GetEfficiency(1);
            double gains2 = gains[2] * o.GetEfficiency(2) > o.materialsAvailable[2] ? o.materialsAvailable[2] : gains[2] * o.GetEfficiency(2);
            o.Owner.Food += gains0;
            o.Owner.Materials += gains1;
            o.Owner.Fuel += gains2;

            o.materialsAvailable[0] -= gains0;
            o.materialsAvailable[1] -= gains1;
            o.materialsAvailable[2] -= gains2;
        }
    }
}
