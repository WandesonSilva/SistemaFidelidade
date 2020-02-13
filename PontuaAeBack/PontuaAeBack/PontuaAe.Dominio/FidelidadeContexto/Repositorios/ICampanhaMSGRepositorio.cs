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
    public interface ICampanhaMSGRepositorio : IMensagem<Mensagem>
    {
        IEnumerable<ObterListaCampanhaSMS> listaCampanha(int IdEmpresa);
        IEnumerable<ObterListaCampanhaSMS> listaCampanhaAgendada(int IdEmpresa);
        IEnumerable<Telefone> ListaTelefones(int IdEmpresa, string SegCustomizado);  //filtro lista de telefones
        void AtualizarEstadoCampanha(Mensagem model);  // editar o atributo EstadoEnvio
        IList<Mensagem> ListaMensagem();// usar no metodo automatico
       

    }
}
