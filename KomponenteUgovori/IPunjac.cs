﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KomponenteUgovori
{
    [ServiceContract]
    public interface IPunjac
    {
        [OperationContract]
        void GetUkupnaPotrosnjaPunjaca(double ukupnaSnaga);

    }
}
