using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class ConfiguracaoPontos : Notifiable
    {


        public ConfiguracaoPontos(int idEmpresa, decimal pontosFidelidade, decimal reais, int validadePontos)
        {
            IdEmpresa = idEmpresa;
            Reais = reais;
            PontosFidelidade = pontosFidelidade;            
            ValidadePontos = validadePontos;
           
        }

        public int IdEmpresa { get; private set; } 
        public decimal Reais { get; private set; } 
        public decimal PontosFidelidade { get; private set; }  
        public int ValidadePontos { get; private set; } 
        //public decimal PontosInicial { get; private set; } 

    }
}
