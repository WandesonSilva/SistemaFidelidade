using FluentValidator;
using System;


namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Pontuacao : Notifiable
    {
        public Pontuacao(decimal saldo, int idEmpresa, int idCliente)
        {
            IdCliente = idCliente;
            IdEmpresa = idEmpresa;
            Saldo = saldo;
            DataVisita = DateTime.Now.Date;

        }
        public Pontuacao(int idClinte, int idEmpresa)
        {
            IdCliente = idClinte;
            IdEmpresa = idEmpresa;
        }

        public Pontuacao(decimal saldo)
        {

            Saldo = saldo;
            DataVisita = DateTime.Now.Date;

        }

        public Pontuacao()
        {

        }

        public int ID { get; set; }
        public int IdEmpresa { get; private set; }
        public int IdCliente { get; private set; }
        public DateTime Validade { get; private set; }
        public decimal SaldoTransacao { get; private set; }
        public decimal Saldo { get; private set; }
        public DateTime DataVisita { get; private set; }


        public void Pontuar(decimal valorGasto, decimal pontosFidelidade, decimal gastoNecessario, double validadePontos)
        {
            var _saldoTransacao = (valorGasto * pontosFidelidade) / gastoNecessario;
            SaldoTransacao = Math.Round(_saldoTransacao, 0);
            Saldo += SaldoTransacao;
            // Mudar esta regra,  para valida a data do programa, colocar uma data de expiração do programa 
            Validade = DateTime.Now.AddDays(validadePontos); //adiciono a validade exemplo   360 dias
        }

        public void Resgatar(decimal qtdPontos)
        {
            Saldo -= qtdPontos;
            // Validade = DateTime.Now.AddDays(validadePontos).ToString();   
        }




    }
}
