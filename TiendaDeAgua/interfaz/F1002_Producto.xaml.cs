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
using Utilidades.Recursos;

namespace TiendaDeAgua.interfaz
{
    /// <summary>
    /// Lógica de interacción para F1002_Producto.xaml
    /// </summary>
    public partial class F1002_Producto : Page
    {
        public F1002_Producto()
        {
            InitializeComponent();

        }

        private void BotonMenuPrincipal_btnMenuPrincipal(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void dtgProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// Otro metodo de accesibilidad para poder ir hacia atras
        /// con un simple click derecho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Sesion.VolverAtras();
        }
    }
}
