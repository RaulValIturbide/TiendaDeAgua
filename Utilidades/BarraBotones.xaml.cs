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
        public EventHandler? NuevoClick;
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

        public Visibility VisibilidadBotonNuevo
        {
            get => (Visibility)GetValue(VisibilidadBotonNuevoProperty);
            set => SetValue(VisibilidadBotonNuevoProperty, value);
        }

        public static readonly DependencyProperty VisibilidadBotonNuevoProperty =
            DependencyProperty.Register(
                nameof(VisibilidadBotonNuevo),
                typeof(Visibility),
                typeof(BarraBotones),
                new PropertyMetadata(Visibility.Visible, OnVisibilidadBotonNuevoChanged));
        private static void OnVisibilidadBotonNuevoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (BarraBotones)d;
            control.btnNuevo.Visibility = (Visibility)e.NewValue;
        }

        public Visibility VisibilidadBotonModificar
        {
            get => (Visibility)GetValue(VisibilidadBotonModificarProperty);
            set => SetValue(VisibilidadBotonModificarProperty, value);
        }
        private static readonly DependencyProperty VisibilidadBotonModificarProperty =
            DependencyProperty.Register(
                nameof(VisibilidadBotonModificar), 
                typeof(Visibility), 
                typeof(BarraBotones), 
                new PropertyMetadata(Visibility.Visible, OnVisibilidadBotonModificarChanged));    
        private static void OnVisibilidadBotonModificarChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
                var control = (BarraBotones)d;
                control.btnModificar.Visibility = (Visibility)e.NewValue;
        }
    }

}
