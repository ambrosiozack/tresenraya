using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tresenraya.Clases;

namespace tresenraya
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Tablero> tablero = new List<Tablero>();
        String coordX = "abc";
        String coordY = "123";
        Jugador player1 = new Jugador() { Nombre = "Jugador 1", Ficha = "X" };
        Jugador player2 = new Jugador() { Nombre = "Jugador 2", Ficha = "O" };

        public MainWindow()
        {
            InitializeComponent();
            CrearTablero();
            Simular();
        }

        private void CrearTablero()
        {
            

            for (int i = 0; i < 3; i++)
            {
                for (int x = 1; x <= 3; x++)
                {
                    var tab = new Tablero() { X = coordX[i].ToString(), Y = x.ToString() };
                    tablero.Add(tab);
                }
            }
        }

        private void Jugada(Jugador jugador, string x, string y)
        {
            var tab = tablero.First(z => z.X.Equals(x) && z.Y.Equals(y));
            tab.Ficha = jugador.Ficha;
        }

        private void TresEnRaya()
        {
            bool haGanado = false;

            /* Horizontal */
            if(!haGanado)
            {
                for (int i = 0; i < 3; i++)
                {
                    var horizontal = tablero.Where(x => x.X.Equals(coordX[i].ToString()) && x.Ficha != null).ToList();
                    if (horizontal.Count == 3)
                    {
                        Console.WriteLine("Tres en raya horizontal en " + coordX[i].ToString());
                        haGanado = true;
                        break;
                    }
                }
            }


            /* Vertical */
            if (!haGanado)
            {
                for (int i = 0; i < 3; i++)
                {
                    var vertical = tablero.Where(x => x.X.Equals(coordY[i].ToString()) && x.Ficha != null).ToList();
                    if (vertical.Count == 3)
                    {
                        Console.WriteLine("Tres en raya vertical en " + coordY[i].ToString());
                        haGanado = true;
                        break;
                    }
                }
            }


            /* Diagonal */
            if (!haGanado)
            {

            }
        }

        private void Simular()
        {
            Jugada(player1, "b", "1");
            Jugada(player1, "a", "2");
            Jugada(player1, "a", "3");

            TresEnRaya();
        }
    }
}
