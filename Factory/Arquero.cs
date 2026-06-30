using System;

namespace Factory
{
    public class Arquero : Personaje
    {
        private static readonly Random rng = new();

        public Arquero(string nombre) : base(
            nombre,
            fuerza:       rng.Next(9, 16),
            percepcion:   rng.Next(15, 22),
            resistencia:  rng.Next(7, 14),
            carisma:      rng.Next(7, 14),
            inteligencia: rng.Next(9, 16),
            agilidad:     rng.Next(13, 20),
            suerte:       rng.Next(11, 18),
            vidaMaxima:   190)
        { }
    }
}