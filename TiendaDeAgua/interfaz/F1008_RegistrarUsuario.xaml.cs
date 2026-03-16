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
    /// Lógica de interacción para F1008_RegistrarUsuario.xaml
    /// </summary>
    public partial class F1008_RegistrarUsuario : Window
    {
        UsuarioDTO _UsuarioDTO = new() { EsNuevo = true};
        public F1008_RegistrarUsuario()
        {
            InitializeComponent();

            dtgUsuario.DataContext = _UsuarioDTO;
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            dtgUsuario.DataContext = _UsuarioDTO;
            _UsuarioDTO.Contrasenya = passPrimera.Password; //Esto porque no se bindea
            ResultadoDTO res = new(); ;
            if (ContrasenyaDoble())
            {
                res = UsuarioBD.GuardarDatos(_UsuarioDTO);
                if(res.codigoError == 0)
                {
                    HerramientaVentana.Show(res.mensajeInformacion);
                    this.Close();
                }
                else
                {
                    HerramientaVentana.MostrarError(res);
                }
            }
            else
            {
                res.codigoError = 101;
                res.mensajeInformacion = "Ambas contraseñas deben coincidir";
                HerramientaVentana.MostrarError(res);

            }
        }
        /// <summary>
        /// Comprueba que el usuario ha metido la misma contraseña en ambas textbox
        /// </summary>
        private  bool ContrasenyaDoble()
        {
            return passPrimera.Password.Equals(passRepetir.Password);
        }
    }
}
