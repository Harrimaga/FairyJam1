using FairyJam.UI;
using OpenTK;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Orbitals
{
    public class Orbital
    {
        private Orbital parent;
        protected ulong radiusFromParent;
        protected float angleFromParent;
        protected int radius;
        protected ulong mass;
        private Sprite Sprite;
        protected double velocity;
        protected double timeToOrbit;
        protected System.Numerics.Vector2 position;
        public CircleButton button;

        public Nation Owner { get; set; }
        public string Name { get; set; }

        public double maxPop;
        public double[] materialsAvailable; // Food / Materials / Fuel

        protected System.Drawing.Color Color { get; set; }

        public Orbital(Orbital parent, ulong radiusFromParent, float startingAngle, int radius, ulong mass, System.Drawing.Color color)
        {
            this.parent = parent;
            this.radiusFromParent = radiusFromParent;
            this.angleFromParent = startingAngle;
            this.radius = radius;
            this.mass = mass;
            this.Color = color;
            this.position = new System.Numerics.Vector2(1920 / 2, 1080 / 2);
            Owner = null;
            Sprite = new Sprite((int)radius, (int)radius, 0, Textures.Get(Textures.circle));

            button = new CircleButton(position.X, position.Y, radius, () => { OnClick(); } );

            velocity = parent != null ? Math.Sqrt((6.6720e-08 * parent.mass) / radiusFromParent) : 0;
            timeToOrbit = (radiusFromParent * 2 * Math.PI) / velocity;

            Name = Globals.random.Next(10000, 99999).ToString();
        }

        public virtual void OnClick()
        {
            new PlanetUI(this);
            Owner = Globals.PlayerNation;
        }

        public virtual void Draw()
        {
            if (parent == null)
            {
                Sprite.Draw(position.X - radius / 2, position.Y - radius / 2, true, 0, 1, 1, 1);
            }
            else
            {
                position = parent.position + new System.Numerics.Vector2(radiusFromParent * (float)Math.Cos(angleFromParent * 2 * Math.PI), radiusFromParent * (float)Math.Sin(angleFromParent * 2 * Math.PI));
                if (Owner != null && !(this is Asteroid))
                {
                    Sprite temp = new Sprite(Sprite.w + 4, Sprite.h + 4, 0, Sprite.texture);

                    if (materialsAvailable[0] == 0 && materialsAvailable[1] == 0 && materialsAvailable[2] == 0)
                    {
                        temp.Draw(position.X - (radius + 4) / 2, position.Y - (radius + 4) / 2, true, 0, 1, 0, 0, 1);
                    }
                    else
                    {
                        temp.Draw(position.X - (radius + 4) / 2, position.Y - (radius + 4) / 2, true, 0, 1, 1, 1, 1);
                    }  
                }
                Sprite.Draw(position.X - radius / 2, position.Y - radius / 2, true, 0, Color.R / 256f, Color.G / 256f, Color.B / 256f);
            }
        }

        public virtual void Update()
        {
            if (Globals.currentSystem.clockWise)
            {
                angleFromParent += (float)(2f * Math.PI * Globals.DeltaTime) / (float)timeToOrbit;
            }
            else
            {
                angleFromParent -= (float)(2f * Math.PI * Globals.DeltaTime) / (float)timeToOrbit;
            }
            button.Update(position.X, position.Y);
        }

        public virtual void Turn()
        {

        }
    }
}