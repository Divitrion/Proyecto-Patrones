using System;
using Factory;
using Observer;
using Decorator;
using Composite;
using State;
using Facade;

class Program
{
    static void Main(string[] args)
    {
        var juego = new ControladorJuego();

        // ── Módulo A: Factory Method ─────────────────────────────────────
        Console.WriteLine("=== FACTORY METHOD ===");
        Personaje guerrero  = juego.CrearPersonaje("Luchador",  "Aldric",  esParty: true);
        Personaje arquera   = juego.CrearPersonaje("Arquero",   "Lyra",    esParty: true);
        Personaje enemigo1  = juego.CrearPersonaje("Barbaro",   "Gruk",    esParty: false);
        Personaje enemigo2  = juego.CrearPersonaje("Hechicero", "Malakar", esParty: false);

        // ── Módulo D: Decorator (armas con encantamientos apilados) ──────
        Console.WriteLine("\n=== DECORATOR ===");
        Arma espadaFuego = new DecoradorFuego(new DecoradorFilo(new ArmaBase(20, "Espada Corta")));
        Arma arcoVeneno  = new DecoradorVeneno(new ArmaBase(18, "Arco Largo"));

        juego.EquiparArma(guerrero, espadaFuego);
        juego.EquiparArma(arquera,  arcoVeneno);
        Console.WriteLine($"  Espada de Aldric: {espadaFuego.GetDescripcion()}");
        Console.WriteLine($"  Arco de Lyra:     {arcoVeneno.GetDescripcion()}");

        // ── Módulo C: Composite (inventario jerárquico) ──────────────────
        Console.WriteLine("\n=== COMPOSITE ===");
        var bolsa = new Contenedor("Bolsa de cuero");
        bolsa.AgregarElemento(new Item("Poción de vida",  1, 50));
        bolsa.AgregarElemento(new Item("Gema rubí",       0, 200));

        var cofre = new Contenedor("Cofre pequeño");
        cofre.AgregarElemento(new Item("Llave de mazmorra", 1, 30));
        cofre.AgregarElemento(bolsa); // sub-contenedor dentro del cofre

        juego.AgregarItemAInventario(guerrero, cofre);
        juego.MostrarInventario(guerrero);

        // ── Módulo B: State (transición de estados) ──────────────────────
        Console.WriteLine("\n=== STATE ===");
        Console.WriteLine($"  Estado inicial: {guerrero.EstadoActual.GetType().Name}");
        guerrero.CambiarEstado(EstadoEnvenenado.Instancia);
        Console.WriteLine($"  Tras envenenar: {guerrero.EstadoActual.GetType().Name}");
        guerrero.AplicarEfectoDeTurno(); // pierde vida por veneno
        guerrero.CambiarEstado(EstadoSaludable.Instancia);
        Console.WriteLine($"  Curado: {guerrero.EstadoActual.GetType().Name}");

        // ── Módulo E: Observer (automático al morir) ─────────────────────
        Console.WriteLine("\n=== OBSERVER (demo: forzar muerte) ===");
        Personaje dummy = juego.CrearPersonaje("Hechicero", "DummyMago", esParty: false);
        dummy.RecibirDaño(9999); // dispara NotificarObservadores → Logger + Logros

        // ── Facade: combate completo interactivo ─────────────────────────
        Console.WriteLine("\n=== FACADE: COMBATE ===");
        juego.MostrarEstado();
        juego.IniciarCombate();
    }
}