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
            SolarniPanel sp1 = new SolarniPanel()
            {
                Ime = "prvi",
                MaxSnaga = 50
            };

            Potrosac p1 = new Potrosac()
            {
                JedinstvenoIme = "prvi",
                Potrosnja = 100
            };
            Data.potrosaci = new Dictionary<string, Potrosac>();
            Data.solarniPaneli = new Dictionary<string, SolarniPanel>();

            Data.potrosaci.Add(p1.JedinstvenoIme, p1);
            Data.solarniPaneli.Add(sp1.Ime, sp1);

            Thread t1 = new Thread(Upis);
            Thread t2 = new Thread(Slanje);
            Thread t3 = new Thread(PosaljiUkupnuSnagu);

            t1.Start();
            t2.Start();
            t3.Start();
            
        }
        
        static void PosaljiUkupnuSnagu()
        {
            ChannelFactory<ISolarniPanel> factoryPanel = new ChannelFactory<ISolarniPanel>("ImplementacijaSolarniPanel");
            ISolarniPanel proxyPanel = factoryPanel.CreateChannel();
            while (true)
            {
                double ukupnaSnaga = 0;
                lock (Data.solarniPaneli)
                {
                    foreach (var item in Data.solarniPaneli)
                    {
                        //procenat u odnosu na snagu sunca -> snaga sunca 50% -> 50%max snage panela se salje
                        ukupnaSnaga += (item.Value.MaxSnaga * (Data.snagaSunca / 100));
                        
                    }
                }
                proxyPanel.GetUkupnaSnaga(ukupnaSnaga);
                Thread.Sleep(1000);
            }
        }

        static void Upis()
        {
            while (true)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Dodavanje novog potrosaca -> 1");
                Console.WriteLine("Dodavanje novog solarnog panela -> 2");
                Console.WriteLine("Unos snage sunca(%) -> 3");
                Console.WriteLine("Unesite vas izbor: ");
                int izbor = Int32.Parse(Console.ReadLine());

                switch (izbor)
                {
                    case 1:
                        Potrosac p = new Potrosac() { };
                        Console.WriteLine("Unesite jedinstveno ime potrosaca");
                        p.JedinstvenoIme = Console.ReadLine();
                        Console.WriteLine("Unesite potrosnju");
                        p.Potrosnja = Int32.Parse(Console.ReadLine());
                        lock (Data.potrosaci)
                        {
                            Data.potrosaci.Add(p.JedinstvenoIme, p);
                        }
                        break;
                    case 2:
                        SolarniPanel sp = new SolarniPanel() { };
                        Console.WriteLine("Unesite ime solarnog panela: ");
                        //pri dodavanju provjeriti da li to ime postoji vec //foreach kroz dictionary if->continue
                        sp.Ime = Console.ReadLine();
                        Console.WriteLine("Unesite maksimalnu snagu solarnog panela: ");
                        sp.MaxSnaga = Int32.Parse(Console.ReadLine());
                        lock (Data.solarniPaneli)
                        {
                            Data.solarniPaneli.Add(sp.Ime, sp);
                        }
                        break;
                    case 3:  Console.WriteLine("Unesite snagu sunca u %: ");
                             Data.snagaSunca = double.Parse(Console.ReadLine());
                             break;
                    default: Console.WriteLine("Unesite neku od ponudjenih opcija!");
                             break;

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
