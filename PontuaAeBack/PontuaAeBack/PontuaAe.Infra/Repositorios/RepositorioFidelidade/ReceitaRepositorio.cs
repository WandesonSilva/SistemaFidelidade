using System.Collections.Generic;
using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;


namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class ReceitaRepositorio : IReceitaRepositorio
    {
        private readonly PontuaAeDataContexto _db;


        public ReceitaRepositorio(PontuaAeDataContexto db)
        {
            _db = db;
        }       

        public void Salvar(Receita receita)
        {
            _db.Connection
                 .Execute("INSERT INTO RECEITA ( IdEmpresa, IdCliente, IdUsuario, Valor, DataVenda, TipoAtividade) values (@IdEmpresa, @IdCliente, @IdUsuario, @Valor, @DataVenda, @TipoAtividade)", 
                 new { receita });
        }

        public int QtdVisitasSessentaDias(int IdEmpresa, int IdCliente) //https://www.devmedia.com.br/funcoes-de-data-no-sql-server/1946
        {
            return _db.Connection.ExecuteScalar<int>("SELECT COUNT (DataVenda) FROM RECEITA WHERE IdCliente = @IdCliente and IdEmpresa = @IdEmpresa and DataVenda  BETWEEN DATEADD(MONTH, -1, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new
            { @IdCliente = IdCliente, @IdEmpresa = IdEmpresa });
        }

        public int QtdVisitasTrintaDias(int IdEmpresa, int IdCliente)
        {
            return _db.Connection.ExecuteScalar<int>("SELECT COUNT (DataVenda) FROM RECEITA WHERE IdCliente = IdCliente and IdEmpresa = @IdEmpresa and DataVenda  BETWEEN DATEADD(MONTH, 0, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new
            { @IdEmpresa = IdEmpresa, @IdCliente = IdCliente});
        }

        public int QtdVisitasNoventaDias(int IdEmpresa, int IdCliente)
        {
            return _db.Connection.ExecuteScalar<int>("SELECT COUNT ( DataVenda) FROM RECEITA WHERE IdCliente = 8 and IdEmpresa = 3 and DataVenda BETWEEN DATEADD(MONTH, -3, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new
            { @IdCliente = IdCliente, @IdEmpresa = IdEmpresa });
        }

        //CALCULO DO TICKETMEDIO   REMOVE O  IdCliente
        public decimal ObterTicketMedio(int IdEmpresa)
        {
            return _db.Connection.ExecuteScalar<decimal>("SELECT SUM(r.Valor) / COUNT(r.IdCliente) FROM RECEITA  AS r  WHERE IdEmpresa = @IdEmpresa AND IdCliente = IdCliente AND DataVenda BETWEEN DATEADD(MONTH, -1, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa });
        }

        public decimal ObterTotalVendasMes(int IdEmpresa)
        {
            return _db.Connection.ExecuteScalar<decimal>("SELECT SUM(r.Valor) FROM RECEITA AS r WHERE IdEmpresa = @IdEmpresa AND DataVenda BETWEEN DATEADD(MONTH, 0, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa});
        }

        public decimal ObterReceitaRetidosMes(int IdEmpresa)
        {
            return _db.Connection.ExecuteScalar<decimal>("SELECT SUM(r.Valor) FROM RECEITA AS r CLIENTE AS c WHERE c.TipoCliente = 'Vip' AND IdEmpresa = @IdEmpresa AND DataVenda BETWEEN DATEADD(MONTH, 0, CONVERT(DATE, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa });
        }

        //NÃO FOI TESTADO, LEMBRA DE TESTA ESSE COMANDO
        public decimal ObterReceitaRetidosSemana(int IdEmpresa)  
        {
            return _db.Connection.ExecuteScalar<decimal>("SELECT SUM(r.Valor) FROM RECEITA AS r CLIENTE AS c WHERE c.TipoCliente = 'Vip' AND IdEmpresa = @IdEmpresa AND  DATEADD(WEEK, -1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa });
        }

        public decimal ObterReceitaRetidosDia(int IdEmpresa)
        {
            return _db.Connection.ExecuteScalar<decimal>("SELECT SUM(r.Valor) FROM RECEITA AS r, CLIENTE AS c WHERE c.TipoCliente = 'Vip' AND IdEmpresa = @IdEmpresa AND  DATEADD(Day, 1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa });
        }

        //public IList<ObterDiasPicoVendasConsulta> ObterDiasPicoVendas(int IdEmpresa) Verifica esse comando
        //{
        //    return _db.Connection.QueryFirstOrDefault <List<ObterDiasPicoVendasConsulta>>("SELECT DATEPART(DAY,r.DataVenda) AS Dias_Semana COUNT(*) AS contador FROM RECEITA AS r WHERE IdEmpresa = @IdEmpresa GROUP BY DATEPART(DAY, r.DataVenda) ORDER BY contador", new { @IdEmpresa = IdEmpresa });
        //}

    }
}
