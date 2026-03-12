using System.Windows;
using TiendaDeAgua.DTOs;
using TiendaDeAgua.interfaz;

namespace TiendaDeAgua
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static UsuarioDTO _UsuarioDTO = new();
        public bool proveedoresActivo = false;

        public MainWindow()
        {
            InitializeComponent();
            ComunServicioAiron.Conectar.ActivarConexion();
            F1000_Login f1000 = new(framePantalla);

            framePantalla.Navigate(f1000);
        }





    }
}