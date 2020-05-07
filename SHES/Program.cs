﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SERVER");
            ServiceHost service = new ServiceHost(typeof(ImplementacijaPotrosaca));
            ServiceHost servicePanel = new ServiceHost(typeof(ImplementacijaSolarniPanel));

            service.Open();
            servicePanel.Open();

            Console.ReadLine();
            service.Close();
            servicePanel.Close();
        }
    }
}
