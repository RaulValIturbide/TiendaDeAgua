using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace ComunServicioAiron
{
    public class Conectar
    {
        private static readonly string dbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "data",
            "database",
            "airon.db"
        );

        public static void ActivarConexion()
        {
            string folder = Path.GetDirectoryName(dbPath);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            string sql = @"
        CREATE TABLE IF NOT EXISTS usuarios (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            nombre TEXT NOT NULL,
            contrasenya TEXT NOT NULL
        );
    ";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Metodo para usar Insert,Delete,Update...
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parametros"></param>
        /// <returns>Devuelve el numero de filas afectadas por el sql, si devuelve -1 ha saltado una excepción</returns>
        public static int EjecutarNonQuery(string sql, SqliteParameter[]? parametros)
        {
            try
            {
                using (var conexion = new SqliteConnection($"Data Source={dbPath}"))
                {
                    conexion.Open();

                    using (var comando = new SqliteCommand(sql, conexion))
                    {
                        // Agregar parámetros si existen
                        if (parametros != null)
                        {
                            comando.Parameters.AddRange(parametros);
                        }

                        return comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en EjecutarNonQuery: " + ex.Message);
                return -1;
            }
        }




    }
}
