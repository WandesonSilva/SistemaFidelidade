using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Entidades.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Entradas
{
    public class AddCampanhaComando: IComando
    {

        public string Nome { get; set; }
        public int IdEmpresa { get; set; }
        public string[] Contatos { get; set; } 
        public string Segmentacao { get; set; }
        public int QtdSelecionado { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataEnvio { get; set; } //usa tor string para configura o formato da tada
        public TimeSpan HoraEnvio { get; set; } //ingual ao de cima
        public int AgendamentoAtivo { get; set; }

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
