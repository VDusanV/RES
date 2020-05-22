using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baterija
{
    public class Baterija
    {
        public string Ime { get; set; }
        public int Kapacitet { get; set; } //kapacitet u satima
        public double MaxSnaga { get; set; }
        public int Rezim { get; set; }    //generator, potrosac ili je iskljucena                                
                                                                           
        public Baterija()
        {
            Ime = "BaterijaNaziv";
            Kapacitet = 3;
            MaxSnaga = 900;
        }


    }
}
