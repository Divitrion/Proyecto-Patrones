using System;

namespace Decorator
{
    public class ArmaBase : Arma
    {
        private int dañoBase;
        private string nombre;

        public ArmaBase(int dañoBase, string nombre)
        {
            this.dañoBase = dañoBase;
            this.nombre = nombre;
        }

        public override int CalcularDaño()
        {
            return dañoBase;
        }

        public override string GetDescripcion()
        {
            return nombre;
        }
    }
}