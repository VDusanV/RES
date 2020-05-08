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
            //Default-ni solarni panel
            SolarniPanel sp1 = new SolarniPanel()
            {
                Ime = "prvi",
                MaxSnaga = 50
            };
            //Default-ni potrosac
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
                //MENI
                Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~ MENI ~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("---- 1 -> Dodavanje novog potrosac          ----");
                Console.WriteLine("---- 2 -> Dodavanje novog solarnog panela   ----");
                Console.WriteLine("---- 3 -> Unos snage sunca(%)               ----");
                Console.WriteLine("------------------------------------------------\n");
                Console.WriteLine("Unesite vas izbor: ");
                try
                {
                    int izbor = Int32.Parse(Console.ReadLine());

                    switch (izbor)
                    {
                 
                        case 1:
                            Potrosac p = new Potrosac() { };
                            Console.WriteLine("Unesite jedinstveno ime potrosaca:");
                            string ime = Console.ReadLine();
                            if (!Data.potrosaci.ContainsKey(ime))
                            {
                                Console.WriteLine("Unesite potrosnju:");
                                try
                                {
                                    p.Potrosnja = double.Parse(Console.ReadLine());
                                    p.JedinstvenoIme = ime;
                                }
                                catch
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("-------------GRESKA------------",Console.ForegroundColor);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Greska pri unosu potrosnje.");
                                    break;
                                }
                                lock (Data.potrosaci)
                                {
                                    Data.potrosaci.Add(p.JedinstvenoIme, p);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("-------------GRESKA------------", Console.ForegroundColor);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("OPERACIJA NIJE USPELA, potrosac vec postoji.");
                                break;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("-------OPERACIJA IZVRSENA--------");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case 2:
                            SolarniPanel sp = new SolarniPanel() { };
                            Console.WriteLine("Unesite ime solarnog panela: ");
                            string naziv = Console.ReadLine();
                            if (!Data.solarniPaneli.ContainsKey(naziv))
                            {
                                Console.WriteLine("Unesite maksimalnu snagu solarnog panela: ");
                                try
                                {
                                    sp.MaxSnaga = Int32.Parse(Console.ReadLine());
                                    sp.Ime = naziv;
                                }
                                catch
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("-------------GRESKA------------", Console.ForegroundColor);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Greska pri unosu maksimalne snage solarnog panela.");
                                    break;
                                }
                                lock (Data.solarniPaneli)
                                {
                                    Data.solarniPaneli.Add(sp.Ime, sp);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("-------------GRESKA------------", Console.ForegroundColor);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("OPERACIJA NIJE USPELA, solarni panel vec postoji.");
                                break;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("-------OPERACIJA IZVRSENA--------");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;



                        case 3:
                            Console.WriteLine("Unesite snagu sunca u %: ");
                            try
                            {
                                double snaga = double.Parse(Console.ReadLine());
                                if (snaga >= 0 && snaga <= 100)
                                {
                                    Data.snagaSunca = snaga;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("-------------GRESKA------------", Console.ForegroundColor);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("OPERACIJA NIJE USPELA, Opseg snage sunca je 0%-100%.");
                                    break;
                                }

                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("-------------GRESKA------------", Console.ForegroundColor);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("OPERACIJA NIJE USPELA, neispravan unos.");
                                break;
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("-------OPERACIJA IZVRSENA--------");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;




                        default:
                            Console.WriteLine("---------------------------------");
                            Console.WriteLine("Unesite neku od ponudjenih opcija!");
                                 break;

                    }

                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-------------GRESKA------------", Console.ForegroundColor);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("OPERACIJA NIJE USPELA, neispravan unos opcije menija.");                    
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
