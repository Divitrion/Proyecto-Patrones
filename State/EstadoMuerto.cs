using System;
using Factory;

namespace State
{
    public sealed class EstadoMuerto : IEstadoPersonaje
    {
        private static readonly Lazy<EstadoMuerto> _instancia =
            new(() => new EstadoMuerto());

        public static EstadoMuerto Instancia => _instancia.Value;

        private EstadoMuerto() { }

        public void AplicarEfecto(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] está Muerto. No recibe efectos ni toma turnos.");

        public void Atacar(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] está Muerto y no puede atacar.");

        public void Defender(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] está Muerto y no puede defenderse.");

        public void UsarHabilidad(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] está Muerto y no puede usar habilidades.");
    }
}