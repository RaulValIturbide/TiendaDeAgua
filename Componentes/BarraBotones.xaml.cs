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

namespace Componentes
{
    /// <summary>
    /// Lógica de interacción para BarraBotones.xaml
    /// </summary>
    public partial class BarraBotones : UserControl
    {
        public event RoutedEventHandler? NuevoClick;
        public event RoutedEventHandler? PermitirModificarClick;
        public BarraBotones()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            NuevoClick?.Invoke(this, e);
        }

        private void btnPermitirModificar_Click(object sender, RoutedEventArgs e)
        {
            PermitirModificarClick?.Invoke(this, e);
        }
    }
}
