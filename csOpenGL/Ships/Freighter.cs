using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    class Freighter : Ship
    {
        private const int minHealth = 6, maxHealth = 10;
        private const int minDamage = 2, maxDamage = 3;
        private const int minEvasion = 3, maxEvasion = 7;
        private const int minSpeed = 2, maxSpeed = 4;
        private const int maxPeople = 2, maxResource = 100;

        public Freighter(int healthBonus, int damageBonus, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
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
