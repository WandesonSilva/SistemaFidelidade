using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.PremiosConsulta
{
    public class ListarPremiosConsulta
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string imagem { get; set; }
        public string descricao { get; set; }
        public int quantidade { get; set; }
        public decimal pontosNecessario { get; set; }
        public string Validade { get; set; }
    }
}
