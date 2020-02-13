using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.UsuarioConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IEmpresaRepositorio
    {
        void Salvar(Empresa empresa);
        void Editar(Empresa empresa);
        bool ChecarDocumento(string Documento);
        bool ChecarEmail(string EnderecoEmail);
        Empresa ObterNome(int IdEmpresa);
        ObterIDConsulta ObterIdEmpresa(int IdUsuario);
        ObterDetalheEmpresa ObterDetalheEmpresa(int ID);
        List<Empresa> ListaEmpresa();




    }
}
