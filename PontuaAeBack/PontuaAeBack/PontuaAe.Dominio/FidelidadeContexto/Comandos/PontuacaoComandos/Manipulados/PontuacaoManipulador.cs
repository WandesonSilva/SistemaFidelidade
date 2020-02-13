using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using System;
using System.Collections.Generic;


namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Manipulador
{
    public class PontuacaoManipulador : Notifiable,
        IComandoManipulador<PontuarClienteComando>,
        IComandoManipulador<ResgatarPontosComando>
    {
        private readonly IConfigPontosRepositorio _configPontosRepositorio;
        private readonly IPontuacaoRepositorio _pontuacaoRepositorio;
        private readonly IReceitaRepositorio _receitaRepositorio;
        private readonly IPremioRepositorio _premiosRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IAutomacaoMSGRepositorio _automacaoRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly ISituacaoRepositorio _situacaoRepositorio;
        private readonly IEnviarSMS _enviarSMS;

        public PontuacaoManipulador(
            IClienteRepositorio clienteRepositorio,
            IPontuacaoRepositorio pontuacaoRepositorio,
            IReceitaRepositorio receitaRepositorio,
            IPremioRepositorio premiosRepositorio,
            IConfigPontosRepositorio configPontosRepositorio,
            IAutomacaoMSGRepositorio automacaoRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            ISituacaoRepositorio situacaoRepositorio,
            IEnviarSMS enviarSMS
)
        {
            _clienteRepositorio = clienteRepositorio;
            _pontuacaoRepositorio = pontuacaoRepositorio;
            _receitaRepositorio = receitaRepositorio;
            _premiosRepositorio = premiosRepositorio;
            _configPontosRepositorio = configPontosRepositorio;
            _automacaoRepositorio = automacaoRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _situacaoRepositorio = situacaoRepositorio;
            _enviarSMS = enviarSMS;
        }

        public IComandoResultado Manipular(PontuarClienteComando comando)
        {

            var dadosPontuacao = _configPontosRepositorio.ObterdadosConfiguracao(comando.IdEmpresa);

            //OBS  OBTER IDCLIENTE PELA TABELA DE PONTUACAO E NÃO CLIENTE 
            var VerificaCliente = _clienteRepositorio.ObterIDCliente(comando.Telefone);

            if (comando.ValorInfor <= dadosPontuacao.Reais)
                return new ComandoResultado(false, "O valor minimo necessário para pontua é", dadosPontuacao.Reais);

            if (VerificaCliente == null)
                AddNotification("Error", "Cliente não encontrado. Deseja cadastra-lo?");


            int _ObterIdCliente = _clienteRepositorio.ObterIDCliente(comando.Telefone);
            Pontuacao validador = new Pontuacao(_ObterIdCliente, comando.IdEmpresa);
            validador.Pontuar(comando.ValorInfor, dadosPontuacao.PontosFidelidade, dadosPontuacao.Reais, dadosPontuacao.ValidadePontos);
            _pontuacaoRepositorio.AtualizarSaldo(validador);

            //RECEITA 
            Receita DadosReceita = new Receita(comando.ValorInfor, comando.IdEmpresa, _ObterIdCliente, comando.IdUsuario, comando.TipoAtividade);
            _receitaRepositorio.Salvar(DadosReceita);

            //Notificação Saldo de Pontos
            var nomeEmpresa = _empresaRepositorio.ObterNome(comando.IdEmpresa).ToString();
            var numero = comando.Telefone;
            string[] contato = new string[] { "numero" };
            //_clienteRepositorio.ObterIDCliente(comando.Telefone);  VE SE PRECISA REMOVE SE O TESTE PASSA
            decimal saldoCliente = _pontuacaoRepositorio.obterSaldo(comando.IdEmpresa, _ObterIdCliente);

            _enviarSMS.EnviarAsync(contato, $"{nomeEmpresa}:Voce Acumulou {saldoCliente} pontos em nosso programa de fidelidade.  Acesse https://pontuaae.com.br e veja como funciona e os premios que pode resgatar");

            var _saldoCliente =  _clienteRepositorio.ObterSaldo(_ObterIdCliente, comando.IdEmpresa);

            IList<Premios> ListPremiosDisponiveis = _premiosRepositorio.PremiosDisponiveis(_saldoCliente.Saldo);

            //Notificação Prêmios disponiveis
            if(ListPremiosDisponiveis != null)
            {
                _enviarSMS.EnviarAsync(contato, $"{nomeEmpresa}:você já pode resgatar: Corte de cabelo em nosso programa fidelidade. Acesse https://pontuaae.com.br e veja o que pode resgatar");
            }

            // agora busca lista de Situacao :  ID, IdSMS, Contato, implementa foreach  USA IF PRA VE SE O STATUS DA SITUACAO E DIFERENTE DE  OK  PRA SEGUI PARA O PROXIMO IF  >>> e if pra verifica contato ==contato se sim invocar metodo CALCULARCONVERSÃO VOLTAAPOSRECEBER SMS E VALOR GASTO E EDITA A TABELA CONVERSAO   DEPOIS INSTANCIA _SITUACOReposiotio para atualizar a SITUACAO  MUDA STATUS PRA OK
            IEnumerable<SituacaoSMS> ListaSituacao;
            ListaSituacao = _situacaoRepositorio.ListaSituacaoSMS(comando.IdEmpresa);
            foreach (var linha in ListaSituacao)
            {
                if(linha.Contato == comando.Telefone && linha.Verificado != "ok")
                {                   
                    int qtd = _situacaoRepositorio.ObterQtdRetorno(comando.IdEmpresa, linha.ID);
                   
                    if(qtd == null)
                    {
                        qtd += 1;                       
                    }
                    var s = new SituacaoSMS("ok", linha.IdEmpresa, linha.ID, DateTime.Now);
                    s.CalcularConversao(qtd, comando.ValorInfor);

                    _situacaoRepositorio.atualizarSituacaoSMS(s);
  
                }
            }

            return new ComandoResultado(true, "A pontuação foi registrada com sucesso ", Notifications);
    
        }

        public IComandoResultado Manipular(ResgatarPontosComando comando)
        {

            var contaPontuacao = _configPontosRepositorio.ObterdadosConfiguracao(comando.IdEmpresa);
            var obterIdCliente = _clienteRepositorio.ObterIDCliente(comando.Telefone);
            decimal obterSaldo = _pontuacaoRepositorio.obterSaldo(comando.IdEmpresa, obterIdCliente);

            if (obterSaldo == null)
            {
                return new ComandoResultado(true, "Erro ao efetuar o resgate de prêmio", Notifications);
            }

            Pontuacao resgatar = new Pontuacao(obterSaldo, contaPontuacao.IdEmpresa, obterIdCliente);
            resgatar.Resgatar(comando.PontosNecessario);
            _pontuacaoRepositorio.AtualizarSaldo(resgatar);

            return new ComandoResultado(true, "Resgate efetuado com sucesso. Pode entregar o prêmio ao cliente", Notifications);

           
        }



    }
}
