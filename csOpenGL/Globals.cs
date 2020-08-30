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
        public static Events.EventHandler eventHandler;
        public static QFont buttonFont = new QFont("Fonts/arial.ttf", 16, new QuickFont.Configuration.QFontBuilderConfiguration(true));
        public static Random random = new Random();
        public static Logger logger = new Logger("logs/log.txt");
        public static Trait[] possibleTraits= { };
        public static Namelist[] nameLists = { };
        public static PlanetarySystem currentSystem;
        public static float mapCamX, mapCamY;
        public static UI.UI currentUI;
        public static Nation PlayerNation, SpacePirates;
        public static bool paused;
        public static DrawnButton typing;
        public static bool num;
        public static List<Nation> players;

        public static Timer timer;

        public static List<DrawnButton> activeButtons;

        // UI for the one minute section
        public static UI.LeaderUI leaderUI;
        public static UI.ScientistUI scientistUI;
        public static UI.SuppliesUI suppliesUI;

        public static bool checkCol(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            return x1 - w2 < x2 && x1 + w1 > x2 && y1 - h2 < y2 && y1 + h1 > y2;
        }

        public static int getTurnDistance(PlanetarySystem ps1, PlanetarySystem ps2, double speed) 
        {
            if(speed <= 0) return 9999999;
            int distance = 1 + (int)(Math.Sqrt((ps1.drawnPosition.X-ps2.drawnPosition.X)*(ps1.drawnPosition.X-ps2.drawnPosition.X) + (ps1.drawnPosition.Y-ps2.drawnPosition.Y)*(ps1.drawnPosition.Y-ps2.drawnPosition.Y))/speed);
            return distance; 
        }

    }
}