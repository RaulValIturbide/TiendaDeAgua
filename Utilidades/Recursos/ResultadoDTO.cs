namespace Utilidades.Recursos
{
    /// <summary>
    /// Esta clase sirve como puente para referenciar si las consultas se han realizado con éxito o cualquier tipo de
    /// conflicto que pudiese tener el usuario (falta de datos por ejemplo) en cualquierade las tablas
    /// </summary>
    public class ResultadoDTO
    {
        public string mensajeInformacion { get; set; } = string.Empty;
        public int codigoError { get; set; } = 0;
    }
}
