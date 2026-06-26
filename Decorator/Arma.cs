using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factory;

namespace Decorator
{
    public abstract class Arma
    {
        public abstract int CalcularDaño();
        public abstract string GetDescripcion();

        public virtual void AplicarEfecto(Personaje objetivo) { }
    }
}