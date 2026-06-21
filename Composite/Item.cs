using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventario;

namespace Composite
{
    public class Item : Inventario
    {
        private readonly string nombre;
        private readonly int peso;
        private readonly int valor;

        public Item(string nombre, int peso, int valor)
        {
            this.nombre = nombre;
            this.peso   = peso;
            this.valor  = valor;
        }

        public int GetPeso()  => peso;
        public int GetValor() => valor;
        public string GetNombre() => nombre;
    }
}