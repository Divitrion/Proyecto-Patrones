using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory
{
    public interface ICreadorPersonaje
    {
        Personaje CrearPersonaje(string nombre);
        string GetTipoCreador();
    }
}