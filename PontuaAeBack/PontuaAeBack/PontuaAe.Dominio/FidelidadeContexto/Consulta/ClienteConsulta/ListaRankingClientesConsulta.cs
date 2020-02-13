using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta
{
    public class ListaRankingClientesConsulta
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public decimal TotalGasto { get; set; }
        public DateTime UltimaVisita { get; set; }
        public decimal Visitas { get; set; }
        public decimal GastoMes { get; set; } //ticket medio por mes
        public string Telefone { get; set; }
 

    }
}
