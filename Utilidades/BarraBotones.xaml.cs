using System.Windows;
using System.Windows.Controls;

namespace Utilidades
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BarraBotones : UserControl
    {
        public event RoutedEventHandler? NuevoClick;
        public event EventHandler? ModificarClick;
        public event EventHandler? GuardarClick;
        public event EventHandler? CancelarClick;
        public event EventHandler? EliminarClick;


        public BarraBotones()
        {
            InitializeComponent();
        }





        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            NuevoClick?.Invoke(this, e);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            ModificarClick?.Invoke(this, e);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            GuardarClick?.Invoke(this, e);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            EliminarClick?.Invoke(this, e);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            CancelarClick?.Invoke(this, e);
        }


        #region PROPIEDADES PUBLICAS 

        public Visibility VisibilidadBotonNuevo
        {
            get { return this.btnNuevo.Visibility; }
            set { this.btnNuevo.Visibility = value; }
        }

        public Visibility VisibilidadBotonModificar
        {
            get { return this.btnModificar.Visibility; }
            set { this.btnModificar.Visibility = value; }
        }

        public Visibility VisibilidadBotonGuardar
        {
            get { return this.btnGuardar.Visibility; }
            set { this.btnGuardar.Visibility = value; }
        }
        public Visibility VisibilidadBotonCancelar
        {
            get { return this.btnCancelar.Visibility; }
            set { this.btnCancelar.Visibility = value; }
        }
        public Visibility VisibilidadBotonEliminar
        {
            get { return this.btnEliminar.Visibility; }
            set { this.btnEliminar.Visibility = value; }
        }

        #endregion

        #region Metodo Publico

        /// <summary>
        /// Este metodo tiene como objetivo establecer si se ve el boton "Modificar" si
        /// no hay filas se oculta
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataGrid"></param>
        public void AparecerBotonModificar<T>(DataGrid dataGrid)
        {
            int contadorFilas = dataGrid.Items.Count;

            if(contadorFilas > 0)
            {
                this.btnModificar.Visibility = Visibility.Visible;
            }
            else
            {
                this.btnModificar.Visibility = Visibility.Collapsed;
            }
        }

        #endregion


    }

}
