using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.Json;
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
using TiendaDeAgua.Informes;
using TiendaDeAgua.Tablas;
using Utilidades.Recursos;

namespace TiendaDeAgua.interfaz
{
    /// <summary>
    /// Lógica de interacción para F1004_Pedido.xaml
    /// </summary>
    public partial class F1004_Pedido : Page
    {
        PedidoDTO _PedidoDTO = new();
        string seleccionFiltro = "id";
        public F1004_Pedido()
        {
            InitializeComponent();

            CargarDatos(seleccionFiltro);

            cboEstado.ItemsSource = PedidoBD.ComboEstado();
            cboEstadoComboBox.ItemsSource = PedidoBD.ComboEstadoBox();
            cboCliente.ItemsSource = PedidoBD.ComboCliente();
            txtAyudaContextual.Visibility = Visibility.Collapsed;

            EstablecerAspectoFormulario(EstadoFormulario.Consulta);
        }

        #region Metodos Privados
        private void CargarDatos(string seleccionFiltro)
        {
            dtgPedido.ItemsSource = PedidoBD.ListaPedidoTodo(seleccionFiltro,Convert.ToInt32(cboEstado.SelectedValue));
            if(dtgPedido.Items.Count > 0)
            {
                _PedidoDTO = (PedidoDTO)dtgPedido.Items.GetItemAt(0);
                gbPedido.DataContext = _PedidoDTO;
            }

        }

        private  void EstablecerAspectoFormulario(EstadoFormulario estadoFormulario)
        {
            switch(estadoFormulario)
            {
                case EstadoFormulario.Nuevo:
                    //dtg
                    dtgPedido.IsEnabled = false;
                    //gb
                    gbFiltros.IsEnabled = false;
                    gbPedido.IsEnabled = true;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonInforme = Visibility.Collapsed;
                    //combobox
                    cboEstado.IsEnabled = false;
                    //Extras
                    _PedidoDTO = new PedidoDTO()
                    {
                        EsNuevo = true
                    };
                    gbPedido.DataContext = _PedidoDTO;
                    break;
                case EstadoFormulario.Edicion:
                    //dtg
                    dtgPedido.IsEnabled = false;
                    //gb
                    gbFiltros.IsEnabled = false;
                    gbPedido.IsEnabled = true;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonInforme = Visibility.Collapsed;

                    //combobox
                    cboEstado.IsEnabled = false;
                    //Extras
                    _PedidoDTO.EsNuevo = false;
                    break;
                case EstadoFormulario.Consulta:
                    //dtg
                    dtgPedido.IsEnabled = true;
                    //gb
                    gbFiltros.IsEnabled = true;
                    gbPedido.IsEnabled = false;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Visible;
                    BarraBotones_Principal.AparecerBotonModificar<PedidoDTO>(dtgPedido);
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonInforme = Visibility.Visible;

                    //combobox
                    cboEstado.IsEnabled = true;                    
                    break;
            }
        }

        #endregion

        #region Eventos
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
            CargarDatos(seleccionFiltro);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(_PedidoDTO != null)
            {
                ResultadoDTO res = PedidoBD.GuardaDatos(_PedidoDTO);

                if(res.codigoError == 0)
                {
                    HerramientaVentana.Show(res.mensajeInformacion);
                    CargarDatos(seleccionFiltro);
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
            //TODO
            if(_PedidoDTO != null)
            {
                ResultadoDTO res = PedidoBD.BorrarFila(_PedidoDTO);
                if(res.codigoError == 0)
                {
                    HerramientaVentana.Show(res.mensajeInformacion);
                    CargarDatos(seleccionFiltro);
                    EstablecerAspectoFormulario(EstadoFormulario.Consulta);
                }
                else
                {
                    HerramientaVentana.MostrarError(res);
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

        private void cboEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarDatos(seleccionFiltro);
            
            gbPedido.DataContext = _PedidoDTO;
            BarraBotones_Principal.AparecerBotonModificar<PedidoDTO>(dtgPedido);
        }

        private void dtgPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filaSeleccionada = dtgPedido.SelectedItem;
            if (filaSeleccionada != null)
            {
                _PedidoDTO = (PedidoDTO)filaSeleccionada;
            }
            else
            {
                _PedidoDTO = new()
                {
                    EsNuevo = true
                };
            }
            gbPedido.DataContext = _PedidoDTO;
        }

        private void AyudaContextualBoton_AyudaBtn(object sender, RoutedEventArgs e)
        {
            Visibility visibilidad = txtAyudaContextual.Visibility;
            if (visibilidad == Visibility.Visible)
            {
                txtAyudaContextual.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtAyudaContextual.Visibility = Visibility.Visible;
            }

        }

        private void dtgPedido_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string json = JsonSerializer.Serialize<PedidoDTO>(_PedidoDTO); //Serializamos

            Sesion.GestorPantalla(new F1007_DetallesPedido(json));
        }

        private void btnInforme_Click(object sender, EventArgs e)
        {
            ProductoReport pr = new();
            pr.Show();
        }

        #region Radio Buttons
        private void rbID_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                seleccionFiltro = "id";
                CargarDatos(seleccionFiltro);
            }
        }

        private void rbNombre_Checked(object sender, RoutedEventArgs e)
        {
            seleccionFiltro = "nombre";
            CargarDatos(seleccionFiltro);
        }

      

        private void rbFecha_Checked(object sender, RoutedEventArgs e)
        {
            seleccionFiltro = "fecha";
            CargarDatos(seleccionFiltro);
        }

        private void rbTotal_Checked(object sender, RoutedEventArgs e)
        {
            seleccionFiltro = "total";
            CargarDatos(seleccionFiltro);
        }

        #endregion

        #endregion


    }
}
