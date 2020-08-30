using FairyJam.Orbitals;
using FairyJam.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Buildings
{
    public class Wormhole : Building
    {
        /// <summary>
        ///     Building which enables instant travel between planetary systems
        /// </summary>
        /// <param name="name">Name of the wormhole</param>
        public Wormhole(string name) : base(name)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPS">The planetary system the fleet is currently in</param>
        /// <param name="endPS">The planetary system the fleet travels to</param>
        /// <param name="fleet">The fleet that will travel</param>
        public void Travel(PlanetarySystem startPS, PlanetarySystem endPS, Fleet fleet)
        {
            startPS.fleets.Remove(fleet);
            endPS.fleets.Add(fleet);
        }
    }
}
