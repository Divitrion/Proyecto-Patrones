using System;
using Factory;

namespace State
{
    public sealed class EstadoParalizado : IEstadoPersonaje
    {
        private static readonly Lazy<EstadoParalizado> _instancia =
            new(() => new EstadoParalizado());

        private static EstadoParalizado Instancia => _instancia.Value;

        public static EstadoParalizado ObtenerInstancia() => Instancia;

        private int _turnosRestantes = 1;

        private EstadoParalizado() { }

        public void AplicarEfecto(Personaje p){
            Console.WriteLine($"  [{p.Nombre}] está Paralizado. Pierde su turno por completo.");
            _turnosRestantes--;
            if (_turnosRestantes <= 0)
            {
                Console.WriteLine($"  [{p.Nombre}] se ha recuperado de la parálisis.");
                p.CambiarEstado(EstadoSaludable.ObtenerInstancia());
                _turnosRestantes = 1; // Reinicia el contador para la próxima vez que se paralice
            }
        }

        public void Atacar(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] intenta atacar, pero está Paralizado y no puede.");

        public void Defender(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] intenta defenderse, pero está Paralizado y no puede.");

        public void UsarHabilidad(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] intenta usar su habilidad, pero está Paralizado y no puede.");
    }
}