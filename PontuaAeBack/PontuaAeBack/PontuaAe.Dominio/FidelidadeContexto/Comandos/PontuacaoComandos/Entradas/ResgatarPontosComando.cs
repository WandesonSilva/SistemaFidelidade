using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas
{
    public class ResgatarPontosComando: Notifiable, IComando
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Telefone { get; set; }
        public decimal PontosNecessario { get; set; }

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }

}

