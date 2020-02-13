using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ConsultaCliente
{
    public class ObterDetalheClienteConsulta
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Imagem { get; set; }
    }
}
