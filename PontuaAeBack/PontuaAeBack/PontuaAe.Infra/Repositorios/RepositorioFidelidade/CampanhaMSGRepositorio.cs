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
    public class CampanhaMSGRepositorio : ICampanhaMSGRepositorio
    {
        private readonly PontuaAeDataContexto _db;

        public CampanhaMSGRepositorio(PontuaAeDataContexto db)
        {
            _db = db;
        }
         

        public IEnumerable<ObterListaCampanhaSMS> listaCampanha(int IdEmpresa) 
        {
            return _db.Connection.Query<ObterListaCampanhaSMS>("SELECT m.Nome, m.QtdCliente, m.Segmento, m.SegmentoCustomizado, m.canal, r.DataEnviada, m.EstadoEnvio, m.QtdEnviada, COUNT(r.Estado='Sucesse') As QtdRecebida, m.ValorInvestido, COUNT(r.DataCompra) AS QtdRetorno, r.TotalVendas, FROM MENSAGEM m, INNER JOIN SITUACAO_MENSAGEM r ON s.IdEmpresa = r.IdEmpresa  WHERE s.Tipo=@Tipo AND m.IdEmpresa=@IdEmpresa AND m.EstadoEnvio= 'Normal' ");
        }
        public IEnumerable<ObterListaCampanhaSMS> listaCampanhaAgendada(int IdEmpresa)
        {
            return _db.Connection.Query<ObterListaCampanhaSMS>("SELECT m.Nome, m.QtdCliente, m.Segmento, m.SegmentoCustomizado, m.canal, r.DataEnviada, m.EstadoEnvio, m.QtdEnviada, COUNT(r.Estado='Sucesse') As QtdRecebida, m.ValorInvestido, COUNT(r.DataCompra) AS QtdRetorno, r.TotalVendas, FROM MENSAGEM m, INNER JOIN SITUACAO_MENSAGEM r ON s.IdEmpresa = r.IdEmpresa  WHERE s.Tipo=@Tipo AND m.IdEmpresa=@IdEmpresa AND m.EstadoEnvio= 'Agendada' ");
        }

        public IEnumerable<Telefone> ListaTelefones(int IdEmpresa, string SegCustomizado)
        {
            return _db.Connection.Query<Telefone>("SELECT c.Contatos  FROM CLIENTE c INNER JOIN  PONTUACAO p ON c.ID = p.IdCliente  WHERE p.IdEmpresa = @IdEmpresa AND c.SegCustomizado = @SegCustomizado  ", new { @IdEmpresa = IdEmpresa,  @SegCustomizado = SegCustomizado });
        }

        public void AtualizarEstadoCampanha(Mensagem model)
        {
            _db.Connection.Execute("UPDATE MENSAGEM SET EstadoEnvio WHERE  IdEmpresa=@IdEmpresa AND IdCampanha=@IdCampanha  ", new { @IdEmpresa = model.IdEmpresa, @IdCampanha = model.IdCampanha });
        }


        public void Deletar(int IdEmpresa, int ID)
        {
            _db.Connection.Execute("DELETE FROM CAMPANHA WHERE IdEmpresa = @IdEmpresa, AND ID = @ID AND  EstadoEnvio = 'Agendado' ", new { @IdEmpresa = IdEmpresa, @ID = ID });
        }

        public void Editar(Mensagem model)
        {
            //inutilizavel
            throw new NotImplementedException();
        }

        public void Salvar(Mensagem model)
        {
            _db.Connection.Execute("INSERT INTO MENSAGEM (IdEmpresa, IdCampanha, EstadoEnvio, Nome, Segmentacao, QtdSelecionado, DataEnvio, HoraEnvio, QtdEnviada, ValorInvestido)VALUES( @IdEmpresa, @IdCampanha, @EstadoEnvio, @_Nome, @Segmentacao, @QtdSelecionado, @DataEnvio, @HoraEnvio, @QtdEnviada, @ValorInvestido)",
                new
                {
                    @IdEmpresa = model.IdEmpresa,
                    @IdCampanha = model.IdCampanha,
                    @EstadoEnvio = model.EstadoEnvio,
                    @_Nome = model._Nome,
                    @Segmentacao = model.Segmentacao,
                    @QtdSelecionado = model.QtdSelecionado,
                    @DataEnvio =  model.Agendar.DataEnvio,
                    @HoraEnvio = model.Agendar.HoraEnvio,
                    @QtdEnviada = model.QtdEnviada,
                    @ValorInvestido = model.ValorInvestido
                });
        }

        public IList<Mensagem> ListaMensagem()
        {
            return _db.Connection.QueryFirstOrDefault("SELECT IdCampanha, IdEmpresa FROM MENSAGEM");
        }

        

        public void Desativar(int IdEmpresa, int ID, int Desativo)
        {
            //Inutilizavel
            throw new NotImplementedException();
        }
    }
}
