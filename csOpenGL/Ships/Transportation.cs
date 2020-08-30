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
        private const int minSpeed = 100, maxSpeed = 200;
        private const int maxPeople = 5, maxResource = 250;
        private const int weaponSlots = 1;

        public Transportation(Nation owner, string name = "Transportation", int healthBonus = 0, int damageBonus = 0, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0, int specialSlots = 0)
            : base(
                  name,
                  Globals.random.Next(minHealth + healthBonus, maxHealth + healthBonus),
                  damageBonus,
                  Globals.random.Next(minEvasion + evasionBonus, maxEvasion + evasionBonus),
                  Globals.random.Next(minSpeed + speedBonus, maxSpeed + speedBonus),
                  weaponSlots,
                  maxPeople + maxPeopleAmountBonus,
                  maxResource + maxResourceAmountBonus,
                  specialSlots,
                  owner
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

        public override Ship CopyHull() 
        {
            Ship s = new Transportation(Owner);
            s.MaxHealth = MaxHealth;
            s.HealthPoints = MaxHealth;
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
