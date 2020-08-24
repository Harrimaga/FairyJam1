using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class Tile
    {

        public int x { get; }
        public int y { get; }

        private Sprite sprite;
        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            sprite = new Sprite(Globals.TileWidth, Globals.TileWidth, 0, Textures.Get(Textures.testTile));
        }

        public void Draw()
        {
            int drawX = x * Globals.TileWidth;
            int drawY = y * Globals.TileHeight;
            if(y %2 == 1)
            {
                drawX += Globals.TileWidth / 2;
            }
            sprite.Draw(drawX - 2*x, drawY);
        }

    }
}
