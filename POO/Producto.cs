using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    internal class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        //constructor
        public Producto(int iD, string nombre, decimal precio)
        {
            ID = iD;
            Nombre = nombre;
            Precio = precio;
        }

        public class ProductoCrud
        {
            public List<Producto> productos = new List<Producto>();
            private int siguienteID = 1; // inicializar en uno 

            //metodo para agregar producto
            public void AgregarProducto()
            {
                Console.WriteLine("Ingrese el nombre del producto: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el precio del producto: ");
                decimal precio = decimal.Parse(Console.ReadLine());

                Producto nuevoProducto = new Producto(siguienteID++, nombre, precio );
                productos.Add(nuevoProducto);
                Console.WriteLine("Producto agregado exitosamente.");
            }

            //metodo para mostrar productos
            public void MostrarProductos()
            {
                Console.WriteLine("Lista de productos:");
                foreach (var producto in productos)
                {
                    Console.WriteLine($"ID: {producto.ID}, " +
                    $"Nombre: {producto.Nombre}, Precio: {producto.Precio}");
                }
            }

            //metodo para actualizar producto
            public void ActualizarProducto()
            {
                Console.WriteLine("Ingrese el ID del producto a actualizar: ");
                int idActualizar = int.Parse(Console.ReadLine());
                var producto = productos.FirstOrDefault(p => p.ID == idActualizar);
                if (producto != null)
                {
                    Console.WriteLine("Ingrese el nuevo nombre del producto: ");
                    producto.Nombre = Console.ReadLine();
                    Console.WriteLine("Ingrese el nuevo precio del producto: ");
                    producto.Precio = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Producto actualizado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }
            //metodo para eliminar producto
            public void EliminarProducto()
            {
                Console.WriteLine("Ingrese el ID del producto a eliminar: ");
                int idEliminar = int.Parse(Console.ReadLine());
                var producto = productos.FirstOrDefault(p => p.ID == idEliminar);
                if (producto != null)
                {
                    productos.Remove(producto);
                    Console.WriteLine("Producto eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }

        }
    }
}
