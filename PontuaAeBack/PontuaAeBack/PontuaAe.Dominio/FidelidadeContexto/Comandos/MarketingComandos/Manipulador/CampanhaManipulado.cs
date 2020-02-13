using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Entidades.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Manipulador
{
    public class CampanhaManipulado : Notifiable,
        IComandoManipulador<AddCampanhaComando>,
        IComandoManipulador<EditarCampanhaComando>,
        IComandoManipulador<RemoverCampanhaComando>
    {

        private readonly IAutomacaoMSGRepositorio _campanhaRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ISituacaoRepositorio _situacaoRepositorio;
        private readonly IEnviarSMS _enviarSMS;
        private readonly IEnviarSMS _agendarSMS;

        public CampanhaManipulado(
            IAutomacaoMSGRepositorio campanhaRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            IClienteRepositorio clienteRepositorio,
            ISituacaoRepositorio situacaoRepositorio,
            IEnviarSMS enviarSMS,
            IEnviarSMS agendarSMS
           )

        {
            _campanhaRepositorio = campanhaRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _enviarSMS = enviarSMS;
            _agendarSMS = agendarSMS;
        }

        public IComandoResultado Manipular(AddCampanhaComando comando)
        {      
          
            //var nomeEmpresa = _empresaRepositorio.ObterNome(comando.IdEmpresa).ToString();
            if (comando.AgendamentoAtivo == 1)
            {

                string data = comando.DataEnvio.ToString("dd/MMMM/yyyy");
                string hora = comando.HoraEnvio.ToString("08:00");
                var _IdCampanha = Convert.ToInt32(_enviarSMS.AgendarAsync(comando.Contatos, comando.Conteudo, data, hora));

                Agenda agenda = new Agenda(data, hora);
                var campanhaSMS = new Mensagem(_IdCampanha, comando.IdEmpresa, comando.Nome, comando.Segmentacao, comando.QtdSelecionado, agenda);
                campanhaSMS.CalcularQtdEnviado(comando.Contatos.Length);
                _campanhaRepositorio.Salvar(campanhaSMS);
            }
            else
            {
                
                var _IdCampanha = Convert.ToInt32(_enviarSMS.EnviarAsync(comando.Contatos, comando.Conteudo));
                var _campanhaSMS = new Mensagem(_IdCampanha, comando.IdEmpresa, comando.Nome, comando.Segmentacao, comando.QtdSelecionado);
                _campanhaSMS.CalcularQtdEnviado(comando.Contatos.Length);
                _campanhaRepositorio.Salvar(_campanhaSMS);
            }

            return new ComandoResultado(true, "OK", Notifications);
        }

        public IComandoResultado Manipular(EditarCampanhaComando comando)//Este metodo editar a campanha que está agendada
        {
         
                string data = comando.DataEnvio.ToString("dd/MMMM/yyyy");
                string hora = comando.HoraEnvio.ToString("hh:mm");
                var _IdCampanha = Convert.ToInt32(_enviarSMS.AgendarAsync(comando.Contatos, comando.Conteudo, data, hora));

                Agenda agenda = new Agenda(data, hora);
                var campanhaSMS = new Mensagem(_IdCampanha, comando.IdEmpresa, comando.Nome, comando.Segmentacao, comando.QtdSelecionado, agenda);
                campanhaSMS.CalcularQtdEnviado(comando.Contatos.Length);
                _campanhaRepositorio.Editar(campanhaSMS);
           

             return new ComandoResultado(true, "OK ", Notifications);
        }

        public IComandoResultado Manipular(RemoverCampanhaComando comando)
        {
            _campanhaRepositorio.Deletar(comando.ID, comando.IdEmpresa);
            return new ComandoResultado(true, "OK ", Notifications);
        }

        public void ObterSituacaoEnvioSMS()
        {
            IList<Mensagem> lista = _campanhaRepositorio.ListaMensagem();

            foreach (var l in lista)
            {
                string dados = _enviarSMS.ObterSituacaoSMSAsync(l.IdCampanha).ToString();
                string[] linhas = dados.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


                foreach (string linha in linhas)
                {
                    
                    if (!string.IsNullOrEmpty(linha))
                    {                          
                        string[] dado = linha.Split(';');

                        // [10] celular = Número do celular de retonro(Tipo de dados String)
                        // [4] data_resposta = Data de recebimento da mensagem(Tipo de Dados String -Formato: yyyy - MM - dd HH: mm: ss)
                        // [6] status = Status da entrega(Tipo de Dados String)                          

                        var mensagemSMS = new SituacaoSMS(dado[4], dado[6], dado[10], l.IdEmpresa, l.IdCampanha);
                        var EstadoCampanha = new Mensagem(l.IdCampanha, l.IdEmpresa, "Concluido");
                        _campanhaRepositorio.AtualizarEstadoCampanha(EstadoCampanha);
                        _situacaoRepositorio.SalvarSituacao(mensagemSMS);
         
                    }
                }  
            }
        }
    }

}

