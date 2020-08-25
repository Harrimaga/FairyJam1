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
            Sprite = new Sprite((int)radius, (int)radius, 0, Textures.Get(Textures.circle));

            button = new CircleButton(position.X, position.Y, radius, () => { OnClick(); } );

            velocity = parent != null ? Math.Sqrt((6.6720e-08 * parent.mass) / radiusFromParent) : 0;
            timeToOrbit = (radiusFromParent * 2 * Math.PI) / velocity;

            Name = Globals.random.Next(10000, 99999).ToString();
        }

        public virtual void OnClick()
        {
            new PlanetUI(this);
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
                Sprite.Draw(position.X - radius / 2, position.Y - radius / 2, true, 0, Color.R / 256f, Color.G / 256f, Color.B / 256f);
            }
        }

        public virtual void Update()
        {
            angleFromParent += (float)(2f * Math.PI * Globals.DeltaTime) / (float)timeToOrbit;
            button.Update(position.X, position.Y);
        }
    }
}
