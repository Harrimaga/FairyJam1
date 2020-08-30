using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Equipment.Weapons
{
    public class BasicLaser : Weapon
    {
        private const string name = "Basic Laser";
        private const int minDamage = 30, maxDamage = 45;
        private const int accuracy = 70;
        private const int slotSize = 3;
        public BasicLaser() : base(name, minDamage + Globals.random.Next(15), maxDamage + Globals.random.Next(20), accuracy +  + Globals.random.Next(25), slotSize)
        {
            Type = DamageType.LASER;
        }
    }
}
