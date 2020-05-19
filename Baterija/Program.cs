using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baterija
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Baterija");
            Thread client = new Thread(klijent);
            Thread server = new Thread(serverBaterija);





            client.Start();
            server.Start();
            Console.ReadLine();
        }

        private static void serverBaterija()
        {
            ServiceHost service = new ServiceHost(typeof(ShesToBatteryCommands));
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
                    ChannelFactory<IBatteryToShesCommands> factory = new ChannelFactory<IBatteryToShesCommands>("BatteryToShesCommands");
                    IBatteryToShesCommands proxy = factory.CreateChannel();

                    BatteryData.vrijeme = proxy.posaljiPodatke(BatteryData.baterija.Kapacitet, BatteryData.baterija.Rezim, BatteryData.baterija.MaxSnaga); //dodati kapacitet i rezim
                    //Console.WriteLine(BatteryData.vrijeme);
                    Thread.Sleep(1000);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Neuspjesno pokretanje klijenta.");
                }
            }
        }

    }
}
