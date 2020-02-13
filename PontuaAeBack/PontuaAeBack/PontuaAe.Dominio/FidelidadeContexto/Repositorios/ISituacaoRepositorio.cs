using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface ISituacaoRepositorio
    {
        void atualizarSituacaoSMS(SituacaoSMS model);
        void SalvarSituacao(SituacaoSMS model);
        //void EditarStatusVerificado(int IdEmpresa, int IdSMS);
        int ObterQtdRetorno(int IdEmpresa, int ID);
        //SituacaoSMS obterId(int IdEmpresa, int IdCampanha);  remover
        IEnumerable<SituacaoSMS> ListaSituacaoSMS(int IdEmpresa);
    }
}
