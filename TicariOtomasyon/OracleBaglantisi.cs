using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
namespace TicariOtomasyon
{
    class OracleBaglantisi
    {
        public OracleConnection baglanti()
        {
            OracleConnection baglan = new OracleConnection(@"Data Source=localhost:1521/xe;User Id=SYSTEM;Password=SYSTEM; ");
            baglan.Open();
            return baglan;

            
        }
    }
}
