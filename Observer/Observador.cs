using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factory;

namespace Observer
{
    public class Observador
    {
        protected Personaje personaje;

        public virtual void Actualizar(Personaje p) { }
    }
}