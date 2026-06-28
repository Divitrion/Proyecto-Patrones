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

        // No aplica ningun efecto adicional, por lo que no se sobrescribe el método AplicarEfecto y corta la cadena de responsabilidad. 
        // Esto significa que si se llama a AplicarEfecto en un arma base, no se realizará ninguna acción adicional.
    }
}