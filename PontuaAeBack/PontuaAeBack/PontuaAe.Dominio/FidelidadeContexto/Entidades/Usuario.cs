using PontuaAe.Compartilhado.Entidades;
using System.Text;
using System;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using FluentValidator;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Usuario : Notifiable
    {
        public Usuario()
        {

        }
        public Usuario(string email)
        {
            Email = email;
        }
        public Usuario(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public Usuario(string email, string senha, int id)
        {
            Email = email;
            Senha = senha;
            ID = id;
        }

        public Usuario(string email, string senha, string claimType, string claimValue)
        {
            Email = email;
            Senha = senha;
            ClaimType = claimType;
            ClaimValue = claimValue;
            Estado = true;
        }


        //contrutor com parametro opcional para Conta de Funcionário e Cliente
        public Usuario(string nome = "Nome Opcional", string email="email", string senha="senha")
        {
            Email = email;
            Senha = senha;
            Nome = nome;
        }

        public int ID { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public bool Estado { get; private set; }
        public string ClaimType { get; private set; }
        public string ClaimValue { get; private set; }


        public bool Autenticar(string email, string senha)
        {
            if (Email == email && Senha == EncriptaSenha(senha))
                return true;

            AddNotification("Usuario", "Email ou Senha Invalido");
            return false;
        }

        private void Ativar() => Estado = true;
        public void Desativar() => Estado = false;

        public string EncriptaSenha(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            var senha = (pass += "|2d331cca-f6c0-40c0-bb43-6e32989c2580");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(senha));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
