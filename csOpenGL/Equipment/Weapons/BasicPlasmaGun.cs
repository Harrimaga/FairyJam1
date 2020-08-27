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
        private const int minDamage = 10, maxDamage = 20;
        private const int accuracy = 100;
        private const int slotSize = 1;
        public BasicPlasmaGun() : base(name, minDamage, maxDamage, accuracy, slotSize)
        {
            Type = DamageType.PLASMA;
        }
    }
}
