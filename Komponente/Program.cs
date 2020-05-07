using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Komponente
{
    class Program
    {
     
        static void Main(string[] args)
        {
     

            Potrosac p1 = new Potrosac()
            {
                JedinstvenoIme = "prvi",
                Potrosnja = 100
            };
            Data.potrosaci = new Dictionary<string, Potrosac>();
            Data.potrosaci.Add(p1.JedinstvenoIme, p1);
            Thread t1 = new Thread(Upis);
            Thread t2 = new Thread(Slanje);

            t1.Start();
            t2.Start();

            
        }

        static void Upis()
        {
            while (true)
            {
                Potrosac p = new Potrosac() { };
                Console.WriteLine("Unesite jedinstveno ime potrosaca");
                p.JedinstvenoIme = Console.ReadLine();
                Console.WriteLine("Unesite potrosnju");
                p.Potrosnja = Int32.Parse(Console.ReadLine());
                lock (Data.potrosaci)
                {
                    Data.potrosaci.Add(p.JedinstvenoIme, p);
                }
            }
        }
        static void Slanje()
        {
            ChannelFactory<IPotrosac> factory = new ChannelFactory<IPotrosac>("ImplementacijaPotrosaca");
            IPotrosac proxyPotrosaci = factory.CreateChannel();
            while (true)
            {
                double potrosnja = 0;
                lock (Data.potrosaci)
                {
                    foreach (var item in Data.potrosaci)
                    {
                        potrosnja += item.Value.Potrosnja;
                    }
                }
                proxyPotrosaci.GetPotrosnjaPotrosaca(potrosnja);
                Thread.Sleep(1000);
            }
        }
    }
}
