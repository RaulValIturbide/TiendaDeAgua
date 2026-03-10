using System.Windows;
using System.Windows.Controls;
using TiendaDeAgua.DTOs;
using TiendaDeAgua.Tablas;
using Utilidades.Recursos;

namespace TiendaDeAgua.interfaz
{
    /// <summary>
    /// Lógica de interacción para F1000_Login.xaml
    /// </summary>
    public partial class F1000_Login : Page
    {
        UsuarioDTO _UsuarioDTO = new();
        Frame _PantallaActiva;
        public F1000_Login(Frame? PantallaActiva)
        {
            InitializeComponent();
            dtgDatosUsuario.DataContext = _UsuarioDTO;
            _PantallaActiva = PantallaActiva;
        }

        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            var usuario = UsuarioBD.AccesoUsuario(_UsuarioDTO);

            if(usuario != null)
            {
                //El usuario existe y por lo tanto se le permite pasar
                _UsuarioDTO = usuario;
                F1003_PaginaPrincipal f1003 = new(_UsuarioDTO,_PantallaActiva);
                _PantallaActiva.Navigate(f1003);
                

            }
            else
            {
                ResultadoDTO res = new();
                res.mensajeInformacion = $"Usuario no encontrado.¿Aún no estás registrado? pulsa el botón registrar.";
                res.codigoError = 101;
                HerramientaVentana.MostrarError(res);
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _UsuarioDTO.Contrasenya = cajaContra.Password;
            txtContra.Text = cajaContra.Password;
        }

        private void ckBoxMostrarContra_Checked(object sender, RoutedEventArgs e)
        {
            txtContra.Visibility = Visibility.Visible;
            cajaContra.Visibility = Visibility.Collapsed;
        }

        private void ckBoxMostrarContra_Unchecked(object sender, RoutedEventArgs e)
        {
            txtContra.Visibility = Visibility.Collapsed;
            cajaContra.Visibility = Visibility.Visible;
        }
    }
}
