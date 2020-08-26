using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    abstract class Weapon
    {
        //Weapon Stats
        public string Name { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Accuracy { get; set; }

        //Weapon slots this weapon occupies
        public int Size { get; set; }

        protected Weapon(int minDamage, int maxDamage)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}
