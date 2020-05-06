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
        public double GetPotrosnjaPotrosaca(double potrosnjaPotrosaca)
        {
            Console.WriteLine(potrosnjaPotrosaca);
            return 0;
        }
    }
}
