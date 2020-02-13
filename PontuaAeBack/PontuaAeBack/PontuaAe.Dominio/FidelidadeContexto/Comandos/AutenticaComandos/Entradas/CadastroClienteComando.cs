using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas
{
    public class CadastroClienteComando : Notifiable, IComando
    {
      
        public int IdUsuario { get;  set; }         
        public string Nome { get;  set; }         
        public DateTime? DataNascimento { get;  set; }
        public string Sexo { get;  set; }
        public string Email { get;  set; }
        public string Telefone { get;  set; }
        public string Senha { get; set; }
        public string ClaimType => "PontuaAe";
        public string ClaimValue => "Cliente";
        public bool Valida()
        {
            throw new NotImplementedException();
        }



        //public bool Valida()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
