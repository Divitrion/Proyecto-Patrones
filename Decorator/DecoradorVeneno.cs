using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Decorator
{
    public class DecoradorVeneno : ModificadorBase
    {
        private const int BonusVeneno = 10;

        public DecoradorVeneno(Arma armaInterna) : base(armaInterna) { }

        public override int CalcularDaño() => armaInterna.CalcularDaño() + BonusVeneno;

        public override string GetDescripcion() =>
            $"{armaInterna.GetDescripcion()} + Veneno (+{BonusVeneno}) = {CalcularDaño()}";
    }
}