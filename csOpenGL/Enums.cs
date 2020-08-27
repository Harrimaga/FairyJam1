using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class Enums
    {
        public enum LeaderTitle
        {
            Admiral, Governor, Merchant, Diplomat, Magnate
        }

        public enum ScientistTitle
        {
            Feiv
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

    // TODO: Create more Types
    public enum PlanetType
    {
        GASGIANT,
        NORMAL,
        DENSE,
        MOON
    }
}
