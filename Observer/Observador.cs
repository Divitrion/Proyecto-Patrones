using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factory;

namespace Observer
{
    public class Observador
    {
        public virtual void Actualizar(Personaje p) { }
    }
}