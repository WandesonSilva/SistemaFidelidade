using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta
{
    public class ObterDetalheConfigPontuacao
    {

        public int IdEmpresa { get; set; }
        public decimal Reais { get; set; }
        public decimal PontosFidelidade { get; set; }
        public DateTime Validade { get; set; }
        public decimal PontosInicial { get; set; }

    }
}
