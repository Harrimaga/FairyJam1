using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Orbitals
{
    public class PlanetarySystem
    {
        private Sun sun; //pls maar 1 sun
        private List<Planet> planets;
        private Vector2 position = new Vector2(1920 / 2, 1080 / 2);
        private Sprite mapSprite = new Sprite(25, 25, 0, Textures.Get(Textures.circle));

        public PlanetarySystem()
        {
            planets = new List<Planet>();
        }

        public void Generate(int planetAmount = 3)
        {
            sun = new Sun();

            // Advanced Generation
            // Create Rings
            ulong[] ringRadi = new ulong[planetAmount];
            for (int i = 0; i < planetAmount; i++)
            {
                ringRadi[i] = i > 0 ? ringRadi[i - 1] + (ulong)Globals.random.Next(100, 300) : (ulong)Globals.random.Next(100, 300);
            }

            for (int j = 0; j < planetAmount; j++)
            {
                planets.Add(new Planet(sun, ringRadi[j], (float)Globals.random.NextDouble(), Globals.random.Next(10, 100), (ulong)Globals.random.Next(100000, 10000000), System.Drawing.Color.FromArgb(Globals.random.Next(0, 256), Globals.random.Next(0, 256), Globals.random.Next(0, 256))));
                planets.Last().GenerateMoons(Globals.random.Next(GenerationSettings.MinMoons, GenerationSettings.MaxMoons));
            }


            // Basic Generation
            //for (int i = 0; i < planetAmount; i++)
            //{
            //    planets.Add(new Planet(sun, (ulong)Globals.random.Next(100,1000), (float)Globals.random.NextDouble(), Globals.random.Next(10,100), (ulong)Globals.random.Next(1000, 10000000), System.Drawing.Color.FromArgb(Globals.random.Next(0,256), Globals.random.Next(0, 256), Globals.random.Next(0, 256))));
            //    planets.Last().GenerateMoons(Globals.random.Next(GenerationSettings.MinMoons, GenerationSettings.MaxMoons));
            //}
        }

        public void Draw()
        {
            sun.Draw();
            foreach (var planet in planets)
            {
                planet.Draw();
            }
        }

        public void DrawMap(int x, int y)
        {
            mapSprite.Draw(x - 25 / 2, y - 25 / 2, true, 0, 5, 0, 0);
        }

        public void Update()
        {
            foreach (var planet in planets)
            {
                planet.Update();
            }
        }
    }
}
