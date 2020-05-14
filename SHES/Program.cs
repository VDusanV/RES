using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SHES
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SERVER");
            Thread vreme = new Thread(SimulacijaVremena);
            Data.CentralnoVreme = DateTime.Now;
            vreme.Start();


            ServiceHost service = new ServiceHost(typeof(ImplementacijaPotrosaca));
            ServiceHost servicePanel = new ServiceHost(typeof(ImplementacijaSolarniPanel));
            ServiceHost servicePunjac = new ServiceHost(typeof(ImplementacijaPunjaca));

            service.Open();
            servicePanel.Open();
            servicePunjac.Open();

            Console.ReadLine();
            service.Close();
            servicePanel.Close();
            servicePunjac.Close();
        }

        private static void SimulacijaVremena()
        {
            while (true)
            {
                try
                {
                    //Odnos realnog i simuliranog vremena se podesava u fajlu App.config
                    //ako crveni ConfigurationManager, dodaj u References System.Configuration
                    int i = Int32.Parse(ConfigurationManager.AppSettings["minuta"]);
                    Data.CentralnoVreme = Data.CentralnoVreme.AddMinutes(i);
                    Console.WriteLine(Data.CentralnoVreme);
                    Thread.Sleep(1000);
                }
                catch
                {
                    Console.WriteLine("Greska prilikom simulacije vremena");
                    return;
                }
            }
        }
    }
}
