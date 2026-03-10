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

namespace Utilidades
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BarraBotones : UserControl
    {
        public event RoutedEventHandler? NuevoClick;
        public EventHandler? ModificarClick;
        public EventHandler? GuardarClick;
        public EventHandler? CancelarClick;
        public EventHandler? EliminarClick;


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
        public Visibility getVisibilidadBotonNuevo()
        {
            return this.btnNuevo.Visibility;
        }

        public void setVisibilidadBotonNuevo(Visibility visibilidad)
        {
            this.btnNuevo.Visibility = visibilidad;
        }

        public Visibility getVisibilidadBotonPermitirModificar()
        {
            return this.btnModificar.Visibility;
        }
        public void setVisibilidadBotonPermitirModificar(Visibility visibilidad)
        {
            this.btnModificar.Visibility = visibilidad ;
        }

        public Visibility getVisibilidadBotonGuardar()
        {
            return this.btnGuardar.Visibility;
        }
        public void setVisibilidadBotonGuardar(Visibility visibilidad)
        {
            this.btnGuardar.Visibility = visibilidad; 
        }

        public Visibility getVisibilidadBotonCancelar()
        {
            return this.btnCancelar.Visibility;
        }
        public void setVisibilidadBotonCancelar(Visibility visibilidad)
        {
            this.btnCancelar.Visibility = visibilidad;
        }

        public Visibility getVisibilidadBotonEliminar()
        {
            return this.btnEliminar.Visibility;
        }
        public void setVisibilidadBotonELiminar(Visibility visibilidad)
        {
            this.btnEliminar.Visibility = visibilidad;
        }
        #endregion


    }

}
