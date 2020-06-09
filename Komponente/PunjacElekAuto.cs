using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komponente
{
    public class PunjacElekAuto
    {
        public double MaxSnagaBaterije { get; set; }
        public bool AutoNaPunjacu { get; set; }
        public bool DaLiZelimoDaSePuni { get; set; }
        public int NapunjenostBaterije { get; set; }

        public DateTime VremePunjenjaOd { get; set; }
        public DateTime VremePunjenjaDo { get; set; }

        public PunjacElekAuto(double m)
        {
            if (m < 0)
            {
                throw new ArgumentException("Maksimalna snaga ne sme biti negativna");

            }
            MaxSnagaBaterije = m;
            AutoNaPunjacu = false;
            DaLiZelimoDaSePuni = true;
            NapunjenostBaterije = 0;
            VremePunjenjaOd = new DateTime(2010, 10, 10, 21, 0, 0) { };
            VremePunjenjaDo = new DateTime(2010, 10, 10, 6, 0, 0) { };
        }

        public void UkljuciIskljuci()
        {
            if (AutoNaPunjacu)
                AutoNaPunjacu = false;
            else
                AutoNaPunjacu = true;
        }
    }
}
