using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Resultado;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;

namespace PontuaAe.Api.Controllers.Marketing
{
    public class CampanhaController : Controller
    {
        private readonly ICampanhaMSGRepositorio _repCampanha;
        private readonly CampanhaManipulado _manipulador;

        public CampanhaController(CampanhaManipulado manipulador, ICampanhaMSGRepositorio repCampanha)
        {
            _manipulador = manipulador;
            _repCampanha = repCampanha;
        }
        
        [HttpPost]
        [Route("v1/Campanha")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        [AllowAnonymous]
        public IComandoResultado CriarCampanha([FromForm]AddCampanhaComando comando)
        {
            var resultado = (ComandoResultado)_manipulador.Manipular(comando);
            return resultado;
        }


        [HttpPut]
        [Route("v1/Campanha/Editar")]
        //[Authorize(Policy = "Admin, Funcionario")]
        [AllowAnonymous]
        public IComandoResultado EditarCampanhaAgendada([FromBody]EditarCampanhaComando comando)
        {
            var resultado = (ComandoResultado)_manipulador.Manipular(comando);
            return resultado;
        }

        [HttpGet]
        [Route("v1/Campanha/{idEmpresa}")]
        [AllowAnonymous]
        public IEnumerable<ObterListaCampanhaSMS> ListaCampanha(int idEmpresa)
        {
            //o numero 1 representa a campanha Instantânea
            return _repCampanha.listaCampanha(idEmpresa);
        }

        [HttpGet]
        [Route("v1/Campanha/{idEmpresa}")]
        [AllowAnonymous]
        public IEnumerable<ObterListaCampanhaSMS> ListaCampanhaAgendada(int idEmpresa)
        {
            //o numero 2 representa a campanha Agendada
            return _repCampanha.listaCampanha(idEmpresa);
        }

        [HttpDelete]
        [Route("v1/Campanha/{Id}/{IdEmpresa}")]
        //[Authorize(Policy = "Admin")]
        [AllowAnonymous]
        public IComandoResultado DeletarCampanhaAgendada(int Id, int IdEmpresa)
        {
            RemoverCampanhaComando delete = new RemoverCampanhaComando(Id, IdEmpresa);
            var deletar = (ComandoResultado)_manipulador.Manipular(delete);
            return deletar;
        }

    }
}