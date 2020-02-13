using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.PremiosConsulta
{
    public class ObterDetalhePremioConsulta
    {
        public int ID { get; set; }
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public decimal PontosNecessario { get; set; }
        public string Descricao { get; private set; }
        public decimal Quantidade { get; private set; }
        public string Imagem { get; private set; }
        public string Validade { get; private set; }
    }
}
