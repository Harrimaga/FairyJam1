using FairyJam.Orbitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class StarMap
    {
        List<PlanetarySystem> stars;

        // Generate Planetary Systems
        // Generate Connections between Planetary Systems

        public StarMap()
        {
            stars = new List<PlanetarySystem>();
        }

        public void Generate(int starAmount)
        {
            stars.Add(new PlanetarySystem());
            int amount = 1;



        }
    }
}
