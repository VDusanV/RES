using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektroDistribucija
{
    public class Elektrodistribucija
    {
        public double SnagaRazmene { get; set; }
        public double CenaPoKilovatcasu { get; set; }

        public Elektrodistribucija(double snaga, double cena)
        {
            if (snaga < 0 || cena < 0)
            {
                throw new Exception();
            }
            SnagaRazmene = snaga;
            CenaPoKilovatcasu = cena;
        }

        public double IzracunajCenu()
        {
            return SnagaRazmene * CenaPoKilovatcasu;
        }
    }
}
