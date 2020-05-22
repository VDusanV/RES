using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komponente
{
    public class Data
    {
        public static DateTime vreme;
        public static double snagaSunca = 50; //snaga sunca u procentima
        public static Dictionary<string, Potrosac> potrosaci;
        public static Dictionary<string, SolarniPanel> solarniPaneli;
        public static PunjacElekAuto PunjacEA = new PunjacElekAuto(400)
        {
            VremePunjenjaOd = new DateTime(2010, 10, 10, 21, 0, 0) { },
            VremePunjenjaDo = new DateTime(2010, 10, 10, 6, 0, 0) { }
        };
    }
}
