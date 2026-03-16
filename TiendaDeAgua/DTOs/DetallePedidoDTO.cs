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
    public class DetallePedidoDTO
    {
        [Required,NotNull]
        public int ID { get; set; }
        [Required,NotNull]
        public long PedidoID { get; set; }
        [Required,NotNull]
        public long ProductoID { get; set; }
        public string? ProductoNombre { get; set; }
        [NotNull]
        public int Cantidad { get; set; }
        [NotNull]
        public double PrecioUnitario { get; set; }
        public bool EsNuevo { get; set; }


        public ResultadoDTO ValidarDatos()
        {
            ResultadoDTO res = new();
            StringBuilder mensajero = new();
            int contadorErrores = 0;

            if(ProductoID == 0)
            {
                contadorErrores++;
                res.codigoError = 101;
                mensajero.AppendLine($"{contadorErrores}-Seleccione un Producto para el pedido");
            }
            if(Cantidad <= 0)
            {
                contadorErrores++;
                res.codigoError = 102;
                mensajero.AppendLine($"{contadorErrores}-Escriba una cantidad.");
            }
            if(PrecioUnitario <= 0.0)
            {
                contadorErrores++;
                res.codigoError = 103;
                mensajero.AppendLine($"{contadorErrores}-Escriba un precio unitario.");
            }
            if(res.codigoError != 0)
            {
                res.mensajeInformacion = $"Compruebe los siguientes campos:{Environment.NewLine}{mensajero.ToString()}";
            }
            return res;
        }

    }
}
