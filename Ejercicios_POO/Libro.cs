using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios_POO
{
    internal class Libro
    {
        public string titulo;
        public string autor;
        public string editorial;
        public int anioPublicacion;

        public Libro(string t, string a, string e, int anio)
        {
            titulo = t;
            autor = a;
            editorial = e;
            anioPublicacion = anio;
        }

        public void MostrarLibro()
        {
            Console.WriteLine("Titulo: " + titulo);
            Console.WriteLine("Autor: " + autor);
            Console.WriteLine("Editorial: " + editorial);
            Console.WriteLine("Anio de publicacion: " + anioPublicacion);
            Console.WriteLine();
        }
    }

    class Biblioteca
    {
        public List<Libro> listaLibros = new List<Libro>();

        public void AgregarLibro(Libro libro)
        {
            listaLibros.Add(libro);
            Console.WriteLine("Libro agregado correctamente.");
        }

        public void ListarLibros()
        {
            if (listaLibros.Count == 0)
            {
                Console.WriteLine("No hay libros registrados.");
            }
            else
            {
                Console.WriteLine("Lista de libros:");
                for (int i = 0; i < listaLibros.Count; i++)
                {
                    Console.WriteLine((i + 1) + ".");
                    listaLibros[i].MostrarLibro();
                }
            }
        }

        public void BuscarLibro(string nombre)
        {
            bool encontrado = false;
            foreach (var libro in listaLibros)
            {
                if (libro.titulo.ToLower() == nombre.ToLower())
                {
                    Console.WriteLine("Libro encontrado:");
                    libro.MostrarLibro();
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("No se encontro ningun libro con ese nombre.");
            }
        }
    }
}