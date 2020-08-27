using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public abstract class Weapon
    {
        //Weapon Stats
        public string Name { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Accuracy { get; set; }
        public DamageType Type { get; set; }

        //Weapon slots this weapon occupies
        public int Slotsize { get; set; }

        protected Weapon(string name, int minDamage, int maxDamage, int accuracy, int slots)
        {
            Name = name;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            Accuracy = accuracy;

            Slotsize = slots;
        }
    }
}
