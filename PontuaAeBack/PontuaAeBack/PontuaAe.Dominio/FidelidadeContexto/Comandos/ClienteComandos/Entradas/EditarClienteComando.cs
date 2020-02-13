using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;
using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas
{
    public class EditarClienteComando : Notifiable, IComando
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string Cpf { get; set; }




        public bool Valida()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(Nome, 3, "FirstName", "O nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(Nome, 40, "FirstName", "O nome deve conter no máximo 40 caracteres")               
                .IsEmail(Email, "Email", "O E-mail é inválido")
               
            );
            return IsValid;
        }
    }
}
