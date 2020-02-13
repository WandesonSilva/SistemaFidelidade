using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ObterListaCampanhaSMS
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Canal { get; set; }
        public string Segmento { get; set; }
        public int QtdCliente { get; set; }
        public string DataRecebida { get; set; }
        public string EstadoEnvio { get; set; }

        public string QtdEnviado { get; set; }
        public decimal ValorInvestido { get; set; }
        public int QtdRetorno { get; set; }
        public decimal TotalVendas { get; set; }
        public int QtdRecebida { get; set; }
    }
}
