using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    class Freighter : Ship
    {
        private const int minHealth = 60, maxHealth = 100;
        private const int minDamage = 20, maxDamage = 30;
        private const int minEvasion = 20, maxEvasion = 35;
        private const int minSpeed = 2, maxSpeed = 4;
        private const int maxPeople = 3, maxResource = 100;

        public Freighter(int healthBonus = 0, int damageBonus = 0, int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
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
