using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    class ImplementacijaPotrosaca : IPotrosac
    {
        public DateTime GetPotrosnjaPotrosaca(double potrosnjaPotrosaca)
        {
          //  Console.WriteLine("Ukupna potrosnja potrosaca je--------------------" + potrosnjaPotrosaca);
            
            Data.Potrosac = potrosnjaPotrosaca * Double.Parse(ConfigurationManager.AppSettings["minuta"]) / 60;

            return Data.CentralnoVreme;
        }
    }
}
