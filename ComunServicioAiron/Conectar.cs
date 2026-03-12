using Microsoft.Data.Sqlite;

namespace ComunServicioAiron
{
    
    public class Conectar
    {

#if DEBUG

        private static readonly string rutaBD = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
           "data",
           "database",
            "airon.db"
        );


#else
        private static readonly string rutaBD = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Airon",
            "airon.db"
        );
#endif


        // private static readonly string rutaBD = Path.GetFullPath("..\\..\\..\\data\\database\\airon.db");

        public static void ActivarConexion()
        {
            string folder = Path.GetDirectoryName(rutaBD);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            // Ruta de la BD incluida en el instalador
            string rutaOrigen = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "data",
                "database",
                "airon.db"
            );

            // Si la BD no existe en AppData, la copiamos desde el instalador
            if (!File.Exists(rutaBD) && File.Exists(rutaOrigen))
            {
                File.Copy(rutaOrigen, rutaBD,overwrite: true);
            }

            using var connection = new SqliteConnection($"Data Source={rutaBD}");
            connection.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS usuarios (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Email Text NULL,
                    Contrasenya TEXT NOT NULL,
                    ModoEntrada INTEGER DEFAULT 0);";

            EjecutarNonQuery(sql, null);
            
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
                using (var conexion = new SqliteConnection($"Data Source={rutaBD}"))
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
        /// <summary>
        /// Metodo para realizar consultas SQL con valor único: Count,Max,Min,Sum...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static T EjecutarEscalar<T>(string sql, SqliteParameter[] parametros)
        {
            try
            {
                using (SqliteConnection conexion = new SqliteConnection($"Data Source={rutaBD}"))
                {
                    conexion.Open();

                    using (var comando = new SqliteCommand(sql, conexion))
                    {
                        if (parametros != null)
                        {
                            comando.Parameters.AddRange(parametros);
                        }

                        object valor = comando.ExecuteScalar();

                        if (valor == null || valor == DBNull.Value)
                        {
                            return default(T);
                        }

                        return (T)Convert.ChangeType(valor, typeof(T));
                    }
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }


        public static T ObternerDatos<T>(string sql, SqliteParameter[] parametros) where T : new()
        {
            using (var conexion = new SqliteConnection($"Data Source={rutaBD}"))
            {
                conexion.Open();

                using (var comando = new SqliteCommand(sql, conexion))
                {
                    if (parametros != null)
                    {
                        comando.Parameters.AddRange(parametros);
                    }

                    using (var reader = comando.ExecuteReader())
                    {
                        if (!reader.Read())
                            return default;

                        T entidad = new T();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string nombreColumna = reader.GetName(i);
                            object valor = reader.GetValue(i);

                            var propiedad = typeof(T).GetProperty(nombreColumna);

                            if (propiedad != null && valor != DBNull.Value)
                            {
                                propiedad.SetValue(entidad, Convert.ChangeType(valor, propiedad.PropertyType));
                            }
                        }
                        return entidad;
                    }
                }
            }
        }

        public static List<T> ObtenerLista<T>(string sql, SqliteParameter[] parametros) where T : new()
        {
            var lista = new List<T>();

            using (var conexion = new SqliteConnection($"Data Source={rutaBD}"))
            {
                conexion.Open();

                using (var comando = new SqliteCommand(sql, conexion))
                {
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);

                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T entidad = new T();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string nombreColumna = reader.GetName(i);
                                object valor = reader.GetValue(i);

                                var propiedad = typeof(T).GetProperty(nombreColumna);

                                if (propiedad != null && valor != DBNull.Value)
                                {
                                    propiedad.SetValue(entidad, Convert.ChangeType(valor, propiedad.PropertyType));
                                }
                            }

                            lista.Add(entidad);
                        }
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Vamos a crear el combo dto para los combobox
        /// TODO : Mejorar esto con mas datos si da tiempo 12/03/2026
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static List<ComboListDTO> ObtenerCombo(string sql, SqliteParameter[] parametros)
        {
            //Creamos lista para devolverla
            List<ComboListDTO> lista = new List<ComboListDTO>();
            using(SqliteConnection conexion = new SqliteConnection($"Data Source={rutaBD}"))
            {
                //Conexión
                conexion.Open();

                using(SqliteCommand comando = new SqliteCommand(sql,conexion))
                {
                    //Añadimos los parametros si los hubiera
                    if(parametros != null)
                    {
                        comando.Parameters.AddRange(parametros);
                    }
                    using (SqliteDataReader reader = comando.ExecuteReader())
                    {
                        //Ahora leemos  y vamos creando los combolISTdto
                        while (reader.Read())
                        {
                            lista.Add(new ComboListDTO
                            {
                                Identificador = reader.GetInt32(0),
                                Texto = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return lista;
        }


    }
}
