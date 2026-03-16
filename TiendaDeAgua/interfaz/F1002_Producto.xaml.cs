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
using TiendaDeAgua.DTOs;
using TiendaDeAgua.Tablas;
using Utilidades.Recursos;

namespace TiendaDeAgua.interfaz
{
    /// <summary>
    /// Lógica de interacción para F1002_Producto.xaml
    /// </summary>
    public partial class F1002_Producto : Page
    {
        ProductoDTO _ProductoDTO = new();
        public F1002_Producto()
        {
            InitializeComponent();

            CargarDatos();

            cboCategoria.ItemsSource = ProductoBD.ComboCategoria();            

            EstablecerAspectoFormulario(EstadoFormulario.Consulta);
        }

        #region Metodos Privados
        private void CargarDatos()
        {
            dtgProducto.ItemsSource = ProductoBD.ListaProducto();
            if(dtgProducto.Items.Count > 0)
            {
                dtgProducto.SelectedItem = dtgProducto.Items.GetItemAt(0);
            }
        }

        private void EstablecerAspectoFormulario(EstadoFormulario estadoFormulario)
        {
            switch(estadoFormulario)
            {
                case EstadoFormulario.Nuevo:
                    //dtg
                    dtgProducto.IsEnabled = false;
                    //gb
                    gbProducto.IsEnabled = true;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonInforme = Visibility.Collapsed;
                    //Extras
                    _ProductoDTO = new ProductoDTO() { EsNuevo = true };
                    gbProducto.DataContext = _ProductoDTO;
                    break;
                case EstadoFormulario.Edicion:
                    //dtg
                    dtgProducto.IsEnabled = false;
                    //gb
                    gbProducto.IsEnabled = true;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonInforme = Visibility.Collapsed;
                    //Extras
                    _ProductoDTO.EsNuevo = false;
                    break;

                case EstadoFormulario.Consulta:
                    //dtg
                    dtgProducto.IsEnabled = true;
                    //gb
                    gbProducto.IsEnabled = false;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Visible;
                    BarraBotones_Principal.AparecerBotonModificar<ProductoDTO>(dtgProducto);
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonInforme = Visibility.Visible;
                    break;
            }
        }

        #endregion

        #region Eventos
        private void BotonMenuPrincipal_btnMenuPrincipal(object sender, EventArgs e)
        {            
            Sesion.GestorPantalla(new F1003_PaginaPrincipal());
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            EstablecerAspectoFormulario(EstadoFormulario.Nuevo);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            EstablecerAspectoFormulario(EstadoFormulario.Edicion);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EstablecerAspectoFormulario(EstadoFormulario.Consulta);
            CargarDatos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(_ProductoDTO != null)
            {
                //TODO : ARREGLAR BINDING
                ResultadoDTO res = ProductoBD.GuardarDatos(_ProductoDTO);
                if(res.codigoError == 0)
                {
                    HerramientaVentana.Show(res.mensajeInformacion);
                    CargarDatos();
                    EstablecerAspectoFormulario(EstadoFormulario.Consulta);
                }
                else
                {
                    HerramientaVentana.MostrarError(res);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(_ProductoDTO != null)
            {
                ResultadoDTO res = ProductoBD.BorrarFila(_ProductoDTO);
                if(res.codigoError == 0)
                {
                    HerramientaVentana.Show(res.mensajeInformacion);
                    CargarDatos();
                    EstablecerAspectoFormulario(EstadoFormulario.Consulta);
                }
                else
                {
                    HerramientaVentana.MostrarError(res);
                }
            }
        }

        private void dtgProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filaSeleccionada = dtgProducto.SelectedItem;

            if(filaSeleccionada != null)
            {
                _ProductoDTO = (ProductoDTO)filaSeleccionada;
            }
            else
            {
                _ProductoDTO = new() { EsNuevo = true };
            }
            gbProducto.DataContext = _ProductoDTO;
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

        private void btnInforme_Click(object sender, EventArgs e)
        {
            InformeProducto informe = new();
            informe.ShowDialog();
        }
        #endregion
    }
}
