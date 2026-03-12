using System.Windows;
using System.Windows.Media.Imaging;

namespace Utilidades.Recursos
{
    /// <summary>
    /// Lógica de interacción para VentanaError.xaml
    /// </summary>
    public partial class VentanaError : Window
    {
        public VentanaError()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Esto es si el usuario mete algo incorrecto
        /// </summary>
        /// <param name="res"></param>
        public VentanaError(ResultadoDTO res)
        {
            InitializeComponent();

            this.Title = "Ventana de Información";
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Utilidades;component/data/img/cancel.png"));

            imgVentanaError.Source = new BitmapImage(new Uri("pack://application:,,,/Utilidades;component/data/img/cancel.png"));
            txtBlockInformacion.Text = res.mensajeInformacion;
            txtBkCodigo.Text = $"Código Error: {res.codigoError}";
            btnAceptar.Focus();

        }

        public VentanaError(String mensaje)
        {
            InitializeComponent();

            this.Title = "Ventana de Información";
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Utilidades;component/data/img/check.png"));

            imgVentanaError.Source = new BitmapImage(new Uri("pack://application:,,,/Utilidades;component/data/img/check.png"));
            txtBlockInformacion.Text = mensaje;
            btnAceptar.Focus();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento para mejorar la accesibilidad y la fluidez a través del código,
        /// si le damos a enter le daremos a aceptar de manera directa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAceptar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                this.Close();
            }
        }
    }
}
