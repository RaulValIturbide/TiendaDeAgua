using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.Recursos;
namespace TiendaDeAgua.DTOs
{
    public class UsuarioDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Contrasenya { get; set; } = string.Empty;
        public int ModoEntrada { get; set; } = 0;

        public bool EsNuevo { get; set; }
        public ResultadoDTO ValidarDatos()
        {
            StringBuilder mensajero = new StringBuilder();
            int contadorErrores = 0;
            ResultadoDTO res = new();

            if(string.IsNullOrEmpty(Nombre) || string.IsNullOrWhiteSpace(Nombre))
            {
                contadorErrores++;
                mensajero.AppendLine($"{contadorErrores}-Escriba su nombre de Usuario.");
                res.codigoError = 101;
            }

            if(string.IsNullOrEmpty(Contrasenya) || string.IsNullOrWhiteSpace(Contrasenya))
            {
                contadorErrores++;
                mensajero.AppendLine($"{contadorErrores}-Escriba su contraseña.");
                res.codigoError = 102;
            }

            if(res.codigoError != 0)
            {
                res.mensajeInformacion = $"Compruebe los siguientes campos:{Environment.NewLine}{mensajero.ToString()}";
            }
            return res;
        }
        


    }
}
