using System;

namespace Factory
{
    public class Barbaro : Personaje
    {
        private static readonly Random rng = new();

        public Barbaro(string nombre) : base(
            nombre,
            fuerza:       rng.Next(19, 26),
            percepcion:   rng.Next(4, 11),
            resistencia:  rng.Next(11, 18),
            carisma:      rng.Next(3, 10),
            inteligencia: rng.Next(1, 8),
            agilidad:     rng.Next(5, 12),
            suerte:       rng.Next(7, 14),
            vidaMaxima:   300)
        { }
    }
}