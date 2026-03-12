using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace Utilidades.Recursos
{
    /// <summary>
    /// Esta clase sirve para todo aquello que envuelve la sesión de un usuario
    /// quien es, como lo usa,gestiona a través de toda la app con datos estáticos
    /// </summary>
    public class Sesion
    {
        //Aqui guardamos datos importantes, ahora mismo solo se guarda el nombre y modo entrada del usuario, pero es escalable
        public static Dictionary<string,string> llaves = new();
        public static Frame? NavegadorPantallas;

        //Este metodo lo usamos para renovar el diccionario cuando el usuario sale de la sesion
        public static void RenovarLlaves()
        {
            llaves = new();
        }
        /// <summary>
        /// Guardamos el frame para usarlo de manera estática en el resto
        /// de la app
        /// </summary>
        /// <param name="frame"></param>
        public static void GuardarFrame(Frame frame)
        {
            NavegadorPantallas = frame;
        }
        /// <summary>
        /// Lo usamos para cambiar de pestañas usando el frame inicial
        /// </summary>
        /// <returns></returns>
        public static Frame GestorPantalla()
        {
            if(NavegadorPantallas != null)
            {
                return NavegadorPantallas;
            }
            else
            {
                return null;
            }
        }

        //Vuelve a la pantalla anterior si la hay
        public static void VolverAtras()
        {
            if(NavegadorPantallas != null)
            {
                NavegadorPantallas.NavigationService.GoBack();
            }          
        }
    }
}
