using FairyJam.Ships;
using FairyJam.UI;
using OpenTK.Input;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FairyJam.Orbitals
{
    public class PlanetarySystem
    {
        private Sun sun; //pls maar 1 sun
        private List<Planet> planets;
        private List<Asteroid> asteroids;
        private Vector2 position = new Vector2(1920 / 2, 1080 / 2);
        private Sprite mapSprite = new Sprite(25, 25, 0, Textures.Get(Textures.circle));
        public Nation Owner { get; set; }
        public Vector2 drawnPosition;
        public int bfsVisited = 0;

        public bool clockWise;

        public List<Fleet> fleets;
        public List<Ship> ships;

        public Asteroid selectedAsteroid;

        public List<PlanetarySystem> neighbours;

        public PlanetarySystem()
        {
            fleets = new List<Fleet>();
            ships = new List<Ship>();
            planets = new List<Planet>();
            asteroids = new List<Asteroid>();
            neighbours = new List<PlanetarySystem>();
            selectedAsteroid = null;
            Owner = null;
        }

        public int GetCount(int i)
        {
            switch (i)
            {
                case 0:
                    return planets.Count;
                case 1:
                    int x = 0;
                    foreach (Planet planet in planets)
                    {
                        x += planet.moons.Count;
                    }
                    return x;
                case 2:
                    return asteroids.Count;
                default:
                    return 0;
            }
        }

        public void ShowUI()
        {
            new psUI(this);
        }

        public List<Fleet>[] GetFleets()
        {
            List<Fleet>[] res = new List<Fleet>[]{new List<Fleet>(), new List<Fleet>()};

            foreach (Fleet f in fleets)
            {
                if (f.owner == Globals.PlayerNation)
                {
                    res[0].Add(f);
                }
                else
                {
                    res[1].Add(f);
                }
            }
            return res;
        }

        public void AddNeighbour(PlanetarySystem ps)
        {
            if (!neighbours.Contains(ps) && ps != null) neighbours.Add(ps);
        }

        public void Generate(int planetAmount = 3)
        {
            sun = new Sun();

            if (Globals.random.Next(0, 100) > 50)
            {
                clockWise = true;
            }
            else
            {
                clockWise = false;
            }

            // Advanced Generation
            // Create Rings
            ulong[] ringRadi = new ulong[planetAmount];
            for (int i = 0; i < planetAmount; i++)
            {
                ringRadi[i] = i > 0 ? ringRadi[i - 1] + (ulong)Globals.random.Next(100, 300) : (ulong)Globals.random.Next(100, 300);
            }

            // Put planets in those rings
            for (int j = 0; j < planetAmount; j++)
            {
                planets.Add(new Planet(sun, ringRadi[j], (float)Globals.random.NextDouble(), Globals.random.Next(10, 100), (ulong)Globals.random.Next(100000, 10000000), System.Drawing.Color.FromArgb(Globals.random.Next(0, 256), Globals.random.Next(0, 256), Globals.random.Next(0, 256)), false));
                planets.Last().GenerateMoons(Globals.random.Next(GenerationSettings.MinMoons, GenerationSettings.MaxMoons));
            }

            // Generate Asteroids
            int numAS = Globals.random.Next(1000, 10000);
            for (int k = 0; k < numAS; k++)
            {
                asteroids.Add(new Asteroid(sun, (ulong)Globals.random.Next(1500, 1700), (float)Globals.random.NextDouble(), Globals.random.Next(1,5), 10, System.Drawing.Color.Gray));
            }


            // Basic Generation
            //for (int i = 0; i < planetAmount; i++)
            //{
            //    planets.Add(new Planet(sun, (ulong)Globals.random.Next(100,1000), (float)Globals.random.NextDouble(), Globals.random.Next(10,100), (ulong)Globals.random.Next(1000, 10000000), System.Drawing.Color.FromArgb(Globals.random.Next(0,256), Globals.random.Next(0, 256), Globals.random.Next(0, 256))));
            //    planets.Last().GenerateMoons(Globals.random.Next(GenerationSettings.MinMoons, GenerationSettings.MaxMoons));
            //}
        }

        public string Name()
        {
            return sun.Name;
        }

        public void Draw()
        {
            sun.Draw();
            foreach (var planet in planets)
            {
                planet.Draw();
            }
            foreach (var a in asteroids)
            {
                a.Draw();
            }
        }

        public void DrawMap(int x, int y)
        {
            if(Owner != null) 
            {
                mapSprite.Draw(x - 25 / 2, y - 25 / 2, true, 0, Owner.Color.R/ 255f, Owner.Color.G/ 255f, Owner.Color.B/ 255f);
            }
            else 
            {
                mapSprite.Draw(x - 25 / 2, y - 25 / 2, true, 0, 0.1f, 0.1f, 0.1f);
            }
            drawnPosition = new Vector2(x - 25 / 2, y - 25 / 2);
        }

        public void Update()
        {
            foreach (var planet in planets)
            {
                planet.Update();
            }
            foreach (var a in asteroids)
            {
                a.Update();
            }
        }

        public void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if (selectedAsteroid != null)
            {
                selectedAsteroid.UnSelect();
            }
            if (e.Button == MouseButton.Left)
            {
                foreach (Planet p in planets)
                {
                    CircleButton button = p.button;
                    if (button != null && button.IsInButton(mx + Window.camX, my + Window.camY))
                    {
                        button.OnClick();
                        return;
                    }
                    foreach (Planet moon in p.moons)
                    {
                        CircleButton btn = moon.button;
                        if (btn != null && btn.IsInButton(mx + Window.camX, my + Window.camY))
                        {
                            btn.OnClick();
                            return;
                        }
                    }
                }
                foreach (Asteroid a in asteroids)
                {
                    CircleButton button = a.button;
                    if (button != null && button.IsInButton(mx + Window.camX, my + Window.camY))
                    {
                        button.OnClick();
                        return;
                    }
                }
            }
        }

        public void Turn()
        {
            foreach (Planet planet in planets)
            {
                planet.Turn();
            }

            for (int i = fleets.Count - 1; i >= 0; i--)
            {
                fleets[i].Turn();
            }
        }
    }
}
