using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Entidades.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IAutomacaoMSGRepositorio : IMensagem<Mensagem>
    {

     IEnumerable<ObterAutomacaoTipoAniversario> ObterDadosAutomacaoAniversario(string AutomacaoAniversariante, string Segmentacao, string SegCustomizado, int IdEmpresa);
     IEnumerable<ObterAutomacaoTipoDiaSemana> ObterDadosAutomacaoSemana(string AutomacaoSemana, string Segmentacao, string SegCustomizado, int IdEmpresa);
     IEnumerable<ObterAutomacaoTipoDiaMes> ObterDadosAutomacaoMes(string AutomacaoMes, string Segmentacao, string SegCustomizado, int IdEmpresa);
     IEnumerable<ObterListaAutomacao> listaAutomacao(int IdEmpresa);
     IEnumerable<Mensagem> ListaTipoAutomacao();
     string[,] ListaTelefones(int IdEmpresa, string SegCustomizado, string Segmentacao);
     void AtualizarEstadoCampanha(Mensagem model);
     IList<Mensagem> ListaMensagem();
    

    }
}
