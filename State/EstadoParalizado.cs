using System;
using Factory;

namespace State
{
    public sealed class EstadoParalizado : IEstadoPersonaje
    {
        private static readonly Lazy<EstadoParalizado> _instancia =
            new(() => new EstadoParalizado());

        public static EstadoParalizado Instancia => _instancia.Value;

        private EstadoParalizado() { }

        public void AplicarEfecto(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] está Paralizado. Pierde su turno por completo.");

        public void Atacar(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] intenta atacar, pero está Paralizado y no puede.");

        public void Defender(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] intenta defenderse, pero está Paralizado y no puede.");

        public void UsarHabilidad(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] intenta usar su habilidad, pero está Paralizado y no puede.");
    }
}