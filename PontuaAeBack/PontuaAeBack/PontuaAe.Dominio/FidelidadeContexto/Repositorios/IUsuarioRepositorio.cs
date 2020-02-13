using PontuaAe.Dominio.FidelidadeContexto.Consulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IUsuarioRepositorio
    {
        void Salvar(Usuario usuario);
        void Editar(Usuario usuario);
        void Deletar(int ID);
        void Desativar(int ID);
        bool ValidaEmail(string Email);        
        bool Autenticar(string Email, string Senha);
        Usuario OterUsuario(string Email);
       


    }
}
