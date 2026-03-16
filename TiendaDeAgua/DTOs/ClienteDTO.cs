using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.Recursos;

namespace TiendaDeAgua.DTOs
{
    public class ClienteDTO
    {
        [Required, NotNull]
        public int ID { get; set; }
        [NotNull]
        public string Nombre { get; set; } = string.Empty;
        [AllowNull]
        public string? Contacto {get;set;}
        [AllowNull]
        public string? Direccion { get; set; }
        public bool EsNuevo { get; set; }


        public ResultadoDTO ValidarDatos()
        {
            ResultadoDTO res = new();
            StringBuilder mensajero = new();
            int contadorError = 0;

            if(string.IsNullOrEmpty(Nombre) || string.IsNullOrWhiteSpace(Nombre))
            {
                contadorError++;
                res.codigoError = 101;
                mensajero.AppendLine($"{contadorError}-Rellene el campo Nombre.");
            }

            if(res.codigoError != 0)
            {
                res.mensajeInformacion = $"Compruebe los siguientes campos:{Environment.NewLine}{mensajero.ToString()}";
            }

            return res;
        }
    }
}
