using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace State
{
    public interface IEstadoPersonaje
    {
        void AplicarEfecto(Core.Personaje personaje);

        void Atacar(Core.Personaje personaje);
        void Defender(Core.Personaje personaje);
        void UsarHabilidad(Core.Personaje personaje);
    }
}