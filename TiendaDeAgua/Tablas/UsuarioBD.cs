using Microsoft.Data.Sqlite;
using TiendaDeAgua.DTOs;
using Utilidades.Recursos;

namespace TiendaDeAgua.Tablas
{
    public class UsuarioBD
    {

        public static List<UsuarioDTO> ListaUsuario()
        {
            string sql = " SELECT ID,Nombre,Contrasenya,ModoEntrada " +
                         " FROM usuarios ";

            return ComunServicioAiron.Conectar.ObtenerLista<UsuarioDTO>(sql, null);
        }

        /// <summary>
        /// Busca el usuario con los datos dados en la lista de usuarios de la BBDD
        /// </summary>
        /// <param name="pUsuarioDTO"></param>
        /// <returns></returns>
        public static UsuarioDTO AccesoUsuario(UsuarioDTO pUsuarioDTO)
        {
            string sql = " SELECT ID,Nombre,Contrasenya,ModoEntrada " +
                         " FROM usuarios " +
                         " WHERE Nombre = @pNombre " +
                         " AND Contrasenya = @pContrasenya ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pNombre",pUsuarioDTO.Nombre),
                new SqliteParameter("@pContrasenya",pUsuarioDTO.Contrasenya)
            };

            return ComunServicioAiron.Conectar.ObternerDatos<UsuarioDTO>(sql, parametros);
        }

        public static ResultadoDTO GuardarDatos(UsuarioDTO pUsuarioDTO)
        {
            ResultadoDTO res = pUsuarioDTO.ValidarDatos();

            if (res.codigoError == 0)
            {
                if (pUsuarioDTO.EsNuevo)
                {
                    res = AltaFila(pUsuarioDTO);
                }
                else
                {
                    res = ModificarFila(pUsuarioDTO);
                }
            }
            return res;
        }

        private static ResultadoDTO ModificarFila(UsuarioDTO pUsuarioDTO)
        {
            ResultadoDTO res = new();
            string sql = " UPDATE usuarios " +
                         " SET Nombre = @pNombre, " +
                         " Contrasenya = @pContrasenya, " +
                         " ModoEntrada = @pModoEntrada " +
                         " WHERE ID = @pID ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pNombre",pUsuarioDTO.Nombre),
                new SqliteParameter("@pContrasenya",pUsuarioDTO.Contrasenya),
                new SqliteParameter("@pModoEntrada",pUsuarioDTO.ModoEntrada),
                new SqliteParameter("@pID",pUsuarioDTO.ID)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if (filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Usuario\"{pUsuarioDTO.Nombre}\" modificado con éxito.";
            }
            else
            {
                res.codigoError = 104;
                res.mensajeInformacion = $"Error al modificar el usuario.{Environment.NewLine}Consulte Base de Datos.";
            }
            return res;

        }
        private static ResultadoDTO AltaFila(UsuarioDTO pUsuarioDTO)
        {
            ResultadoDTO res = new();

            res = pUsuarioDTO.ValidarDatos();

            if (res.codigoError == 0)
            {
                string sql = " INSERT INTO usuarios(Nombre,Contrasenya) " +
                             " VALUES(@pNombre,@pContrasenya) ";

                var parametros = new SqliteParameter[]
                {
                    new SqliteParameter("@pNombre",pUsuarioDTO.Nombre),
                    new SqliteParameter("@pContrasenya",pUsuarioDTO.Contrasenya)
                };

                int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

                if (filasAfectadas > 0)
                {
                    res.mensajeInformacion = "Usuario dado de alta con éxito!";
                }
                else
                {
                    res.codigoError = 103;
                    res.mensajeInformacion = "Error al registrar el usuario!";
                }
            }

            return res;
        }

        public static ResultadoDTO BorrarFila(UsuarioDTO pUsuarioDTO)
        {
            ResultadoDTO res = new();
            string sql = " DELETE FROM usuarios " +
                         " WHERE ID = @pID ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pID",pUsuarioDTO.ID)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if (filasAfectadas > 0)
            {
                res.mensajeInformacion = $"Usuario \"{pUsuarioDTO.Nombre}\" eliminado correctamente.";
            }
            else
            {
                res.codigoError = 105;
                res.mensajeInformacion = $"Error tratando de eliminar el usuario.{Environment.NewLine}Consulte Base de Datos.";
            }
            return res;
        }

    }
}
