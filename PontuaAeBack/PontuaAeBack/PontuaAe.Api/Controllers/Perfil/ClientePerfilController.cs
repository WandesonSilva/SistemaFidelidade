using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConsultaCliente;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.RelatoriosConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.Repositorios;
using PontuaAe.Infra.Repositorios.RepositorioAvaliacao;
using PontuaAe.Infra.Repositorios.RepositorioFidelidade;
using System.Collections.Generic;

namespace PontuaAe.Api.Controllers.Perfil
{
    public class ClientePerfilController : Controller
    {
        private readonly ClienteManipulador _manipulador;
        private readonly IClienteRepositorio _repCliente;

        public ClientePerfilController(ClienteManipulador manipulador, IClienteRepositorio repCliente)
        {
            _manipulador = manipulador;
            _repCliente = repCliente;
        }

        [HttpPost]
        [Route("v1/Cliente/PreCadastro")]
        [AllowAnonymous]
        public IComandoResultado Post([FromBody] PreCadastroClienteComando comando)
        {
            var resultado = (ComandoClienteResultado)_manipulador.Manipular(comando);

            return resultado;
        }

        [HttpPut]
        [Route("v1/Cliente/put")]
        [AllowAnonymous]
        public IComandoResultado Put([FromBody] EditarClienteComando comando)
        {
            var cliente = (ComandoClienteResultado)_manipulador.Manipular(comando);
            return cliente;
        }

        [HttpGet]
        [Route("v1/Cliente/ListaRanking/{IdEmpresa}")]
        [Authorize(Policy = "Admin")]
        [Authorize(Policy = "Funcionario")]
        public IList<ListaRankingClientesConsulta> ListClientesRanking(int IdEmpresa)
        {
            return _repCliente.ListaRankingClientes(IdEmpresa);
        }

        [HttpGet]
        [Route("v1/Cliente/ListaSaldo/{IdCliente}")]
        [AllowAnonymous]
        public List<ObterSaldoClienteConsulta> ObterSaldoCliente(int IdCliente)
        {
            return _repCliente.ListaDeSaldoPorEmpresa(IdCliente);
        }


        [HttpGet]
        [Route("v1/Cliente/TotalClientes/{IdEmpresa}")]
        [AllowAnonymous]
        public int ObterTotalClientes(int IdEmpresa)
        {
            var a =_repCliente.ObterTotalClientes(IdEmpresa);
            return a;
        }


        [HttpGet]
        [Route("v1/Cliente/totalRetidos/{IdEmpresa}")]
        [AllowAnonymous]
        public int ObterTotalClientesRetidos(int IdEmpresa)
        {
            return _repCliente.ObterTotalClientesRetidos(IdEmpresa);
        }

        [HttpGet]
        [Route("v1/Cliente/{id}")]
        [AllowAnonymous]
        public ClienteConsulta BuscarCliente(int id)
        {
            return _repCliente.BuscarCliente(id);
        }






    }
}
