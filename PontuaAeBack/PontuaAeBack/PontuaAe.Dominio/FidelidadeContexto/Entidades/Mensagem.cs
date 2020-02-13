using PontuaAe.Dominio.FidelidadeContexto.Entidades.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Mensagem
    {
        public Mensagem()
        {

        }
       
        //Alteração da campanha automatica   
        public Mensagem(
            int id, 
            int idEmpresa,
            string tipoAutomacao,
            string diaSemana,
            int diasAntesAniversario,
            int diaMes,
            string tipoCanal,
            string conteudo,
            string segmentacao,
            string segCustomizado)
        {
            ID = id;
            IdEmpresa = idEmpresa;
            TipoAutomacao = tipoAutomacao;
            DiaSemana = diaSemana;
            DiasAntesAniversario = diasAntesAniversario;
            DiaMes = diaMes;
            TipoCanal = tipoCanal;
            Conteudo = conteudo;
            SegCustomizado = segCustomizado;
            Segmentacao = segmentacao;
        }
       
        //Criação da campanha automatica 
        public Mensagem(
            int idEmpresa, 
            string tipoAutomacao,
            string diaSemana, 
            int diasAntesAniversario, 
            int diaMes,
            string tipoCanal,
            string conteudo,
            string segmentacao,
            string segCustomizado)
        {
            IdEmpresa = idEmpresa;
            TipoAutomacao = tipoAutomacao;
            DiaSemana = diaSemana;
            DiasAntesAniversario = diasAntesAniversario;
            DiaMes = diaMes;
            TipoCanal = tipoCanal;
            Conteudo = conteudo;
            Segmentacao = segmentacao;
            SegCustomizado = segCustomizado;
            Estado = true;
            ValorInvestido = 0;
            QtdEnviada = 0;
            EstadoEnvio = "Automatico";
        }


        //Campanha Agendada
        public Mensagem(int idCampanha, int idEmpresa, string nome, string segmentacao, int qtdSelecionado, Agenda agendar)
        {
            IdCampanha = idCampanha;
            IdEmpresa = idEmpresa;
            _Nome = nome;
            Segmentacao = segmentacao;
            QtdSelecionado = qtdSelecionado;
            Agendar = agendar;
            EstadoEnvio = "Agendada";




        }

        //Campanha envio normal
        public Mensagem(int idCampanha, int idEmpresa, string nome, string segmentacao, int qtdSelecionado)
        {
            IdCampanha = idCampanha;
            IdEmpresa = idEmpresa;
            _Nome = nome;
            Segmentacao = segmentacao;
            QtdSelecionado = qtdSelecionado;
            EstadoEnvio = "Enviada";



        }        

        //apos recebe o status da campanha da API SMS altera o status da campanhar no banco Local
        public Mensagem(int idEmpresa, int idCampanha, string estadoEnvio, Agenda agendar)
        {
            EstadoEnvio = estadoEnvio;
            IdEmpresa = idEmpresa;
            IdCampanha = idCampanha;
            Agendar = agendar;

        }

        public Mensagem(int idEmpresa, int idCampanha, string estadoEnvio)
        {
            EstadoEnvio = estadoEnvio;
            IdEmpresa = idEmpresa;
            IdCampanha = idCampanha;
        }

        public int ID { get; private set; }
        public int IdCampanha { get; private set; }
        public int IdEmpresa { get; private set; }

        public string TipoAutomacao { get; private set; } //Esse atributo representa Tipo em campanha automatizada, na campanha normal representa Nome da campanha  
        public string Segmentacao { get; private set; }
        public string SegCustomizado { get; private set; }
        public string DiaSemana { get; private set; } // definir que todo dia de uma semana vai envia a oferta  ex: toda terça feira dispara o sms com a mensagem
        public int DiasAntesAniversario { get; private set; } // definir o tempo antes do aniversario ex  2 dias antes sera enviado o sms da promoção  
        public int DiaMes { get; private set; }//  dia(1 a 31) de todo mes vai envia uma mensagem definida 
        public string TipoCanal { get; private set; }
        public string Conteudo { get; private set; }
        public string _Nome { get; private set; }
        public int QtdSelecionado { get; private set; }
        public Agenda Agendar { get; set; }
        public int QtdEnviada { get; private set; }
        public double ValorInvestido { get; private set; }
        public bool Estado { get; private set; }
        public string EstadoEnvio { get; private set; }
        public string TipoBusca { get; private set; }
   
        public void CalcularQtdEnviado(int qtdEnviada) => ValorInvestido = 0.15 * (QtdEnviada += qtdEnviada); 



    }
}
