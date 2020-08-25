using OpenTK.Graphics.ES10;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class SupplyCompany
    {

        public double Materials { get; set; }
        public double Fuel { get; set; }
        public double Food { get; set; }
        public double Money { get; set; }
        public double Population { get; set; }

        public bool Hired {get; set; }

        public SupplyCompany()
        {
            Generate();
        }

        public void Generate()
        {
            int x = 0;
            do 
            {
                AddSupplies(Globals.random.Next(0, 5), Globals.random.Next(GenerationSettings.MinSupplyPoints, GenerationSettings.MaxSupplyPoints));
                x++;
            } while(Globals.random.Next(2) == 0 && x < 3) ;
        }

        public void AddSupplies(int num, double amount) {
            switch(num) {
            case 0:
                Materials += amount;
                break;
            case 1:
                Food += amount;
                break;
            case 2:
                Fuel += amount;
                break;
            case 3:
                Population += amount;
                break;
            case 4:
                Money += amount;
                break;
            }
        }

        public string SupplyName(int num) {
            switch(num) {
            case 0:
                return "Materials";
            case 1:
                return "Food";
            case 2:
                return "Fuel";
            case 3:
                return "Population";
            case 4:
                return "Money";
            }
            return "No";
        }

        public string[] GetSupplies()
        {
            string[] res = new string[5]
            {
                Materials + "",
                Food + "",
                Fuel + "",
                Population + "",
                Money + ""
                
            };
            return res;
        }

        public override string ToString()
        {
            return "Henk SupplyTank";
        }

    }
}
