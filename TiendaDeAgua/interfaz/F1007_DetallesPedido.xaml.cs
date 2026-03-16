using System;
using System.Collections.Generic;
using System.Linq;
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
using TiendaDeAgua.Tablas;
using Utilidades.Recursos;

namespace TiendaDeAgua.interfaz
{
    /// <summary>
    /// Lógica de interacción para DetallesPedido.xaml
    /// </summary>
    public partial class F1007_DetallesPedido : Page
    {
        PedidoDTO _PedidoDTO = new();
        DetallePedidoDTO _DetallePedidoDTO = new();
        public F1007_DetallesPedido(string pPedido)
        {
            InitializeComponent();

            TraerDatos(pPedido); //Traemos el dto serializado para usar su ID

            CargarDatos(); //Cargamos los datos como hacemos de normal pero con el ID ya en la clase

            lbTitulo.Content = $"DETALLE PEDIDO DE {_PedidoDTO.ClienteNombre.ToUpper()} : {_PedidoDTO.Fecha}";//Información de los detalles

            cboProductoID.ItemsSource = DetallePedidoBD.ComboProducto();

            EstablecerAspectoFormulario(EstadoFormulario.Consulta);
        }

        private void TraerDatos(string pPedido)
        {
            _PedidoDTO = JsonSerializer.Deserialize<PedidoDTO>(pPedido);//Deserializamos
        }

        private void CargarDatos()
        {
            if(_PedidoDTO != null)
            {


                dtgDetallePedido.ItemsSource = DetallePedidoBD.ListaDetallePedido(_PedidoDTO.ID);

                if(dtgDetallePedido.Items.Count > 0)
                {
                    dtgDetallePedido.SelectedItem = dtgDetallePedido.Items.GetItemAt(0);
                }
            }                   
        }

        private void EstablecerAspectoFormulario(EstadoFormulario estadoFormulario)
        {
            switch(estadoFormulario)
            {
                case EstadoFormulario.Nuevo:
                    //dtg
                    dtgDetallePedido.IsEnabled = false;
                    //gb
                    gbProducto.IsEnabled = true;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Collapsed;
                    //Extras
                    _DetallePedidoDTO = new()
                    {
                        EsNuevo = true,
                        PedidoID = _PedidoDTO.ID
                    };
                    gbProducto.DataContext = _DetallePedidoDTO;
                    break;
                case EstadoFormulario.Edicion:
                    //dtg
                    dtgDetallePedido.IsEnabled = false;
                    //gb
                    gbProducto.IsEnabled = true;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Visible;
                    //Extras
                    _DetallePedidoDTO.EsNuevo = false;
                    break;
                case EstadoFormulario.Consulta:
                    //dtg
                    dtgDetallePedido.IsEnabled = true;
                    //gb
                    gbProducto.IsEnabled = false;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Visible;
                    BarraBotones_Principal.AparecerBotonModificar<DetallePedidoDTO>(dtgDetallePedido);
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Collapsed;                   
                    break;
            }
        }

        private void BotonMenuPrincipal_btnMenuPrincipal(object sender, EventArgs e)
        {
            Sesion.GestorPantalla(new F1003_PaginaPrincipal());
        }

        private void dtgDetallePedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filaSeleccionada = dtgDetallePedido.SelectedItem;
            if(filaSeleccionada != null)
            {
                _DetallePedidoDTO = (DetallePedidoDTO)filaSeleccionada;
            }
            else
            {
                _DetallePedidoDTO = new()
                {
                    EsNuevo = true,
                    PedidoID = _PedidoDTO.ID
                };
            }
            gbProducto.DataContext = _DetallePedidoDTO;
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
            if(_DetallePedidoDTO != null)
            {
                ResultadoDTO res = DetallePedidoBD.GuardarDatos(_DetallePedidoDTO);
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
            if(_DetallePedidoDTO != null)
            {
                ResultadoDTO res = DetallePedidoBD.BorrarFila(_DetallePedidoDTO);
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

        private void Grid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(Sesion.ActivarVolverAtras)
            {
                //Esto lo hacemos asi porque si hacemos el navigate 
                //no se cargan los datos del total
                Sesion.GestorPantalla(new F1004_Pedido());
            }
            
        }

        private void btnInforme_Click(object sender, EventArgs e)
        {
            InformeDetallePedido informeDetallePedido = new(_PedidoDTO.ID);
            informeDetallePedido.ShowDialog();
        }
    }
}
