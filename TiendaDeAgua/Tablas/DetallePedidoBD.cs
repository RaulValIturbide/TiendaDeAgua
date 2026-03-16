using ComunServicioAiron;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaDeAgua.DTOs;
using Utilidades.Recursos;

namespace TiendaDeAgua.Tablas
{
    public class DetallePedidoBD
    {
        public static List<DetallePedidoDTO> ListaDetallePedido(int pPedidoID)
        {
            string sql = " SELECT dp.ID,dp.PedidoID,dp.ProductoID,pr.Nombre as ProductoNombre, " +
                         "        dp.cantidad,dp.PrecioUnitario " +
                         " FROM DetallePedido dp " +
                         " LEFT JOIN Producto pr on(dp.ProductoID = pr.ID) " +
                         " WHERE dp.PedidoID = @pPedidoID ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pPedidoID",pPedidoID)
            };
            return ComunServicioAiron.Conectar.ObtenerLista<DetallePedidoDTO>(sql, parametros);
        }

        public static ResultadoDTO GuardarDatos(DetallePedidoDTO pDetallePedidoDTO)
        {
            ResultadoDTO res = pDetallePedidoDTO.ValidarDatos();
            if(res.codigoError == 0)
            {
                if(pDetallePedidoDTO.EsNuevo)
                {
                    res = AltaFila(pDetallePedidoDTO);
                }
                else
                {
                    res = ModificarFila(pDetallePedidoDTO);
                }
            }
            return res;
        }

        private static ResultadoDTO AltaFila(DetallePedidoDTO pDetallePedidoDTO)
        {
            ResultadoDTO res = new();
            string sql = " INSERT INTO DetallePedido(PedidoID,ProductoID,Cantidad,PrecioUnitario) " +
                         " VALUES(@pPedidoID,@pProductoID,@pCantidad,@pPrecioUnitario) ";
            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pPedidoID",pDetallePedidoDTO.PedidoID),
                new SqliteParameter("@pProductoID",pDetallePedidoDTO.ProductoID),
                new SqliteParameter("@pCantidad",pDetallePedidoDTO.Cantidad),
                new SqliteParameter("@pPrecioUnitario",pDetallePedidoDTO.PrecioUnitario)
            };
            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);
            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = "Detalle de pedido agregado con éxito.";
            }
            else
            {
                res.codigoError = 107;
                res.mensajeInformacion = $"Error agregando un detalle de pedido.{Environment.NewLine}Consulte Base de Datos.";
            }
            return res;
        }

        private static ResultadoDTO ModificarFila(DetallePedidoDTO pDetallePedidoDTO)
        {
            ResultadoDTO res = new();
            string sql = " UPDATE DetallePedido " +
                         " SET PedidoID = @pPedidoID," +
                         "     ProductoID = @pProductoID, " +
                         "     Cantidad = @pCantidad, " +
                         "     PrecioUnitario = @pPrecioUnitario " +
                         " WHERE ID = @pID ";
            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pPedidoID",pDetallePedidoDTO.PedidoID),
                new SqliteParameter("@pProductoID",pDetallePedidoDTO.ProductoID),
                new SqliteParameter("@pCantidad",pDetallePedidoDTO.Cantidad),
                new SqliteParameter("@pPrecioUnitario",pDetallePedidoDTO.PrecioUnitario),
                new SqliteParameter("@pID",pDetallePedidoDTO.ID)
            };
            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Detalle Pedido {pDetallePedidoDTO.ProductoNombre} modificado correctamente.";
            }
            else
            {
                res.codigoError = 108;
                res.mensajeInformacion = $"Error tratando de modificar el pedido.{Environment.NewLine}Consulte Base de datos.";
            }
            return res;
        }

        public static ResultadoDTO BorrarFila(DetallePedidoDTO pDetallePedidoDTO)
        {
            ResultadoDTO res = new();
            string sql = " DELETE FROM DetallePedido " +
                         " WHERE ID = @pID ";
            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pID",pDetallePedidoDTO.ID)
            };
            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = "Pedido eliminado correctamente.";
            }
            else
            {
                res.codigoError = 108;
                res.mensajeInformacion = $"Error al intentar borrar el detalle pedido.{Environment.NewLine}Consulte Base de Datos.";
            }
            return res;
        }

        public static List<ComboListDTO> ComboProducto()
        {
            string sql = " SELECT ID as Identificador, Nombre as Texto " +
                         " FROM Producto ";
            return ComunServicioAiron.Conectar.ObtenerCombo(sql,null,false);
        }
    }
}
