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
    public class ClienteBD
    {
        //Llamamos los datos de cliente y lo ordenamos por uno de los campos que el usuario elija
        public static List<ClienteDTO> ListaCliente(string ordenarPor)
        {
            string sql = " SELECT ID,Nombre,Contacto,Direccion " +
                         " FROM Cliente ";

            switch (ordenarPor)
            {
                case "id":
                    sql += " ORDER BY ID ";
                    break;
                case "nombre":
                    sql += " ORDER BY Nombre ";
                    break;
                case "contacto":
                    sql += " ORDER BY Contacto ";
                    break;
                case "direccion":
                    sql += " ORDER BY Direccion";
                    break;
                case null:
                    break;
            }

            return ComunServicioAiron.Conectar.ObtenerLista<ClienteDTO>(sql, null);
        }

        public static ResultadoDTO GuardarDatos(ClienteDTO pClienteDTO)
        {
            ResultadoDTO res = pClienteDTO.ValidarDatos();

            if (res.codigoError == 0)
            {
                if (pClienteDTO.EsNuevo)
                {
                    res = AltaFila(pClienteDTO);
                }
                else
                {
                    res = ModificarFila(pClienteDTO);
                }
            }
            return res;
        }

        private static ResultadoDTO AltaFila(ClienteDTO pClienteDTO)
        {
            ResultadoDTO res = new();
            string sql = " INSERT INTO Cliente(Nombre,Contacto,Direccion) " +
                         " VALUES(@pNombre,@pContacto,@pDireccion) ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pNombre",pClienteDTO.Nombre),
                new SqliteParameter("@pContacto",pClienteDTO.Contacto),
                new SqliteParameter("@pDireccion",pClienteDTO.Direccion)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if (filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Cliente \"{pClienteDTO.Nombre}\" agregado con éxito.";
            }
            else
            {
                res.codigoError = 106;
                res.mensajeInformacion = $"Error al registrar el Cliente.{Environment.NewLine}Compruebe la Base de datos";
            }

            return res;
        }

        private static ResultadoDTO ModificarFila(ClienteDTO pClienteDTO)
        {
            ResultadoDTO res = new();
            string sql = " UPDATE Cliente " +
                         " SET Nombre = @pNombre," +
                         "     Contacto = @pContacto, " +
                         "     Direccion = @pDireccion" +
                         " WHERE ID = @pID ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pNombre",pClienteDTO.Nombre),
                new SqliteParameter("@pContacto",pClienteDTO.Contacto),
                new SqliteParameter("@pDireccion",pClienteDTO.Direccion),
                new SqliteParameter("@pID",pClienteDTO.ID)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if (filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Cliente \"{pClienteDTO.Nombre}\" modificado correctamente.";
            }
            else
            {
                res.codigoError = 107;
                res.mensajeInformacion = $"Error al registrar el Cliente.{Environment.NewLine}Compruebe la Base de Datos.";
            }
            return res;
        }

        public static ResultadoDTO BorrarFila(ClienteDTO pClienteDTO)
        {
            ResultadoDTO res = new();
            string sql = " DELETE FROM Cliente " +
                         " WHERE ID = @pID ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pID", pClienteDTO.ID)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);
            if (filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Cliente \"{pClienteDTO.Nombre}\" borrado correctamente.";
            }
            else
            {
                res.codigoError = 108;
                res.mensajeInformacion = $"Error al intentar eliminar el cliente.{Environment.NewLine}Compruebe la Base de Datos.";
            }
            return res;
        }
    }
}
