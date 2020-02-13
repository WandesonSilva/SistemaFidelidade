using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Entidades.ObjetoValor;
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
    public class AutomacaoSMSReposiorio : IAutomacaoMSGRepositorio
    {
        private readonly PontuaAeDataContexto _db;

        public AutomacaoSMSReposiorio(PontuaAeDataContexto db)
        {
            _db = db;
        }
        public void Deletar(int IdEmpresa, int Id)
        {
            _db.Connection.Execute("DELETE FROM SMS WHERE IdEmpresa=@IdEmpresa AND ID=@ID", new { IdEmpresa = IdEmpresa, @ID = Id });
        }

        public void Desativar(int IdEmpresa, int Id, int Estado)
        {
            _db.Connection.Execute("UPDATE SMS SET Estado=@Estado WHERE IdEmpresa=@IdEmpresa AND ID=@ID", new { @IdEmpresa = IdEmpresa, @ID = Id, @Estado = Estado });
        }

        public void Editar(Mensagem model)
        {
            string query = "UPDATE SMS SET TipoAutomacao=@TipoAutomacao, DiaSemana=@DiaSemana, DiasAntesAniversario=@DiasAntesAniversario, DiaMes=@DiaMes, TipoCanal=@TipoCanal, Conteudo=@Conteudo, Estado=@Estado, Segmentacao=@Segmentacao, SegCustomizado=@SegCustomizado, QtdEnviado=@QtdEnviado, ValorInvestido=@ValorInvestido  WHERE IdEmpresa=@IdEmpresa AND ID=@ID";
            _db.Connection.Execute(query, model);
        }

        public void Salvar(Mensagem model)
        {
            string query = "INSERT INTO SMS (IdEmpresa, TipoAutomacao, DiaSemana, DiasAntesAniversario, DiaMes, TipoCanal, Conteudo, SegCustomizado, Segmentacao, Estado, QtdEnviado, ValorInvestido )VALUES(@IdEmpresa, @TipoAutomacao, @DiaSemana, @DiasAntesAniversario, @DiaMes, @TipoCanal, @Conteudo, @SegCustomizado, @Segmentacao, @Estado, @QtdEnviado, @ValorInvestido)";
            _db.Connection.Execute(query, model);
        }

        public IEnumerable<ObterAutomacaoTipoAniversario> ObterDadosAutomacaoAniversario(string AutomacaoAniversariante, string Segmentacao, string SegCustomizado, int IdEmpresa) //MELHORA ESSE METODO,  USA APENA O ID DA AUTOMAÇÃO GERADA E IDEMPRESA
        {
            return _db.Connection.QueryFirstOrDefault("SELECT a.ID, a.TipoAutomacao, a.Conteudo c.Segmentacao, a.DiasAntesAniversario, c.SegCustomizado c.DataNascimento, e.NomeFantasia FROM SMS a, CLIENTE c JOIN PONTUACAO p ON c.ID = p.IdCliente  WHERE a.IdEmpresa = @IdEmpresa AND  a.TipoAutomacao=@AutomacaoAniversariante AND Segmentacao=@Segmentacao OR SegCustomizado=@SegCustomizado", new { @AutomacaoAniversariante = AutomacaoAniversariante, @Segmentacao = Segmentacao, @SegCustomizado = SegCustomizado, @IdEmpresa = IdEmpresa }); //WHERE  Estado= true 1
        }

        public IEnumerable<ObterAutomacaoTipoDiaSemana> ObterDadosAutomacaoSemana(string AutomacaoSemana, string Segmentacao, string SegCustomizado, int IdEmpresa) //MELHORA ESSE METODO,  USA APENA O ID DA AUTOMAÇÃO GERADA E IDEMPRESA
        {
            return _db.Connection.QueryFirstOrDefault("SELECT a.ID, a.TipoAutomacao, a.Conteudo c.Segmentacao, c.DiaSemana, DATENAME(WEEKDAY, GETDATE()) AS Dia, c.SegCustomizado, e.NomeFantasia FROM SMS a, CLIENTE c JOIN PONTUACAO p ON c.ID = p.IdCliente WHERE a.IdEmpresa = @IdEmpresa AND a.TipoAutomacao=@AutomacaoSemana", new { @AutomacaoSemana = AutomacaoSemana, @Segmentacao = Segmentacao, @SegCustomizado = SegCustomizado, @IdEmpresa = IdEmpresa });
        }

        public IEnumerable<ObterAutomacaoTipoDiaMes> ObterDadosAutomacaoMes(string AutomacaoMes, string Segmentacao, string SegCustomizado, int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mensagem> ListaTipoAutomacao()
        {
            return _db.Connection.QueryFirstOrDefault("SELECT IdEmpresa, Segmentacao, SegCustomizado FROM SMS");
        }

        public void atualizarSituacaoSMS(SituacaoSMS model)
        {
            _db.Connection
                .Execute("UPDATE SITUACAOSMS SET Estado=@Estado, DataRecebida=@DataRecebida, Contato=@Contato WHERE IdEmpresa=@IdEmpresa AND IdCampanha=@IdCampanha", new
                {
                    @Estado = model.Estado,
                    @DataRecebida = model.DataRecebida,
                    @Contato = model.Contato,
                    @IdEmpresa = model.IdEmpresa,
                    @IdCampanha = model.IdCampanha

                });
        }

        public IList<Mensagem> ListaMensagem()
        {
            return _db.Connection.QueryFirstOrDefault("SELECT IdCampanha, IdEmpresa FROM MENSAGEM");
        }

        public void AtualizarEstadoCampanha(Mensagem model)
        {
            _db.Connection.Execute("UPDATE MENSAGEM SET EstadoEnvio=@EstadoEnvio WHERE IdEmpresa=@IdEmpresa AND IdCampanha=@IdCampanha", new
            {
                @EstadoEnvio = model.EstadoEnvio,
                @IdEmpresa = model.IdEmpresa,
                @IdCampanha = model.IdCampanha

            });
        }

        public IEnumerable<ObterListaAutomacao> listaAutomacao(int IdEmpresa)
        {
            return _db.Connection.Query<ObterListaAutomacao>("SELECT m.ID m.Segmento, m.SegmentoCustomizado, m.canal, m.Estado, m.QtdEnviada, COUNT(r.Estado='Sucesse') As QtdRecebida m.ValorInvestido, COUNT(r.DataCompra) AS QtdRetorno, r.TotalVendas, FROM MENSAGEM m, INNER JOIN SITUACAO_MENSAGEM r ON s.IdEmpresa = r.IdEmpresa  WHERE m.EstadoEnvio='Automatico' AND m.IdEmpresa=@IdEmpresa ",new { @IdEmpresa = IdEmpresa });
        }

        public string[,] ListaTelefones(int IdEmpresa, string SegCustomizado, string Segmentacao)
        {
            return _db.Connection.QueryFirstOrDefault<string[,]>("SELECT c.Contato  FROM CLIENTE c INNER JOIN  PONTUACAO p ON c.ID = p.IdCliente  WHERE p.IdEmpresa = @IdEmpresa AND  c.Segmentacao = Segmentacao  OR c.SegCustomizado = @SegCustomizado  ", new { @IdEmpresa = IdEmpresa, @Segmentacao = Segmentacao, @SegCustomizado = SegCustomizado});
        }
    }
}