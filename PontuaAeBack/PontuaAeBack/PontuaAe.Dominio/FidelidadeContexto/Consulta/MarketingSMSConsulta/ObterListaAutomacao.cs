using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ObterListaAutomacao
    {

        public int ID { get; set; }
        public int IdEmpresa { get; set; }
        public string CampanhaAutomatica { get; set; }
        public string TipoCanal { get; set; }
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public string QtdEnviado { get; set; }
        public decimal ValorInvestido { get; set; }
        public int QtdRetorno { get; set; }
        public decimal TotalVendas { get; set; }
        public int QtdRecebida { get; set; }


    }
}
