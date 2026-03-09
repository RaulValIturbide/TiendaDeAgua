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
        public static ResultadoDTO UsuarioExistente(UsuarioDTO pUsuarioDTO) 
        {
            ResultadoDTO res = new();
            res = pUsuarioDTO.ValidarDatos();
            if(res.codigoError == 0)
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

                int filasEncontradas = ComunServicioAiron.Conectar.EjecutarEscalar<int>(sql, parametros);

                if(filasEncontradas <= 0)
                {
                    res.codigoError = 104;
                    res.mensajeInformacion = $"Usuario o contraseña incorrecta.{Environment.NewLine}Registrese primero si aún no lo está.";
                }
            }
            return res;
        }

        public static UsuarioDTO AccesoUsuario(UsuarioDTO pUsuarioDTO)
        {
            string sql = " SELECT Nombre,Contrasenya,ModoEntrada " +
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

        public static ResultadoDTO AltaFila(UsuarioDTO pUsuarioDTO)
        {
            ResultadoDTO res = new();

            res = pUsuarioDTO.ValidarDatos();

            if(res.codigoError == 0)
            {
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
            }

            return res;
        }
    }
}
