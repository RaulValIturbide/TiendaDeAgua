using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Utilidades.Recursos
{
    public class Sesion
    {

        //Aqui guardamos datos importantes
        public static Dictionary<string,string> llaves = new();


        //Este metodo lo usamos para renovar el diccionario cuando el usuario sale de la sesion
        public static void RenovarLlaves()
        {
            llaves = new();
        }



        
    }
}
