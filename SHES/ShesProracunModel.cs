using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    public class ShesProracunModel
    {
        public string Datum { get; set; }
        public double ProizvodnjaPanela { get; set; }
        public double EnergijaIzBaterije { get; set; }
        public double PotrosnjaPotrosaca { get; set; }
        public double UvozIzElektrodistribucije { get; set; }
        //
        public double UkupnaCijena { get; set; }


    }
}
