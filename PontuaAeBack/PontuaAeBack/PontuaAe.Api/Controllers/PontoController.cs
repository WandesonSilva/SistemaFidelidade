using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Resultados;

namespace PontuaAe.Api.Controllers
{
    public class PontoController : Controller
    {
        private readonly PontuacaoManipulador _manipulador;


        public PontoController(PontuacaoManipulador manipulador)
        {
            _manipulador = manipulador;
        }

        [HttpPost]
        [Route("v1/Pontos")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        [AllowAnonymous]
        public IComandoResultado Post([FromForm]PontuarClienteComando comando)
        {
            var resultado = (ComandoResultado)_manipulador.Manipular(comando);
            return resultado;
        }

        [HttpPut]
        [Route("v1/Pontos")]
        //[Authorize(Policy = "Admin, Funcionario")]
        [AllowAnonymous]
        public IComandoResultado ResgatarPontos([FromBody]ResgatarPontosComando comando)
        {
            var resultado = (ComandoResultado)_manipulador.Manipular(comando);
            return resultado;
        }

 

    }
}