using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Equipment.Weapons
{
    public class BasicGun : Weapon
    {
        private const string name = "Basic Gun";
        private const int minDamage = 20, maxDamage = 30;
        private const int accuracy = 100;
        private const int slotSize = 1;
        public BasicGun() : base(name, minDamage, maxDamage, accuracy, slotSize)
        {

        }
    }
}
