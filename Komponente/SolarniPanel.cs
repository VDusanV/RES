using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komponente
{
    public class SolarniPanel
    {
        public string Ime { get; set; }
        public double MaxSnaga { get; set; }

        public SolarniPanel() { }
        public SolarniPanel(string ime, double snaga)
        {
            if (ime == null)
            {
                throw new ArgumentException("ime ne sme biti null vrednost");
            }
            if (snaga < 0)
            {
                throw new ArgumentException("Potrosnja ne sme biti negativna");
            }
            if (ime.Trim() == "")
            {
                throw new ArgumentException("Naziv ne sme biti prazan");
            }
            Ime = ime.Trim();
            MaxSnaga = snaga;
        }


    }
}
