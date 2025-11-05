using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Array
            //int[] numeros = new int[3];
            //for (int i = 0;  i<3; i++) {
            //    Console.WriteLine("Ingrese numero: "+ (i+i)+ ": ");
            //    numeros[i] = int.Parse(Console.ReadLine());
            //}
            //Console.WriteLine("\n numeros ingresados");
            //foreach (var item in numeros)
            //{
            //    Console.WriteLine(item);
            //}
            //int suma = 0;
            //for (int i=0; i<3; i++){
            //    suma += numeros[i];
            //}
            //Console.WriteLine("La suma de los numeros es: "+ suma);

            //Listas
            //List<int> numeros = new List<int>();
            //numeros.Add(10); //agregar numeros a la lista
            //numeros.Add(20);
            //numeros.Add(30);
            //Console.WriteLine("numeros de la lista");
            //foreach (int item in numeros)
            //{
            //    Console.WriteLine(item);
            //}
            //// acceder a un elemneto del indice
            //int primerNumero = numeros[1];
            //Console.WriteLine($"El numero en la lista es: {primerNumero}" );
            ////Modificar un elemento de la lista
            //numeros[2] = 50;
            //Console.WriteLine($"Numero Modificado: {numeros[2]}");
            ////eliminar un elemento de la lista especifica
            //numeros.RemoveAt(1);// elimina la posicion 0

            ////eliminar un elemento por su valor
            //numeros.Remove(10);

            //ejercicio 1
            //List<string> productos = new List<string>();
            //List<double> precios = new List<double>();

            //int opcion = 0;

            //while (opcion != 5)
            //{
            //    Console.WriteLine("====== Menu De Productos ======");
            //    Console.WriteLine("1. Agregar producto");
            //    Console.WriteLine("2. Mostrar productos");
            //    Console.WriteLine("3. Actualizar producto");
            //    Console.WriteLine("4. Eliminar producto");
            //    Console.WriteLine("5. Irse");
            //    Console.WriteLine("Elige una opción para proceder: ");
            //    opcion = int.Parse(Console.ReadLine());

            //    if (opcion == 1)
            //    {
            //        Console.WriteLine("Nombre del producto: ");
            //        string nombre = Console.ReadLine();
            //        Console.WriteLine("Precio: ");
            //        double precio = double.Parse(Console.ReadLine());
            //        productos.Add(nombre);
            //        precios.Add(precio);
            //        Console.WriteLine("Producto agregado ");
            //    }
            //    else if (opcion == 2)
            //    {
            //        Console.WriteLine("=== LISTA DE PRODUCTOS ===");
            //        for (int i = 0; i < productos.Count; i++)
            //        {
            //            Console.WriteLine($"{i + 1}. {productos[i]} - ${precios[i]}");
            //        }
            //    }
            //    else if (opcion == 3)
            //    {
            //        Console.WriteLine("Número del producto a actualizar: ");
            //        int num = int.Parse(Console.ReadLine()) - 1;
            //        if (num >= 0 && num < productos.Count)
            //        {
            //            Console.WriteLine("Nuevo nombre: ");
            //            productos[num] = Console.ReadLine();
            //            Console.WriteLine("Nuevo precio: ");
            //            precios[num] = double.Parse(Console.ReadLine());
            //            Console.WriteLine("Producto actualizado ");
            //        }

            //    }
            //    else if (opcion == 4)
            //    {
            //        Console.WriteLine("Número del producto a eliminar: ");
            //        int num = int.Parse(Console.ReadLine()) - 1;
            //        if (num >= 0 && num < productos.Count)
            //        {
            //            productos.RemoveAt(num);
            //            precios.RemoveAt(num);
            //            Console.WriteLine("Producto eliminado ");
            //        }

            //    }
            //    else if (opcion == 5)
            //    {
            //        Console.WriteLine("Saliendo del programa... ");
            //    }

            //    Console.WriteLine();
            //}

            //creamos el objeto auto
            //Auto miauto = new Auto("Toyota", "Corolla", "2025");//instancia de la clase auto
            //miauto.Mostrarinfo();

            //Auto bus = new Auto("Honda", "Civic", "2019");
            //bus.Mostrarinfo();

            //Auto camion = new Auto("Ford", "F-150", "2021");
            //camion.Mostrarinfo();

            ////editar inforamcion del auto
            //camion.Año = "2026";
            //camion.Mostrarinfo();

            //crear objeto estudiante
            //Console.WriteLine("Ingrese el nombre del estudiante: ");
            //string nombre = Console.ReadLine();
            //Console.WriteLine("Ingrese la edad del estudiante: ");
            //int edad = int.Parse(Console.ReadLine());

            //Estudiante estudiante = new Estudiante(nombre, edad);
            //estudiante.VerificarEdad();

            //Producto.ProductoCrud Producto = new Producto.ProductoCrud();
            //bool salir = false;
            //while (!salir)
            //{
            //    Console.WriteLine("=== Menu De Productos ===");
            //    Console.WriteLine("1. Agregar producto");
            //    Console.WriteLine("2. Mostrar productos");
            //    Console.WriteLine("3. Actualizar producto");
            //    Console.WriteLine("4. Eliminar producto");
            //    Console.WriteLine("5. Salir");
            //    Console.Write("Elige una opción para proceder: ");
            //    string opcion = Console.ReadLine();
            //    switch (opcion)
            //    {
            //        case "1":
            //            Producto.AgregarProducto();
            //            break;
            //        case "2":
            //            Producto.MostrarProductos();
            //            break;
            //        case "3":
            //            Producto.ActualizarProducto();
            //            break;
            //        case "4":
            //            Producto.EliminarProducto();
            //            break;
            //        case "5":
            //            salir = true;
            //            break;
            //        default:
            //            Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
            //            break;
            //    }
            //    Console.WriteLine();

            //}


            //Clase de Persona

            // Pedir datos al usuario
            Console.Write("Ingrese el nombre: ");
            string Nombre = Console.ReadLine();

            Console.Write("Ingrese la edad: ");
            int Edad = int.Parse(Console.ReadLine());

            Console.Write("Ingrese el género (F o M): ");
            char Genero = char.Parse(Console.ReadLine().ToUpper());

            Console.Write("Ingrese el teléfono: ");
            string Telefono = Console.ReadLine();

            // Crear objeto Persona
            Persona persona1 = new Persona(Nombre, Edad, Genero, Telefono);
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n--- MENÚ ---");
                Console.WriteLine("1. Imprimir detalles de la persona");
                Console.WriteLine("2. Calcular edad en días");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");
                int opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        persona1.ImprimirDetalles();
                        break;

                    case 2:
                        Console.WriteLine($"La edad en días es: {persona1.CalcularEdadEnDias()} días.");
                        break;

                    case 3:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } 

        }

    }
}

