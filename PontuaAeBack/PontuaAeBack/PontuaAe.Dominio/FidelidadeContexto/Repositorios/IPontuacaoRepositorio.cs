using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IPontuacaoRepositorio
    {
        
        void resgatar(Pontuacao resgatar);        
        //bool ChecarCelular(int IdEmpresa, string Telefone);        
        void AtualizarSaldo(Pontuacao update);
        void CriarPontuacao(Pontuacao pontuacao);
        Pontuacao ObterEstadoFidelidade(int IdCliente, int IdEmpresa);
        ObterIdEmpresaConsulta ChecarCampoIdEmpresa(int IdEmpresa);
        Pontuacao ObterUltimaVisita(int IdEmpresa, int IdCliente);
        decimal obterSaldo(int IdEmpresa, int IdCliente);


        //Não sei qual foi a interção de criar metodos  listas de pontuação, depois voltarei para verifica
        IEnumerable<Pontuacao> ListaPontuacao(int IdEmpresa);
        IEnumerable<Pontuacao> ListaPontuacaoPorData(int IdEmpresa, DateTime Date);
   


        

    }
}
