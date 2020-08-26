using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class Scientist : Person
    {
        public Enums.ScientistTitle Title { get; set; }
        public List<Trait> Traits { get; set; }
        public Scientist(int healthiness, string givenName, string familyName, Enums.ScientistTitle title, List<Trait> traits, bool eastAsianName = false) : base(healthiness, givenName, familyName, eastAsianName)
        {
            Title = title;
            Traits = traits;
        }

        public override void Update(OnDeath onDeath)
        {
            base.Update(onDeath);
            // Still get the bonusses this turn if they died this turn, they'll fade next turn
            foreach (Trait trait in Traits)
            {
                trait.Update();
            }
        }
    }
}
