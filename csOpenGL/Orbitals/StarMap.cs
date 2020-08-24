using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Orbitals
{
    public class StarMap
    {
        List<PlanetarySystem> stars;

        // Generate Planetary Systems
        // Generate Connections between Planetary Systems

        public StarMap()
        {

        }

        public void Generate(int starAmount)
        {
            stars.Add(new PlanetarySystem());

        }
    }
}
