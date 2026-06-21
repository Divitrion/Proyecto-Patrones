using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory
{
    public class CreadorArquero : ICreadorPersonaje
    {
        public Personaje CrearPersonaje(string nombre) => new Arquero(nombre);
        public string GetTipoCreador() => "Arquero";
    }
}