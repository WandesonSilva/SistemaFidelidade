using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;

namespace PontuaAe.Api.Controllers
{
    //INDICADORES
    public class ReceitaController : Controller
    {

        private readonly IReceitaRepositorio _repReceita;

        public ReceitaController(IReceitaRepositorio repReceita)
        {
            _repReceita = repReceita;
          
        }

        [HttpGet]
        [Route("v1/Receita/Ticket/{id}")]
        [AllowAnonymous]
        public decimal ObterTicketMedio(int IdEmpresa)
        {
            return _repReceita.ObterTicketMedio(IdEmpresa);
        }

        [HttpGet]
        [Route("v1/Receita/VendasMes/{id}")]
        [AllowAnonymous]
        public decimal ObterTotalVendasMes(int IdEmpresa)
        {
            return _repReceita.ObterTotalVendasMes(IdEmpresa);
        }

        [HttpGet]
        [Route("v1/Receita/RetidosMes/{id}")]
        [AllowAnonymous]
        public decimal ObterReceitaRetidosMes(int IdEmpresa)
        {
            return _repReceita.ObterReceitaRetidosMes(IdEmpresa);
        }

        [HttpGet]
        [Route("v1/Receita/RetidosSemana/{id}")]
        [AllowAnonymous]
        public decimal ObterReceitaRetidosSemana(int IdEmpresa)
        {
            return _repReceita.ObterReceitaRetidosSemana(IdEmpresa);
        }

        [HttpGet]
        [Route("v1/Receita/RetidosDia/{id}")]
        [AllowAnonymous]
        public decimal ObterReceitaRetidosDia(int IdEmpresa)
        {
            return _repReceita.ObterReceitaRetidosDia(IdEmpresa);
        }
    }
}