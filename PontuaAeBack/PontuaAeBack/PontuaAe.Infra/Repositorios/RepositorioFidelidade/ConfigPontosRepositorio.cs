using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade  
{
    public class ConfigPontosRepositorio : IConfigPontosRepositorio
    {
        private readonly PontuaAeDataContexto _db;
        public ConfigPontosRepositorio(PontuaAeDataContexto db)
        {
            _db = db;
        }

        public ObterDetalheConfigPontuacao ObterDetalheConfigPontuacao(int IdEmpresa)
        {
            return _db.Connection
                 .QueryFirstOrDefault<ObterDetalheConfigPontuacao>("SELECT * FROM CONTA_PONTUACAO WHERE IdEmpresa=@IdEmpresa", new {@IdEmpresa = IdEmpresa });

        }

        public void EditarConfiguracaoPontuacao(ConfiguracaoPontos regra)
        {
            _db.Connection
              .Execute("UPDATE CONTA_PONTUACAO SET Reais=@Reais, PontosFidelidade=@PontosFidelidade, ValidadePontos=@ValidadePontos WHERE IdEmpresa = @IdEmpresa ", new
              {
                  @Reais = regra.Reais,
                  @PontosFidelidade = regra.PontosFidelidade,
                  @ValidadePontos = regra.ValidadePontos,
                  @IdEmpresa = regra.IdEmpresa
              });
        }

        public void SalvaConfiguracaoPontuacao(ConfiguracaoPontos regra)
        {
            _db.Connection
                .Execute("INSERT INTO CONTA_PONTUACAO (Reais, PontosFidelidade, ValidadePontos, IdEmpresa) VALUES (@Reais, @PontosFidelidade, @ValidadePontos, @IdEmpresa)", new
                {
                    @Reais = regra.Reais,
                    @PontosFidelidade = regra.PontosFidelidade,
                    @ValidadePontos = regra.ValidadePontos,
                    @IdEmpresa = regra.IdEmpresa,

                });
        }


        public ConfiguracaoPontos ObterValidade(int IdEmpresa)
        {
            return _db.Connection
                .QueryFirstOrDefault<ConfiguracaoPontos>("SELECT ValidadePontos  FROM CONTA_PONTUACAO WHERE @IdEmpresa = @IdEmpresa", new { @IdEmpresa = IdEmpresa });
        }

        public ConfiguracaoPontos ObterdadosConfiguracao(int IdEmpresa)
        {
            return _db.Connection
                .QueryFirstOrDefault<ConfiguracaoPontos>("SELECT Reais, PontosFidelidade, ValidadePontos FROM CONTA_PONTUACAO WHERE @IdEmpresa = @IdEmpresa", new { @IdEmpresa = IdEmpresa });
        }
    }

}

