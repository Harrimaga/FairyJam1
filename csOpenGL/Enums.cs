using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class Enums
    {
        public enum LeaderTitle
        {
            Admiral, Governor, Merchant, Diplomat, Magnate
        }

        public enum TechType
        {
            Diplomatic, Military, Economic, Technology
        }

        public enum Resource
        {
            POPULATION_GROWTH, TECH_POINTS
        }
    }
    public enum GameState
    {
        MAINMENU,
        TUTORIAL,
        SETTINGS,
        MINUTE,
        PAUSED,
        PLAYING,
        MAPVIEW,
        SYSTEMVIEW
    }
}
