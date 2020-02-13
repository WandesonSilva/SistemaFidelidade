using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas
{
    public class AddContaFuncionario : IComando
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ClaimType => "PontuaAe";
        public string ClaimValue => "Funcionario";

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
