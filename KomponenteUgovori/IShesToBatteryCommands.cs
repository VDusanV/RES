using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KomponenteUgovori
{
    [ServiceContract]
    public interface IShesToBatteryCommands
    {
        [OperationContract]
        void posaljiKomandu(int punjenjePraznjenje); //punjenje 1, praznjenje 2, iskljuci 0 
    }
}
