using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2
{
    internal class Declara
    {
        public static string fileName = @"C:\Prog1\TP_2\Agenda.txt";
        public static string fileName_log = @"C:\Prog1\TP_2\Log.txt";
        public static StreamWriter escritor;
        public static StreamReader lector;
        public static string var_registro = "", var_opcmnu = "";
        public static List<Contacto> list_agenda = new List<Contacto>();
        public static bool bool_cambiosSesion = false;

        public static string[] arr_dominiosEmail = new string[]
        {
            "gmail.com",
            "outlook.com",
            "outlook.com.ar",
            "hotmail.com",
            "hotmail.com.ar",
            "live.com",
            "live.com.ar",
            "yahoo.com",
            "yahoo.com.ar"
        };

        public static string[] arr_campos = new string[]
        {
            "Apellido",
            "Nombre",
            "Teléfono",
            "Email",
            "Dirección"
        };
    }
}
