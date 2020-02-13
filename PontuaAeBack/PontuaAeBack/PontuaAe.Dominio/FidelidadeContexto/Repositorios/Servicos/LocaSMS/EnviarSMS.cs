using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS
{
    public class EnviarSMS : IEnviarSMS
    {
        private static HttpClient client = new HttpClient();
        private readonly string login = "@ddd";
        private string senha = "123";
        public string BaseUrl = "http://209.133.205.2/painel/";   
               
        public async Task<string> AgendarAsync(string[] contatos, string Conteudo, string DataEnvio, string HoraEnvio)
        {
            string action = string.Format($"api.ashx?action=sendsms&lgn={login}&pwd={senha}&msg={Conteudo}&numbers={contatos}&jobdate={DataEnvio}&jobtime={HoraEnvio}");
            StringContent queryString = new StringContent(action);
            HttpResponseMessage response = await client.PostAsync(new Uri(BaseUrl), queryString);
            response.EnsureSuccessStatusCode();
            string resultado = response.Content.ReadAsStringAsync().Result;

            dynamic _resultado = JsonConvert.DeserializeObject(resultado);
            var idCampanha = _resultado.data.results[0];
            return idCampanha;
        }

        public async Task<string> EnviarAsync(string[] contatos, string Conteudo)
        {
            string action = string.Format($"api.ashx?action=sendsms&lgn={login}&pwd={senha}&msg={Conteudo}&numbers={contatos}");
            StringContent queryString = new StringContent(action);
            HttpResponseMessage response = await client.PostAsync(new Uri(BaseUrl), queryString);
            response.EnsureSuccessStatusCode();
            string resultado = response.Content.ReadAsStringAsync().Result;
            
            dynamic _resultado = JsonConvert.DeserializeObject(resultado);
            var idCampanha = _resultado.data.results[0];
            return idCampanha;            
        }

        public async Task<string> ObterSituacaoSMSAsync(int IdCampanhar)
        {           
            HttpResponseMessage response = await client.GetAsync(new Uri(BaseUrl) + $"api.ashx?action=getstatus&lgn={login}&pwd={senha}&id={IdCampanhar}");
            response.EnsureSuccessStatusCode();
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            string _dado = json["JsonValues"]["data"].ToString();
            return _dado;
            
        }

        public Task<string> AgendarAsync(string[,] contatos, string Conteudo, string DataEnvio, string HoraEnvio)
        {
            throw new NotImplementedException();
        }
    }
}
