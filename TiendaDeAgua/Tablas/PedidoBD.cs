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
    public class PedidoBD
    {
        //Esta es para listar todos los Pedidos teniendo en cuenta su filtro(default es ID)
        public static List<PedidoDTO> ListaPedidoTodo(string selecionFiltro,int pID)
        {
            //string sql = " SELECT p.ID,p.ClienteID,c.Nombre as ClienteNombre,p.Fecha,p.Total,p.EstadoID,e.Nombre as EstadoNombre  " +
            //             " FROM Pedido p left join Cliente c on(p.ClienteID = c.ID)" +
            //             " LEFT JOIN Estado e on(p.EstadoID = e.ID) ";

            string sql = " SELECT  p.ID, p.ClienteID, c.Nombre AS ClienteNombre, p.Fecha, " +
                         "        ROUND(IFNULL(SUM(dp.Cantidad * dp.PrecioUnitario), 0), 2) AS Total, p.EstadoID, e.Nombre AS EstadoNombre" +
                         " FROM Pedido p " +
                         " LEFT JOIN Cliente c ON p.ClienteID = c.ID " +
                         " LEFT JOIN Estado e ON p.EstadoID = e.ID " +
                         " LEFT JOIN DetallePedido dp ON dp.PedidoID = p.ID ";
                        

            SqliteParameter[] parametros = null; //Lo ponemos en nulo para que si no lo usamos respete el null cuando llegue a ComunServicio

            //Si el usuario ha puesto un filtro se añade a la sql
            if (pID != 0)
            {
                sql += " Where p.EstadoID = @pID ";

                parametros = new SqliteParameter[]
                {
                    new SqliteParameter("@pID",pID)
                };
            }
            //EJECUTAMOS AHORA EL GROUP BY PORQUE TIENE QUE IR DESPUES DEL WHERE
            sql += " GROUP BY  p.ID, p.ClienteID, c.Nombre, p.Fecha, p.EstadoID, e.Nombre ";

            //Lo ordenamos por lo que elija el usuario
            switch (selecionFiltro)
            {
                case "id":
                    sql += " ORDER BY p.ID ";
                    break;
                case "nombre":
                    sql += " ORDER BY c.Nombre ";
                    break;
                case "fecha":
                    sql += " ORDER BY p.Fecha Desc ";
                    break;
                case "total":
                    sql += " ORDER BY p.Total";
                    break;
            }

            return ComunServicioAiron.Conectar.ObtenerLista<PedidoDTO>(sql,parametros);
        }
        public static List<ComboListDTO> ComboEstado()
        {
            string sql = " SELECT ID as Identificador,Nombre as Texto " +
                         " FROM Estado ";

            return ComunServicioAiron.Conectar.ObtenerCombo(sql,null,true);
        }
        public static List<ComboListDTO> ComboEstadoBox()
        {
            string sql = " SELECT ID as Identificador,Nombre as Texto " +
             " FROM Estado ";

            return ComunServicioAiron.Conectar.ObtenerCombo(sql, null, false);

        }
        public static List<ComboListDTO> ComboCliente()
        {
            string sql = " SELECT ID as Identificador,Nombre as Texto " +
                         " FROM Cliente ";
            return ComunServicioAiron.Conectar.ObtenerCombo(sql, null, false);
        }

        public static ResultadoDTO GuardaDatos(PedidoDTO pPedidoDTO)
        {
            ResultadoDTO res = pPedidoDTO.ValidarDatos();

            if(res.codigoError == 0)
            {
                if(pPedidoDTO.EsNuevo)
                {
                    res = AltaFila(pPedidoDTO);
                }
                else
                {
                    res = ModificarFila(pPedidoDTO);
                }
            }
            return res;            
        }
        private static ResultadoDTO AltaFila(PedidoDTO pPedido)
        {
            ResultadoDTO res = new();
            string sql = " INSERT INTO Pedido(ClienteID,Fecha,Total,EstadoID) " +
                         " VALUES(@pClienteID,@pFecha,@pTotal,@pEstadoID) ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pClienteID",pPedido.ClienteID),
                new SqliteParameter("@pFecha",pPedido.Fecha),
                new SqliteParameter("@pTotal",pPedido.Total),
                new SqliteParameter("@pEstadoID",pPedido.EstadoID)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Pedido Registrado con éxito.";
            }
            else
            {
                res.codigoError = 108;
                res.mensajeInformacion = $"Error agregando el Pedido.{Environment.NewLine}Compruebe Base de Datos.";
            }
            return res;
        }
        private static ResultadoDTO ModificarFila(PedidoDTO pPedido)
        {
            ResultadoDTO res = new();
            string sql = " UPDATE Pedido " +
                         " SET ClienteID = @pClienteID, " +
                         "     Fecha = @pFecha, " +
                         "     EstadoID = @pEstadoID " +
                         " WHERE ID = @pID ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pClienteID",pPedido.ClienteID),
                new SqliteParameter("@pFecha",pPedido.Fecha),
                new SqliteParameter("@pEstadoID",pPedido.EstadoID),
                new SqliteParameter("@pID",pPedido.ID)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Pedido modificado con éxito.";
            }
            else
            {
                res.codigoError = 109;
                res.mensajeInformacion = $"Error al modificar el pedido.{Environment.NewLine}Compruebe Base de Datos.";
            }
            return res;                        
        }
        public static ResultadoDTO BorrarFila(PedidoDTO pPedido)
        {
            ResultadoDTO res = new();
            string sql = " DELETE FROM Pedido " +
                         " WHERE ID = @pID ";
            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pID",pPedido.ID)
            };
            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);
            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = "Pedido eliminado correctamente.";
            }
            else
            {
                res.codigoError = 107;
                res.mensajeInformacion = $"Error al eliminar el pedido.";
            }
            return res;
        }






    }
}
