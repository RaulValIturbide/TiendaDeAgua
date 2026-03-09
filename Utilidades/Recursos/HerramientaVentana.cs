using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.Recursos
{
    public class HerramientaVentana
    {
        public static void MostrarError(ResultadoDTO res)
        {
            VentanaError v = new VentanaError(res);
            v.ShowDialog();
        }

        public static void Show(string mensaje)
        {
            VentanaError v = new VentanaError(mensaje);
            v.ShowDialog();
        }

    }
}
