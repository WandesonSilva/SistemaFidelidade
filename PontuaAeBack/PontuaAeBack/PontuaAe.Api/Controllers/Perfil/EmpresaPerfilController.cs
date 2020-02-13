using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;

namespace PontuaAe.Api.Controllers.Perfil
{

    public class EmpresaPerfilController:Controller
    {

        private readonly EmpresaManipulador _manipulador;
        private readonly IEmpresaRepositorio _repEmp;


        public EmpresaPerfilController(EmpresaManipulador manipulador, IEmpresaRepositorio repEmp)
        {
            _manipulador = manipulador;
            _repEmp = repEmp;
        }



        [HttpPost]
        [Route("v1/Empresa/post")]
        [AllowAnonymous]
        public IComandoResultado Post([FromBody] AddDadosEmpresaComando comando)
        {
            var resultado = (ComandoEmpresaResultado)_manipulador.Manipular(comando);
            return resultado;
        }

        [HttpPut]        
        [Route("v1/Empresa/edit")]
        [AllowAnonymous]
        public IComandoResultado PutPefil([FromBody] EditarPerfilEmpresaComando comando)
        {
            var resultado = (ComandoEmpresaResultado)_manipulador.Manipular(comando);
            return resultado;
        }

        [HttpGet]
        [Route("v1/Empresa/{Id}")]
        [Authorize(Policy ="Admin")]
        public ObterDetalheEmpresa DetalheEmpresa(int Id)
        {
            var resultado = _repEmp.ObterDetalheEmpresa(Id);
            return resultado;
        }


        [HttpGet]
        [Route("v1/Empresa/Lista")]
        [Authorize(Policy = "Admin")]
        public List<Empresa> ListaEmpresa()
        {
            var resultado = _repEmp.ListaEmpresa();
            return resultado;
        }


        //Não faz parte das Rodas Empresa
        [HttpGet]
        [Route("V2/Teste")]
        [AllowAnonymous]
        public IActionResult GET()
        {
            return Ok(new { User.Identity.Name });
        } 



        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public string Inicio()
        {

            return $"<h4>ApiPontuaAe {DateTime.Now.ToString()}</h4>";
        }


    }

}

