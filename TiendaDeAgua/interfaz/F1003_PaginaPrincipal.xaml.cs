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
using Utilidades.Recursos;

namespace TiendaDeAgua.interfaz
{
    /// <summary>
    /// Lógica de interacción para F1003_PaginaPrincipal.xaml
    /// </summary>
    public partial class F1003_PaginaPrincipal : Page
    {
        int ModoEntrada;
        
        public F1003_PaginaPrincipal()
        {
            InitializeComponent();
           
            Sesion.llaves.TryGetValue("ModoEntrada", out string? modoEntrada); //Recibimos el modoEntrada del usuario activo
            ModoEntrada = Convert.ToInt32(modoEntrada);

            Sesion.llaves.TryGetValue("Nombre", out string? NombreUsuario);
            lbBienvenida.Content += NombreUsuario;

            CargarTarjetasMenu(); //Cargamos las tarjetillas

   

        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            F1000_Login login = new();
            Sesion.RenovarLlaves();
            Sesion.GestorPantalla(login);
            
        }

        /// <summary>
        /// Este metodo sirve para mostra las tarjetas que funcionan como iu principal, si es admin se mostrará 
        /// una tarjeta administrador, es escalable.
        /// </summary>
        private void CargarTarjetasMenu()
        {
            TarjetaDTO proveedores = new TarjetaDTO() { Nombre = "PEDIDOS" };
            TarjetaDTO inventario = new TarjetaDTO() { Nombre = "INVENTARIO" };
            TarjetaDTO clientes = new TarjetaDTO() { Nombre = "CLIENTES" };


            TarjetaPedido.DataContext = proveedores;
            TarjetaInventario.DataContext = inventario;
            TarjetaCliente.DataContext = clientes;

            if (ModoEntrada == 1)
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

        //TARJETAS DE ACCESO A LOS FORMULARIOS DE MANTENIMIENTO:
        private void TarjetaAdministrador_btnTarjeta(object sender, EventArgs e)
        {
            F1001_Usuarios f1001 = new();
            Sesion.GestorPantalla(f1001);
            
        }

        private void TarjetaCliente_btnTarjeta(object sender, EventArgs e)
        {
            F1005_Cliente f1005 = new();
            Sesion.GestorPantalla(f1005);
        }

        private void TarjetaPedido_btnTarjeta(object sender,EventArgs e)
        {
            F1004_Pedido f1004 = new();
            Sesion.GestorPantalla(f1004);
        }

        private void TarjetaInventario_btnTarjeta(object sender, EventArgs e)
        {
            F1002_Producto f1002 = new();
            Sesion.GestorPantalla(f1002);
        }

        private void btnConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            Sesion.GestorPantalla(new F1006_Configuracion());
        }
    }
}
