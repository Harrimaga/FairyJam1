using FairyJam.Orbitals;
using QuickFont;
using Secretary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class Globals
    {
        public static int Width, Height;
        public static int TileWidth = 512, TileHeight = 240;
        public static Map map { get; set; }
        public static double DeltaTime;
        public static GameState currentState;
        public static QFont buttonFont = new QFont("Fonts/arial.ttf", 16, new QuickFont.Configuration.QFontBuilderConfiguration(true));
        public static Random random = new Random();
        public static Logger logger = new Logger("logs/log.txt");
        public static Trait[] possibleTraits= { };
        public static PlanetarySystem currentSystem;
        public static float mapCamX, mapCamY;

        public static bool checkCol(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            return x1 - w2 < x2 && x1 + w1 > x2 && y1 - h2 < y2 && y1 + h1 > y2;
        }
    }
}