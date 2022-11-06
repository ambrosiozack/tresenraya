using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tresenraya.Models;

namespace tresenraya.ViewModels
{
    public class TresEnRayaViewModel : ViewModelBase
    {
        #region Propiedades
        string nombreJugador;
        public string NombreJugador
        {
            get { return nombreJugador; }   
            set 
            { 
                nombreJugador = value; 
                NotifyPropertyChanged("NombreJugador");
            }
        }

        string mensajeTurno;
        public string MensajeTurno
        {
            get { return mensajeTurno; }
            set
            {
                mensajeTurno = value;
                NotifyPropertyChanged("MensajeTurno");
            }
        }

        public string[] ContenidoBoton { get; private set; }
        Models.TresEnRaya model;
        #endregion

        #region Commands
        public ICommand[] PulsadoBotonCommand { get; private set; }
        public ICommand ReiniciarCommand { get; private set; }
        #endregion

        public TresEnRayaViewModel(Models.TresEnRaya model)
        {
            this.model = model;
            model.TurnoCambiado += CuandoTurnoCambiado;
            model.JuegoReiniciado += CuandoJuegoReiniciado;
            model.FichaColocada += CuandoFichaColocada;
            model.JuegoFinalizado += CuandoJuegoFinalizado;

            ReiniciarCommand = new RelayCommand(model.ReiniciarJuego, () => true);
            PulsadoBotonCommand = new RelayCommand[9];

            for (int y = 0; y < 3; ++y)
            {
                int j = y;
                for (int x = 0; x < 3; ++x)
                {
                    int i = x;
                    PulsadoBotonCommand[y * 3 + x] = new RelayCommand(() => Pulsar(i, j), () => PuedoPulsar(i, j));
                }
            }

            ContenidoBoton = new string[10];
            model.ReiniciarJuego();
        }

        bool PuedoPulsar(int x, int y)
        {
            return model.PuedoPonerFichaEn(x, y);
        }
        void Pulsar(int x, int y)
        {
            model.PonerFichaEn(x, y);
        }

        void CuandoJuegoReiniciado(Marca jugador)
        {
            MensajeTurno = "Turno jugador:";
            NombreJugador = (jugador == Marca.Cruz) ? "X" : "O";

            for (int i = 0; i < 9; i++)
            {
                ContenidoBoton[i] = "";
                NotifyPropertyChanged("ContenidoBoton");
            }
        }

        void CuandoFichaColocada(int x, int y, Marca jugador)
        {
            ContenidoBoton[y * 3 + x] = (jugador == Marca.Cruz) ? "X" : "O";
            NotifyPropertyChanged("ContenidoBoton");
        }

        void CuandoJuegoFinalizado(Marca jugador)
        {
            MensajeTurno = "";
            switch (jugador)
            {
                case Marca.Nada:
                    NombreJugador = "¡Empate!";
                    break;
                case Marca.Cruz:
                    NombreJugador = "¡X gana!";
                    break;
                case Marca.Circulo:
                    NombreJugador = "¡O gana!";
                    break;
                default:
                    break;
            }
        }

        void CuandoTurnoCambiado(Marca jugador)
        {
            NombreJugador = (jugador == Marca.Cruz) ? "X" : "O";
        }
    }
}
