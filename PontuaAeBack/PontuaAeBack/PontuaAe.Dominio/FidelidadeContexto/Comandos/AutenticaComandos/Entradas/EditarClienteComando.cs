﻿using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas
{
    public class EditarUsuarioComando : IComando
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }       
        public string ClaimType { get; set; }       
        public string ClaimValue { get; set; }       

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}