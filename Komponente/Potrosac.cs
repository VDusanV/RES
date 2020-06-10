using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komponente
{
    public class Potrosac
    {
        public string JedinstvenoIme { get; set; }
        public double Potrosnja { get; set; }
        public Potrosac() { }

        public Potrosac(string ime, double potrosnja)
        {
            if (ime == null)
            {
                throw new ArgumentException("Argument ime ne sme biti null vrednost");
            }
            if (potrosnja < 0)
            {
                throw new ArgumentException("Potrosnja ne sme biti negativna");
            }
            if (ime.Trim() == "")
            {
                throw new ArgumentException("Naziv ne sme biti prazan");
            }
            JedinstvenoIme = ime.Trim();
            Potrosnja = potrosnja;


        }
    }
}
