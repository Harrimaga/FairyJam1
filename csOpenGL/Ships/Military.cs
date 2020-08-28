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
        private const int minSpeed = 300, maxSpeed = 500;
        private const int maxPeople = 1, maxResource = 0;
        private const int weaponSlots = 3;

        public Military(string name = "Military", int healthBonus = 0, int damageBonus = 0, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0, int specialSlots = 0)
            : base(
                  name,
                  Globals.random.Next(minHealth + healthBonus, maxHealth + healthBonus),
                  damageBonus,
                  Globals.random.Next(minEvasion + evasionBonus, maxEvasion + evasionBonus),
                  Globals.random.Next(minSpeed + speedBonus, maxSpeed + speedBonus),
                  weaponSlots,
                  maxPeople + maxPeopleAmountBonus,
                  maxResource + maxResourceAmountBonus,
                  specialSlots
                  )
        {
            AddWeaponTypes();
            Type = ShipType.MILITARY;
        }

        protected override void AddWeaponTypes()
        {
            AllowedWeaponTypes.Add(Enums.WeaponType.Bullet, true);
            AllowedWeaponTypes.Add(Enums.WeaponType.Laser, true);
            AllowedWeaponTypes.Add(Enums.WeaponType.Plasma, true);
        }

        public override Ship CopyHull() 
        {
            Ship s = new Military();
            s.MaxHealth = MaxHealth;
            s.HealthPoints = maxHealth;
            s.Evasiveness = Evasiveness;
            s.Speed = Speed;
            s.MaxSlots = MaxSlots;
            s.MaxResourceLoad = MaxResourceLoad;
            s.MaxPeopleLoad = MaxPeopleLoad;
            s.SpecialSlots = SpecialSlots;
            s.Name = Name;
            s.materialCost = materialCost;
            s.moneyCost = moneyCost;
            return s;
        }

    }
}
