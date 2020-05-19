using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KomponenteUgovori
{
    [ServiceContract]
    public interface IBatteryToShesCommands
    {
        [OperationContract]
        DateTime posaljiPodatke(int kapacitet, int rezim, double maxSnaga);
    }
}
