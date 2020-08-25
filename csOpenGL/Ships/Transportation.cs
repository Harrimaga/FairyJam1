using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    class Transportation : Ship
    {
        private const int minHealth = 6, maxHealth = 10;
        private const int minDamage = 0, maxDamage = 0;
        private const int minEvasion = 2, maxEvasion = 4;
        private const int minSpeed = 3, maxSpeed = 5;
        private const int maxPeople = 3, maxResource = 250;

        public Transportation(int healthBonus, int damageBonus, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
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
