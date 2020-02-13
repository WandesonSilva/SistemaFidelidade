using System;
using System.Collections.Generic;
using System.Text;

namespace PontuaAe.Compartilhado.Comandos
{
   public interface IComandoManipulador<T> where T: IComando
    {
        IComandoResultado Manipular(T comando);
    }
}
