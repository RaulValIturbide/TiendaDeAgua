namespace Utilidades.Recursos
{
    public class HerramientaVentana
    {
        /// <summary>
        /// Lo usamos para mostrar la ventana de error 
        /// </summary>
        /// <param name="res"></param>
        public static void MostrarError(ResultadoDTO res)
        {
            VentanaError v = new VentanaError(res);
            v.ShowDialog();
        }

        /// <summary>
        /// La usaremos como mensaje de información al usuario, normalmente para comunicarle
        /// el éxito de algo
        /// </summary>
        /// <param name="mensaje"></param>
        public static void Show(string mensaje)
        {
            VentanaError v = new VentanaError(mensaje);
            v.ShowDialog();
        }

    }
}
