using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    public class DataBaseAccess
    {
        public static List<ShesProracunModel> UcitajProracun(string trazeniDatumBaza)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ShesProracunModel>("select * from Proracun where Datum == '" + trazeniDatumBaza + "'", new DynamicParameters());
                return output.ToList();
            }

        }

        public static void SacuvajProracun(ShesProracunModel proracun)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Proracun (Datum, ProizvodnjaPanela, EnergijaIzBaterije, PotrosnjaPotrosaca, UvozIzElektrodistribucije) values (@Datum, @ProizvodnjaPanela, @EnergijaIzBaterije, @PotrosnjaPotrosaca, @UvozIzElektrodistribucije)", proracun);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
