using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.PremiosConsulta;
using PontuaAe.Compartilhado.Comandos;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System.ComponentModel;

namespace PontuaAe.Api.Controllers
{
    public class PremiacaoController: Controller
    {
        private readonly IPremioRepositorio _repPremio;
        private readonly PremioManipulador  _manipulador;

        public PremiacaoController(IPremioRepositorio repPremio, PremioManipulador manipulador)
        {
            _repPremio = repPremio;
            _manipulador = manipulador;
        }

        [HttpPost]
        [Route("v1/Premios")]
        //[Authorize(Policy = "Admin, Funcionario")]
        [AllowAnonymous]
        public IComandoResultado Post([FromBody]CadastraPremioComando comando)
        {
            var resultado = (ComandoResultado)_manipulador.Manipular(comando);
            return resultado;
        }

        [HttpDelete]
        [Route("v1/Premios/{Id}/{IdEmpresa}")]
        //[Authorize(Policy = "Admin")]
        [AllowAnonymous]
        public IComandoResultado Deletar(int Id, int IdEmpresa)
        {
            DeletarPremioComando delete= new DeletarPremioComando(Id, IdEmpresa);
            var deletar = (ComandoResultado)_manipulador.Manipular(delete);
            return deletar;
        }

        [HttpPut]
        [Route("v1/Premios")]
        [AllowAnonymous]
        public IComandoResultado Update([FromBody] EditarPremioComando comando)
        {
            var Editar = (ComandoResultado)_manipulador.Manipular(comando);
            return Editar;
        }

        [HttpGet]
        [Route("v1/Premios/{idEmpresa}")]
        [AllowAnonymous]
        public IEnumerable<ListarPremiosConsulta> ListPremios(int idEmpresa)
        {
            return _repPremio.listaPremios(idEmpresa);
        }

        [HttpGet]
        [Route("v1/Premios/{idEmpresa}/{Contato}")]
        [AllowAnonymous]
        public IEnumerable<ListarPremiosConsulta> ListaPremios(int idEmpresa, string Contato)
        {
            return _repPremio.listaPremios(idEmpresa);
        }

    }
}
