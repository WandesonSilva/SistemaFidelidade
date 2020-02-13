using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS
{
    public interface IEnviarSMS
    {
        //void Enviar(string[] contatos, string Conteudo, string nomeEmpresa);
        Task<string> EnviarAsync(string[] contatos, string Conteudo);
        Task<string> AgendarAsync(string[,] contatos, string Conteudo, string DataEnvio, string HoraEnvio);
        Task<string> AgendarAsync(string[] contatos, string Conteudo, string DataEnvio, string HoraEnvio);
        Task<string> ObterSituacaoSMSAsync( int IdCampanhar);

       

    }
}
