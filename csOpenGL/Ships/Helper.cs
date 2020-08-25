using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    class Helper : Ship
    {
        private const int minHealth = 0, maxHealth = 0;
        private const int minEvasion = 0, maxEvasion = 0;
        private const int minSpeed = 0, maxSpeed = 0;
        private const int maxPeople = 0, maxResource = 0;

        public Helper(int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
            : base(
                  Globals.random.Next(minEvasion + evasionBonus, maxEvasion + evasionBonus),
                  maxPeople + maxPeopleAmountBonus,
                  maxResource + maxResourceAmountBonus,
                  Globals.random.Next(minSpeed + speedBonus, maxSpeed + speedBonus)
                  )
        {

        }
        //private const int evasion = 0, maxPeople = 0, maxResource = 0, speed = 0;
        //public Helper(int evasionBonus = 0, int maxPeopleAmountBonus = 0, int maxResourceAmountBonus = 0, int speedBonus = 0)
        //    : base(
        //          evasion + evasionBonus, 
        //          maxPeople + maxPeopleAmountBonus,
        //          maxResource + maxResourceAmountBonus,
        //          speed + speedBonus
        //          )
        //{

        //}
    }
}
