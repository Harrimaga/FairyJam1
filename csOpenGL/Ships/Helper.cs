﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    public class Helper : Ship
    {
        private const int minHealth = 40, maxHealth = 80;
        private const int minEvasion = 30, maxEvasion = 50;
        private const int minSpeed = 3, maxSpeed = 6;
        private const int maxPeople = 2, maxResource = 400;
        private const int weaponSlots = 1;

        public Helper(string name = "Helper", int healthBonus = 0, int damageBonus = 0, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
            : base(
                  name,
                  Globals.random.Next(minHealth + healthBonus, maxHealth + healthBonus),
                  damageBonus,
                  Globals.random.Next(minEvasion + evasionBonus, maxEvasion + evasionBonus),
                  Globals.random.Next(minSpeed + speedBonus, maxSpeed + speedBonus),
                  weaponSlots,
                  maxPeople + maxPeopleAmountBonus,
                  maxResource + maxResourceAmountBonus
                  )
        {
            AddWeaponTypes();

        }

        protected override void AddWeaponTypes()
        {
            AllowedWeaponTypes.Add(Enums.WeaponType.Bullet, false);
            AllowedWeaponTypes.Add(Enums.WeaponType.Laser, false);
            AllowedWeaponTypes.Add(Enums.WeaponType.Plasma, true);
        }
    }
}