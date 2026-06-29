using System;
using System.IO;
using Factory;

namespace Observer
{
    public sealed class SistemaLogger : Observador
    {
        private static readonly Lazy<SistemaLogger> _instancia =
            new(() => new SistemaLogger());

        private static SistemaLogger Instancia => _instancia.Value;

        public static SistemaLogger ObtenerInstancia() => Instancia;

        private readonly string rutaArchivo = "log_partida.txt";

        private SistemaLogger()
        {
            File.AppendAllText(rutaArchivo, $"=== Log de partida iniciado: {DateTime.Now} ==={Environment.NewLine}");
        }

        public override void Actualizar(string evento)
        {
            string entrada = $"[{DateTime.Now:HH:mm:ss}] {evento}";

            Console.WriteLine($"[LOGGER] {entrada}");
            File.AppendAllText(rutaArchivo, entrada + Environment.NewLine);
        }
    }
}