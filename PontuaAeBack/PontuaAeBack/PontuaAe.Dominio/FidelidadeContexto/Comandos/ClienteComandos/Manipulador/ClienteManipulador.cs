using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using System.Collections.Generic;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Manipulador
{
    public class ClienteManipulador : Notifiable,
        IComandoManipulador<PreCadastroClienteComando>,
        IComandoManipulador<EditarClienteComando>,
        IComandoManipulador<RemoverClienteComando>

    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IUsuarioRepositorio _usuRep;
        private readonly IPontuacaoRepositorio _pontuacaoRepositorio;
        private readonly IReceitaRepositorio _receitaRepositorio;
        private readonly IEnviarSMS _enviarSMS;
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public ClienteManipulador(
            IClienteRepositorio clienteRepositorio,
            IPontuacaoRepositorio pontuacaoRepositorio,
            IReceitaRepositorio receitaRepositorio,
            IEnviarSMS enviarSMS,
            IEmpresaRepositorio empresaRepositorio,
            IUsuarioRepositorio usuRep
            )
        {
            _clienteRepositorio = clienteRepositorio;
            _pontuacaoRepositorio = pontuacaoRepositorio;
            _receitaRepositorio = receitaRepositorio;
            _enviarSMS = enviarSMS;
            _empresaRepositorio = empresaRepositorio;
            _usuRep = usuRep;
    }

        public IComandoResultado Manipular(PreCadastroClienteComando comando)
        {
            if (_clienteRepositorio.ChecarCliente(comando.Telefone))
                 return new ComandoClienteResultado(false, "Telefone já cadastrado", Notifications);

            var PreCadastrado = new Cliente(comando.Telefone);
            AddNotifications(PreCadastrado.Notifications);

            if (Invalid)
                return new ComandoClienteResultado(false, "Por favor, corrija ", Notifications);

            _clienteRepositorio.SalvarPreCadastro(PreCadastrado);
            
            int _ObterIdCliente = _clienteRepositorio.ObterIDCliente(comando.Telefone);

            Pontuacao geraPontuacaoSaldoZero = new Pontuacao(0, comando.IdEmpresa, _ObterIdCliente);
            _pontuacaoRepositorio.CriarPontuacao(geraPontuacaoSaldoZero);
        
            //envia sms boas vindas ao programa de fidelidade
            var nomeEmpresa = _empresaRepositorio.ObterNome(comando.IdEmpresa).ToString();
            var numero = comando.Telefone;
            string[] contato = new string[] { "numero" };
            _enviarSMS.EnviarAsync(contato, $"{nomeEmpresa}: Parabens! voce Faz parte do nosso programa de fidelidade. Acesse https://pontuaae.com.br e veja seus pontos");

            return new ComandoClienteResultado(true, "Seja bem vindo", Notifications);
        }

        public IComandoResultado Manipular(CadastroClienteComando comando)
        {
            throw new System.NotImplementedException();
        }

        public IComandoResultado Manipular(EditarClienteComando comando)
        {
      

            var email = new Email(comando.Email);
                AddNotifications(email.Notifications);

            var PerfilUsuario = new Cliente( comando.Telefone, comando.Nome, email, comando.ID);

            if (Invalid)
                return new ComandoClienteResultado(false, "Por favor, corrija os campos abaixo", Notifications);

            _clienteRepositorio.Editar(PerfilUsuario);
            return new ComandoClienteResultado(true, "Salvo com sucesso", Notifications);
        }

        
        public void ClassificaClientesFrequentes() // Tem q refazer
        {
            IEnumerable<Cliente> _ListClassificaoCliente;
            _ListClassificaoCliente = _clienteRepositorio.ObterClassificacaoCliente();

            foreach (var campo in _ListClassificaoCliente)
            {
                Cliente cliente = new Cliente();
                var qtdVistasUmMes = _receitaRepositorio.QtdVisitasTrintaDias( campo.ID, campo.ID);  //Lembra de altera para ID DA PONTUACAO
                var qtdVisitasDoisMeses = _receitaRepositorio.QtdVisitasSessentaDias( campo.ID, campo.ID);
                var qtdVisitasTresMeses = _receitaRepositorio.QtdVisitasNoventaDias( campo.ID, campo.ID);
                cliente.SeguimentarCliente(campo.DataVisita, campo.EstadoFidelidade, qtdVistasUmMes, qtdVisitasDoisMeses, qtdVisitasTresMeses); 
                _clienteRepositorio.EditarClassificacaoCliente(cliente);

            }

        }

        public void ClassificaClientesNaoFrequentes()
        {
            //FALTA IMPLEMENTA A CLASSIFICAÇÃO DE TIPO DE CLIENTES PERDIDOS, INATIVOS
        }

        public IComandoResultado Manipular(RemoverClienteComando comando)
        {
            _clienteRepositorio.Deletar(comando.ID);
            return new ComandoClienteResultado(true, "Removido", Notifications);
        }
    }
}

