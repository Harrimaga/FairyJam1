using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Equipment.Weapons
{
    public class BasicPlasmaGun : Weapon
    {
        private const string name = "Basic Plasma gun";
        private const int minDamage = 10, maxDamage = 15;
        private const int accuracy = 95;
        private const int slotSize = 1;
        public BasicPlasmaGun() : base(name, minDamage + Globals.random.Next(5), maxDamage + Globals.random.Next(10), accuracy + Globals.random.Next(6), slotSize)
        {
            Type = DamageType.PLASMA;
        }
    }
}
