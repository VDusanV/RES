using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    class ImplementacijaPunjaca : IPunjac
    {
        public void GetUkupnaPotrosnjaPunjaca(int ukupnaSnaga)
        {
            Console.WriteLine("Ukupna potrosnja Punjaca je----------------------" + ukupnaSnaga);


        }
    }
}
