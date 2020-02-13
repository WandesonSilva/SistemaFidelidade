using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Manipulador
{
    public class EmpresaManipulador : Notifiable,
        IComandoManipulador<AddDadosEmpresaComando>,
        IComandoManipulador<EditarPerfilEmpresaComando>,
        IComandoManipulador<AddRegraPontuacaoComando>,
        IComandoManipulador<EditarRegraPontuacaoComando>

    {
        private readonly IEmpresaRepositorio _empresaRep;
        private readonly IConfigPontosRepositorio _configPontoRep;
        private readonly IUsuarioRepositorio _repUsuario;
        private readonly IContaSMSRepositorio _contaSMS;




        public EmpresaManipulador(IEmpresaRepositorio empresaRep, IConfigPontosRepositorio cofigPontuacaoRep, IUsuarioRepositorio repUsuario, IContaSMSRepositorio contaSMS)
        {
            _empresaRep = empresaRep;
            _configPontoRep = cofigPontuacaoRep;
            _repUsuario = repUsuario;
            _contaSMS = contaSMS;
        }

        public IComandoResultado Manipular(AddDadosEmpresaComando comando)
        {

            if (_empresaRep.ChecarDocumento(comando.Documento))
                AddNotification("CNPJ", "Este CNPJ está em uso");

            var documento = new Documento(comando.Documento);

            //Criando Conta Admin
            var usuarioAdmin = new Usuario(comando.Email, comando.Senha, comando.ClaimType, comando.ClaimValue);
            _repUsuario.Salvar(usuarioAdmin);
            Usuario usuarioAdmim = _repUsuario.OterUsuario(comando.Email);

            var empresa = new Empresa(usuarioAdmim.ID, comando.NomeFantasia, comando.Descricao, comando.NomeResponsavel, comando.Telefone, comando.Email, documento, comando.Seguimento, comando.Horario, comando.Facebook, comando.Website, comando.Instagram, comando.Delivery, comando.Bairro, comando.Rua, comando.Numero, comando.Cep, comando.Estado, comando.Complemento, comando.Logo, comando.Cidade);
            AddNotifications(documento.Notifications);

            if (Invalid)
                return new ComandoEmpresaResultado(false, "Por favor, corrija os campos abaixo", Notifications);

            _empresaRep.Salvar(empresa);

            var _idEmpresa = _empresaRep.ObterIdEmpresa(usuarioAdmim.ID);

            var configPontos = new ConfiguracaoPontos(_idEmpresa.ID, 0,0,0);

            _configPontoRep.SalvaConfiguracaoPontuacao(configPontos);
            //Adicionar  creditos SMS na Empresa
            ContaSMS creditoSMS = new ContaSMS(_idEmpresa.ID, 500 );
            _contaSMS.Salvar(creditoSMS);



            return new ComandoEmpresaResultado(true, "Dados Salvos", Notifications);

        }

        public IComandoResultado Manipular(EditarPerfilEmpresaComando comando)
        {

            var documento = new Documento(comando.Documento);

            //Usuario usuarioAdmim = _repUsuario.OterUsuario(comando.Email);
            var empresa = new Empresa(comando.IdUsuario, comando.NomeFantasia, comando.Descricao, comando.NomeResponsavel, comando.Telefone, comando.Email, documento, comando.Seguimento, comando.Horario, comando.Facebook, comando.Website, comando.Instagram, comando.Delivery, comando.Bairro, comando.Rua, comando.Numero, comando.Cep, comando.Estado, comando.Complemento, comando.Logo, comando.Cidade);

            AddNotifications(documento.Notifications);

            if (Invalid)
                return new ComandoEmpresaResultado(false, "Por favor, corrija os campos incorretos", Notifications);

            _empresaRep.Editar(empresa);

            return new ComandoEmpresaResultado(true, "Dados Atualizado com sucesso ", Notifications);

        }

        public IComandoResultado Manipular(AddRegraPontuacaoComando comando)
        {
            //criação da regra da pontuação
            var configuracaoPontos = new ConfiguracaoPontos(comando.IdEmpresa, comando.PontosFidelidade, comando.Reais, comando.ValidadePontos);
            _configPontoRep.SalvaConfiguracaoPontuacao(configuracaoPontos);
            return new ComandoEmpresaResultado(true, "Salvo", Notifications);
        }

        public IComandoResultado Manipular(EditarRegraPontuacaoComando comando)
        {
            var configuracaoPontos = new ConfiguracaoPontos(comando.IdEmpresa, comando.PontosFidelidade, comando.Reais, comando.ValidadePontos);
            _configPontoRep.EditarConfiguracaoPontuacao(configuracaoPontos);
            return new ComandoEmpresaResultado(true, "OK", Notifications);
        }


    }
}