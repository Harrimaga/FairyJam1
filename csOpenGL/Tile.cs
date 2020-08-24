﻿using FairyJam.Orbitals;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class Tile
    {

        public int x { get; }
        public int y { get; }

        private Sprite sprite;

        private PlanetarySystem ps;
        public int psOffsetX, psOffsetY;
        private DrawnButton button;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            sprite = new Sprite(Globals.TileWidth, Globals.TileWidth, 0, Textures.Get(Textures.testTile));
        }

        public void GenerateSystem()
        {
            if (Globals.random.Next(0,100) > 80)
            {
                return;
            }
            ps = new PlanetarySystem();
            ps.Generate(Globals.random.Next(3, 12));
            psOffsetX = Globals.random.Next(-Globals.TileWidth / 4, Globals.TileWidth / 4);
            psOffsetY = Globals.random.Next(-Globals.TileWidth / 4, Globals.TileWidth / 4);

            int drawX = x * Globals.TileWidth - 2 * x;
            int drawY = y * Globals.TileHeight;
            if (y % 2 == 1)
            {
                drawX += Globals.TileWidth / 2;
            }
            button = new DrawnButton("?", drawX + Globals.TileWidth / 2 + psOffsetX - 25 / 2, drawY + Globals.TileWidth / 2 + psOffsetY - 25 / 2, 25, 25, () => { Game.switchViewToSystem(ps); }, 0, 0, 0);
        }

        public bool HasSystem()
        {
            if (ps == null)
            {
                return false;
            }
            return true;
        }

        public void Draw()
        {
            int drawX = x * Globals.TileWidth - 2*x;
            int drawY = y * Globals.TileHeight;
            if(y %2 == 1)
            {
                drawX += Globals.TileWidth / 2;
            }
            //sprite.Draw(drawX, drawY);
            if (!HasSystem()) return; 
            ps.DrawMap(drawX + Globals.TileWidth / 2 + psOffsetX, drawY + Globals.TileWidth / 2 + psOffsetY);
        }

        public void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if (e.Button == MouseButton.Left)
            {
                if (button != null && button.IsInButton(mx + Window.camX, my + Window.camY))
                {
                    button.OnClick();
                }
            }
        }
    }
}
