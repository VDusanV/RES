using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektroDistribucija
{
    public class ImplementacijaElekDis : IElekDistribucija
    {
        private Elektrodistribucija e = new Elektrodistribucija(0, 200) { };
        public double Izracunaj(double snaga)
        {
            e.SnagaRazmene = snaga;
            return e.IzracunajCenu();
        }
    }
}
