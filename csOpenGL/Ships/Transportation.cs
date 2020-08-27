using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    /// <summary>
    ///     Transportation ship, used to transport people and resources
    /// </summary>
    public class Transportation : Ship
    {
        private const int minHealth = 80, maxHealth = 100;
        private const int minEvasion = 10, maxEvasion = 20;
        private const int minSpeed = 3, maxSpeed = 4;
        private const int maxPeople = 5, maxResource = 250;
        private const int weaponSlots = 1;

        public Transportation(string name = "Transportation", int healthBonus = 0, int damageBonus = 0, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
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
            Type = ShipType.TRANSPORTATION;
        }

        protected override void AddWeaponTypes()
        {
            AllowedWeaponTypes.Add(Enums.WeaponType.Bullet, false);
            AllowedWeaponTypes.Add(Enums.WeaponType.Laser, false);
            AllowedWeaponTypes.Add(Enums.WeaponType.Plasma, false);
        }
    }
}
