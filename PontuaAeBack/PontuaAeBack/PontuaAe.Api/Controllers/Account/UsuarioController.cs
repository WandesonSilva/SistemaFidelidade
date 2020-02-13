using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Resultados;

namespace PontuaAe.Api.Controllers.Account
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioManipulador _manipulador;

        public UsuarioController(UsuarioManipulador manipulador)
        {
            _manipulador = manipulador;

            //dfs
        }

        [HttpPost]        
        [Route("v1/NovoCliente")]
        [Authorize(Policy = "Cliente")]
        public IComandoResultado CreateCliente([FromBody] AddContaCliente comando)
        {
       
            var resultado = (ComandoUsuarioResultado)_manipulador.Manipular(comando);
            return resultado;

        }


        [HttpPost]
        [Route("v1/NovoFuncionario")]
        [Authorize(Policy = "Funcionario")]
        public IComandoResultado CreateFuncionario([FromBody] AddContaFuncionario comando)
        {

            var resultado = (ComandoUsuarioResultado)_manipulador.Manipular(comando);
            return resultado;

        }


        [HttpPut]
        [Route("v1/usuario/edit")]
        [AllowAnonymous]
        public IComandoResultado PutPefil([FromBody] EditarUsuarioComando comando)
        {
            var resultado = (ComandoUsuarioResultado)_manipulador.Manipular(comando);
            return resultado;
        }





    }
}

