using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    internal class Persona
    {
  
            // Propiedades
            public string Nombre { get; set; }
            public int Edad { get; set; }
            public char Genero { get; set; }
            public string Telefono { get; set; }

            // Constructor
            public Persona(string nombre, int edad, char genero, string telefono)
            {
                Nombre = nombre;
                Edad = edad;
                Genero = genero;
                Telefono = telefono;
            }

            // Método para imprimir detalles
            public void ImprimirDetalles()
            {
                Console.WriteLine("----- DETALLES DE LA PERSONA -----");
                Console.WriteLine($"Nombre: {Nombre}");
                Console.WriteLine($"Edad: {Edad} años");
                Console.WriteLine($"Género: {Genero}");
                Console.WriteLine($"Teléfono: {Telefono}");
                Console.WriteLine("----------------------------------");
            }

            // Método para calcular edad en días (aproximada)
            public int CalcularEdadEnDias()
            {
                return Edad * 365;
            }

    }
}
