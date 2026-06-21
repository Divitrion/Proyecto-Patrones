using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factory;

namespace Facade
{
    public class ControladorJuego
    {
        private Personaje *jugador;
        private Personaje *enemigo;
        private List<CreadorPersonaje> *creadores;

        private List<Observador> *sistemasExternos;

        public void IniciarPartida(string jugador, string enemigo)
        {
            // Crear personajes usando Factory
            CreadorPersonaje creadorJugador = creadores.FirstOrDefault(c => c.GetTipoCreador() == jugador);
            CreadorPersonaje creadorEnemigo = creadores.FirstOrDefault(c => c.GetTipoCreador() == enemigo);

            if (creadorJugador != null && creadorEnemigo != null)
            {
                this.jugador = creadorJugador.CreadorPersonaje();
                this.enemigo = creadorEnemigo.CreadorPersonaje();
            }

            // Suscribir sistemas externos a los personajes
            foreach (var sistema in sistemasExternos)
            {
                jugador.AgregarObservador(sistema);
                enemigo.AgregarObservador(sistema);
            }
        }

        public void EjecutarTurno()
        {
            // Lógica de combate entre jugador y enemigo
            // Notificar a los sistemas externos sobre el estado del combate
            foreach (var sistema in sistemasExternos)
            {
                sistema.Actualizar(jugador);
                sistema.Actualizar(enemigo);
            }
        }

        public void EquiparMejoraArma(string mejora)
        {
            // Lógica para equipar mejoras al arma del jugador
            // Notificar a los sistemas externos sobre el cambio en el arma
            foreach (var sistema in sistemasExternos)
            {
                sistema.Actualizar(jugador);
            }
        }

        public void MostrarInventario()
        {
            // Lógica para mostrar el inventario del jugador
        }

        public void AgregarItemInventario(string nombre, int peso, int valor)
        {
            // Lógica para agregar un item al inventario del jugador
            // Notificar a los sistemas externos sobre el cambio en el inventario
            foreach (var sistema in sistemasExternos)
            {
                sistema.Actualizar(jugador);
            }
        }

    }
}