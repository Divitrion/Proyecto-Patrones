using System;
using System.Collections.Generic;
using System.Linq;
using Factory;
using Observer;
using State;
using Decorator;
using Composite;

namespace Facade
{
    /// <summary>
    /// Patrón Facade: punto de entrada único al sistema RPG. Coordina las
    /// fábricas (Factory Method), el bucle de combate (State), los
    /// observadores (Observer), el inventario (Composite) y las armas
    /// (Decorator). Previene el acoplamiento directo entre la interfaz de
    /// usuario y la lógica interna del sistema.
    /// </summary>
    public class ControladorJuego
    {
        // ── Fábricas registradas (Factory Method) ───────────────────────────
        private readonly Dictionary<string, ICreadorPersonaje> fabricas;

        // ── Combatientes ─────────────────────────────────────────────────────
        private readonly List<Personaje> party    = new();
        private readonly List<Personaje> enemigos = new();

        // ── Constructor ──────────────────────────────────────────────────────
        public ControladorJuego()
        {
            fabricas = new Dictionary<string, ICreadorPersonaje>
            {
                { "Luchador",  new CreadorLuchador()  },
                { "Barbaro",   new CreadorBarbaro()   },
                { "Arquero",   new CreadorArquero()   },
                { "Hechicero", new CreadorHechicero() }
            };
        }

        // ── Creación de personajes ───────────────────────────────────────────

        /// <summary>
        /// Crea un personaje usando la fábrica correspondiente al tipo,
        /// suscribe automáticamente los observadores globales y lo agrega
        /// a la Party o a los Enemigos según corresponda.
        /// </summary>
        public Personaje CrearPersonaje(string tipo, string nombre, bool esParty)
        {
            if (!fabricas.ContainsKey(tipo))
                throw new ArgumentException($"Tipo de personaje desconocido: '{tipo}'. " +
                    $"Tipos válidos: {string.Join(", ", fabricas.Keys)}");

            Personaje p = fabricas[tipo].CrearPersonaje(nombre);

            // Suscripción automática a los sistemas observadores (Singleton + Observer)
            p.AgregarObservador(SistemaLogros.Instancia);
            p.AgregarObservador(SistemaLogger.Instancia);

            if (esParty) party.Add(p);
            else         enemigos.Add(p);

            Console.WriteLine($"[SISTEMA] {p} creado y agregado a {(esParty ? "la Party" : "los Enemigos")}.");
            return p;
        }

        // ── Equipamiento ─────────────────────────────────────────────────────

        public void EquiparArma(Personaje p, Arma arma)
        {
            p.EquiparArma(arma);
            Console.WriteLine($"[SISTEMA] {p.Nombre} equipó: {arma.GetDescripcion()}.");
        }

        // ── Inventario ───────────────────────────────────────────────────────

        public void AgregarItemAInventario(Personaje p, Inventario item)
        {
            p.GetInventario().AgregarElemento(item);
            Console.WriteLine($"[SISTEMA] {item.GetNombre()} agregado al inventario de {p.Nombre}.");
        }

        public void MostrarInventario(Personaje p)
        {
            Contenedor inv = p.GetInventario();
            Console.WriteLine($"[INVENTARIO] {p.Nombre}: Peso total={inv.GetPeso()} | Valor total={inv.GetValor()} oro");
            foreach (var e in inv.GetElementos())
                Console.WriteLine($"  → {e.GetNombre()} | {e.GetPeso()} kg | {e.GetValor()} oro");
        }

        // ── Combate ──────────────────────────────────────────────────────────

        /// <summary>
        /// Calcula y aplica el daño de atacante a objetivo. El daño se
        /// compone de la Fuerza del atacante más el daño del arma equipada
        /// (0 si no tiene arma). Valida que ni el atacante ni el objetivo
        /// estén muertos antes de proceder.
        /// </summary>
        public void EjecutarAtaque(Personaje atacante, Personaje objetivo)
        {
            if (atacante.EstaMuerto())
            {
                Console.WriteLine($"[COMBATE] {atacante.Nombre} está muerto y no puede atacar.");
                return;
            }

            if (objetivo.EstaMuerto())
            {
                Console.WriteLine($"[COMBATE] {objetivo.Nombre} ya está muerto.");
                return;
            }

            int dañoArma = atacante.GetArmaEquipada()?.CalcularDaño() ?? 0;
            int dañoTotal = atacante.Fuerza + dañoArma;

            atacante.Atacar(); // Delega al estado actual (muestra mensaje según Estado)
            Console.WriteLine($"[COMBATE] {atacante.Nombre} inflige {dañoTotal} de daño a {objetivo.Nombre}.");
            objetivo.RecibirDaño(dañoTotal);
            Console.WriteLine($"  → {objetivo}");
        }

        /// <summary>
        /// Bucle de combate completo. Ordena a todos los combatientes por
        /// Agilidad (mayor a menor) y ejecuta rondas hasta que la Party o
        /// los Enemigos queden eliminados. Los personajes Muertos se saltean;
        /// los Paralizados pierden su turno. Los de la Party piden acción
        /// al jugador por consola (1/2/3); los Enemigos atacan al primer
        /// vivo de la Party.
        /// </summary>
        public void IniciarCombate()
        {
            if (party.Count == 0 || enemigos.Count == 0)
            {
                Console.WriteLine("[SISTEMA] Se necesitan personajes en la Party y en los Enemigos para combatir.");
                return;
            }

            Console.WriteLine("\n═══════════════════════════════════════");
            Console.WriteLine("           ¡COMBATE INICIADO!          ");
            Console.WriteLine("═══════════════════════════════════════\n");

            int ronda = 1;

            while (party.Any(p => !p.EstaMuerto()) && enemigos.Any(e => !e.EstaMuerto()))
            {
                Console.WriteLine($"\n─── Ronda {ronda} ───────────────────────────");

                // Orden de turno: todos los combatientes ordenados por Agilidad desc
                var turnos = party.Concat(enemigos)
                                  .OrderByDescending(p => p.Agilidad)
                                  .ToList();

                foreach (var combatiente in turnos)
                {
                    // Saltear muertos
                    if (combatiente.EstaMuerto()) continue;

                    Console.WriteLine($"\n► Turno de {combatiente}");

                    // Aplicar efecto de inicio de turno (veneno, parálisis, etc.)
                    combatiente.AplicarEfectoDeTurno();

                    // Si murió por el efecto (ej. veneno), salteamos
                    if (combatiente.EstaMuerto()) continue;

                    // Paralizados no actúan
                    if (combatiente.EstadoActual is EstadoParalizado) continue;

                    if (party.Contains(combatiente))
                        TurnoJugador(combatiente);
                    else
                        TurnoEnemigo(combatiente);
                }

                ronda++;
            }

            Console.WriteLine("\n═══════════════════════════════════════");
            if (party.Any(p => !p.EstaMuerto()))
                Console.WriteLine("  ¡La Party ha ganado el combate!");
            else
                Console.WriteLine("  Los enemigos han derrotado a la Party.");
            Console.WriteLine("═══════════════════════════════════════\n");
        }

        // ── Lógica interna de turnos ──────────────────────────────────────────

        private void TurnoJugador(Personaje combatiente)
        {
            List<Personaje> objetivosValidos = enemigos.Where(e => !e.EstaMuerto()).ToList();
            if (objetivosValidos.Count == 0) return;

            Console.WriteLine("  ¿Qué acción realizás?");
            Console.WriteLine("  [1] Atacar  [2] Defender  [3] Usar Habilidad");

            string input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    Personaje objetivo = ElegirObjetivo(objetivosValidos);
                    EjecutarAtaque(combatiente, objetivo);
                    break;
                case "2":
                    combatiente.Defender();
                    break;
                case "3":
                    combatiente.UsarHabilidad();
                    break;
                default:
                    Console.WriteLine("  Entrada inválida. Se pierde el turno.");
                    break;
            }
        }

        private void TurnoEnemigo(Personaje enemigo)
        {
            Personaje objetivo = party.FirstOrDefault(p => !p.EstaMuerto());
            if (objetivo == null) return;

            EjecutarAtaque(enemigo, objetivo);
        }

        private Personaje ElegirObjetivo(List<Personaje> objetivos)
        {
            if (objetivos.Count == 1) return objetivos[0];

            Console.WriteLine("  Elegí un objetivo:");
            for (int i = 0; i < objetivos.Count; i++)
                Console.WriteLine($"  [{i + 1}] {objetivos[i]}");

            if (int.TryParse(Console.ReadLine()?.Trim(), out int idx) && idx >= 1 && idx <= objetivos.Count)
                return objetivos[idx - 1];

            Console.WriteLine("  Entrada inválida. Se ataca al primero.");
            return objetivos[0];
        }

        // ── Estado general ────────────────────────────────────────────────────

        public void MostrarEstado()
        {
            Console.WriteLine("\n── Party ──────────────────────────────");
            foreach (var p in party)    Console.WriteLine($"  {p}");
            Console.WriteLine("── Enemigos ───────────────────────────");
            foreach (var e in enemigos) Console.WriteLine($"  {e}");
            Console.WriteLine();
        }

        public List<Personaje> GetParty()    => party;
        public List<Personaje> GetEnemigos() => enemigos;
    }
}