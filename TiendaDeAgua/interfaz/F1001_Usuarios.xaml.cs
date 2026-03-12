using System.Windows;
using System.Windows.Controls;
using TiendaDeAgua.DTOs;
using TiendaDeAgua.Tablas;
using Utilidades.Recursos;

namespace TiendaDeAgua.interfaz
{
    /// <summary>
    /// Lógica de interacción para F1001_Usuarios.xaml
    /// </summary>
    public partial class F1001_Usuarios : Page
    {
        #region Propiedades Privadas
        private static UsuarioDTO _UsuarioDTO = new();
        private static Frame _PaginaActiva = new();
        #endregion
        public F1001_Usuarios()
        {
            InitializeComponent();

            InicializacionSecundaria();

        }

        #region Metodos Privados

        private void InicializacionSecundaria()
        {
            EstadoFormulario(Utilidades.Recursos.EstadoFormulario.Consulta);

            CargarDatos();         
        }
        private void CargarDatos()
        {
            dtgUsuarios.ItemsSource = UsuarioBD.ListaUsuario();
            if (dtgUsuarios.Items.Count > 0)
            {
                dtgUsuarios.SelectedItem = dtgUsuarios.Items.GetItemAt(0);
            }
        }

        private void EstadoFormulario(EstadoFormulario estadoFormulario)
        {
            switch (estadoFormulario)
            {
                case Utilidades.Recursos.EstadoFormulario.Nuevo:
                    //dtg
                    dtgUsuarios.IsEnabled = false;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;

                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Collapsed;

                    //EXTRAS
                    _UsuarioDTO = new()
                    {
                        EsNuevo = true
                    };
                    gbUsuario.DataContext = _UsuarioDTO;
                    break;
                case Utilidades.Recursos.EstadoFormulario.Edicion:
                    //dtg
                    dtgUsuarios.IsEnabled = false;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonModificar = Visibility.Collapsed;

                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Visible;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Visible;
                    //EXTRAS
                    _UsuarioDTO.EsNuevo = false;
                    break;
                case Utilidades.Recursos.EstadoFormulario.Consulta:
                    //dtg
                    dtgUsuarios.IsEnabled = true;
                    //BarraBotones
                    BarraBotones_Principal.VisibilidadBotonNuevo = Visibility.Visible;
                    BarraBotones_Principal.AparecerBotonModificar<UsuarioDTO>(dtgUsuarios);

                    BarraBotones_Principal.VisibilidadBotonCancelar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonGuardar = Visibility.Collapsed;
                    BarraBotones_Principal.VisibilidadBotonEliminar = Visibility.Collapsed;
                    break;
            }
        }
        #endregion

        #region Eventos

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            EstadoFormulario(Utilidades.Recursos.EstadoFormulario.Nuevo);
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            EstadoFormulario(Utilidades.Recursos.EstadoFormulario.Edicion);
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_UsuarioDTO != null)
            {
                ResultadoDTO res = new();
                res = UsuarioBD.GuardarDatos(_UsuarioDTO);

                if (res.codigoError == 0)
                {
                    HerramientaVentana.Show(res.mensajeInformacion);
                    CargarDatos();
                    EstadoFormulario(Utilidades.Recursos.EstadoFormulario.Consulta);
                }
                else
                {
                    HerramientaVentana.MostrarError(res);
                }
            }

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EstadoFormulario(Utilidades.Recursos.EstadoFormulario.Consulta);
            CargarDatos();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_UsuarioDTO != null)
            {
                ResultadoDTO res = new();
                res = UsuarioBD.BorrarFila(_UsuarioDTO);

                if (res.codigoError == 0)
                {
                    HerramientaVentana.Show(res.mensajeInformacion);
                    CargarDatos();
                    EstadoFormulario(Utilidades.Recursos.EstadoFormulario.Consulta);
                }
                else
                {
                    HerramientaVentana.MostrarError(res);
                }
            }
        }
        private void dtgUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filaSeleccionada = (UsuarioDTO)dtgUsuarios.SelectedItem;
            if (filaSeleccionada != null)
            {
                _UsuarioDTO = (UsuarioDTO)dtgUsuarios.SelectedItem;
            }
            else
            {
                _UsuarioDTO = new() { EsNuevo = true };
            }
            BarraBotones_Principal.AparecerBotonModificar<UsuarioDTO>(dtgUsuarios);
            gbUsuario.DataContext = _UsuarioDTO;
        }
     
        private void BotonMenuPrincipal_btnMenuPrincipal(object sender, EventArgs e)
        {
            F1003_PaginaPrincipal f1003 = new();
            Sesion.GestorPantalla().Navigate(f1003);
        }



        #endregion

        private void Page_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Sesion.VolverAtras();
        }
    }
}
