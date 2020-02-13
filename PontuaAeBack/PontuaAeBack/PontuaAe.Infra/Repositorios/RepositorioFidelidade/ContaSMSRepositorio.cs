using Dapper;
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
    public class ContaSMSRepositorio : IContaSMSRepositorio
    {
        private readonly PontuaAeDataContexto _db;

        public ContaSMSRepositorio(PontuaAeDataContexto db)
        {
            _db = db;
        }

        public void Editar(ContaSMS model)
        {
            string query = "UPDATE CONTA_SMS SET Saldo=@Saldo WHERE IdEmpresa=@IdEmpresa ";
            _db.Connection.Execute(query, model);
        }

        public void Salvar(ContaSMS model)
        {
            string query = "INSERT INTO CONTA_SMS (saldos) VALUES(@Saldo)";
            _db.Connection.Execute(query, new {@Saldo = model.Saldo });
        }
    }
}
