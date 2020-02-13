﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades.ObjetoValor
{
    public class Agenda
    {
        public Agenda(string dataEnvio, string horarioEnvio)
        {
            DataEnvio = dataEnvio;
            HoraEnvio = horarioEnvio;
   

        }
        public string DataEnvio { get; private set; }
        public string HoraEnvio { get; private set; }



    }
}
