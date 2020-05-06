using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Komponente
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IPotrosac> factory = new ChannelFactory<IPotrosac>("ImplementacijaPotrosaca");
            IPotrosac proxyPotrosaci = factory.CreateChannel();

            Potrosac p1 = new Potrosac()
            {
                JedinstvenoIme = "prvi",
                Potrosnja = 200

            };

            proxyPotrosaci.GetPotrosnjaPotrosaca(p1.Potrosnja);
        }
    }
}
