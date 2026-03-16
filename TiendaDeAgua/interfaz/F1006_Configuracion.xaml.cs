using ComunServicioAiron;
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
    /// Lógica de interacción para F1006_Configuracion.xaml
    /// </summary>
    public partial class F1006_Configuracion : Page
    {
        public F1006_Configuracion()
        {
            InitializeComponent();

            CargarComboAtajoVolverAtras();
            CargarComboPantallaCompleta();
        }

        private void CargarComboAtajoVolverAtras()
        {
            ComboListDTO opcion1 = new ComboListDTO() { Identificador = 1, Texto = "No Activado" };
            ComboListDTO opcion2 = new ComboListDTO() { Identificador = 2, Texto = "Activado" };
            List<ComboListDTO> cbAtajo = [opcion1,opcion2];

            cboAtajoVolverAtras.ItemsSource = cbAtajo;
            if (Sesion.ActivarVolverAtras)
            {
                cboAtajoVolverAtras.SelectedValue = 2;
            }
            else
            {
                cboAtajoVolverAtras.SelectedValue = 1;
            }
            
        }
        private void CargarComboPantallaCompleta()
        {
            ComboListDTO opcion1 = new ComboListDTO() { Identificador = 1, Texto = "Pantalla Completa" };
            ComboListDTO opcion2 = new ComboListDTO() { Identificador = 2, Texto = "Ventana" };
            List<ComboListDTO> cbPantallaCompleta = [opcion1, opcion2];

            cboPantallaCompleta.ItemsSource = cbPantallaCompleta;
            if (!Sesion.PantallaCompleta)
            {
                cboPantallaCompleta.SelectedValue = 2;
            }
            else
            {
                cboPantallaCompleta.SelectedValue = 1;
            }

        }

        private void cboAtajoVolverAtras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboListDTO filaSeleccionada = (ComboListDTO) cboAtajoVolverAtras.SelectedItem;
            if(filaSeleccionada != null)
            {
                if(filaSeleccionada.Identificador == 2)
                {
                    Sesion.ActivarVolverAtras = true;
                }
                else 
                {
                    Sesion.ActivarVolverAtras = false;
                }
            }
        }

        private void Grid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Sesion.VolverAtras();
        }

        private void BotonMenuPrincipal_btnMenuPrincipal(object sender, EventArgs e)
        {
            Sesion.GestorPantalla(new F1003_PaginaPrincipal());
        }

        private void cboPantallaCompleta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;

            ComboListDTO filaSeleccionada = (ComboListDTO)cboPantallaCompleta.SelectedItem;
            if (filaSeleccionada == null) return;

            Window ventanaPadre = Window.GetWindow(this);
            if (ventanaPadre == null) return; // ESTO LO HACEMOS POR SI FALLA LA VENTANA PADRE

            if (filaSeleccionada.Identificador == 1) // Pantalla Completa
            {
                Sesion.PantallaCompleta = true;
                ventanaPadre.WindowStyle = WindowStyle.None;
                ventanaPadre.ResizeMode = ResizeMode.NoResize;
                ventanaPadre.WindowState = WindowState.Maximized;
            }
            else // Ventana normal
            {
                Sesion.PantallaCompleta = false;
                ventanaPadre.WindowStyle = WindowStyle.SingleBorderWindow;
                ventanaPadre.ResizeMode = ResizeMode.CanResize;
                ventanaPadre.WindowState = WindowState.Normal;
            }
        }
    }
}
