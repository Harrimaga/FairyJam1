using FairyJam.Equipment.SpecialEquipment;
using FairyJam.Equipment.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    public class Military : Ship
    {
        private const int minHealth = 80, maxHealth = 110;
        private const int minEvasion = 10, maxEvasion = 20;
        private const int minSpeed = 2, maxSpeed = 3;
        private const int maxPeople = 1, maxResource = 0;
        private const int weaponSlots = 3;

        public Military(string name = "Military", int healthBonus = 0, int damageBonus = 0, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
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
            Type = ShipType.MILITARY;
            if (Globals.random.Next(0, 100) > 50)
            {
                WeaponList.Add(new BasicGun());
            }
            if (Globals.random.Next(0, 100) > 50)
            {
                WeaponList.Add(new BasicLaser());
            }
            if (Globals.random.Next(0, 100) > 50)
            {
                WeaponList.Add(new BasicPlasmaGun());
            }

            Special = new WormholeDevice();
        }

        protected override void AddWeaponTypes()
        {
            AllowedWeaponTypes.Add(Enums.WeaponType.Bullet, true);
            AllowedWeaponTypes.Add(Enums.WeaponType.Laser, true);
            AllowedWeaponTypes.Add(Enums.WeaponType.Plasma, true);
        }
    }
}
