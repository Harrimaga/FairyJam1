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
            

            origin = new Vector2(from.x * Globals.TileWidth - 2 * from.x + Globals.TileWidth / 2 + from.psOffsetX, from.y * Globals.TileHeight + Globals.TileWidth / 2 + from.psOffsetX);
            end = new Vector2(to.x * Globals.TileWidth - 2 * to.x + Globals.TileWidth / 2 + to.psOffsetX, to.y * Globals.TileHeight + Globals.TileWidth / 2 + to.psOffsetY);

            if (from.y % 2 == 1)
            {
                origin = new Vector2(origin.X + Globals.TileWidth / 2, origin.Y);
                end = new Vector2(end.X + Globals.TileWidth / 2, end.Y);
            }

            sprite = new Sprite(5, (int)(origin - end).Length(), 0, Textures.Get(Textures.pixel));
        }

        public void Draw()
        {
            sprite.Draw(origin.X, origin.Y, true, (float)Math.Cos((origin - end).Y / (origin - end).Length()), 1, 1, 1);
        }
    }
}
