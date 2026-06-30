using System;

namespace Factory
{
    public class Luchador : Personaje
    {
        private static readonly Random rng = new();

        public Luchador(string nombre) : base(
            nombre,
            fuerza:       rng.Next(15, 22),
            percepcion:   rng.Next(7, 14),
            resistencia:  rng.Next(13, 20),
            carisma:      rng.Next(5, 12),
            inteligencia: rng.Next(3, 10),
            agilidad:     rng.Next(7, 14),
            suerte:       rng.Next(5, 12),
            vidaMaxima:   250)
        { }
    }
}