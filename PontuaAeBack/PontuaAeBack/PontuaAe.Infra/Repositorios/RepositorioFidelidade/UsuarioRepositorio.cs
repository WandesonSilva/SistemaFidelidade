using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;

using System;
using System.Linq;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly PontuaAeDataContexto _db;

        public UsuarioRepositorio(PontuaAeDataContexto db)
        {
            _db = db;
        }

        public bool Autenticar(string Email, string Senha)
        {
            return _db.Connection
                .QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS( SELECT ID FROM USUARIO WHERE Email = @Email AND Senha = @Senha) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new
                {
                    @Email = Email,
                    @Senha = Senha
                });

        }

        public void Deletar(int ID)
        {
            _db.Connection.Execute("DELETE FROM USUARIO WHERE ID=@ID", new {@ID = ID });
        }

        public void Desativar(int ID)
        {
            _db.Connection.Execute("UPDATE USUARIO SET Estado = 0 WHERE ID=@ID", new { @ID = ID });
        }

        public void Editar(Usuario usuario)
        {
            _db.Connection
                .Execute("UPDATE USUARIO SET Email = @Email, Senha = @Senha WHERE ID=@ID", new
                {
                    @Email = usuario.Email,
                    @Senha = usuario.Senha,
                    @ID = usuario.ID
                } );
        }

        public Usuario OterUsuario(string Email)
        {
            return _db.Connection
                .QueryFirstOrDefault<Usuario>("SELECT ID, ClaimType, ClaimValue FROM USUARIO WHERE  Email = @Email", new { @Email = Email });

        }

       
        public void Salvar(Usuario usuario)
        {
             _db.Connection
                .Execute("INSERT INTO USUARIO ( NomeCompleto, Email, Senha, ClaimType, ClaimValue, Estado) VALUES ( @Nome, @Email, @Senha, @ClaimType, @ClaimValue, @Estado)", new
                {
                    @Nome = usuario.Nome,
                    @Email = usuario.Email,
                    @Senha = usuario.Senha,                    
                    @ClaimType = usuario.ClaimType,
                    @ClaimValue =usuario.ClaimValue,
                    @Estado = usuario.Estado,
               
                });
                
        }
        
        public bool ValidaCPF(string NumeroCpf)
        {
            return _db.Connection
                .QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS( SELECT ID FROM USUARIO WHERE NumeroCpf = @NumeroCpf) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @NumeroCpf = NumeroCpf }); 
            
        }


        public bool ValidaEmail(string Email)
        {
            return _db.Connection
                 .QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS( SELECT ID FROM USUARIO WHERE Email = @Email) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Email = Email });

        }
    }
}
