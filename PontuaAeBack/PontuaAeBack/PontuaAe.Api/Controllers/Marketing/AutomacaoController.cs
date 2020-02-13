using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Resultado;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;

namespace PontuaAe.Api.Controllers.Marketing
{
    public class AutomacaoController : Controller
    {
        private readonly IAutomacaoMSGRepositorio _repAutomacao;
        private readonly AutomacaoManipulador _manipulador;


        public AutomacaoController(AutomacaoManipulador manipulador, IAutomacaoMSGRepositorio repAutomacao)
        {
            _manipulador = manipulador;
            _repAutomacao = repAutomacao;
        }

        [HttpPost]
        [Route("v1/Campanha")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        [AllowAnonymous]
        public IComandoResultado Post([FromForm]AddAutomacaoComando comando)
        {
            var resultado = (ComandoResultado)_manipulador.Manipular(comando);
            return resultado;
        }


        [HttpPut]
        [Route("v1/Campanha/Editar")]
        //[Authorize(Policy = "Admin, Funcionario")]
        [AllowAnonymous]
        public IComandoResultado Put([FromBody]EditarAutomacaoComando comando)
        {
            var resultado = (ComandoResultado)_manipulador.Manipular(comando);
            return resultado;
        }

        [HttpGet]
        [Route("v1/Campanha/{idEmpresa}")]
        [AllowAnonymous]
        public IEnumerable<ObterListaAutomacao> ListaCampanha(int idEmpresa)
        {
            // 3 representa campanha por automacao
            return _repAutomacao.listaAutomacao(idEmpresa);
        }

        [HttpDelete]
        [Route("v1/Campanha/{Id}/{IdEmpresa}")]
        //[Authorize(Policy = "Admin")]
        [AllowAnonymous]
        public IComandoResultado Deletar(int Id, int IdEmpresa)
        {
            RemoverAutomacaoComando delete = new RemoverAutomacaoComando(Id, IdEmpresa);
            var deletar = (ComandoResultado)_manipulador.Manipular(delete);
            return deletar;
        }

    }
}