using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    class Military : Ship
    {
        private const int minHealth = 8, maxHealth = 11;
        private const int minDamage = 3, maxDamage = 5;
        private const int minEvasion = 0, maxEvasion = 4;
        private const int minSpeed = 2, maxSpeed = 5;
        private const int maxPeople = 1, maxResource = 0;

        public Military(int healthBonus, int damageBonus, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
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
