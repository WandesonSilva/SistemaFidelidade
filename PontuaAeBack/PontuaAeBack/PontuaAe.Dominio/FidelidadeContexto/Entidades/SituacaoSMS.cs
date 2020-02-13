using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.ObjetoValor
{
    public class SituacaoSMS
    {
        public SituacaoSMS()
        {

        }
        public SituacaoSMS(string verificado, int idEmpresa, int id, DateTime dataVenda)
        {
            Verificado = verificado;
            IdEmpresa = idEmpresa;
            ID = id;
            DataCompra = dataVenda;
        }
        public SituacaoSMS(string dataRecebida, string estado, string celular, int idEmpresa, int idCampanha)
        {
            Contato = celular;
            DataRecebida = Convert.ToDateTime(dataRecebida);
            Estado = estado;
            IdEmpresa = idEmpresa;
            IdCampanha = idCampanha;
            TotalVendas = 0;
        }

        public int ID { get; private set; }
        public int IdEmpresa { get; private set; }
        public int IdCampanha { get; private set; }
        public string Contato { get; private set; }
        public DateTime DataRecebida { get; private set; }
        public DateTime DataCompra { get; private set; }
        public string Estado { get; private set; }
        public string Verificado { get; private set; }
        public decimal TotalVendas { get; private set; }

        public void CalcularConversao(int qtdRetorno, decimal valorGasto) => TotalVendas = valorGasto * qtdRetorno;

    }
}
