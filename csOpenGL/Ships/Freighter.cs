using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    class Freighter : Ship
    {
        private const int minHealth = 60, maxHealth = 100;
        private const int minEvasion = 20, maxEvasion = 35;
        private const int minSpeed = 2, maxSpeed = 4;
        private const int maxPeople = 3, maxResource = 100;
        private const int weaponSlots = 1;

        public Freighter(string name = "Freighter", int healthBonus = 0, int damageBonus = 0, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
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

        private void AddWeaponTypes()
        {
            AllowedWeaponTypes.Add(Enums.WeaponType.Bullet, false);
            AllowedWeaponTypes.Add(Enums.WeaponType.Laser, true);
            AllowedWeaponTypes.Add(Enums.WeaponType.Plasma, false);
        }
    }
}
