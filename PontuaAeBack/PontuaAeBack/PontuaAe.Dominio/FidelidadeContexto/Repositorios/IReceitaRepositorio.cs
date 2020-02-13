using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IReceitaRepositorio
    {
        void Salvar(Receita receita);
        int QtdVisitasTrintaDias(int IdEmpresa, int IdCliente);
        int QtdVisitasSessentaDias(int IdEmpresa, int IdCliente);
        int QtdVisitasNoventaDias(int IdEmpresa, int IdCliente);

        //Consultas Dashboard
        decimal ObterTicketMedio(int IdEmpresa);
        decimal ObterTotalVendasMes(int IdEmpresa);
        decimal ObterReceitaRetidosMes(int IdEmpresa);
        decimal ObterReceitaRetidosSemana(int IdEmpresa);
        decimal ObterReceitaRetidosDia(int IdEmpresa);





    }
}
