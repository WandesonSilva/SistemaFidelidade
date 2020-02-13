using PontuaAe.Dominio.FidelidadeContexto.Consulta.PremiosConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IPremioRepositorio
    {
        void Salvar(Premios premio);
        void Editar(Premios premio);
        void Deletar(int ID, int IdEmpresa);
        IEnumerable<ListarPremiosConsulta> listaPremios(int IdEmpresa);
        IList<Premios> PremiosDisponiveis(decimal Saldo);
        Premios obterPontosNecessario(int IdEmpresa);
        ObterDetalhePremioConsulta DetalhePremiacao(int Id, int IdEmpresa);
        
    }
}
