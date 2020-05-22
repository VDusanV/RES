using KomponenteUgovori;
using System;
using System.Collections.Generic;
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
            
            Data.Potrosac = potrosnjaPotrosaca;

            return Data.CentralnoVreme;
        }
    }
}
