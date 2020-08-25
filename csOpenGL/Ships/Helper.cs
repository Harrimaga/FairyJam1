using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    class Helper : Ship
    {
        private const int minHealth = 4, maxHealth = 8;
        private const int minDamage = 1, maxDamage = 2;
        private const int minEvasion = 5, maxEvasion = 10;
        private const int minSpeed = 3, maxSpeed = 6;
        private const int maxPeople = 1, maxResource = 400;

        public Helper(int healthBonus, int damageBonus, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
            : base(
                  Globals.random.Next(minHealth + healthBonus, maxHealth + healthBonus),
                  Globals.random.Next(minDamage + damageBonus, maxDamage + damageBonus),
                  Globals.random.Next(minEvasion + evasionBonus, maxEvasion + evasionBonus),
                  Globals.random.Next(minSpeed + speedBonus, maxSpeed + speedBonus),
                  maxPeople + maxPeopleAmountBonus,
                  maxResource + maxResourceAmountBonus
                  )
        {

        }
    }
}
