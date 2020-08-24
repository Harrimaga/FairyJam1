using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class Map
    {

        public int mapWidth {get; }
        public int mapHeight { get; }
        private Tile[,] grid;

        public Map(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            grid = new Tile[mapWidth,mapHeight];
            for(int i = 0; i < mapWidth; i++)
            {
                for(int j = 0; j < mapHeight; j++)
                {
                    grid[i, j] = new Tile(i, j);
                }
            }
        }

        public void Generate()
        {
            foreach (var hex in grid)
            {
                hex.GenerateSystem();
            }
        }

        public void Draw()
        {
            for(int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    grid[i, j].Draw();
                }
            }
        }

    }
}
