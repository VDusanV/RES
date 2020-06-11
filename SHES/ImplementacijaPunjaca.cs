using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    class ImplementacijaPunjaca : IPunjac
    {
        public void GetUkupnaPotrosnjaPunjaca(double ukupnaSnaga)
        {
          //  Console.WriteLine("Ukupna potrosnja Punjaca je----------------------" + ukupnaSnaga);
            Data.Punjac = ukupnaSnaga * Double.Parse(ConfigurationManager.AppSettings["minuta"]) / 60;

        }
    }
}
