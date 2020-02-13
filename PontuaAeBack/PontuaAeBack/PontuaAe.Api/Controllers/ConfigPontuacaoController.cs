using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using Microsoft.AspNetCore.Authorization;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Resultados;

namespace PontuaAe.Api.Controllers
{
    public class ConfigPontuacaoController : Controller
    {
        private readonly IConfigPontosRepositorio _repConfigPontos;
        private readonly EmpresaManipulador _manipulador;

        public ConfigPontuacaoController(IConfigPontosRepositorio repConfigPontos, EmpresaManipulador manipulador)
        {

            _repConfigPontos = repConfigPontos;
            _manipulador = manipulador;

        }

        [HttpPut]
        [Route("v1/Config/Editar")]
        [Authorize(Policy ="Admin")]
        public IComandoResultado Edit([FromBody]EditarRegraPontuacaoComando comando)
        {
            var resultado = (ComandoEmpresaResultado)_manipulador.Manipular(comando);
            return resultado;
        }

        [HttpGet]
        [Route("v1/Config/{IdEmpresa}")]
        //[Authorize(Policy ="Admin")]
        [AllowAnonymous]
        public ObterDetalheConfigPontuacao GetRegra(int IdEmpresa)
        {
            return _repConfigPontos.ObterDetalheConfigPontuacao(IdEmpresa);
        }
    }
}
