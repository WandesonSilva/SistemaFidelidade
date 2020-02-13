using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.PremiosConsulta;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;


namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class PremioRepositorio : IPremioRepositorio
    {
        private readonly PontuaAeDataContexto _db;

        public PremioRepositorio(PontuaAeDataContexto db)
        {
            _db = db;
        }

        public void Salvar(Premios premios)
        {
            _db.Connection
                .Execute("INSERT INTO PREMIOS ( IdEmpresa, Nome,  Descricao, Quantidade, Imagem, validade, PontosNecessario) values (@IdEmpresa, @Nome,  @Descricao, @Quantidade, @Imagem, @validade, @PontosNecessario)", new
                {
                    @IdEmpresa = premios.IdEmpresa,
                    @Nome = premios.Nome,
                    @Descricao = premios.Descricao.Texto,
                    @Quantidade = premios.Quantidade,
                    @Imagem = premios.Imagem,
                    @validade = premios.Validade,
                    @PontosNecessario = premios.PontosNecessario

                });
        }

        public void Editar(Premios premio)
        {
            _db.Connection.Execute("UPDATE PREMIOS SET  Nome=@Nome, Descricao=@Descricao, Quantidade=@Quantidade, Imagem=@Imagem, validade=@validade, PontosNecessario=@PontosNecessario WHERE  ID=@ID AND IdEmpresa=@IdEmpresa", new
            {
                @Nome = premio.Nome,
                @Descricao = premio.Descricao.Texto,
                @Quantidade = premio.Quantidade,
                @Imagem = premio.Imagem,
                @validade = premio.Validade,
                @PontosNecessario = premio.PontosNecessario,
                @ID = premio.ID,
                @IdEmpresa = premio.IdEmpresa,

            });
        }
        public void Deletar(int IdEmpresa, int ID)
        {
            _db.Connection
            .Execute("DELETE FROM PREMIOS WHERE ID = @ID AND  IdEmpresa=@IdEmpresa", new { @ID = ID, @IdEmpresa = IdEmpresa });

        }


        public ObterDetalhePremioConsulta DetalhePremiacao(int ID, int IdEmpresa)
        {
            return _db.Connection
                 .QueryFirstOrDefault<ObterDetalhePremioConsulta>("SELECT * FROM PREMIOS WHERE ID=@ID AND IdEmpresa=@IdEmpresa  ", new
                 {
                     @IdEmpresa = IdEmpresa,
                     @ID = ID
                 });
        }


        public IEnumerable<ListarPremiosConsulta> listaPremios(int IdEmpresa)
        {
            return _db.Connection
                .Query<ListarPremiosConsulta>("SELECT * FROM PREMIOS WHERE (SELECT ID  FROM EMPRESA WHERE IdEmpresa=@IdEmpresa)", new
                {
                    @IdEmpresa = IdEmpresa
                });
        }

        //public IEnumerable<ListarPremiosConsulta> listaPremios(int IdEmpresa, string Contato)
        //{
        //    return _db.Connection
        //        .Query<ListarPremiosConsulta>("SELECT Id, Nome, QtdPontosNecessario, Saldo,  pontosAtigindo  ", new
        //        {
        //            @IdEmpresa = IdEmpresa
        //        });C:\Users\wande\Source\Workspaces\PontuaAeBack\PontuaAeBack\PontuaAe.Api\Controllers\
        
        //}

        public Premios obterPontosNecessario(int IdEmpresa, int ID)
        {
            return _db.Connection
                  .QueryFirstOrDefault<Premios>("SELECT PontosNecessario FROM PREMIOS   IdEmpresa=@IdEmpresa AND ID=@ID",
                  new
                  {
                      @IdEmpresa = IdEmpresa,
                      @ID = ID,
                  });
        }

        public Premios obterPontosNecessario(int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public IList<Premios> PremiosDisponiveis(decimal Saldo)
        {
            return _db.Connection.QueryFirstOrDefault<List<Premios>>("select p nome, p pontoNecessario from PREMIO p  WHERE p pontoNecessario >= @SaldoCliente AND IdEmpresa=@IdEmpresa");
        }
    }
}
