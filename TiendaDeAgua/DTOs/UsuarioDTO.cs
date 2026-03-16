using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Utilidades.Recursos;
namespace TiendaDeAgua.DTOs
{
    public class UsuarioDTO
    {
        [Required,NotNull]
        public int ID { get; set; }
        [Required,NotNull]
        public string Nombre { get; set; } = string.Empty;
        [Required,NotNull]
        public string Contrasenya { get; set; } = string.Empty;
        //Esto sirve para saber si el usuario es admin o darle un rol específico
        //Ahora mismo diferencia entre 0 -> default-> usuario base Y 1 -> admin, teniendo en cuenta el usuario
        //que está usando la app podríamos mostrar u ocultar diferentes cosas
        public int ModoEntrada { get; set; } = 0; 
        public string Email { get; set; } = string.Empty;

        public bool EsNuevo { get; set; }
        public ResultadoDTO ValidarDatos()
        {
            StringBuilder mensajero = new StringBuilder();
            int contadorErrores = 0;
            ResultadoDTO res = new();

            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrWhiteSpace(Nombre))
            {
                contadorErrores++;
                mensajero.AppendLine($"{contadorErrores}-Escriba su nombre de Usuario.");
                res.codigoError = 101;
            }

            if (string.IsNullOrEmpty(Contrasenya) || string.IsNullOrWhiteSpace(Contrasenya))
            {
                contadorErrores++;
                mensajero.AppendLine($"{contadorErrores}-Escriba su contraseña.");
                res.codigoError = 102;
            }

            if (res.codigoError != 0)
            {
                res.mensajeInformacion = $"Compruebe los siguientes campos:{Environment.NewLine}{mensajero.ToString()}";
            }
            return res;
        }



    }
}
