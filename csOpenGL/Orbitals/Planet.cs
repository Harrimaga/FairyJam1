using FairyJam.Buildings;
using SharpFont.PostScript;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Orbitals
{
    public class Planet : Orbital
    {
        public List<Planet> moons;
        public List<Building> buildings;
        public PlanetType type;


        public Planet(Orbital parent, ulong radiusFromParent, float startingAngle, int radius, ulong mass, System.Drawing.Color color, bool moon) : base(parent, radiusFromParent, startingAngle, radius, mass, color)
        {
            moons = new List<Planet>();
            buildings = new List<Building>();

            Name = moon ? "Moon" : "Planet";

            ulong mr = mass / (ulong)radius;

            if (!moon)
            {
                // Gassy boi
                if (mr < 100000 && radius > 50)
                {
                    materialsAvailable[0] = 0;
                    materialsAvailable[1] = 0;
                    materialsAvailable[2] = Globals.random.NextDouble() * Globals.random.Next(5000, 10000);
                    maxPop = 0;
                    type = PlanetType.GASGIANT;

                    Color = System.Drawing.Color.SkyBlue;

                }
                else if (mr < 500000)
                {
                    materialsAvailable[0] = Globals.random.NextDouble() * Globals.random.Next(500, 1000);
                    materialsAvailable[1] = Globals.random.NextDouble() * Globals.random.Next(5000, 10000);
                    materialsAvailable[2] = Globals.random.NextDouble() * Globals.random.Next(500, 1000);
                    maxPop = Globals.random.NextDouble() * Globals.random.Next(500, 1000);
                    type = PlanetType.NORMAL;

                    Color = System.Drawing.Color.Green;
                }
                else
                {
                    materialsAvailable[0] = Globals.random.NextDouble() * Globals.random.Next(300, 700);
                    materialsAvailable[1] = Globals.random.NextDouble() * Globals.random.Next(9000, 13700);
                    materialsAvailable[2] = Globals.random.NextDouble() * Globals.random.Next(600, 1300);
                    maxPop = Globals.random.NextDouble() * Globals.random.Next(200, 760);
                    type = PlanetType.DENSE;

                    Color = System.Drawing.Color.Orange;
                }
            }
            else
            {
                materialsAvailable[0] = 0;
                materialsAvailable[1] = Globals.random.NextDouble() * Globals.random.Next(50, 10000);
                materialsAvailable[2] = 0;
                maxPop = 0;
                type = PlanetType.MOON;
            }
        }

        public override void OnClick()
        {
            base.OnClick();
        }

        public void GenerateMoons(int numMoons = 1)
        {
            // Advanced Generation
            ulong[] ringRadi = new ulong[numMoons];
            for (int i = 0; i < numMoons; i++)
            {
                ringRadi[i] = i > 0 ? ringRadi[i - 1] + (ulong)Globals.random.Next(1, 20) : (ulong)Globals.random.Next(radius, radius + 20);
            }

            for (int j = 0; j < numMoons; j++)
            {
                moons.Add(new Planet(this, ringRadi[j], (float)Globals.random.NextDouble(), Globals.random.Next(5, 20), (ulong)Globals.random.Next(10, 100), System.Drawing.Color.FromArgb(100, 100, 100), true));

            }


            // Basic Generation
            //for (int i = 0; i < numMoons; i++)
            //{
            //    moons.Add(new Planet(this, (ulong)Globals.random.Next(radius, radius + 50), (float)Globals.random.NextDouble(), Globals.random.Next(5, radius/2), (ulong)Globals.random.Next(10, 100), System.Drawing.Color.FromArgb(Globals.random.Next(0, 256), Globals.random.Next(0, 256), Globals.random.Next(0, 256))));
            //}
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

        public override void Turn()
        {
            foreach (Building building in buildings)
            {

                building.Turn(this);
            }
        }
    }
}
