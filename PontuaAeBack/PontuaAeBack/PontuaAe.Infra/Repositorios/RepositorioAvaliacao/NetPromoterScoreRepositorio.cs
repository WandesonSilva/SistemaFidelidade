using Dapper;
using PontuaAe.Dominio.Pesquisa.Entidades;
using PontuaAe.Dominio.Questionario.Repositorios;
using PontuaAe.Infra.Avaliacao.DataContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioAvaliacao
{
    public class NetPromoterScoreRepositorio : INetPromoterScoreRepositorio
    {
        private readonly PontuaAeDataContextoAvalicao _db;

        public NetPromoterScoreRepositorio(PontuaAeDataContextoAvalicao db)
        {
            _db = db;
        }

        public int obterNPS(int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public int obterNumeroDetratores(int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public int obterNumeroPromotores(int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public int obterNumeroTotalNeutro(int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public int obterNumeroTotalRespondentes(int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public void salva(NetPromoterScore nps)
        {
            _db.Connection.
                Execute("INSERT INTO PESQUISA (Nota, IdCliente, IdEmpresa, Data)", new
                {
                    @Nota = nps.Nota,
                    @IdCliente = nps.IdCliente,
                    @IdEmpresa = nps.IdEmpresa,
                    @Data = nps.Data
                });

        }

        public void salvaAvaliacaoNps(NetPromoterScore avaliacao)
        {
            throw new NotImplementedException();
        }
    }
}
