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

        private int _turnosRestantes = 3;

        private EstadoEnvenenado() { }

        public void AplicarEfecto(Personaje p)
        {
            int daño = (int)(p.VidaMaxima * PorcentajeDanoPorTurno);
            Console.WriteLine($"  [{p.Nombre}] está Envenenado. Pierde {daño} HP antes de actuar.");
            p.RecibirDaño(daño);
            _turnosRestantes--;
            if (_turnosRestantes <= 0)
            {
                Console.WriteLine($"  [{p.Nombre}] se ha recuperado del veneno.");
                p.CambiarEstado(EstadoSaludable.ObtenerInstancia());
                _turnosRestantes = 3; // Reinicia el contador para la próxima vez que se envenene
            }
        }

        public void Atacar(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] ataca debilitado por el veneno.");

        public void Defender(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] se defiende debilitado por el veneno.");

        public void UsarHabilidad(Personaje p) =>
            Console.WriteLine($"  [{p.Nombre}] usa su habilidad debilitado por el veneno.");
    }
}