using Microsoft.Data.Sqlite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TiendaDeAgua.DTOs;
using TiendaDeAgua.Tablas;
using Utilidades.Recursos;

namespace TiendaDeAgua
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static UsuarioDTO _UsuarioDTO = new();

        public MainWindow()
        {
            InitializeComponent();

            dtgDatosUsuario.DataContext = _UsuarioDTO;
            ComunServicioAiron.Conectar.ActivarConexion();

        }
        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            ResultadoDTO res = UsuarioBD.AltaFila(_UsuarioDTO);

            if(res.codigoError == 0)
            {
                VentanaError v = new VentanaError(res.mensajeInformacion);
                v.Show();
            }
            else
            {
                VentanaError v = new VentanaError(res);
                v.Show();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _UsuarioDTO.Contrasenya = cajaContra.Password;
        }
    }
}