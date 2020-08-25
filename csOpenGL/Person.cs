using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    abstract class Person
    {
        public int Healthiness { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public bool EastAsianName { get; set; }
        public bool Hired { get; set; }
        public delegate void OnDeath();

        protected Person(int healthiness, string givenName, string familyName, bool eastAsianName = false)
        {
            Healthiness = healthiness;
            GivenName = givenName;
            FamilyName = familyName;
            EastAsianName = eastAsianName;
            Hired = false;
        }

        public virtual void Update(OnDeath onDeath)
        {
            // Check if you are unhealthy enough to die
            if (Globals.random.Next(101) > Healthiness)
            {
                onDeath();
            }
        }

        public override string ToString()
        {
            // If the person uses east asian naming convention flip names
            return EastAsianName ? FamilyName + " " + GivenName : GivenName + " " + FamilyName;
        }

        public string ToStringShort()
        {
            return ToString().Length > 25 ? ToString().Substring(0, 24) + "..." : ToString();
        }
    }
}
