using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using State;
using Factory;

namespace Decorator
{
    public class DecoradorVeneno : ModificadorBase
    {
        private int bonusVeneno;

        private static readonly Random rng = new();

        public DecoradorVeneno(Arma armaInterna) : base(armaInterna)
        {
            Random random = new Random();
            this.bonusVeneno = random.Next(2, 10); // Genera un bonus de daño aleatorio entre 2 y 10
        }

        public override int CalcularDano() => armaInterna.CalcularDano() + bonusVeneno;

        public override string GetDescripcion() =>
            $"{armaInterna.GetDescripcion()} + Veneno (+{bonusVeneno}) = {CalcularDano()}";
        
        public override void AplicarEfecto(Personaje objetivo)
        {
            armaInterna.AplicarEfecto(objetivo);

            if (objetivo.EstadoActual is not EstadoSaludable) return;

            int probabilidad = rng.Next(1, 20); // Genera un número aleatorio entre 1 y 20

            if (probabilidad >= 8 && probabilidad <= 17)
            {
                objetivo.CambiarEstado(EstadoEnvenenado.ObtenerInstancia());
                Console.WriteLine($"  {objetivo.Nombre} ha sido envenenado por el golpe.");
                return;
            }
            else if (probabilidad >= 18)
            {
                objetivo.CambiarEstado(EstadoParalizado.ObtenerInstancia());
                Console.WriteLine($"  {objetivo.Nombre} ha sido paralizado por el golpe.");
                return;
            }
            else
            {
                Console.WriteLine($"  {objetivo.Nombre} resistió los efectos del veneno.");
            }
        }
    }
}