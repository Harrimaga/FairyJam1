using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class Namelist
    {
        public string Language { get; set; }
        public string[] FamilyNames { get; set; }
        public string[] GivenNames { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }

        public Namelist(string language)
        {
            Language = language;
        }

        public void Next()
        {
            FamilyName = FamilyNames[Globals.random.Next(FamilyNames.Length)];
            GivenName = GivenNames[Globals.random.Next(GivenNames.Length)];
        }
    }
}
