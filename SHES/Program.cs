using KomponenteUgovori;
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

            Thread client = new Thread(klijent); //shes bateriji salje komande
            Thread server = new Thread(serverShes); //shes dobija informacije od baterije

            Data.CentralnoVreme = DateTime.Now;
            vreme.Start();

            client.Start();
            server.Start();

            ServiceHost service = new ServiceHost(typeof(ImplementacijaPotrosaca));
            ServiceHost servicePanel = new ServiceHost(typeof(ImplementacijaSolarniPanel));
            ServiceHost servicePunjac = new ServiceHost(typeof(ImplementacijaPunjaca));

            service.Open();
            servicePanel.Open();
            servicePunjac.Open();

            Thread ispis = new Thread(Ispis);
            ispis.Start();

            Console.ReadLine();
            service.Close();
            servicePanel.Close();
            servicePunjac.Close();
        }

        private static void Ispis()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("--------" + Data.CentralnoVreme + "---------");
                Console.WriteLine("--Trenutna potrosnja potrosaca je-------------->" + Data.Potrosac);
                Console.WriteLine("--Trenutna potrosnja punjaca je---------------->" + Data.Punjac);
                Console.WriteLine("--Trenutna prooizvodnja solarnih panela je----->" + Data.SolarniPanel);
                Console.WriteLine("--Baterija potrosnja--------------------------->" + Data.Baterija);
                Console.Write("--Ukupno stanje je ");
                TrenutnoStanje(Data.IzracunajUkupnoStanje());

                Thread.Sleep(2000);
            }
        }

        private static void TrenutnoStanje(double d)
        {
            
            
            if (d >= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(d);
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(d);
                Console.ForegroundColor = ConsoleColor.White;

            }
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
                    
                    
                    Thread.Sleep(1000);
                }
                catch
                {
                    Console.WriteLine("Greska prilikom simulacije vremena");
                    return;
                }
            }
        }

        private static void serverShes()
        {

            ServiceHost service = new ServiceHost(typeof(BatteryToShesCommands));
            service.Open();
            Console.ReadKey();
            service.Close();
        }

        private static void klijent()
        {
            while (true)
            {
                try
                {
                    ChannelFactory<IShesToBatteryCommands> factory1 = new ChannelFactory<IShesToBatteryCommands>("ShesToBatteryCommands");
                    IShesToBatteryCommands proxy = factory1.CreateChannel();

                    if (Data.CentralnoVreme.Hour >= 3 && Data.CentralnoVreme.Hour < 6 && (Data.CentralnoVreme.ToString("tt").Equals("AM")))
                        proxy.posaljiKomandu(1); //punjenje
                    if (Data.CentralnoVreme.Hour >= 14 && Data.CentralnoVreme.Hour < 17 && (Data.CentralnoVreme.ToString("tt").Equals("PM")))
                        proxy.posaljiKomandu(2); //praznjenje
                    if (!(Data.CentralnoVreme.Hour >= 14 && Data.CentralnoVreme.Hour < 17) && !(Data.CentralnoVreme.Hour >= 3 && Data.CentralnoVreme.Hour < 6))
                        proxy.posaljiKomandu(0); //ugasi
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
