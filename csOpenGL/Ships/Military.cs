using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    class Military : Ship
    {
        private const int minHealth = 80, maxHealth = 110;
        private const int minEvasion = 0, maxEvasion = 20;
        private const int minSpeed = 2, maxSpeed = 3;
        private const int maxPeople = 1, maxResource = 0;

        public Military(string name = "Military", int healthBonus = 0, int damageBonus = 0, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
            : base(
                  name,
                  Globals.random.Next(minHealth + healthBonus, maxHealth + healthBonus),
                  damageBonus,
                  Globals.random.Next(minEvasion + evasionBonus, maxEvasion + evasionBonus),
                  Globals.random.Next(minSpeed + speedBonus, maxSpeed + speedBonus),
                  maxPeople + maxPeopleAmountBonus,
                  maxResource + maxResourceAmountBonus
                  )
        {

        }

        public override void changeWeapon(Weapon weapon)
        {
            base.changeWeapon(weapon);
        }
    }
}
