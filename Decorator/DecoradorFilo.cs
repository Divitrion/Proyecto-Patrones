using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Decorator
{
    public class DecoradorFilo : ModificadorBase
    {
        private double multiplicadorDano; // Porcentaje (entre 0 y 1)

        public DecoradorFilo(Arma armaInterna) : base(armaInterna)
        {
            Random random = new Random();
            this.multiplicadorDano = random.Next(1, 50) / 100.0; // Genera un multiplicador de daño aleatorio entre 1% y 50%
        }

        public override int CalcularDaño() =>
            (int)(armaInterna.CalcularDaño() * (1 + multiplicadorDano));

        public override string GetDescripcion() =>
            $"{armaInterna.GetDescripcion()} + Filo (+{(multiplicadorDano * 100):F0}%) = {CalcularDaño()}";
    }
}