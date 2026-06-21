using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory
{
    public class CreadorBarbaro : ICreadorPersonaje
    {
        public Personaje CrearPersonaje(string nombre) => new Barbaro(nombre);
        public string GetTipoCreador() => "Barbaro";
    }
}