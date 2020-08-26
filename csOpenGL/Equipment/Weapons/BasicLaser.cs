using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Equipment.Weapons
{
    class BasicLaser : Weapon
    {
        private const string name = "Basic Laser";
        private const int minDamage = 30, maxDamage = 40;
        private const int accuracy = 70;
        private const int slotSize = 1;
        public BasicLaser() : base(name, minDamage, maxDamage, accuracy, slotSize)
        {

        }
    }
}
