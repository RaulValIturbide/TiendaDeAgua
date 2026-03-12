using System.Windows;
using TiendaDeAgua.DTOs;
using TiendaDeAgua.interfaz;
using Utilidades.Recursos;

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
            Sesion.GuardarFrame(framePantalla);
            
            F1000_Login f1000 = new();


            framePantalla.Navigate(f1000);
        }





    }
}