using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using State;
using Factory;

namespace Decorator
{
    public abstract class ModificadorBase : Arma
    {
        protected readonly Arma armaInterna;

        protected ModificadorBase(Arma armaInterna)
        {
            this.armaInterna = armaInterna;
        }

        public override void AplicarEfecto(Personaje objetivo) =>
            armaInterna.AplicarEfecto(objetivo); // Por defecto, los decoradores delegan la aplicación del efecto al arma interna
    }
}