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
using System.Windows.Shapes;

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
        }

        public VentanaError(String mensaje)
        {
            InitializeComponent();

            this.Title = "Ventana de Información";
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Utilidades;component/data/img/check.png"));

            imgVentanaError.Source = new BitmapImage(new Uri("pack://application:,,,/Utilidades;component/data/img/check.png"));
            txtBlockInformacion.Text = mensaje;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
