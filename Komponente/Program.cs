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
            Thread t4 = new Thread(PosaljiUkupnuPotrosnjuPunjaca);

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            
        }

        private static void PosaljiUkupnuPotrosnjuPunjaca()
        {
            ChannelFactory<IPunjac> factoryPanel = new ChannelFactory<IPunjac>("ImplementacijaPunjaca");
            IPunjac proxyPunjac = factoryPanel.CreateChannel();
            Thread.Sleep(1500);

            while (true)
            {
                //Auto ce se ukljuciti na punjenje izmedju definisanog vremena bez obzira na sve
                if(Data.vreme.Hour>=Data.PunjacEA.VremePunjenjaOd.Hour 
                    || Data.vreme.Hour <= Data.PunjacEA.VremePunjenjaDo.Hour)
                {
                    proxyPunjac.GetUkupnaPotrosnjaPunjaca(Data.PunjacEA.MaxSnagaBaterije);

                }
                else if (Data.PunjacEA.AutoNaPunjacu && Data.PunjacEA.DaLiZelimoDaSePuni)
                {
                    proxyPunjac.GetUkupnaPotrosnjaPunjaca(Data.PunjacEA.MaxSnagaBaterije);
                }
                else
                {
                    proxyPunjac.GetUkupnaPotrosnjaPunjaca(0);

                }
                Thread.Sleep(1000);
            }
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
                Console.WriteLine("---- 4 -> Elektricni automobil              ----");
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
                                    Greska("Greska pri unosu potrosnje.");
                                    break;
                                }
                                lock (Data.potrosaci)
                                {
                                    Data.potrosaci.Add(p.JedinstvenoIme, p);
                                }
                            }
                            else
                            {
                                Greska("potrosac vec postoji.");
                                break;
                            }
                            UspesnaOperacija();
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
                                    sp.MaxSnaga = double.Parse(Console.ReadLine());
                                    sp.Ime = naziv;
                                }
                                catch
                                {
                                    Greska("Greska pri unosu maksimalne snage solarnog panela.");
                                    break;
                                }
                                lock (Data.solarniPaneli)
                                {
                                    Data.solarniPaneli.Add(sp.Ime, sp);
                                }
                            }
                            else
                            {
                                Greska("solarni panel vec postoji.");
                                break;
                            }
                            UspesnaOperacija();

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
                                    Greska("Opseg snage sunca je 0%-100%.");
                                    break;
                                }

                            }
                            catch
                            {
                                Greska("neispravan unos.");
                                break;
                            }

                            UspesnaOperacija();
                            break;

                        case 4:
                            //Po defaultu je punjac iskljucen treba ga ukljuciti da bi slao podatke
                            Elekauto();
                            break;


                        default:
                            Console.WriteLine("---------------------------------");
                            Console.WriteLine("Unesite neku od ponudjenih opcija!");
                                 break;

                    }

                }
                catch
                {
                    Greska("unos opcije menija.");                 
                }

            }
        }
        
        private static void Elekauto()
        {
            while (true)
            {
                Console.WriteLine("\n~~~~~~~~~~~ ELEKTRICNI AUTOMOBIL ~~~~~~~~~~~~~");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("---- 1 -> Trenutno stanje                   ----");
                Console.WriteLine("---- 2 -> Ukljuci / Iskljuci                ----");
                Console.WriteLine("---- 3 -> Promeni procenat napunjenosti     ----");
                Console.WriteLine("---- 4 -> MENI                              ----");
                Console.WriteLine("------------------------------------------------\n");
                Console.WriteLine("Unesite vas izbor: ");
              
                try
                {
                    int izbor = Int32.Parse(Console.ReadLine());
                    if (izbor == 4)
                    {
                        break;
                    }
                    switch (izbor)
                    {
                        case 1:
                            Console.WriteLine("------Trenutno stanje je------");
                            Console.WriteLine("---Max snaga:               " + Data.PunjacEA.MaxSnagaBaterije);
                            Console.WriteLine("---Ukljucen/Iskljucen:      " + Data.PunjacEA.AutoNaPunjacu);
                            Console.WriteLine("---Procenat napunjenosti:   " + Data.PunjacEA.NapunjenostBaterije);
                            break;

                        case 2:
                            Data.PunjacEA.UkljuciIskljuci();
                            Console.WriteLine("Stanje nakon izvresene metode Ukljuci/Iskljuci --> " + Data.PunjacEA.AutoNaPunjacu);
                            UspesnaOperacija();
                            break;
                        case 3:
                            Console.WriteLine("Unesite procenat (%) napunjenosti baterije");
                            int pro = Int32.Parse(Console.ReadLine());
                            if (pro >= 0 && pro <= 100)
                            {
                                Data.PunjacEA.NapunjenostBaterije = pro;
                                UspesnaOperacija();
                            }
                            else
                            {
                                Greska("Greska pri unosu procenta.");
                            }
                            break;

                        default:
                            Console.WriteLine("Unesite komandu iz menija");
                            break;
                    }
                }
                catch
                {
                    Greska("neispravan unos.");
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
                Data.vreme = proxyPotrosaci.GetPotrosnjaPotrosaca(potrosnja);
               // Vreme se konstantno menja, odkomentarisi i pokreni
               // Console.WriteLine(Data.vreme);

                Thread.Sleep(1000);
            }
        }
        static void Greska(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-------------GRESKA------------", Console.ForegroundColor);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("OPERACIJA NIJE USPELA, "+s);
        }
        static void UspesnaOperacija()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-------OPERACIJA IZVRSENA--------");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
