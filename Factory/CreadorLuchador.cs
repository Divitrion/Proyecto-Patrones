using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory
{
    public class CreadorLuchador : ICreadorPersonaje
    {
        public Personaje CrearPersonaje(string nombre) => new Luchador(nombre);
        public string GetTipoCreador() => "Luchador";
    }
}