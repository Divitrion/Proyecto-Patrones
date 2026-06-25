using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factory;

namespace State
{
    public interface IEstadoPersonaje
    {
        void AplicarEfecto(Personaje personaje);

        void Atacar(Personaje personaje);
        void Defender(Personaje personaje);
        void UsarHabilidad(Personaje personaje);
    }
}