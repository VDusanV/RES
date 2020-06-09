using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KomponenteUgovori
{
    [ServiceContract]
    public interface IElekDistribucija
    {
        [OperationContract]
        double Izracunaj(double snaga);
    }
}
