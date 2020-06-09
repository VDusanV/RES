using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ElektroDistribucija
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost service = new ServiceHost(typeof(ImplementacijaElekDis));
            service.Open();
            Console.WriteLine("Pritisnite taster ako zelite da prekinete komunikaciju sa Elektrodistribucijom");
            Console.ReadKey();
            service.Close();
        }
    }
}
