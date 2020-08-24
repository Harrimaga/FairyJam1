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

        public enum GameState
        {
            MAINMENU,
            TUTORIAL,
            SETTINGS,
            MINUTE,
            PAUSED,
            PLAYING
        }
    }
}
}
