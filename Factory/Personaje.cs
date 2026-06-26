using System;
using System.Collections.Generic;
using Observer;
using Composite;
using Decorator;
using State;

namespace Factory
{
    /// Clase base abstracta de todo combatiente. Nunca se instancia
    /// directamente: las subclases concretas (Luchador, Barbaro, Arquero,
    /// Hechicero) son creadas por sus respectivos CreadorX (Factory Method)
    /// y solo definen atributos base vía el constructor de esta clase.
    ///
    /// Atacar/Defender/UsarHabilidad/AplicarEfectoDeTurno son CONCRETOS:
    /// delegan siempre al estado actual (patrón State), nunca se
    /// reimplementan en las subclases.
    
    public abstract class Personaje
    {
        public string Nombre        { get; }
        public int    Fuerza        { get; }
        public int    Percepcion    { get; }
        public int    Resistencia   { get; }
        public int    Carisma       { get; }
        public int    Inteligencia  { get; }
        public int    Agilidad      { get; }
        public int    Suerte        { get; }
        public int    VidaMaxima    { get; }

        private int _vida;
        public int Vida
        {
            get => _vida;
            private set
            {
                // Un personaje muerto bloquea cualquier intento de modificar
                // sus estadísticas vitales (informe, Módulo B).
                if (EstadoActual is EstadoMuerto) return;

                _vida = Math.Max(0, value);
                if (_vida == 0 && !(EstadoActual is EstadoMuerto))
                {
                    CambiarEstado(EstadoMuerto.ObtenerInstancia());
                    NotificarObservadores();
                }
            }
        }

        // Patrón State
        public IEstadoPersonaje EstadoActual { get; private set; }

        public void CambiarEstado(IEstadoPersonaje nuevoEstado) => EstadoActual = nuevoEstado;

        // Patrón Observer
        private readonly List<Observador> observadores;

        public void AgregarObservador(Observador observador)    => observadores.Add(observador);
        public void EliminarObservador(Observador observador)   => observadores.Remove(observador);
        public void NotificarObservadores()
        {
            foreach (var obs in observadores)
                obs.Actualizar(this);
        }

        // Patrón Composite (inventario) y Decorator (arma)
        protected Contenedor inventario;
        protected Arma armaEquipada;

        public Contenedor GetInventario()    => inventario;
        public Arma       GetArmaEquipada()  => armaEquipada;

        public void EquiparArma(Arma arma)
        {
            armaEquipada = arma;
            NotificarObservadores();
        }

        // Constructor (llamado por las subclases concretas)
        protected Personaje(string nombre, int fuerza, int percepcion, int resistencia,
                             int carisma, int inteligencia, int agilidad, int suerte, int vidaMaxima)
        {
            Nombre        = nombre;
            Fuerza        = fuerza;
            Percepcion    = percepcion;
            Resistencia   = resistencia;
            Carisma       = carisma;
            Inteligencia  = inteligencia;
            Agilidad      = agilidad;
            Suerte        = suerte;
            VidaMaxima    = vidaMaxima;

            _vida = vidaMaxima;
            EstadoActual  = EstadoSaludable.ObtenerInstancia();
            observadores  = new List<Observador>();
            inventario    = new Contenedor(nombre + " - Inventario");
            armaEquipada  = null;
        }

        // Acciones de combate (delegadas al estado actual, NO reimplementar) 
        public void Atacar()                 => EstadoActual.Atacar(this);
        public void Defender()               => EstadoActual.Defender(this);
        public void UsarHabilidad()          => EstadoActual.UsarHabilidad(this);
        public void AplicarEfectoDeTurno()   => EstadoActual.AplicarEfecto(this);

        // Resta vida de forma uniforme; dispara notificación si llega a 0.
        public void RecibirDaño(int daño)
        {
            Random random = new Random();

            int roll = random.Next(1, 21);

            if (roll <= 15 && roll > 10)
            {
                Console.WriteLine($"[{Nombre}] Fue evenenado");
                CambiarEstado(EstadoEnvenenado.ObtenerInstancia());
            }

            else if (roll > 15 && roll <= 20)
            {
                Console.WriteLine($"[{Nombre}] Fue paralizado");
                CambiarEstado(EstadoParalizado.ObtenerInstancia());
            }

            Vida -= daño;
            if (Vida > 0)
                NotificarObservadores();
        }

        public void AgregarItemAInventario(Inventario item) => inventario.AgregarElemento(item);
        public void QuitarItemDeInventario(Inventario item)  => inventario.QuitarElemento(item);
        
        public bool EstaMuerto() => EstadoActual is EstadoMuerto;

        public override string ToString() =>
            $"[{GetType().Name}] {Nombre} | HP: {Vida}/{VidaMaxima} | Estado: {EstadoActual.GetType().Name}";
    }
}