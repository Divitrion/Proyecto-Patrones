using System;
using Factory;

namespace State
{
    public sealed class EstadoSaludable : IEstadoPersonaje
    {
        private static readonly Lazy<EstadoSaludable> _instancia =
            new(() => new EstadoSaludable());

        private static EstadoSaludable Instancia => _instancia.Value;

        public static EstadoSaludable ObtenerInstancia() => Instancia;

        private EstadoSaludable() { }

        public void AplicarEfecto(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] está Saludable. Sin efectos al inicio del turno.");

        public void Atacar(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] ataca con plena efectividad ({p.Fuerza} de Fuerza).");

        public void Defender(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] se defiende con plena efectividad ({p.Resistencia} de Resistencia).");

        public void UsarHabilidad(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] usa su habilidad con plena efectividad ({p.Inteligencia} de Inteligencia).");
    }
}