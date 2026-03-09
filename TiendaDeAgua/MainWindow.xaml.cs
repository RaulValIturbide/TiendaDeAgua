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
        public bool proveedoresActivo = false;

        public MainWindow()
        {
            InitializeComponent();

            dtgDatosUsuario.DataContext = _UsuarioDTO;
            ComunServicioAiron.Conectar.ActivarConexion();
            tbItemPaginaPrincipal.Visibility = Visibility.Collapsed;

            TarjetaProveedor.btnTarjeta += btnProveedor_Click;
        }

        /// <summary>
        /// Esto se activa cuando el usuario entra con una cuenta existente en la BBDD
        /// </summary>
        private void CargarAcceso()
        {
            tbItemLogin.Visibility = Visibility.Collapsed;
            tbItemPaginaPrincipal.Visibility = Visibility.Visible;
            tabControlPrincipal.SelectedItem = tbItemPaginaPrincipal;
            lbSaludo.Content = $"HOLA {_UsuarioDTO.Nombre} 👋🏻!!{Environment.NewLine}¿Qué quieres hacer hoy?";

            CargarTarjetasMenu();
        }

        #region MetodoPrivado MenuPrincipal
        private void CargarTarjetasMenu()
        {
            TarjetaDTO proveedores = new TarjetaDTO() { Nombre = "PROVEEDORES" };
            TarjetaDTO inventario = new TarjetaDTO() { Nombre = "INVENTARIO" };
            TarjetaDTO clientes = new TarjetaDTO() { Nombre = "CLIENTES" };
            
            
            TarjetaProveedor.DataContext = proveedores;
            TarjetaInventario.DataContext = inventario;
            TarjetaCliente.DataContext = clientes;

            if (_UsuarioDTO.ModoEntrada == 1)
            {
                //Esto lo hacemos si es el administrador
                TarjetaAdministrador.Visibility = Visibility.Visible;
                TarjetaDTO administrador = new TarjetaDTO() { Nombre = "ADMINISTRADOR" };
                TarjetaAdministrador.DataContext = administrador;
            }
            else
            {
                TarjetaAdministrador.Visibility = Visibility.Collapsed;
            }

        }

        
        #endregion

      

        #region Eventos

        #region Eventos Login
        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            ResultadoDTO res = new();
            
            res = UsuarioBD.UsuarioExistente(_UsuarioDTO);

            if(res.codigoError == 0)
            {
                _UsuarioDTO = UsuarioBD.AccesoUsuario(_UsuarioDTO); //Para cargar el modo de entrada tal vez mejorar si da tiempo(redundante)
                CargarAcceso();
            }
            else
            {
                HerramientaVentana.MostrarError(res);
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            ResultadoDTO res = UsuarioBD.AltaFila(_UsuarioDTO);

            if(res.codigoError == 0)
            {
                HerramientaVentana.Show(res.mensajeInformacion);
            }
            else
            {
                HerramientaVentana.MostrarError(res);
            }
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
            cajaContra.Password = txtContra.Text;
        }

        #endregion

        #region Eventos MenuPrincipal

        private void btnProveedor_Click(object sender,EventArgs e)
        {
            
            if (!proveedoresActivo)
            {
                tbItemProveedor.Visibility = Visibility.Visible;
                TiendaDeAgua.interfaz.Proveedor proveedor = new interfaz.Proveedor();
                frameProveedor.Navigate(proveedor);
                tabControlPrincipal.SelectedItem = tbItemProveedor;
                proveedoresActivo = true;
            }
            else
            {
                tabControlPrincipal.SelectedItem = tbItemProveedor;
            }
        }
        #endregion


        #endregion
    }

}