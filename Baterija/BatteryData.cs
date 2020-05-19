using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baterija
{
    public class BatteryData
    {
        public static DateTime vrijeme;
        public static Baterija baterija = new Baterija { };
        public static int promjenaKapaciteta = 0; //na kojem ssatu je bilo posljednje uvecanje kapaciteta
        public static int promjenaKapPraznjenje = 0; //na kojem satu je bilo posljednje smanjenje kapaciteta
    }
}
