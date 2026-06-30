using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Composite
{
    public class Contenedor : Inventario
    {
        private string nombre;
        private List<Inventario> elementos;

        public Contenedor(string nombre)
        {
            this.nombre = nombre;
            this.elementos = new List<Inventario>();
        }

        public void AgregarElemento(Inventario elemento)
        {
            this.elementos.Add(elemento);
        }

        public void QuitarElemento(Inventario elemento)
        {
            this.elementos.Remove(elemento);
        }

        public int GetPeso()
        {
            return elementos.Sum(e => e.GetPeso());
        }

        public int GetValor()
        {
            return elementos.Sum(e => e.GetValor());
        }

        public string GetNombre()
        {
            return nombre;
        }

        public void MostrarContenido()
        {
            Console.WriteLine($"→ Contenedor: {nombre}, (Peso total: {GetPeso()}, Valor total: {GetValor()})");
            foreach (var elemento in elementos)
            {
                elemento.MostrarContenido();
            }
        }

        public List<Inventario> GetElementos() => elementos;
    }
}