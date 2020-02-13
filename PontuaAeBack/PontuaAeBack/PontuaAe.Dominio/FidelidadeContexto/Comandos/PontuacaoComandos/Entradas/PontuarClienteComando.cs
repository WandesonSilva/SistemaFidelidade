using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas
{
    public class PontuarClienteComando: Notifiable, IComando
    {

        public int IdEmpresa { get; set; }
        public int IdUsuario { get; set; }
        public string TipoAtividade { get; set; }

        public string Telefone { get; set; }
        public decimal ValorInfor { get; set; }

        public bool Valida()
        {
            return IsValid;
        }
    }
}
