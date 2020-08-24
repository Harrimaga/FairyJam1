using SharpFont.PostScript;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Orbitals
{
    public class Planet : Orbital
    {
        private List<Planet> moons;
        public Planet(Orbital parent, ulong radiusFromParent, float startingAngle, int radius, ulong mass, System.Drawing.Color color) : base(parent, radiusFromParent, startingAngle, radius, mass, color)
        {
            moons = new List<Planet>();
        }

        public void GenerateMoons(int numMoons = 1)
        {
            for (int i = 0; i < numMoons; i++)
            {
                moons.Add(new Planet(this, (ulong)Globals.random.Next(radius, radius + 50), (float)Globals.random.NextDouble(), Globals.random.Next(5, radius/2), (ulong)Globals.random.Next(10, 100), System.Drawing.Color.FromArgb(Globals.random.Next(0, 256), Globals.random.Next(0, 256), Globals.random.Next(0, 256))));
            }
        }

        public override void Draw()
        {
            base.Draw();

            foreach (var moon in moons)
            {
                moon.Draw();
            }
        }

        public override void Update()
        {
            base.Update();
            foreach (var moon in moons)
            {
                moon.Update();
            }
        }
    }
}
