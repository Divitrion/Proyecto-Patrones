using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Decorator
{
    public class DecoradorFuego : ModificadorBase
    {
        private const int BonusFuego = 15;

        public DecoradorFuego(Arma armaInterna) : base(armaInterna) { }

        public override int CalcularDaño() => armaInterna.CalcularDaño() + BonusFuego;

        public override string GetDescripcion() =>
            $"{armaInterna.GetDescripcion()} + Fuego (+{BonusFuego}) = {CalcularDaño()}";
    }
}