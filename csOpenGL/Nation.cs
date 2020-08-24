using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{

    class Nation
    {

        public double Materials { get; set; }
        public double Fuel { get; set; }
        public double Food { get; set; }
        public double Money { get; set; }
        public double Happiness { get; set; }
        public double TechPoints { get; set; }
        public double Population { get; set; }

        public Nation()
        {
            Materials = 5;
            Fuel = 5;
            Food = 5;
            Money = 5;
            Happiness = 5;
            TechPoints = 5;
            Population = 5;
        }

    }

}
