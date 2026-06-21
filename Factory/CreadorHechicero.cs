using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory
{
    public class CreadorHechicero : ICreadorPersonaje
    {
        public Personaje CrearPersonaje(string nombre) => new Hechicero(nombre);
        public string GetTipoCreador() => "Hechicero";
    }
}