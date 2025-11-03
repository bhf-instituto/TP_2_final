using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2
{
    // Validaciones interactivas por consola para nombre, teléfono, email, dirección, etc.
    internal class Validar
    {
        // Valida y construye un string (nombre/apellido). Si buscarContacto=true admite menor restricción.
        public static string func_validarString(bool buscarContacto = false)
        {
            string resultado = "";
            ConsoleKeyInfo tecla;
            int[] cursorPos = { Console.CursorLeft, Console.CursorTop };
            bool espacioUsado = false;
            int maxCaracteres = 15;

            do
            {
                tecla = Console.ReadKey(true);

                if (char.IsLetter(tecla.KeyChar))
                {
                    if (resultado.Length < maxCaracteres)
                    {
                        resultado += tecla.KeyChar;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(tecla.KeyChar);
                    }
                }
                else if (tecla.Key == ConsoleKey.Backspace && resultado.Length > 0)
                {
                    // Si se borra un espacio permitimos volver a usar otro
                    if (resultado[resultado.Length - 1] == ' ')
                        espacioUsado = false;

                    resultado = resultado.Substring(0, resultado.Length - 1);
                    Console.Write("\b \b");
                }
                else if (tecla.Key == ConsoleKey.Spacebar)
                {
                    // permito un solo espacio 
                    if (!espacioUsado && resultado.Length > 0)
                    {
                        resultado += " ";
                        espacioUsado = true;
                        Console.Write(" ");
                    }
                }
                else if (tecla.Key == ConsoleKey.Enter)
                {
                    if (buscarContacto)
                    {
                        if (!string.IsNullOrEmpty(resultado.Trim())) break;
                        else
                        {
                            Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1],
                                "No puede estar vacío");
                            resultado = "";
                            espacioUsado = false;
                            continue;
                        }
                    }
                    else
                    {
                        if (resultado.Trim().Length > 2 && !buscarContacto) break;
                        else
                        {
                            Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1],
                                "El nombre o apellido debe tener al menos 3 caracteres");
                            resultado = "";
                            espacioUsado = false;
                        }
                    }
                }

            } while (true);

            Funciones.func_borrarLinea(cursorPos[1]);
            Console.ResetColor();

            // en el archivo de texto, todos los string se guardan en minuscula.
            // pero al renderizar las tablas, los nombres se formatean correctamente
            // respetando las mayusculas. 
            return resultado.Trim().ToLower();
        }

        // Valida entrada de teléfono interactiva (prefijos y longitud).
        public static string func_validarTelefono(bool buscarTelefono = false)
        {
            string telefono = "";
            ConsoleKeyInfo tecla;
            int[] cursorPos = { Console.CursorLeft, Console.CursorTop };
            int maxCaracteres = 10;

            do
            {
                tecla = Console.ReadKey(true);

                if (char.IsDigit(tecla.KeyChar))
                {
                    // Chequeo de prefijos (11 o 2224)
                    if (telefono.Length == 0)
                    {
                        if (tecla.KeyChar == '1' || tecla.KeyChar == '2')
                        {
                            telefono += tecla.KeyChar;
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(tecla.KeyChar);
                        }
                    }
                    else if (telefono.Length == 1)
                    {
                        if (telefono[0] == '1' && tecla.KeyChar == '1')
                        {
                            telefono += tecla.KeyChar;
                            Console.Write(tecla.KeyChar);
                        }
                        else if (telefono[0] == '2' && tecla.KeyChar == '2')
                        {
                            telefono += tecla.KeyChar;
                            Console.Write(tecla.KeyChar);
                        }
                    }
                    else if (telefono.Length == 2)
                    {
                        if (telefono.StartsWith("22") && tecla.KeyChar == '2')
                        {
                            telefono += tecla.KeyChar;
                            Console.Write(tecla.KeyChar);
                        }
                        else if (telefono.StartsWith("11"))
                        {
                            // Chequeo de prefijo 11
                            telefono += tecla.KeyChar;
                            Console.Write(tecla.KeyChar);
                        }
                    }
                    else if (telefono.Length == 3)
                    {
                        if (telefono.StartsWith("222") && tecla.KeyChar == '4')
                        {
                            telefono += tecla.KeyChar;
                            Console.Write(tecla.KeyChar);
                        }
                        else if (telefono.StartsWith("11"))
                        {
                            telefono += tecla.KeyChar;
                            Console.Write(tecla.KeyChar);
                        }
                    }
                    else
                    {
                        // prefijo completo, a partir de aca cualquier número es válido. 
                        if (telefono.Length < maxCaracteres)
                        {
                            telefono += tecla.KeyChar;
                            Console.Write(tecla.KeyChar);
                        }
                    }
                }
                else if (tecla.Key == ConsoleKey.Backspace && telefono.Length > 0)
                {
                    telefono = telefono.Substring(0, telefono.Length - 1);
                    Console.Write("\b \b");
                }
                else if (tecla.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    Console.ResetColor();
                    if (buscarTelefono)
                    {
                        if (!string.IsNullOrEmpty(telefono)) return telefono;
                        else
                        {
                            Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1],
                            "No puede estar vacío\n");
                            //Console.ResetColor();
                            telefono = "";
                            continue;
                        }
                    }
                    else
                    {
                        if (telefono.Length == 10)
                        {
                            Funciones.func_borrarLinea(cursorPos[1]);
                            Console.ResetColor();
                            //Console.WriteLine();
                            return telefono;
                        }
                        else
                        {
                            Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1],
                            "Debe empezar con 11 o 2224 y tener 10 dígitos\n");
                            telefono = "";
                            continue;
                        }
                    }
                }

            } while (true);
        }

        // Valida número de línea existente en la lista (usado para eliminar/editar).
        public static string func_validarNumeroDeLinea()
        {
            ConsoleKeyInfo tecla;
            int cantContactos = Declara.list_agenda.Count();
            string numero = "";
            int numeroInt = 0;
            int maxLongitud = 10;
            int[] cursorPos = { Console.CursorLeft, Console.CursorTop };

            do
            {
                tecla = Console.ReadKey(true);

                if (char.IsDigit(tecla.KeyChar))
                {
                    if (numero.Length < maxLongitud)
                    {
                        numero += tecla.KeyChar;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(tecla.KeyChar);
                    }
                }
                else if (tecla.Key == ConsoleKey.Escape) return "volver";
                else if (tecla.Key == ConsoleKey.Backspace && numero.Length > 0)
                {
                    numero = numero.Substring(0, numero.Length - 1);
                    Console.Write("\b \b");
                }
                else if (tecla.Key == ConsoleKey.Enter)
                {
                    if (string.IsNullOrWhiteSpace(numero))
                    {
                        Funciones.func_mostrarMensajeError
                            (cursorPos[0], cursorPos[1], "No puede estar vacío    ");
                        numero = "";
                        continue;
                    }
                    else
                    {
                        numeroInt = Convert.ToInt32(numero);
                    }

                    if (numeroInt > 0 && numeroInt <= cantContactos)
                    {
                        Console.ResetColor();
                        return numero;
                    }
                    else
                    {
                        Funciones.func_mostrarMensajeError
                            (cursorPos[0], cursorPos[1], "Contacto no existe      ");
                        numero = "";
                        numeroInt = 0;
                        continue;
                    }
                }

            } while (true);
        }

        // Valida email interactivo y comprueba dominios permitidos.
        public static string func_validarEmail()
        {
            string email = "";
            ConsoleKeyInfo tecla;
            int[] cursorPos = { Console.CursorLeft, Console.CursorTop };
            bool arrobaUsada = false;
            int maxUsuario = 25;
            int maxEmail = 40;

            do
            {
                tecla = Console.ReadKey(true);
                if (char.IsLetterOrDigit(tecla.KeyChar) || tecla.KeyChar == '.' || tecla.KeyChar == '-' || tecla.KeyChar == '_')
                {
                    if (!arrobaUsada && email.Length < maxUsuario)
                    {
                        email += tecla.KeyChar;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(tecla.KeyChar);
                    }
                    else if (arrobaUsada && email.Length < maxEmail)
                    {
                        email += tecla.KeyChar;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(tecla.KeyChar);
                    }
                }
                else if (tecla.KeyChar == '@')
                {
                    if (!arrobaUsada && email.Length > 0)
                    {
                        email += "@";
                        arrobaUsada = true;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("@");
                    }
                }
                else if (tecla.Key == ConsoleKey.Backspace && email.Length > 0)
                {
                    if (email[email.Length - 1] == '@') arrobaUsada = false;
                    email = email.Substring(0, email.Length - 1);
                    Console.Write("\b \b");
                }
                else if (tecla.Key == ConsoleKey.Enter)
                {
                    email = email.Trim().ToLower();

                    if (string.IsNullOrWhiteSpace(email))
                    {
                        Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1], "No puede estar vacío                                ");
                        email = "";
                        arrobaUsada = false;
                        continue;
                    }

                    if (!email.Contains("@"))
                    {
                        Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1], "Debe contener @                                      ");
                        email = "";
                        arrobaUsada = false;
                        continue;
                    }

                    string[] partes = email.Split('@');
                    string usuario = partes[0];

                    if (usuario.Length < 6)
                    {
                        Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1], "La parte antes del @ debe tener al menos 6 caracteres");
                        email = "";
                        arrobaUsada = false;
                        continue;
                    }

                    bool tieneLetra = false;
                    foreach (char c in usuario)
                    {
                        if (char.IsLetter(c))
                        {
                            tieneLetra = true;
                            break;
                        }
                    }

                    if (!tieneLetra)
                    {
                        Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1], "La parte antes del @ debe contener al menos una letra");
                        email = "";
                        arrobaUsada = false;
                        continue;
                    }

                    bool emailValido = false;
                    foreach (string dominioEmail in Declara.arr_dominiosEmail)
                    {
                        if (email.EndsWith("@" + dominioEmail))
                        {
                            emailValido = true;
                            break;
                        }
                    }
                    if (!emailValido)
                    {
                        Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1], "Dominio de correo inválido                             ");
                        email = "";
                        arrobaUsada = false;
                        continue;
                    }

                    Console.WriteLine();
                    break;
                }

            } while (true);

            Funciones.func_borrarLinea(cursorPos[1]);
            Console.ResetColor();
            return email;
        }

        // Valida dirección (calle + número) y la normaliza en minúsculas.
        public static string func_validarDireccion()
        {
            string direccion = "";
            ConsoleKeyInfo tecla;
            int[] cursorPos = { Console.CursorLeft, Console.CursorTop };
            int maxDireccion = 30;

            do
            {
                tecla = Console.ReadKey(true);

                if (char.IsLetter(tecla.KeyChar))
                {
                    if (direccion.Length < maxDireccion)
                    {
                        direccion += tecla.KeyChar;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(tecla.KeyChar);
                    }
                }
                else if (char.IsDigit(tecla.KeyChar))
                {
                    if (direccion.Length < maxDireccion)
                    {
                        direccion += tecla.KeyChar;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(tecla.KeyChar);
                    }
                }
                else if (tecla.Key == ConsoleKey.Spacebar)
                {
                    // Evita espacios al inicio o dobles espacios
                    if (direccion.Length > 0 && direccion[direccion.Length - 1] != ' ' && direccion.Length < maxDireccion)
                    {
                        direccion += " ";
                        Console.Write(" ");
                    }
                }
                else if (tecla.Key == ConsoleKey.Backspace && direccion.Length > 0)
                {
                    direccion = direccion.Substring(0, direccion.Length - 1);
                    Console.Write("\b \b");
                }
                else if (tecla.Key == ConsoleKey.Enter)
                {
                    direccion = direccion.Trim();

                    if (string.IsNullOrWhiteSpace(direccion))
                    {
                        Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1], "No puede estar vacío         ");
                        direccion = "";
                        continue;
                    }

                    string[] partes = direccion.Split(' ');
                    if (partes.Length < 2)
                    {
                        Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1], "Debe ingresar calle y número ");
                        direccion = "";
                        continue;
                    }

                    string numero = partes[partes.Length - 1];
                    bool esNumero = true;

                    foreach (char c in numero)
                    {
                        if (!char.IsDigit(c))
                        {
                            esNumero = false;
                            break;
                        }
                    }

                    if (!esNumero)
                    {
                        Funciones.func_mostrarMensajeError(cursorPos[0], cursorPos[1], "Debe terminar con un número  ");
                        direccion = "";
                        continue;
                    }

                    Console.WriteLine();
                    break;
                }

            } while (true);

            Funciones.func_borrarLinea(cursorPos[1]);
            Console.ResetColor();
            return direccion.ToLower();
        }
    }
}
