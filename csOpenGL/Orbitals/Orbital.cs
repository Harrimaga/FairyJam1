﻿using OpenTK;
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

        private System.Drawing.Color color;

        public Orbital(Orbital parent, ulong radiusFromParent, float startingAngle, int radius, ulong mass, System.Drawing.Color color)
        {
            this.parent = parent;
            this.radiusFromParent = radiusFromParent;
            this.angleFromParent = startingAngle;
            this.radius = radius;
            this.mass = mass;
            this.color = color;
            this.position = new System.Numerics.Vector2(1920 / 2, 1080 / 2);
            Sprite = new Sprite((int)radius, (int)radius, 0, Textures.Get(Textures.circle));

            velocity = parent != null ? Math.Sqrt((6.6720e-08 * parent.mass) / radiusFromParent) : 0;
            timeToOrbit = (radiusFromParent * 2 * Math.PI) / velocity;
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
                Sprite.Draw(position.X - radius / 2, position.Y - radius / 2, true, 0, color.R / 256f, color.G / 256f, color.B / 256f);
            }
        }

        public virtual void Update()
        {
            angleFromParent += (float)(2f * Math.PI * Globals.DeltaTime) / (float)timeToOrbit;
        }
    }
}