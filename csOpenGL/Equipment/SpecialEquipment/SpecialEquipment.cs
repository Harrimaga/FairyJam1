using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Equipment.SpecialEquipment
{
    public abstract class SpecialEquipment
    {
        public string Name { get; set; }
        public bool passive { get; set; }
        public double Cost { get; set; }

        protected SpecialEquipment()
        {
            Cost = 100;
        }

        public abstract void UseSpecial();

    }
}
