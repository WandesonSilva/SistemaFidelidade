
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Receita
    {
        public Receita()
        {

        }

        public Receita( decimal valor, int idEmpresa, int idCliente, int idUsuario, string tipoAtividade)
        {

            IdEmpresa = idEmpresa;
            IdCliente = idCliente;
            IdUsuario = idUsuario;
            Valor = valor;
            TipoAtividade = tipoAtividade;
            DataVenda = DateTime.Now;//.ToString("dd-MM-yyyy);
  
        }

        public int IdUsuario { get; set; }
        public int IdEmpresa { get; private set; }
        public int IdCliente { get; private set; }
        public decimal Valor { get; private set; }
        public string TipoAtividade { get; private set; }
        public DateTime DataVenda { get; private set; }

       

    }
}

