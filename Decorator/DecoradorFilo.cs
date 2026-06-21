using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Decorator
{
    public class DecoradorFilo : ModificadorBase
    {
        private const double BonusFilo = 0.10;

        public DecoradorFilo(Arma armaInterna) : base(armaInterna) { }

        public override int CalcularDaño() =>
            (int)(armaInterna.CalcularDaño() * (1 + BonusFilo));

        public override string GetDescripcion() =>
            $"{armaInterna.GetDescripcion()} + Filo (+10%) = {CalcularDaño()}";
    }
}