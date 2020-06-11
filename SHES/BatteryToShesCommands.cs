using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    public class BatteryToShesCommands : IBatteryToShesCommands
    {
        public DateTime posaljiPodatke(int kapacitet, int rezim, double maxSnaga)
        {
          //  Console.WriteLine("------ Baterija ------");
          //  Console.WriteLine("kapacitet: {0}", kapacitet);
            string rezimStr = "";
            maxSnaga *= Double.Parse(ConfigurationManager.AppSettings["minuta"]) / 60;
            if (rezim == 1) //ako je rezim 1 onda generator
            {
                rezimStr = "generator";
                Data.Baterija = maxSnaga ;
            }
            if (rezim == 2)     // 2 -> potrosac
            {
                rezimStr = "potrosac";
                Data.Baterija = ((-1)*maxSnaga);
            }
            if (rezim == 0) // 0-> iskljucena 
            {
                Data.Baterija = 0;
                rezimStr = "iskljucena";
            }

          //  Console.WriteLine("rezim: " + rezimStr);
          //  Console.WriteLine("----------------------)"); //neku bazu
            return Data.CentralnoVreme;
        }
    }
}
