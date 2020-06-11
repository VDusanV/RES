using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    class ImplementacijaSolarniPanel : ISolarniPanel
    {
        public void GetUkupnaSnaga(double ukupnaSnaga)
        {
          //  Console.WriteLine("Ukupna snaga dobijena sa svih panela je----------" + ukupnaSnaga);
            Data.SolarniPanel = ukupnaSnaga * Double.Parse(ConfigurationManager.AppSettings["minuta"]) / 60;
            
        }
    }
}
