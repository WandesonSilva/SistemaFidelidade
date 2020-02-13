using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class SituacaoRepositorio : ISituacaoRepositorio
    {
        private readonly PontuaAeDataContexto _db;
        public SituacaoRepositorio(PontuaAeDataContexto db)
        {
            _db = db;
        }
        public void atualizarSituacaoSMS(SituacaoSMS model)
        {
            _db.Connection.Execute("UPDATE SITUACAO_SMS SET Contato=@Contato, DataRecebida=@DataRecebida, Estado=@Estado, TotalVendas=@TotalVendas, WHERE IdEmpresa=@IdEmpresa AND IdCampanha=@IdCampanha ",
                new
                {
                    @Contato = model.Contato,
                    @DataRecebida = model.DataRecebida,
                    @Estado = model.Estado,
                    @TotalVendas = model.TotalVendas,
                    @IdEmpresa = model.IdEmpresa,
                    @IdCampanha = model.IdCampanha

                });
        }
   

        public IEnumerable<SituacaoSMS> ListaSituacaoSMS(int IdEmpresa)
        {
            return _db.Connection.Query<SituacaoSMS>("SELECT ID, IdMensagem, Contato, verificado FROM SITUACAO_SMS WHERE IdEmpresa=@IdEmpresa ");
        }

        public SituacaoSMS obterId(int IdEmpresa, int IdCampanha)
        {
            return _db.Connection.QueryFirst<SituacaoSMS>("SELECT ID FROM SITUACAO_SMS WHERE IdEmpresa=@IdEmpresa, IdCampanha=@IdCampanha", 
                new { @IdEmpresa = IdEmpresa, @IdCampanha = IdCampanha});
        }

        public int ObterQtdRetorno(int IdEmpresa, int ID)
        {
            return _db.Connection.ExecuteScalar<int>("SELECT COUNT(DataCompra) WHERE IdEmpresa=@IdEmpresa, ID=@ID", new {@IdEmpresa = IdEmpresa, @ID=ID });
        }

        public void SalvarSituacao(SituacaoSMS model)
        {
            _db.Connection.Execute("INSERT INTO (Contato, DataRecebida, Estado, IdEmpresa, IdCampanha, TotalVendas)VALUES(@Contato, @DataRecebida, @Estado, @IdEmpresa, @IdCampanha, @TotalVendas)");
        }
    }
}
