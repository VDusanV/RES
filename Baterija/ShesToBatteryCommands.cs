using KomponenteUgovori;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baterija
{
    class ShesToBatteryCommands : IShesToBatteryCommands
    {
        public void posaljiKomandu(int punjenjePraznjenje)
        {
            // KAKO KAPACITET POVECAVATI..PROBLEMI: ako preskacem period punjenja, npr key na 24h.
            if (punjenjePraznjenje == 1) // PUNJENJE
            {

                BatteryData.baterija.Rezim = 1;
                if (BatteryData.promjenaKapaciteta == 0 && (BatteryData.vrijeme.Hour == 4)) // znaci da je 4 sata i da trebam povecati kapacitet za 1sat
                {
                    BatteryData.baterija.Kapacitet++;
                    BatteryData.promjenaKapaciteta = 4;

                }
                if (BatteryData.promjenaKapaciteta == 4 && (BatteryData.vrijeme.Hour == 5))// znaci bila je promjena na 4 sata, pa ako je 5 sati trebam samo za jos 1 sat povecati
                {
                    BatteryData.baterija.Kapacitet++;
                    BatteryData.promjenaKapaciteta = 5;

                }


            }
            if (punjenjePraznjenje == 2) // PRAZNJENJE
            {
                //mozda dodati novi exception da baci na SHESU ako je kapacitet baterije 0. 

                BatteryData.baterija.Rezim = 2;
                if (BatteryData.promjenaKapPraznjenje == 0 && (BatteryData.vrijeme.Hour == 15))
                {
                    BatteryData.baterija.Kapacitet--;
                    BatteryData.promjenaKapPraznjenje = 15;

                }
                if (BatteryData.promjenaKapPraznjenje == 15 && (BatteryData.vrijeme.Hour == 16))
                {
                    BatteryData.baterija.Kapacitet--;
                    BatteryData.promjenaKapPraznjenje = 16;

                }
            }
            if (punjenjePraznjenje == 0) //ISKLJUCIVANJE
            {
                BatteryData.baterija.Rezim = 0;


                if (BatteryData.promjenaKapaciteta == 5) //kada dodje 6, baterija se iskljucuje sa punjenja pa ovdje moram povecati
                {                                        //kapacitet da bi ukupno kapacitet bio povecan za 3 sata.
                    BatteryData.baterija.Kapacitet++;
                    BatteryData.promjenaKapaciteta = 0;
                }
                if (BatteryData.promjenaKapPraznjenje == 16)
                {
                    BatteryData.baterija.Kapacitet--;
                    BatteryData.promjenaKapPraznjenje = 0;
                }

            }

        }
    }
}
