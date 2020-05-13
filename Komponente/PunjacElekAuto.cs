using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komponente
{
    public class PunjacElekAuto
    {
        public int MaxSnagaBaterije { get; set; }
        public bool AutoNaPunjacu { get; set; }
        public bool DaLiZelimoDaSePuni { get; set; }
        public int NapunjenostBaterije { get; set; }
       // Definisati vreme kada se auto puni i to implementirati
       // public DateTime VremePunjenja { get; set; }

        public PunjacElekAuto()
        {
            MaxSnagaBaterije = 250;
            AutoNaPunjacu = false;
            DaLiZelimoDaSePuni = true;
            NapunjenostBaterije = 0;
        }
        public PunjacElekAuto(int m)
        {
            MaxSnagaBaterije = m;
            AutoNaPunjacu = false;
            DaLiZelimoDaSePuni = true;
            NapunjenostBaterije = 0;
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
