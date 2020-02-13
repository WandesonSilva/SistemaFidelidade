using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConsultaCliente;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System.Collections.Generic;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.RelatoriosConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.UsuarioConsulta;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
   public interface IClienteRepositorio
    {
        int ObterIDCliente(string Telefone);
        ObterIDConsulta ObterID(int IdUsuario);
        ClienteConsulta BuscarCliente(int id);
        bool ChecarCliente(string Telefone);
        void SalvarPreCadastro(Cliente cliente);
        void Salvar(Cliente cliente);
        void Editar(Cliente cliente);
        void Deletar(int ID);
        IEnumerable<Cliente> ObterClassificacaoCliente();     
        List<Cliente> ObterDadosClientes(int IdEmpresa, string Segmento, string SegCustomizado); //SELECT * FROM FUNCIONA WHERE MONTH(DT_NASC) = ´03´    MES ATUAL  (month(GETDATE())
        IList<ListaRankingClientesConsulta> ListaRankingClientes(int IdEmpresa);
        void EditarClassificacaoCliente(Cliente cliente);
        Cliente ObterDadosEmCliente(string Telefone, int IdEmpresa);
        ObterSaldoClienteConsulta ObterSaldo(int IdCliente, int IdEmpresa);
        List<ObterSaldoClienteConsulta> ListaDeSaldoPorEmpresa (int IdCliente);
        int ObterTotalClientes(int IdEmpresa);
        int ObterTotalClientesRetidos(int IdEmpresa);
        





    }
}
