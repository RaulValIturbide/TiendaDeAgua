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
    public class ProductoBD
    {
        public static List<ProductoDTO> ListaProducto()
        {
            string sql = " Select p.ID,p.Nombre,Precio,Stock,p.CategoriaID,c.Nombre as CategoriaNombre " +
                         " FROM Producto p  " +
                         " LEFT JOIN Categoria c on(p.CategoriaID = c.ID) ";

            return ComunServicioAiron.Conectar.ObtenerLista<ProductoDTO>(sql,null);
        }
        public static ResultadoDTO GuardarDatos(ProductoDTO pProductoDTO)
        {
            ResultadoDTO res = pProductoDTO.ValidarDatos();

            if(res.codigoError == 0)
            {
                if(pProductoDTO.EsNuevo)
                {
                    res = AltaFila(pProductoDTO);
                }
                else
                {
                    res = ModificarFila(pProductoDTO);
                }
            }
            return res;
        }
        private static ResultadoDTO AltaFila(ProductoDTO pProductoDTO)
        {
            ResultadoDTO res = new();
            string sql = " INSERT INTO Producto(Nombre,Precio,Stock,CategoriaID) " +
                         " VALUES(@pNombre,@pPrecio,@pStock,@pCategoria) ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pNombre",pProductoDTO.Nombre),
                new SqliteParameter("@pPrecio",pProductoDTO.Precio),
                new SqliteParameter("@pStock",pProductoDTO.Stock),
                new SqliteParameter("@pCategoria",pProductoDTO.CategoriaID)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Producto añadido con éxito.";
            }
            else
            {
                res.codigoError = 103;
                res.mensajeInformacion = $"Error al agregar el producto.{Environment.NewLine}Consulte Base de Datos.";
            }
            return res;
        }
        private static ResultadoDTO ModificarFila(ProductoDTO pProductoDTO)
        {
            ResultadoDTO res = new();
            string sql = " UPDATE Producto " +
                         " SET Nombre = @pNombre, " +
                         "     Precio = @pPrecio, " +
                         "     Stock = @pStock, " +
                         "     CategoriaID = @pCategoria " +
                         " WHERE ID = @pID ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pNombre",pProductoDTO.Nombre),
                new SqliteParameter("@pPrecio",pProductoDTO.Precio),
                new SqliteParameter("@pStock",pProductoDTO.Stock),
                new SqliteParameter("@pCategoria",pProductoDTO.CategoriaID),
                new SqliteParameter("@pID",pProductoDTO.ID)
            };
            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);
            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Producto \"{pProductoDTO.Nombre}\" modificado con éxito.";
            }
            else
            {
                res.codigoError = 104;
                res.mensajeInformacion = $"Error al intentar modificar el producto.{Environment.NewLine}Consulte la Base de Datos.";
            }
            return res;
        }
        public static ResultadoDTO BorrarFila(ProductoDTO pProductoDTO)
        {
            ResultadoDTO res = new();
            string sql = " DELETE FROM Producto " +
                         " WHERE ID = @pID ";
            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pID",pProductoDTO.ID)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);
            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Producto \"{pProductoDTO.Nombre}\" eliminado correctamente.";
            }
            else
            {
                res.codigoError = 105;
                res.mensajeInformacion = $"Error al tratar de eliminar el Producto.{Environment.NewLine}Consulte Base de Datos.";
            }
            return res;
        }
        public static List<ComboListDTO> ComboCategoria()
        {
            string sql = " SELECT ID as Identificador, Nombre as Texto " +
                         " FROM Categoria ";
            return ComunServicioAiron.Conectar.ObtenerCombo(sql, null,false);
        }


    }
}
