using System;
using Factory;

namespace State
{
    public sealed class EstadoEnvenenado : IEstadoPersonaje
    {
        private static readonly Lazy<EstadoEnvenenado> _instancia =
            new(() => new EstadoEnvenenado());

        private static EstadoEnvenenado Instancia => _instancia.Value;

        public static EstadoEnvenenado ObtenerInstancia() => Instancia;

        private const float PorcentajeDanoPorTurno = 0.10f;

        private EstadoEnvenenado() { }

        public void AplicarEfecto(Personaje p)
        {
            int daño = (int)(p.VidaMaxima * PorcentajeDanoPorTurno);
            Console.WriteLine($"  [{p.Nombre}] está Envenenado. Pierde {daño} HP antes de actuar.");
            p.RecibirDaño(daño);
        }

        public void Atacar(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] ataca debilitado por el veneno.");

        public void Defender(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] se defiende debilitado por el veneno.");

        public void UsarHabilidad(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] usa su habilidad debilitado por el veneno.");
    }
}