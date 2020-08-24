using FairyJam.Orbitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class StarConnection
    {
        public Tile from, to;
        public bool oneway;
        public Vector2 origin, end;
        private Sprite sprite;

        public StarConnection(Tile from, Tile to, bool oneway)
        {
            this.from = from;
            this.to = to;
            this.oneway = oneway;
            

            origin = new Vector2(from.x * Globals.TileWidth - 2 * from.x + Globals.TileWidth / 2 + from.psOffsetX, from.y * Globals.TileHeight + Globals.TileWidth / 2 + from.psOffsetY);
            end = new Vector2(to.x * Globals.TileWidth - 2 * to.x + Globals.TileWidth / 2 + to.psOffsetX, to.y * Globals.TileHeight + Globals.TileWidth / 2 + to.psOffsetY);

            if (from.y % 2 == 1)
            {
                origin = new Vector2(origin.X + Globals.TileWidth / 2, origin.Y);
            }
            if(to.y % 2 == 1)
            {
                end = new Vector2(end.X + Globals.TileWidth / 2, end.Y);
            }

            sprite = new Sprite(5, (int)(origin - end).Length(), 0, Textures.Get(Textures.pixel));
        }

        static double speedToRot(Vector2 v2)
        {
            double xs = v2.X;
            double ys = v2.Y;
            double dir = 3.141592654 * 0.5;
            if (xs > 0)
            {
                dir += Math.Asin(ys / Math.Sqrt(xs * xs + ys * ys));
            }
            else
            {
                dir = 3.141592654 * 1.5;
                dir -= Math.Asin(ys / Math.Sqrt(xs * xs + ys * ys));
            }
            return dir;
        }

        public void Draw()
        {
            Vector2 mid = (origin + end) / 2;
            sprite.Draw(mid.X - 3, mid.Y - (origin - end).Length()/2, true, (float)speedToRot(end - origin), 1, 1, 1);
        }
    }
}
