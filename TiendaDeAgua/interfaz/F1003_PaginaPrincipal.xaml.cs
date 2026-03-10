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
using TiendaDeAgua.DTOs;

namespace TiendaDeAgua.interfaz
{
    /// <summary>
    /// Lógica de interacción para F1003_PaginaPrincipal.xaml
    /// </summary>
    public partial class F1003_PaginaPrincipal : Page
    {
        UsuarioDTO _UsuarioDTO = new();
        Frame _PaginaActiva = new();
        public F1003_PaginaPrincipal(UsuarioDTO usuarioActivo,Frame PaginaActiva)
        {
            InitializeComponent();
            _UsuarioDTO = usuarioActivo; //Cargamos el usuario
            _PaginaActiva = PaginaActiva;

            CargarTarjetasMenu(); //Cargamos las tarjetillas
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            F1000_Login login = new(_PaginaActiva);
            _PaginaActiva.Navigate(login);
            
        }

        /// <summary>
        /// Este metodo sirve para mostra las tarjetas que funcionan como iu principal, si es admin se mostrará 
        /// una tarjeta administrador, es escalable.
        /// </summary>
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

        private void TarjetaAdministrador_btnTarjeta(object sender, EventArgs e)
        {
            F1001_Usuarios f1001 = new(_UsuarioDTO,_PaginaActiva);
            _PaginaActiva.Navigate(f1001);
            
        }

        private void TarjetaCliente_btnTarjeta(object sender, EventArgs e)
        {

        }

        private void TarjetaInventario_btnTarjeta(object sender, EventArgs e)
        {

        }
    }
}
