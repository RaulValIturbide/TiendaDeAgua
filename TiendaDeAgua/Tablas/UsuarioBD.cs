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
    public class UsuarioBD
    {
        public static bool UsuarioExistente(UsuarioDTO pUsuarioDTO) 
        {
            string sql = " SELECT Count(*) " +
                         " FROM usuarios " +
                         " WHERE nombre = @pNombre " +
                         " AND Contrasenya = @pContrasenya ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pContrasenya",pUsuarioDTO.Contrasenya),
                new SqliteParameter("@pNombre",pUsuarioDTO.Nombre)
            };

            return ComunServicioAiron.Conectar.EjecutarEscalar<int>(sql, parametros) > 0 ? true : false;
        }


        public static ResultadoDTO AltaFila(UsuarioDTO pUsuarioDTO)
        {
            ResultadoDTO res = new();
            string sql = " INSERT INTO usuarios(nombre,contrasenya) " +
                         " VALUES(@pNombre,@pContrasenya) ";

            var parametros = new SqliteParameter[]
            {
                new SqliteParameter("@pNombre",pUsuarioDTO.Nombre),
                new SqliteParameter("@pContrasenya",pUsuarioDTO.Contrasenya)
            };

            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);

            if(filasAfectadas > 0)
            {
                res.mensajeInformacion = "Usuario dado de alta con éxito!";
            }
            else
            {
                res.codigoError = 103;
                res.mensajeInformacion = "Error al registrar el usuario!";
            }

            return res;
        }
    }
}
