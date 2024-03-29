﻿using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Entidades.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Entradas
{
    public class EditarAutomacaoComando : IComando
    {
        public int ID { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoAutomacao { get; set; } //nome da automatização
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public string DiaSemana { get; set; } // definir que todo dia de uma semana vai envia a oferta  ex: toda terça feira dispara o sms com a mensagem
        public int DiasAntesAniversario { get; set; } // definir o tempo antes do aniversario ex  2 dias antes sera enviado o sms da promoção  
        public int DiaMes { get; set; } //  dia(1 a 31) de todo mes vai envia uma mensagem definida 
        public string TipoCanal { get; set; }
        public string Conteudo { get; set; }
        public string[] Contatos { get; set; } 

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
