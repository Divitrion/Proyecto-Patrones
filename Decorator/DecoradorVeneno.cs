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
        private const int BonusVeneno = 10;
        private const double ChanceEnvenenar  = 0.50;
        private const double ChanceParalizar = 0.10;

        private static readonly Random rng = new();

        public DecoradorVeneno(Arma armaInterna) : base(armaInterna) { }

        public override int CalcularDaño() => armaInterna.CalcularDaño() + BonusVeneno;

        public override string GetDescripcion() =>
            $"{armaInterna.GetDescripcion()} + Veneno (+{BonusVeneno}) = {CalcularDaño()}";
        
        public override void AplicarEfecto(Personaje objetivo)
        {
            armaInterna.AplicarEfecto(objetivo);

            if (objetivo.EstaMuerto()) return;
            if (objetivo.EstadoActual is EstadoEnvenenado) return;

            if (rng.NextDouble() < ChanceEnvenenar)
            {
                objetivo.CambiarEstado(EstadoEnvenenado.ObtenerInstancia());
                Console.WriteLine($"  {objetivo.Nombre} ha sido envenenado por el golpe.");
                return;
            }
            else
            {
                Console.WriteLine($"  {objetivo.Nombre} resistió el veneno.");
            }

            if (rng.NextDouble() < ChanceParalizar)
            {
                objetivo.CambiarEstado(EstadoParalizado.ObtenerInstancia());
                Console.WriteLine($"  {objetivo.Nombre} ha sido paralizado por el golpe.");
                return;
            }
            else
            {
                Console.WriteLine($"  {objetivo.Nombre} resistió el efecto paralizante.");
            }
        }
    }
}