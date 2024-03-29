﻿using FluentValidator;
using FluentValidator.Validation;
using Microsoft.AspNetCore.Http;
using PontuaAe.Compartilhado.Comandos;
using System.Collections.Generic;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas
{
    public class AddDadosEmpresaComando : Notifiable, IComando
    {
        public int IdUsuario { get; set; }
        public string NomeFantasia { get; set; }
        public string Descricao { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        public string Documento { get; set; }
        public string Seguimento { get; set; }
        public string Horario { get; set; }
        public string Facebook { get; set; }
        public string Website { get; set; }
        public string Instagram { get; set; }
        public string Delivery { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get;  set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Logo { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ClaimType => "PontuaAe";
        public string ClaimValue => "Admin";
        public bool Valida()
        {
            AddNotifications(new ValidationContract()

              .IsEmail(Email, "Email", "O E-mail é inválido")
              .HasLen(Documento, 11, "CPF/CNPJ", "CPF/CNPJ inválido")
          );
            return IsValid;
        }
    }
}
