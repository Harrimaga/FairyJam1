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
        private List<StarConnection> connections;

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
            connections = new List<StarConnection>();
        }

        public void Generate()
        {
            foreach (var hex in grid)
            {
                hex.GenerateSystem();

                // Find if even/uneven:
                if (hex.y % 2 == 0)
                {
                    // even:
                    //  - y=-1
                    //  - x=+1, y=-1
                    //  - x=+1
                    //  - x=+1, y=+1
                    //  - y=+1
                    //  - x=-1

                    // Find neighbours
                    if (hex.y - 1 >= 0)
                    {
                        // Get possible connections
                        StarConnection c = new StarConnection(hex, grid[hex.x, hex.y - 1], false);
                        StarConnection c2 = new StarConnection(grid[hex.x, hex.y - 1], hex, false);

                        // Add if connection doesn't exist
                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.y - 1 >= 0 && hex.x + 1 < grid.GetLength(0))
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x + 1, hex.y - 1], false);
                        StarConnection c2 = new StarConnection(grid[hex.x + 1, hex.y - 1], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.x + 1 < grid.GetLength(0))
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x + 1, hex.y], false);
                        StarConnection c2 = new StarConnection(grid[hex.x + 1, hex.y], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.y + 1 < grid.GetLength(1) && hex.x + 1 < grid.GetLength(0))
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x + 1, hex.y + 1], false);
                        StarConnection c2 = new StarConnection(grid[hex.x + 1, hex.y + 1], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.y + 1 < grid.GetLength(1))
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x, hex.y + 1], false);
                        StarConnection c2 = new StarConnection(grid[hex.x, hex.y + 1], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.x - 1 >= 0)
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x - 1, hex.y], false);
                        StarConnection c2 = new StarConnection(grid[hex.x - 1, hex.y], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }
                }
                else
                {
                    // uneven:
                    //  - x=-1, y=-1
                    //  - y=-1
                    //  - x=+1
                    //  - y=+1
                    //  - x=-1, y=+1
                    //  - x=-1

                    if (hex.y - 1 >= 0 && hex.x - 1 >= 0)
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x - 1, hex.y - 1], false);
                        StarConnection c2 = new StarConnection(grid[hex.x - 1, hex.y - 1], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.y - 1 >= 0)
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x, hex.y - 1], false);
                        StarConnection c2 = new StarConnection(grid[hex.x, hex.y - 1], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.x + 1 < grid.GetLength(0))
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x + 1, hex.y], false);
                        StarConnection c2 = new StarConnection(grid[hex.x + 1, hex.y], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.y + 1 < grid.GetLength(0))
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x, hex.y + 1], false);
                        StarConnection c2 = new StarConnection(grid[hex.x, hex.y + 1], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.x - 1 >= 0 && hex.y + 1 < grid.GetLength(0))
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x - 1, hex.y + 1], false);
                        StarConnection c2 = new StarConnection(grid[hex.x - 1, hex.y + 1], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }

                    if (hex.x - 1 >= 0)
                    {
                        StarConnection c = new StarConnection(hex, grid[hex.x - 1, hex.y], false);
                        StarConnection c2 = new StarConnection(grid[hex.x - 1, hex.y], hex, false);

                        if (!connections.Contains(c) && !connections.Contains(c2))
                        {
                            connections.Add(c);
                        }
                    }
                }


            }
        }

        public void Draw()
        {
            foreach (var c in connections)
            {
                c.Draw();
            }

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    grid[i, j].Draw();
                }
            }
        }
    }
}
