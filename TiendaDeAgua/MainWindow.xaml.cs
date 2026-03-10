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
using TiendaDeAgua.interfaz;
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
            TarjetaAdministrador.btnTarjeta += btnAdministrador_Click;
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
                tbItemProveedor.Visibility = Visibility.Visible;
                F1002_Proveedor proveedor = new F1002_Proveedor();
                frameProveedor.Navigate(proveedor);
                tabControlPrincipal.SelectedItem = tbItemProveedor;


        }

        private void btnAdministrador_Click(object sender,EventArgs e)
        {
            tbItemAdministrador.Visibility = Visibility.Visible;
            F1001_Usuarios f1001 = new F1001_Usuarios();
            frameAdministrador.Navigate(f1001);
            tabControlPrincipal.SelectedItem = tbItemAdministrador;
        }
        /// <summary>
        /// Usamos el click derecho para "cerrar" una pestaña del tabItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItem_RightClick(object sender,EventArgs e)
        {
            var tab = (TabItem)sender;
            tab.Visibility = Visibility.Collapsed;
            //Aquí miramos si el usuario ha cerrado la pagina que está mirando
            //Si es así le devolvemos a la pagina principal
            var tabSeleccionado = tabControlPrincipal.SelectedItem;
            if(tabSeleccionado == tab)
            {
                tabControlPrincipal.SelectedItem = tbItemPaginaPrincipal;
            }
        }
        #endregion

        #endregion

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            tabControlPrincipal.SelectedItem = tbItemLogin;

            foreach(TabItem t in tabControlPrincipal.Items)
            {
                if(t != tbItemLogin)
                {
                    t.Visibility = Visibility.Collapsed;
                }
            }
        }
    }

}