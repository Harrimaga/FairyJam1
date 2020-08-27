using FairyJam.Orbitals;
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

        public PlanetarySystem ps;
        public int psOffsetX, psOffsetY;
        public DrawnButton button;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            sprite = new Sprite(Globals.TileWidth, Globals.TileWidth, 0, Textures.Get(Textures.testTile));
        }

        public void Turn()
        {
            if (ps == null) return;
            ps.Turn();
        }

        public void GenerateSystem()
        {
            if (Globals.random.Next(0,100) > 80)
            {
                return;
            }
            ps = new PlanetarySystem();
            ps.Generate(Globals.random.Next(GenerationSettings.MinPlanets, GenerationSettings.MaxPlanets));
            psOffsetX = Globals.random.Next(-Globals.TileWidth / 4, Globals.TileWidth / 4);
            psOffsetY = Globals.random.Next(-Globals.TileWidth / 4, Globals.TileWidth / 4);

            int drawX = x * Globals.TileWidth - 2 * x;
            int drawY = y * Globals.TileHeight;
            if (y % 2 == 1)
            {
                drawX += Globals.TileWidth / 2;
            }

            button = new DrawnButton("?", drawX + Globals.TileWidth / 2 + psOffsetX - 25 / 2, drawY + Globals.TileWidth / 2 + psOffsetY - 25 / 2, 25, 25, () => { if (Globals.currentUI != null) { return; } Game.switchViewToSystem(ps); }, 0, 0, 0);
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
            if (e.Button == MouseButton.Right)
            {
                // Open ps UI
                if (button != null && button.IsInButton(mx + Window.camX, my + Window.camY))
                {
                    ps.ShowUI();
                }
            }
        }
    }
}
