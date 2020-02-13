using PontuaAe.Compartilhado.DbConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.FidelidadeContexto.DataContexto
{
    public class PontuaAeDataContexto : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public PontuaAeDataContexto()
        {
          
            Connection = new SqlConnection(@"Server=DESKTOP-VI9V6I4;Database=PONTUAE;Trusted_Connection=True;");
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}

