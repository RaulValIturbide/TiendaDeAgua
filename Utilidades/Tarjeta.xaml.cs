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

namespace Utilidades
{
    /// <summary>
    /// Lógica de interacción para Tarjeta.xaml
    /// </summary>
    public partial class Tarjeta : UserControl
    {
        public EventHandler? btnTarjeta;
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
