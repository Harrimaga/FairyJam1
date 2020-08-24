using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Orbitals
{
    public class Sun : Orbital
    {
        public Sun() : base(null, 1, 0, 10, 100000000, System.Drawing.Color.White)
        {

        }
    }
}
