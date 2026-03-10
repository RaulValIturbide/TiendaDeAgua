using System.Windows;
using System.Windows.Controls;

namespace Utilidades
{
    /// <summary>
    /// Lógica de interacción para Tarjeta.xaml
    /// </summary>
    public partial class Tarjeta : UserControl
    {
        public event EventHandler? btnTarjeta;
        public Tarjeta()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnTarjeta?.Invoke(this, e);
        }
    }
}
