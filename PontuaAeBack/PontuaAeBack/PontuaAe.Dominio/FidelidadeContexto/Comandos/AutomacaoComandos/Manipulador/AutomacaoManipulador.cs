using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninject.Activation;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using PontuaAe.Dominio.FidelidadeContexto.Entidades.ObjetoValor;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Manipulador
{
    public class AutomacaoManipulador : Notifiable,
        IComandoManipulador<AddAutomacaoComando>,
        IComandoManipulador<EditarAutomacaoComando>,
        IComandoManipulador<RemoverAutomacaoComando>
    {
        private readonly IAutomacaoMSGRepositorio _automacaoRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ISituacaoRepositorio _situacaoRepositorio;
        private readonly IEnviarSMS _enviarSMS;

        public AutomacaoManipulador(
             IAutomacaoMSGRepositorio automacaoRepositorio,
             IEmpresaRepositorio empresaRepositorio,
             IClienteRepositorio clienteRepositorio,
             IEnviarSMS enviarSMS

            )
        {
            _automacaoRepositorio = automacaoRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _enviarSMS = enviarSMS;
        }

        public IComandoResultado Manipular(EditarAutomacaoComando comando)
        {

            var automacao = new Mensagem(
                comando.ID,
                comando.IdEmpresa,
                comando.TipoAutomacao,
                comando.DiaSemana,
                comando.DiasAntesAniversario,
                comando.DiaMes,
                comando.TipoCanal,
                comando.Conteudo,
                comando.Segmentacao,
                comando.SegCustomizado);

            _automacaoRepositorio.Editar(automacao);

            return new ComandoResultado(true, "OK", Notifications);
        }

        public IComandoResultado Manipular(AddAutomacaoComando comando)
        {

           var automacao = new Mensagem(
               comando.IdEmpresa,
               comando.TipoAutomacao,
               comando.DiaSemana,
               comando.DiasAntesAniversario,
               comando.DiaMes,
               comando.TipoCanal,
               comando.Conteudo,
               comando.Segmentacao,
               comando.SegCustomizado);

            _automacaoRepositorio.Salvar(automacao);
            return new ComandoResultado(true, "OK", Notifications);
        }

        public IComandoResultado Manipular(RemoverAutomacaoComando comando)
        {
            throw new NotImplementedException();
        }

        //IMPLEMENTA Classe JOBS NO QUARTZ   PROGRAMA NA Startup o dia e horario da execução   AS 9:00HS

        public void TipoAutomacaoAniversario() 
        {
           
            IEnumerable<Mensagem> Lista = _automacaoRepositorio.ListaTipoAutomacao();
            foreach (var linha in Lista) 
            { 
                IEnumerable<ObterAutomacaoTipoAniversario> listDadosAutomacao;
                listDadosAutomacao = _automacaoRepositorio.ObterDadosAutomacaoAniversario("Aniversariante", linha.Segmentacao, linha.SegCustomizado, linha.IdEmpresa);

                int contador = 0;
                Mensagem campanhaSMS = new Mensagem();
                foreach (var l in listDadosAutomacao)
                {
                    int diasAntes = l.DiasAntesAniversario;
                    DateTime data = l.DataNascimento.AddDays(-diasAntes);

                    string dataEnvio = data.ToString("dd-MM-yyyy");
                    string horaEnvio = "18:00";

                    int _idCampanha = Convert.ToInt32( _enviarSMS.AgendarAsync(l.Telefone, l.Conteudo, dataEnvio, horaEnvio));
                    Agenda agendar = new Agenda(dataEnvio, horaEnvio);


                    contador ++;
                    new Mensagem(_idCampanha, l.IdEmpresa, "Agendada", agendar); 

                }
                campanhaSMS.CalcularQtdEnviado(contador);
                _automacaoRepositorio.Editar(campanhaSMS);
               
            }
        }      
            
        public void AutomacaoTipoSemana()
        {
            //TODO DIA VERIFICA  E AGENDA O ENVIO PARA O MESMO DIA  EXECUTA O JOB AS 10:00HS
            IEnumerable<Mensagem> Lista = _automacaoRepositorio.ListaTipoAutomacao();
            foreach (var l in Lista)
            {
                IEnumerable<ObterAutomacaoTipoDiaSemana> listDadosAutomacao;
                listDadosAutomacao = _automacaoRepositorio.ObterDadosAutomacaoSemana("AutomacaoSemana", l.Segmentacao, l.SegCustomizado, l.IdEmpresa);
                                             
                string[,] Contato =  _automacaoRepositorio.ListaTelefones(l.IdEmpresa, l.SegCustomizado, l.Segmentacao);
                int QtdContatos = Contato.Length;

                foreach (var linha in listDadosAutomacao)
                {                   

                    if (linha.DiaSemana == linha.Dia) //OBS SE NÃO FUNCIONAR, ENTÃO IMPLEMENTA DateTime.Now.DayOfWeek PARA OBTER O DIA DA SEMANA NA DATA ATUAL NA VARIAVEL Dia
                    {
                        string dataEnvio = DateTime.Now.ToString("dd-mm-yyyy");
                        string horaEnvio = "12:00";

                        var _IdCampanha = Convert.ToInt32(_enviarSMS.AgendarAsync(Contato, linha.Conteudo, dataEnvio, horaEnvio));

                        Agenda agenda = new Agenda(dataEnvio, horaEnvio);
                        var campanhaSMS = new Mensagem(_IdCampanha, l.IdEmpresa, "Agendada", agenda);
                        campanhaSMS.CalcularQtdEnviado(QtdContatos);
                        _automacaoRepositorio.Editar(campanhaSMS);

                    }
                }
            }
        }

        public void AutomacaoTipoMes()
        {


        }

        public void TipoAutomacaoEmRisco()
        {

        }

        public void ObterSituacaoEnvioSMS()
        {
            IList<Mensagem> lista =  _automacaoRepositorio.ListaMensagem();

            foreach (var l in lista)
            {                               
                string dados = _enviarSMS.ObterSituacaoSMSAsync(l.IdCampanha).ToString();
                string[] linhas = dados.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (string linha in linhas)
                {

                    if (!string.IsNullOrEmpty(linha))
                    {
                        string[] dado= linha.Split(';');

                        // [10] celular = Número do celular de retonro(Tipo de dados String)
                        // [4] data_resposta = Data de recebimento da mensagem(Tipo de Dados String -Formato: yyyy - MM - dd HH: mm: ss)
                        // [6] status = Status da entrega(Tipo de Dados String)                        

                        var mensagemSMS = new SituacaoSMS(dado[4], dado[6], dado[10], l.IdEmpresa, l.IdCampanha);
                        _situacaoRepositorio.SalvarSituacao(mensagemSMS);

                    }

                }
            }
        }

    }

}
