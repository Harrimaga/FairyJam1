using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Orbitals
{
    public class Asteroid : Orbital
    {
        public Asteroid(Orbital parent, ulong radiusFromParent, float startingAngle, int radius, ulong mass, Color color) : base(parent, radiusFromParent, startingAngle, radius, mass, color)
        {

        }

        public override void OnClick()
        {
            base.OnClick();
            Color = Color.Red;
            Globals.currentSystem.selectedAsteroid = this;
        }

        public void UnSelect()
        {
            Color = Color.Gray;
        }
    }
}
