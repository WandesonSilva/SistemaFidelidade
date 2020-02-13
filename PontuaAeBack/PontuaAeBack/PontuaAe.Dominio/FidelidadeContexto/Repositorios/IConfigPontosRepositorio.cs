using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IConfigPontosRepositorio
    {
        void SalvaConfiguracaoPontuacao(ConfiguracaoPontos regraPontuacao);
        void EditarConfiguracaoPontuacao(ConfiguracaoPontos configuracaoPontos);
        ConfiguracaoPontos ObterdadosConfiguracao(int IdEmpresa);
        ConfiguracaoPontos ObterValidade(int IdEmpresa);
        ObterDetalheConfigPontuacao ObterDetalheConfigPontuacao(int IdEmpresa);




    }
}
