using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2
{
    // Modelo simple que representa un contacto de la agenda.
    internal class Contacto
    {
        public string Apellido, Nombre, Telefono, Email, Direccion;

        // Convierte el contacto a la línea que se guarda en el archivo (separador ';').
        public override string ToString()
        {
            return Apellido + ";" + Nombre + ";" + Telefono + ";" + Email + ";" + Direccion;
        }

        // Devuelve una copia superficial del contacto (útil para logs antes/después).
        public Contacto Clonar()
        {
            return new Contacto
            {
                Apellido = this.Apellido,
                Nombre = this.Nombre,
                Telefono = this.Telefono,
                Email = this.Email,
                Direccion = this.Direccion
            };
        }
    }
}
