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
            Thread upisBaza = new Thread(bazaUpis); 

            Data.CentralnoVreme = new DateTime(2021, 12, 04, 0, 0, 0);
            Data.dan = Data.CentralnoVreme.Day;
            vreme.Start();

            client.Start();
            server.Start();
            upisBaza.Start();

            ServiceHost service = new ServiceHost(typeof(ImplementacijaPotrosaca));
            ServiceHost servicePanel = new ServiceHost(typeof(ImplementacijaSolarniPanel));
            ServiceHost servicePunjac = new ServiceHost(typeof(ImplementacijaPunjaca));

            service.Open();
            servicePanel.Open();
            servicePunjac.Open();

            Thread ispis = new Thread(Ispis);
            ispis.Start();

           

            
            //grafik
            while (true)
            {
                Console.WriteLine("Unesite zeljeni datum za koji zelite analizu: ");
                string datumUnos = Console.ReadLine();
                string[] split = datumUnos.Split(':');
                int dan = Int32.Parse(split[0]);
                int mjesec = Int32.Parse(split[1]);
                int godina = Int32.Parse(split[2]);

                string trazeniDatumBaza = "";
                trazeniDatumBaza += dan + ";" + mjesec + ";" + godina.ToString();


                ShesProracunModel modelIzBaze = new ShesProracunModel();
                List<ShesProracunModel> proracun = DataBaseAccess.UcitajProracun(trazeniDatumBaza);
                foreach (ShesProracunModel md in proracun)
                {
                    modelIzBaze = md;
                }

                Graph graph = new Graph(datumUnos, modelIzBaze.ProizvodnjaPanela, modelIzBaze.PotrosnjaPotrosaca, modelIzBaze.EnergijaIzBaterije);//dodati Elektro kao parametar
                graph.ShowDialog();
            
            }



            Console.ReadLine();
            service.Close();
            servicePanel.Close();
            servicePunjac.Close();
        }

        private static void Ispis()
        {
            while (true)
            {
                /*Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("--------" + Data.CentralnoVreme + "---------");
                Console.WriteLine("--Trenutna potrosnja potrosaca je-------------->" + Data.Potrosac);
                Console.WriteLine("--Trenutna potrosnja punjaca je---------------->" + Data.Punjac);
                Console.WriteLine("--Trenutna prooizvodnja solarnih panela je----->" + Data.SolarniPanel);
                Console.WriteLine("--Baterija potrosnja--------------------------->" + Data.Baterija);
                Console.Write("--Ukupno stanje je ");
                
                TrenutnoStanje(Data.IzracunajUkupnoStanje());
                */
                Data.Proizvodnja += Data.SolarniPanel;
                Data.Potrosnja += Data.Potrosac + Data.Punjac;
                Data.EnergijaIzBaterije += Data.Baterija;
                //Data.UvozIzElektroDistribucije += 

                Thread.Sleep(2000);
            }
        }
        private static void bazaUpis()
        {
            while (true)
            {
                if (Data.dan != Data.CentralnoVreme.Day)
                {

                    Data.dan = Data.CentralnoVreme.Day;

                    DateTime tempVr = Data.CentralnoVreme; //moram upisati prosli dan u bazu
                    tempVr = tempVr.Date.AddDays(-1);

                    
                    ShesProracunModel noviModel = new ShesProracunModel();
                    noviModel.Datum = ""; //primarni kljuc u bazi
                    noviModel.Datum += tempVr.Day + ";" + tempVr.Month + ";" + tempVr.Year;
                    noviModel.EnergijaIzBaterije = Data.EnergijaIzBaterije; //ukupna energija za dan
                    noviModel.PotrosnjaPotrosaca = Data.Potrosnja; //ukupna potrosnja za dan
                    noviModel.ProizvodnjaPanela = Data.Proizvodnja; //ukupna proizvodnja za dan
                    noviModel.UvozIzElektrodistribucije = 300; //ukupan uvoz iz elektrod Data.UvozIzElektrodistribucije
                    DataBaseAccess.SacuvajProracun(noviModel);

                    //osvjezi vrijednosti za novi dan
                    Data.EnergijaIzBaterije = 0;
                    Data.Potrosnja = 0;
                    Data.Proizvodnja = 0;
                }
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
