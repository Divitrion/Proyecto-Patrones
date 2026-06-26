using System;
using Factory;

namespace Observer
{
    public sealed class SistemaLogros : Observador
    {
        private static readonly Lazy<SistemaLogros> _instancia =
            new(() => new SistemaLogros());

        private static SistemaLogros Instancia => _instancia.Value;

        public static SistemaLogros ObtenerInstancia() => Instancia;

        private SistemaLogros() { }

        public override void Actualizar(Personaje p)
        {
            Console.WriteLine($"[LOGROS] Evento de {p.Nombre} recibido. Verificando condiciones de logro...");

            if (p.EstaMuerto())
            {
                Console.WriteLine($"[LOGROS] {p.Nombre} ha caído. Evaluando logros de combate y condecoraciones.");
            }
        }
    }
}