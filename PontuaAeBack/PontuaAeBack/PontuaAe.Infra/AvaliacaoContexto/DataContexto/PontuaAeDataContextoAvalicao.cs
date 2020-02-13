using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Avaliacao.DataContexto
{
    public class PontuaAeDataContextoAvalicao
    {
        public SqlConnection Connection { get; set; }

        public PontuaAeDataContextoAvalicao()
        {
            Connection = new SqlConnection("Server=den1.mssql7.gear.host; Database=pontuaedb; User ID=pontuaedb; Password=Co8s89u_Xr5! ;");
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
