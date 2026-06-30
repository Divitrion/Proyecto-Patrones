using System;

namespace Factory
{
    public class Hechicero : Personaje
    {
        private static readonly Random rng = new();

        public Hechicero(string nombre) : base(
            nombre,
            fuerza:       rng.Next(3, 10),
            percepcion:   rng.Next(11, 18),
            resistencia:  rng.Next(3, 10),
            carisma:      rng.Next(13, 20),
            inteligencia: rng.Next(19, 26),
            agilidad:     rng.Next(7, 14),
            suerte:       rng.Next(9, 16),
            vidaMaxima:   170)
        { }
    }
}