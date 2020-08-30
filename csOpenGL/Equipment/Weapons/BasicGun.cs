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
        private const int accuracy = 80;
        private const int slotSize = 2;
        public BasicGun() : base(name, minDamage + Globals.random.Next(10), maxDamage + Globals.random.Next(10), accuracy + Globals.random.Next(21), slotSize)
        {
            Type = DamageType.PHYSICAL;
        }
    }
}
