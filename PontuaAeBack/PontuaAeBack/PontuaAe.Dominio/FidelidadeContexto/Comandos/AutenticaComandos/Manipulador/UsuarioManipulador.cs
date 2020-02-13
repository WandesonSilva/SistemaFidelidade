using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;


namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Manipulador
{
    public class UsuarioManipulador : Notifiable,
        IComandoManipulador<EditarUsuarioComando>,
        IComandoManipulador<DesativarContaComando>,
        IComandoManipulador<AutenticarUsuarioComando>,
        IComandoManipulador<RemoveUsuarioComando>,
        IComandoManipulador<CadastroClienteComando>,
        IComandoManipulador<CadastroEmpresaComando>
    {
        private readonly IEmpresaRepositorio _empresaRep;
        private readonly IClienteRepositorio _clienteRep;
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioManipulador(IUsuarioRepositorio repositorio, IEmpresaRepositorio empresaRep, IClienteRepositorio clienteRep)
        {
            _clienteRep = clienteRep;
            _repositorio = repositorio;
            _empresaRep = empresaRep;
        }

        public IComandoResultado Manipular(AutenticarUsuarioComando comando)
        {
            Email email = new Email(comando.Email);
            Usuario login = new Usuario(email.Endereco, comando.Senha);
            //AddNotifications(email);
            AddNotifications(login);
            if (Invalid)
                return new ComandoUsuarioResultado(false, "Por favor, corrija os campos", Notifications);
            if (_repositorio.Autenticar(login.Email, login.Senha))
                AddNotification("Error", "Usuário não encontrado!");

            AddNotifications(login);

            return new ComandoUsuarioResultado(true);
        }
        public IComandoResultado Manipular(AddContaFuncionario comando)
        {
            if (_repositorio.ValidaEmail(comando.Email))
                AddNotification("Email", "Este Email já está em uso");
            var email = new Email(comando.Email);
            var usuario = new Usuario(email.Endereco, comando.Senha, comando.ClaimType, comando.ClaimValue);
            if (Invalid)
                return new ComandoUsuarioResultado(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

            _repositorio.Salvar(usuario);
            return new ComandoUsuarioResultado(true, "", Notifications);
        }



        public IComandoResultado Manipular(AddContaCliente comando)
        {
            if (_repositorio.ValidaEmail(comando.Email))
                AddNotification("Email", "Este Email já está em uso");
            var email = new Email(comando.Email);
            var usuario = new Usuario(email.Endereco, comando.Senha, comando.ClaimType, comando.ClaimValue);
            if (Invalid)
                return new ComandoUsuarioResultado(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

            _repositorio.Salvar(usuario);
            return new ComandoUsuarioResultado(true, "", Notifications);
        }

        
        public IComandoResultado Manipular(EditarUsuarioComando comando)
        {
            if (_repositorio.ValidaEmail(comando.Email))
                AddNotification("Email", "Este Email já está em uso");
            var email = new Email(comando.Email);
            var usuario = new Usuario( email.Endereco, comando.Senha);
            if (Invalid)
                return new ComandoUsuarioResultado(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

            _repositorio.Editar(usuario);
            return new ComandoUsuarioResultado(true, "", Notifications);
        }


        public IComandoResultado Manipular(RemoveUsuarioComando comando)
        {
            _repositorio.Deletar(comando.ID);
            return new ComandoUsuarioResultado(true, "Removido", Notifications);
        }

        public IComandoResultado Manipular(DesativarContaComando comando)
        {
            throw new System.NotImplementedException();
        }

        public IComandoResultado Manipular(CadastroEmpresaComando comando)
        {
            throw new System.NotImplementedException();
        }

        public IComandoResultado Manipular(CadastroClienteComando comando)
        {
            throw new System.NotImplementedException();
        }
    }
}
