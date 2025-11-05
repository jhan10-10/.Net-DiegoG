using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios_POO
{
    internal class Persona
    {
        public string nombre;
        public int edad;
        public string genero;
        public string telefono;

       
        public Persona(string n, int e, string g, string t)
        {
            nombre = n;
            edad = e;
            genero = g;
            telefono = t;
        }

        public void EditarInformacion()
        {
            Console.WriteLine("Editar informacion de la persona:");
            Console.Write("Nuevo nombre: ");
            nombre = Console.ReadLine();

            Console.Write("Nueva edad: ");
            edad = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nuevo genero (F o M): ");
            genero = Console.ReadLine();

            Console.Write("Nuevo telefono: ");
            telefono = Console.ReadLine();

            Console.WriteLine("Informacion actualizada.");
        }

        public void ImprimirDetalles()
        {
            Console.WriteLine();
            Console.WriteLine("Detalles de la persona:");
            Console.WriteLine("Nombre: " + nombre);
            Console.WriteLine("Edad: " + edad);
            Console.WriteLine("Genero: " + genero);
            Console.WriteLine("Telefono: " + telefono);
        }

        public void CalcularEdadEnDias()
        {
            int dias = edad * 365;
            Console.WriteLine("La edad en dias es: " + dias + " dias");
        }
    }
}
