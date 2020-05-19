using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    public class BatteryToShesCommands : IBatteryToShesCommands
    {
        public DateTime posaljiPodatke(int kapacitet, int rezim, double maxSnaga)
        {
            Console.WriteLine("------ Baterija ------");
            Console.WriteLine("kapacitet: {0}", kapacitet);
            string rezimStr = "";
            if (rezim == 1) //ako je rezim 1 onda generator
            {
                rezimStr = "generator";
            }
            if (rezim == 2)     // 2 -> potrosac
            {
                rezimStr = "potrosac";
            }
            if (rezim == 0) // 0-> iskljucena 
            {
                rezimStr = "iskljucena";
            }

            Console.WriteLine("rezim: " + rezimStr);
            Console.WriteLine("----------------------)"); //neku bazu
            return Data.CentralnoVreme;
        }
    }
}
