using System;
using System.IO;
using Factory;

namespace Observer
{
    public sealed class SistemaLogger : Observador
    {
        private static readonly Lazy<SistemaLogger> _instancia =
            new(() => new SistemaLogger());

        public static SistemaLogger Instancia => _instancia.Value;

        private readonly string rutaArchivo = "log_partida.txt";

        private SistemaLogger()
        {
            File.AppendAllText(rutaArchivo, $"=== Log de partida iniciado: {DateTime.Now} ==={Environment.NewLine}");
        }

        public override void Actualizar(Personaje p)
        {
            string entrada = p.EstaMuerto()
                ? $"[{DateTime.Now:HH:mm:ss}] MUERTE: {p.Nombre} ({p.GetType().Name}) ha muerto en combate."
                : $"[{DateTime.Now:HH:mm:ss}] EVENTO: {p.Nombre} ({p.GetType().Name}) - HP actual: {p.Vida}/{p.VidaMaxima}.";

            Console.WriteLine($"[LOGGER] {entrada}");
            File.AppendAllText(rutaArchivo, entrada + Environment.NewLine);
        }
    }
}