using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IMensagem<Entity>
    {
        void Salvar(Entity model);
        void Editar(Entity model);
        void Desativar(int IdEmpresa, int ID, int Desativo);
        void Deletar(int IdEmpresa, int ID);



    }
}
