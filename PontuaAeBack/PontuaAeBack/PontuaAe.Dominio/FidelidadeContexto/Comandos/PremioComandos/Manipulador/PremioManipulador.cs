using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Manipulador
{
    public class PremioManipulador : Notifiable,
        IComandoManipulador<CadastraPremioComando>,
        IComandoManipulador<DeletarPremioComando>,
        IComandoManipulador<EditarPremioComando>
    {
        private readonly IPremioRepositorio _premioRepositorio;
        private readonly IEmpresaRepositorio _Empresarepositorio;

        public PremioManipulador(IPremioRepositorio premioRepositorio, IEmpresaRepositorio empresarepositorio)
        {
            _premioRepositorio = premioRepositorio;
            _Empresarepositorio = empresarepositorio;
        }

        public IComandoResultado Manipular(CadastraPremioComando comando)
        {

            Descricao descricao = new Descricao(comando.Descricao);
            Premios premio = new Premios(comando.IdEmpresa, comando.Nome, descricao, comando.QtdPontos, comando.QtdPremio, comando.imagem, comando.validade);

            AddNotifications(descricao.Notifications);
            if (Invalid)
                return new ComandoResultado(true, "Descrição deve conter, 60 a 250 caracteres ", Notifications);

            _premioRepositorio.Salvar(premio);

            return new ComandoResultado(true, "Premio Cadastrado com sucesso", Notifications);
        }

        public IComandoResultado Manipular(DeletarPremioComando comando)
        {
            _premioRepositorio.Deletar(comando.Id, comando.IdEmpresa);
            return new ComandoResultado(true, "Premio Deletado", Notifications);
        }

        public IComandoResultado Manipular(EditarPremioComando comando)
        {
            Descricao descricao = new Descricao(comando.Descricao);
            Premios premio = new Premios(comando.IdEmpresa, comando.Nome, descricao, comando.QtdPontos, comando.QtdPremio, comando.imagem, comando.Validade);
            _premioRepositorio.Editar(premio);
            return new ComandoResultado(true, "Salvando Informações", Notifications);

        }

    }
}
