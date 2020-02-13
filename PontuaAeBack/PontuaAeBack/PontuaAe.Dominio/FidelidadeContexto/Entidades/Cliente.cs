using System;
using PontuaAe.Compartilhado.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;


namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Cliente : Entidade
    {

        public Cliente()
        {

        }


        public Cliente(string telefone)
        {
            Telefone = telefone;

        }

        public Cliente(int IDusuario, string nome, string sexo, string telefone, Email email, DateTime? dataNascimento )
        {
            IdUsuario = IDusuario;
            Nome = nome;
            Sexo = sexo;
            Telefone = telefone;
            Email = email;
            DataNascimento = dataNascimento;

            AddNotifications(email.Notifications);
        }

        public Cliente(string nome, int Id, string sexo, string telefone, Email email, DateTime? dataNascimento)
        {
            Nome = nome;
            ID = Id;
            Sexo = sexo;
            Telefone = telefone;
            Email = email;
            DataNascimento = dataNascimento;

            AddNotifications(email.Notifications);
        }

        public Cliente(string telefone, string nome, int IDusuario)
        {
            Telefone = telefone;
            Nome = nome;
             IdUsuario = IDusuario;
        }

        public Cliente(string telefone, string nome, Email email, int id)
        {
            Telefone = telefone;
            Nome = nome;
            Email = email;
            ID = id;
        }
            public Cliente(string telefone, int IDusuario)
        {
            Telefone = telefone;
            IdUsuario = IDusuario;
        }

        public int ID { get; set; }
        public int IdUsuario { get; set; }
        public string Nome { get; private set; }
        public DateTime? DataNascimento { get; set; }
        //public CPF Cpf { get; private set; }
        public string Sexo { get; private set; }
        public Email Email { get; private set; }
        public string Telefone { get; private set; }
        public Usuario Usuario { get; private set; }
        public string EstadoFidelidade { get; set; }
        public string TipoCliente { get; set; }
        public DateTime DataVisita { get; private set; }


        public void SeguimentarCliente(DateTime ultimaVisita, string estadoFidelidade, int qtdVisitasUmMes, int qtdVisitasDoisMeses, int qtdVisitasTresMeses)
        {
            TimeSpan data = DateTime.Now.Subtract(ultimaVisita);
            int TempoEmDias = data.Days;


            if (TempoEmDias <= 30)
            {

                if (qtdVisitasUmMes > 1 && qtdVisitasUmMes <= 3)
                {
                    EstadoAtivo();

                }
                else if (qtdVisitasUmMes >= 10)
                {
                    EstadoOuro();
                    PerfilVip();
                }
            }

            if (TempoEmDias <= 60)
            {

                if (qtdVisitasDoisMeses >= 6 && qtdVisitasUmMes <= 10)
                {
                    EstadoPrata();
                    PerfilVip();
                }
                else if (qtdVisitasDoisMeses > 10)
                {
                    EstadoOuro();
                    PerfilVip();
                }
            }

            if (TempoEmDias <= 90)
            {

                if (qtdVisitasTresMeses >= 4 && qtdVisitasUmMes <= 10)
                {
                    EstadoBronze();
                    PerfilVip();
                }
                else if (qtdVisitasTresMeses > 10)
                {
                    EstadoOuro();
                    PerfilVip();
                }
            }


        }
        public void ClassificaClientesNaoFrequentes(DateTime ultimaVisita)
        {
            TimeSpan data = DateTime.Now.Subtract(ultimaVisita);
            int TempoEmDias = data.Days;

            if (TempoEmDias > 120)
            {
                EstadoEmRisco();

            }

            if (TempoEmDias > 180)
            {
                EstadoPerdido();

            }
            if (TempoEmDias >= 300)
            {
                EstadoInativo();

            }

        }
        //status classificação
        public void EstadoAtivo() => EstadoFidelidade = "Ativo";
        public void EstadoOuro() => EstadoFidelidade = "Ouro";
        public void EstadoPrata() => EstadoFidelidade = "Prata";
        public void EstadoBronze() => EstadoFidelidade = "Bronze";
        public void EstadoEmRisco() => EstadoFidelidade = "Em Risco";
        public void EstadoPerdido() => EstadoFidelidade = "Perdido";
        public void EstadoInativo() => EstadoFidelidade = "Inativo";

        
        //Segmentação em  Perfil:
        public void PerfilNovo() => TipoCliente = "Novo";
        public void PerfilVip() => TipoCliente = "Vip";
        public void PerfilEventual() => TipoCliente = "Eventual";
        public void PerfilFrequente() => TipoCliente = "Frequente";

        











    }
}
