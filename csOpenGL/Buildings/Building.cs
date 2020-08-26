using FairyJam.Orbitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Buildings
{
    public class Building
    {
        
        public string name;

        public Building(string name) 
        {
            this.name = name;
        }

        public virtual void Turn(Orbital o)
        {

        }
    }
}
