using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class Leader : Person
    {
        public Enums.LeaderTitle Title { get; set; }
        public List<Trait> Traits { get; set; }

        public Leader(int healthiness, string givenName, string familyName, Enums.LeaderTitle title, List<Trait> traits, bool eastAsianName = false): base(healthiness, givenName, familyName, eastAsianName)
        {
            Title = title;
            Traits = traits;
        }

        public override void Turn(OnDeath onDeath, Nation n)
        {
            base.Turn(onDeath, n);
            // Still get the bonusses this turn if they died this turn, they'll fade next turn
            foreach(Trait trait in Traits)
            {
                trait.Turn(n);
            }
        }
    }
}
