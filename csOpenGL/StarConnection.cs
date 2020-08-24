using FairyJam.Orbitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class StarConnection
    {
        public PlanetarySystem from, to;
        public bool oneway;

        public StarConnection(PlanetarySystem from, PlanetarySystem to, bool oneway)
        {
            this.from = from;
            this.to = to;
            this.oneway = oneway;
        }
    }
}
