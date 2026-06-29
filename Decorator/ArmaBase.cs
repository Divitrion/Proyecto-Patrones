using System;

namespace Decorator
{
    public class ArmaBase : Arma
    {
        private int danoBase;
        private string nombre;

        public ArmaBase(int danoBase, string nombre)
        {
            this.danoBase = danoBase;
            this.nombre = nombre;
        }

        public override int CalcularDano() => danoBase;

        public override string GetDescripcion() => $"{nombre} (Daño base: {danoBase})";

        // No aplica ningun efecto adicional, por lo que no se sobrescribe el método AplicarEfecto y corta la cadena de responsabilidad. 
        // Esto significa que si se llama a AplicarEfecto en un arma base, no se realizará ninguna acción adicional.
    }
}