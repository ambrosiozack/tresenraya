using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tresenraya.Models
{
    public enum Marca { Nada, Cruz, Circulo };
    public class TresEnRaya
    {
        /* Estas variabes indican:
         * Nuevo jugador
         * Jugador inicial
         * Jugador ganador
         * Que se ha colocado una ficha en una posición determinada
         */
        public event Action<Marca> TurnoCambiado;
        public event Action<Marca> JuegoReiniciado;
        public event Action<Marca> JuegoFinalizado;
        public event Action<int, int, Marca> FichaColocada;

        Marca[,] tablero;
        Marca jugadorActual;
        Marca jugadorGanador;

        public TresEnRaya()
        {
            TurnoCambiado += (p) => { };
            JuegoReiniciado += (p) => { };
            JuegoFinalizado += (p) => { };
            FichaColocada += (i, j, p) => { };
            ReiniciarJuego();
        }

        public void ReiniciarJuego()
        {
            tablero = new Marca[3, 3];
            jugadorGanador = Marca.Nada;
            jugadorActual = Marca.Cruz;
            JuegoReiniciado(jugadorActual);
        }

        public bool PonerFichaEn(int x, int y)
        {
            if (jugadorGanador != Marca.Nada) return false;
            if (tablero[x, y] != Marca.Nada) return false;

            tablero[x, y] = jugadorActual;
            FichaColocada(x, y, jugadorActual);
            if (HayGanador(x, y))
            {
                jugadorGanador = jugadorActual;
                JuegoFinalizado(jugadorActual);
            }
            else if (EstaTableroLleno())
                JuegoFinalizado(Marca.Nada);
            else
                CambiarTurno();
            return true;
        }

        public bool PuedoPonerFichaEn(int x, int y)
        {
            return jugadorGanador == Marca.Nada && tablero[x, y] == Marca.Nada;
        }

        void CambiarTurno()
        {
            if (jugadorActual == Marca.Circulo)
                jugadorActual = Marca.Cruz;
            else
                jugadorActual = Marca.Circulo;
            TurnoCambiado(jugadorActual);
        }

        bool HayGanador(int x, int y)
        {
            var v = tablero[x, y];

            if (v == tablero[0, y] && tablero[0, y] == tablero[1, y]
                && tablero[1, y] == tablero[2, y])
                return true;

            if (v == tablero[x, 0] && tablero[x,0] == tablero[x, 1]
                && tablero[x, 1] == tablero[x, 2])
                return true;

            if (v == tablero[0, 0] && tablero[0, 0] == tablero[1, 1]
                && tablero[1, 1] == tablero[2, 2])
                return true;

            if (v == tablero[0, 2] && tablero[0, 2] == tablero[1, 1]
                && tablero[1, 1] == tablero[2, 0])
                return true;

            return false;
        }

        bool EstaTableroLleno()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    if (tablero[x, y] == Marca.Nada) return false;
            return true;
        }
    }
}
