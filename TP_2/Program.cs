using TP_2;

internal class Program
{
    private static void Main(string[] args)
    {
        Funciones.ConfigInicial();

        do
        {
            if (Declara.bool_cambiosSesion)
            {
                Escritura.func_actualizarArchivo();
            }

            Funciones.func_menu();

            switch (Declara.var_opcmnu)
            {
                case "Cargar Contacto":
                    Escritura.func_cargarContacto();
                    break;
                case "Buscar Contactos":
                    Funciones.func_menuBuscarContacto();
                    break;
                case "Mostrar agenda completa":
                    Funciones.func_menuMostrarAgenda();
                    break;
                case "Eliminar un contacto":
                    Escritura.func_eliminarContacto();
                    break;
                case "Editar un contacto":
                    Escritura.func_editarContacto();
                    break;
            }

            if (Declara.var_opcmnu == "[grey35]Salir[/]") break;
        } while (true);
    }
}