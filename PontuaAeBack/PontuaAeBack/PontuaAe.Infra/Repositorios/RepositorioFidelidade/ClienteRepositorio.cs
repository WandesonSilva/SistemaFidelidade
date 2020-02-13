using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConsultaCliente;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.RelatoriosConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.UsuarioConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PontuaAe.Infra.Repositorios.RepositorioAvaliacao
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly PontuaAeDataContexto _db;

        public ClienteRepositorio(PontuaAeDataContexto db)
        {
            _db = db;
        }

        //public void Atualizar(Cliente cliente, int IdEmpresa)
        //{
        //    _db.Connection
        //       .Execute("UPDATE CLIENTE SET Nome=@Nome, DataNascimeto=@DataNascimento, Cpf=@Cpf, Sexo=@Sexo, Email=@Email, Telefone=@Telefone WHERE IdEmpresa=@IdEmpresa", new
        //       {
        //           @Nome = cliente.Nome,
        //           @DataNascimento = cliente.DataNascimento,
        //           @Cpf = cliente.Cpf.Cpf,
        //           @Sexo = cliente.Sexo,
        //           @Email = cliente.Email.Endereco,
        //           @Telefone = cliente.Telefone,
        //           IdEmpresa = cliente.IdEmpresa
        //       });
        //}

        public bool ChecarDadosCliente(string Telefone, int IdEmpresa)// Esta Queri vai muda
        {
            return _db.Connection
                 .Query<bool>("SELECT CASE WHEN EXISTS( SELECT ID FROM CLIENTE WHERE Telefone = @Telefone, IdEmpresa=@IdEmpresa ) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Telefone = Telefone, @IdEmpresa = IdEmpresa }).FirstOrDefault();
        }

        public void Editar(Cliente cliente)
        {
            _db.Connection
                .Execute("UPDATE CLIENTE SET Nome=@Nome,Email=@Email,  Telefone=@Telefone  WHERE ID=@ID", new
                {

                    @Nome = cliente.Nome,
                    @Email = cliente.Email.Endereco,
                    @Telefone = cliente.Telefone,
                    @ID = cliente.ID,
                });
        }

        //public IList<ListaClienteConsulta> ListaCliente(int IdEmpresa)
        //{
        //    return _db.Connection
        //        .Query<ListaClienteConsulta>("SELECT * FROM CLIENTE WHERE IdEmpresa=@IdEmpresa", new { @IdEmpresa = IdEmpresa }).ToList();
        //}

        public void Salvar(Cliente cliente)
        {
            _db.Connection
                .Execute("INSERT INTO CLIENTE ( IdUsuario, Nome, DataNascimeto, Telefone, Email, Cpf) values (@IdUsuario ,@Nome,@DataNascimento, @Telefone, @Email, @Cpf)", new
                {
                    @IdUsuario = cliente.IdUsuario,
                    @Nome = cliente.Nome,
                    @DataNascimento = cliente.DataNascimento,
                    @Telefone = cliente.Telefone,
                    @Email = cliente.Email.Endereco

                }); ;
        }

        public void SalvarPreCadastro(Cliente cliente)
        {
            _db.Connection
                .Execute("Insert into CLIENTE (Telefone) values (@Telefone)", new
                {
                    @Telefone = cliente.Telefone
                });
        }

        public Cliente ObterDadosEmCliente(string Telefone, int IdEmpresa)
        {
            return _db.Connection.QueryFirstOrDefault<Cliente>("SELECT  Nome, DataNascimeto, Sexo  Telefone, Email, Cpf, EstatusFidelidade, TipoCliente FROM CLIENTE WHERE Telefone=@Telefone AND IdEmpresa = @IdEmpresa", new { @Telefone = Telefone, @IdEmpresa = IdEmpresa });
        }

        public void Deletar(int IdPontuacao)
        {
            _db.Connection.Execute("DELETE FROM CLIENTE WHERE IdPontuacao=@IdPontuacao", new { @IdPontuacao = IdPontuacao });

        }

        public int ObterIDCliente(string Telefone)
        {
            return _db.Connection.QueryFirst<int>("SELECT ID FROM CLIENTE WHERE Telefone=@Telefone ", new { @Telefone = Telefone });
        }

        public IEnumerable<Cliente> ObterClassificacaoCliente()
        {
            return _db.Connection.Query<Cliente>("SELECT C.ID, C.IdEmpresa, C.EstadoFidelidade, C.TipoCliente, P.DataVisita FROM CLIENTE C, PONTUACAO P WHERE C.ID = P.IdCliente");
        }

        public void EditarClassificacaoCliente(Cliente cliente)
        {
            _db.Connection
               .Execute("UPDATE Cliente SET EstadoFidelidade=@EstadoFidelidade, TipoCliente=@TipoCliente WHERE ID=@ID",
               new
               {
                   @EstadoFidelidade = cliente.EstadoFidelidade,
                   @TipoCliente = cliente.TipoCliente,
                   @ID = cliente.ID

               });
        }

        public ObterSaldoClienteConsulta ObterSaldo(int IdCliente, int IdEmpresa)
        {
            return _db.Connection.QueryFirstOrDefault<ObterSaldoClienteConsulta>("SELECT p.IdCliente, p.IdEmpresa, p.Saldo, e.NomeFantasia FROM PONTUACAO AS p, EMPRESA AS e  WHERE  p.IdCliente = @IdCliente AND p.IdEmpresa=@IdEmpresa", new
            {
                @IdEmpresa = IdEmpresa,
                @IdCliente = IdCliente
            });
        }
        public List<ObterSaldoClienteConsulta> ListaDeSaldoPorEmpresa(int IdCliente)
        {
            return _db.Connection.Query<ObterSaldoClienteConsulta>("SELECT p.IdCliente, p.IdEmpresa, p.Saldo, e.NomeFantasia FROM PONTUACAO as p, EMPRESA AS e WHERE  p.IdCliente = @IdCliente AND e.ID= p.IdEmpresa", new
            {
                @IdCliente = IdCliente
            }).ToList();
        }
        public IList<ListaRankingClientesConsulta> ListaRankingClientes(int IdEmpresa)
        {
            return _db.Connection.Query<ListaRankingClientesConsulta>("SELECT Nome, Telefone FROM CLIENTE, PONTUACAO where IdEmpresa =@IdEmpresa", new { @IdEmpresa = IdEmpresa }).ToList();

        }

        public int ObterTotalClientesRetidos(int IdEmpresa)
        {
            return _db.Connection.ExecuteScalar<int>("SELECT COUNT(Telefone) FROM PONTUACAO, CLIENTE  WHERE IdEmpresa =@IdEmpresa AND TipoCliente = 'Vip' and CLIENTE.ID = PONTUACAO.IdCliente", new { @IdEmpresa = IdEmpresa });
        }

        public int ObterTotalClientes(int IdEmpresa)
        {
            return _db.Connection.ExecuteScalar<int>("SELECT COUNT(Telefone) FROM PONTUACAO, CLIENTE  WHERE PONTUACAO.IdEmpresa = @IdEmpresa and CLIENTE.ID = PONTUACAO.IdCliente", new { @IdEmpresa = IdEmpresa });
        }

        public IEnumerable<ObterHistóricoClientesConsulta> ObterHistoricoTodosClientes(int IdEmpresa)
        {
            return _db.Connection.Query<ObterHistóricoClientesConsulta>("SELECT u.NomeCompleto, p.DataVisita, r.TipoAtividade, p.Saldo, r.Valor c.Segmentacao, c.TipoCliente FROM RECEITA  AS r INNER JOIN PONTUACAO AS p ON  r.IdEmpresa = p.IdEmpresa INNER JOIN CLIENTE AS c ON r.IdEmpresa = c.IdEmpresa INNER JOIN USUARIO AS u ON r.IdUsuario = u.ID  WHERE IdEmpresa = @IdEmpresa", new { @IdEmpresa=IdEmpresa});
        }


        public List<Cliente> ObterDadosClientes(int IdEmpresa, string Segmento, string SegCustomizado)
        {
            return _db.Connection.QueryFirstOrDefault<List<Cliente>>("SELECT DataNascimento, Telefone FROM CLIENTE WHERE IdEmpresa = @IdEmpresa AND month(GETDATE()", new { @IdEmpresa = IdEmpresa });
        }

        public List<string> ObterListaContatos(int IdEmpresa, string Segmento, string SegCustomizado)
        {
            return _db.Connection.QueryFirstOrDefault("SELECT Telefone FROM CLIENTE WHERE IdEmpresa=@IdEmpresa", new { IdEmpresa= IdEmpresa });
        }

        bool IClienteRepositorio.ChecarCliente(string Telefone)
        {
            return _db.Connection.QueryFirst<bool>("SELECT CASE WHEN EXISTS( SELECT ID FROM CLIENTE WHERE Telefone = @Telefone ) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { Telefone = @Telefone });
        }

        public ObterIDConsulta ObterID(int IdUsuario)
        {
            return _db.Connection.QueryFirst<ObterIDConsulta>("Select ID from Cliente where IdUsuario = @IdUsuario", new { @IdUsuario = IdUsuario });
        }

        public  ClienteConsulta BuscarCliente(int id)
        {
            return _db.Connection.QueryFirst<ClienteConsulta>("Select * from CLIENTE where ID= @id", new { @id = id });
        }


    }
}
