using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komponente
{
    public class Data
    {
        public static double snagaSunca = 0; //snaga sunca u procentima
        public static Dictionary<string, Potrosac> potrosaci;
        public static Dictionary<string, SolarniPanel> solarniPaneli;
        public static PunjacElekAuto PunjacEA = new PunjacElekAuto(300) { };
    }
}
