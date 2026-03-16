using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.Recursos;

namespace TiendaDeAgua.DTOs
{
    public class ProductoDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public double Precio { get; set; } 
        public int Stock { get; set; }
        public long CategoriaID { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public bool EsNuevo { get; set; }

        public ResultadoDTO ValidarDatos()
        {
            StringBuilder mensajero = new();
            int contadorErrores = 0;
            ResultadoDTO res = new();

            if(string.IsNullOrEmpty(Nombre) || string.IsNullOrWhiteSpace(Nombre))
            {
                contadorErrores++;
                mensajero.AppendLine($"{contadorErrores}-Rellene el campo Nombre.");
                res.codigoError = 101;
            }

            if(Precio <= 0)
            {
                contadorErrores++;
                mensajero.AppendLine($"{contadorErrores}-Rellene un precio.");
                res.codigoError = 102;
            }
            if(Stock <= 0)
            {
                contadorErrores++;
                mensajero.AppendLine($"{contadorErrores}-Rellene el campo Stock.");
                res.codigoError = 103;
            }
            if(CategoriaID == 0)
            {
                contadorErrores++;
                mensajero.AppendLine($"{contadorErrores}-Seleccione una categoria.");
                res.codigoError = 104;
            }

            if(res.codigoError != 0)
            {
                res.mensajeInformacion = $"Compruebe los siguientes campos.{Environment.NewLine}{mensajero.ToString()}";
            }
            return res;
        }
    }
}
