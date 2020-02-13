using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class PontuacaoRepositorio : IPontuacaoRepositorio
    {
        private readonly PontuaAeDataContexto _db;
        public PontuacaoRepositorio(PontuaAeDataContexto db)
        {
            _db = db;

        }

        public void AtualizarSaldo(Pontuacao update)
        {
            _db.Connection
                 .Execute("UPDATE PONTUACAO SET Saldo=@Saldo, DataVisita=@DataVisita,  SaldoTransacao=@SaldoTransacao  WHERE IdEmpresa=@IdEmpresa and IdCliente=@IdCliente", new {
                     @Saldo = update.Saldo,
                     @DataVisita = update.DataVisita,
                     @IdEmpresa = update.IdEmpresa,
                     @IdCliente = update.IdCliente,
                     @SaldoTransacao = update.SaldoTransacao
                 });
        }

        public bool ChecarCelular(string Telefone)   //talvez deleta
        {
            return _db.Connection
                .QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS( SELECT Telefone FROM CLIENTE WHERE IdEmpresa = @IdEmpresa AND Telefone = @Telefone  ) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Telefone = Telefone });
        }

        public void CriarPontuacao(Pontuacao pontuacao)
        {
            _db.Connection
                .Execute("INSERT INTO PONTUACAO (IdEmpresa, IdCliente, DataVisita, Saldo, SaldoTransacao) VALUES (@IdEmpresa, @IdCliente, @DataVisita, @Saldo, @SaldoTransacao)", new
                {
                    @IdEmpresa = pontuacao.IdEmpresa,
                    @IdCliente = pontuacao.IdCliente,
                    @DataVisita = pontuacao.DataVisita.Date,
                    @Saldo = pontuacao.Saldo,
                    @SaldoTransacao = pontuacao.SaldoTransacao,

                });
        }

        //public IEnumerable<Pontuacao> ListaPontuacao(int IdUsuario)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Pontuacao> ListaPontuacaoPorData(int IdUsuario, DateTime Date)
        //{
        //    throw new NotImplementedException();
        //}

        public decimal obterSaldo(int IdEmpresa, int IdCliente)
        {
            return _db.Connection
                .ExecuteScalar<decimal>("SELECT Saldo FROM PONTUACAO WHERE IdEmpresa=@IdEmpresa AND IdCliente=@IdCliente ", new { @IdEmpresa = IdEmpresa, @IdCliente = IdCliente });
        }

        //corrigir resgatar 
        public void resgatar(Pontuacao resgatar)
        {
            _db.Connection.Execute("UPDATE PONTUACAO SET Saldo=@Saldo WHERE IdEmpresa=@IdEmpresa", new
            {
                @Saldo = resgatar.Saldo,

            });

        }

        public ObterIdEmpresaConsulta ChecarCampoIdEmpresa(int IdEmpresa)
        {
            return _db.Connection
                 .QueryFirstOrDefault<ObterIdEmpresaConsulta>("SELECT ID FROM PONTUACAO WHERE IdEmpresa=@IdEmpresa", new { IdEmpresa = @IdEmpresa });
        }


        public Pontuacao ObterUltimaVisita(int IdEmpresa, int IdCliente)
        {
            return _db.Connection
                 .QueryFirst<Pontuacao>("SELECT DataVisita FROM PONTUACAO WHERE IdEmpresa=@IdEmpresa AND IdCliente=@IdCliente", new { IdEmpresa = IdEmpresa, @IdCliente = IdCliente });
        }

        //DELETA
        public Pontuacao ObterEstadoFidelidade(int IdCliente, int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pontuacao> ListaPontuacao(int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pontuacao> ListaPontuacaoPorData(int IdEmpresa, DateTime Date)
        {
            throw new NotImplementedException();
        }
    }
}
