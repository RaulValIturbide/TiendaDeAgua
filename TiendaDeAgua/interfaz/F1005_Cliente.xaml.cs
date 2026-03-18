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
    /// Lógica de interacción para F1005_Cliente.xaml
    /// </summary>
    public partial class F1005_Cliente : Page
    {
        ClienteDTO _ClienteDTO = new();
        string tipoOrden = "id"; //Como se ordenará, tenemos: "id","nombre","contacto" y "direccion"
        public F1005_Cliente()
        {
            InitializeComponent();

            CargarDatos(tipoOrden);

            EstablecerAspectoFormulario(EstadoFormulario.Consulta);
        }

        #region MetodosPrivados

        private void CargarDatos(string ordenarPor)
        {
       
            dtgCliente.ItemsSource = ClienteBD.ListaCliente(ordenarPor);
            //Basicamente: si exite al menos una fila, se selecciona desde la carga
            if(dtgCliente.Items.Count > 0)
            {
                dtgCliente.SelectedItem = dtgCliente.Items.GetItemAt(0);
            }
        }

        private void EstablecerAspectoFormulario(EstadoFormulario estadoFormulario)
        {
            switch(estadoFormulario)
            {
                case EstadoFormulario.Nuevo:
                    //dtg
                    dtgCliente.IsEnabled = false;
                    //gb
                    gbCliente.IsEnabled = true;
                    //BarraBotoens
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonInforme = Visibility.Collapsed;
                    //Extras
                    _ClienteDTO = new() { EsNuevo = true };
                    gbCliente.DataContext = _ClienteDTO;

                    break;

                case EstadoFormulario.Edicion:
                    //dtg
                    dtgCliente.IsEnabled = false;
                    //gb
                    gbCliente.IsEnabled = true;
                    //BarraBotoens
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonInforme = Visibility.Collapsed;
                    //Extras
                    _ClienteDTO.EsNuevo = false;
                    

                    break;

                case EstadoFormulario.Consulta:
                    //dtg
                    dtgCliente.IsEnabled = true;
                    //gb
                    gbCliente.IsEnabled = false;
                    //BarraBotoens
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Visible;
                    BarraBotones_Principal.AparecerBotonModificar<ClienteDTO>(dtgCliente);
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

        private void dtgCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filaSeleccionada = dtgCliente.SelectedItem;

            if(filaSeleccionada != null)
            {
                _ClienteDTO = (ClienteDTO)filaSeleccionada;
            }
            else
            {
                _ClienteDTO = new() { EsNuevo = true };
            }
            gbCliente.DataContext = _ClienteDTO;
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
            CargarDatos(tipoOrden);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(_ClienteDTO != null)
            {
                ResultadoDTO res = ClienteBD.GuardarDatos(_ClienteDTO);

                if(res.codigoError == 0)
                {
                    HerramientaVentana.Show(res.mensajeInformacion);
                    CargarDatos(tipoOrden);
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
            if(_ClienteDTO != null)
            {
                ResultadoDTO res = ClienteBD.BorrarFila(_ClienteDTO);
                
                if(res.codigoError == 0)
                {
                    HerramientaVentana.Show(res.mensajeInformacion);
                    CargarDatos(tipoOrden);
                    EstablecerAspectoFormulario(EstadoFormulario.Consulta);
                }
                else
                {
                    HerramientaVentana.MostrarError(res);
                }
            }
        }

        private void btnInforme_Click(object sender, EventArgs e)
        {

        }

        private void Page_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Sesion.VolverAtras();
        }

        #region RadioButtons

        private void rbID_Checked(object sender, RoutedEventArgs e)
        {
            //Tenemos que comprobar que está cargado ya que de no hacerlo
            //Al iniciar los componenetes salta excepcion al estar preseleccionado el Check en el rbID
            if (IsLoaded)
            {
                tipoOrden = "id";
                CargarDatos(tipoOrden);
            }
        }

        private void rbNombre_Checked(object sender, RoutedEventArgs e)
        {
            tipoOrden = "nombre";
            CargarDatos(tipoOrden);
        }

        private void rbContacto_Checked(object sender, RoutedEventArgs e)
        {
            tipoOrden = "contacto";
            CargarDatos(tipoOrden);
        }

        private void rbDireccion_Checked(object sender, RoutedEventArgs e)
        {
            tipoOrden = "direccion";
            CargarDatos(tipoOrden);
        }


        #endregion

        #endregion




    }
}
