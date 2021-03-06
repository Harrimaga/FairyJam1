﻿using System;
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

        public enum WeaponType
        {
            Bullet, Laser, Plasma
        }
        public enum Modifier 
        {
            PopulationGrowth = 0,
            FuelEfficiency = 1,
            TechGrowth = 2
        }
        public const int modAmount = 3; 
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
        SYSTEMVIEW,
        LOST,
        WON
    }

    public enum ShipType
    {
        MILITARY,
        FREIGHTER,
        HELPER,
        TRANSPORTATION
    }

    public enum DamageType
    {
        PHYSICAL,
        LASER,
        PLASMA
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
