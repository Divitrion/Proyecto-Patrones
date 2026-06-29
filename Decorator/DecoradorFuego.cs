using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Decorator
{
    public class DecoradorFuego : ModificadorBase
    {
        private int bonusFuego;

        public DecoradorFuego(Arma armaInterna) : base(armaInterna)
        {
            Random random = new Random();
            this.bonusFuego = random.Next(5, 30); // Genera un bonus de daño aleatorio entre 5 y 30
        }

        public override int CalcularDano() => armaInterna.CalcularDano() + bonusFuego;

        public override string GetDescripcion() =>
            $"{armaInterna.GetDescripcion()} + Fuego (+{bonusFuego}) = {CalcularDano()}";
    }
}