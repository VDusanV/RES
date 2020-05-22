using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    public class Data
    {
        public static DateTime CentralnoVreme;
        public static double Potrosac { get; set; } = 0;
        public static double Punjac { get; set; } = 0;
        public static double SolarniPanel { get; set; } = 0;
        public static double Baterija { get; set; } = 0;

        public static double IzracunajUkupnoStanje()
        {
            return (SolarniPanel - Potrosac - Punjac - Baterija);
        }


    }
}
