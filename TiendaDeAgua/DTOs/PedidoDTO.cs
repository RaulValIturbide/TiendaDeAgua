using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.Recursos;
using System.Text.RegularExpressions;

namespace TiendaDeAgua.DTOs
{
    public class PedidoDTO
    {
        [Required,NotNull]
        public int ID { get; set; }
        [Required,NotNull]
        public long ClienteID { get; set; } //long por el combo box
        public string ClienteNombre { get; set; } = string.Empty;
        [NotNull]
        public string Fecha { get; set; } = string.Empty;
        [NotNull]
        public double Total { get; set;}
        [NotNull]
        public long EstadoID { get; set; } //long por el combo box
        public string EstadoNombre { get; set; } = string.Empty;
        public bool EsNuevo { get; set; }


        public ResultadoDTO ValidarDatos()
        {
            ResultadoDTO res = new();
            StringBuilder mensajero = new StringBuilder();
            int contadorErrores = 0;

            if(ClienteID == 0)
            {
                contadorErrores++;
                res.codigoError = 101;
                mensajero.AppendLine($"{contadorErrores}-Escriba el nombre de un cliente.");                
            }
            //TODO
            //ESTE PATRÓN ES SIMPLE PARA QUE SE SIGA LA LOGICA DE YYYY-MM-dd
            //No revisa años bisiestos ni 30 febrero, mejorar si da tiempo

            string patron = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";

            if (!Regex.IsMatch(Fecha,patron))
            {
                contadorErrores++;
                res.codigoError = 102;
                mensajero.AppendLine($"{contadorErrores}-Seleccione una fecha,Formato:YYYY-MM-dd.");
            }
            if(res.codigoError != 0)
            {
                res.mensajeInformacion = $"Compruebe los siguientes campos:{Environment.NewLine}{mensajero.ToString()}";
            }
            return res;
        }

    }
}
