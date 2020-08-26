using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Equipment
{
    abstract class SpecialEquipment
    {
        public string Name { get; set; }

        protected SpecialEquipment()
        {

        }

        public abstract void UseSpecial();

    }
}
