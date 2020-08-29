using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Buildings
{
    public class Housing : Building
    {
        public double cap;
        public Housing(string name) : base(name)
        {
            cap = Balance.HousingPopCap;
        }
    }
}
