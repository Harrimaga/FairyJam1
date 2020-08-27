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

            // Food - Materials - Fuel
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

                    baseEfficiency[0] = 0;
                    baseEfficiency[1] = 0;
                    baseEfficiency[2] = 0.8 + Globals.random.NextDouble()*0.2;

                }
                else if (mr < 500000)
                {
                    materialsAvailable[0] = Globals.random.NextDouble() * Globals.random.Next(500, 1000);
                    materialsAvailable[1] = Globals.random.NextDouble() * Globals.random.Next(5000, 10000);
                    materialsAvailable[2] = Globals.random.NextDouble() * Globals.random.Next(500, 1000);
                    maxPop = Globals.random.NextDouble() * Globals.random.Next(500, 1000);
                    type = PlanetType.NORMAL;

                    Color = System.Drawing.Color.Green;
                    baseEfficiency[0] = 0.4 + Globals.random.NextDouble()*0.6;
                    baseEfficiency[1] = 0.3 + Globals.random.NextDouble()*0.5;
                    baseEfficiency[2] = 0.3 + Globals.random.NextDouble()*0.4;
                }
                else
                {
                    materialsAvailable[0] = Globals.random.NextDouble() * Globals.random.Next(300, 700);
                    materialsAvailable[1] = Globals.random.NextDouble() * Globals.random.Next(9000, 13700);
                    materialsAvailable[2] = Globals.random.NextDouble() * Globals.random.Next(600, 1300);
                    maxPop = Globals.random.NextDouble() * Globals.random.Next(200, 760);
                    type = PlanetType.DENSE;

                    Color = System.Drawing.Color.Orange;

                    baseEfficiency[0] = 0.2 + Globals.random.NextDouble()*0.3;
                    baseEfficiency[1] = 0.6 + Globals.random.NextDouble()*0.4;
                    baseEfficiency[2] = 0.4 + Globals.random.NextDouble()*0.4;
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
            maxMaterialsAvailable[0] = materialsAvailable[0];
            maxMaterialsAvailable[1] = materialsAvailable[1];
            maxMaterialsAvailable[2] = materialsAvailable[2];
        }

        public override void OnClick()
        {
            base.OnClick();
        }

        public override void Build(Building b)
        {
            if (b is RAB)
            {
                if (Owner.Materials < Balance.RABCost)
                {
                    // TODO: popup
                    return;
                }

                buildings.Add(b);
                Owner.Materials -= Balance.RABCost;
            }
        }

        public override int GetBuildingCount(int b)
        {
            switch (b)
            {
                case 0:
                    int x = 0;
                    foreach (Building building in buildings)
                    {
                        if (building is RAB)
                        {
                            x++;
                        }
                    }
                    return x;
                case 1:
                    x = 0;
                    foreach (Building building in buildings)
                    {
                        if (building is Shipyard)
                        {
                            x++;
                        }
                    }
                    return x;
                case 2:
                    x = 0;
                    foreach (Building building in buildings)
                    {
                        if (building is Housing)
                        {
                            x++;
                        }
                    }
                    return x;
                default:
                    return 0;
            }
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

        public override double GetEfficiency(int resource) 
        {
            if(maxMaterialsAvailable[resource] == 0) return 0;
            double d = baseEfficiency[resource] * materialsAvailable[resource]/maxMaterialsAvailable[resource];
            return d < 0.05 ? 0.05 : d;
        }
    }
}
