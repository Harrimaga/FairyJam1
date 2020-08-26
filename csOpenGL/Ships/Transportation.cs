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
    class Transportation : Ship
    {
        private const int minHealth = 80, maxHealth = 100;
        private const int minDamage = 0, maxDamage = 0;
        private const int minEvasion = 10, maxEvasion = 20;
        private const int minSpeed = 3, maxSpeed = 4;
        private const int maxPeople = 5, maxResource = 250;

        public Transportation(int healthBonus = 0, int damageBonus = 0, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
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
