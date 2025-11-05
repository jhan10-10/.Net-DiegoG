using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    internal class Auto // clase auto
    {
        //propiedades
        public string Marca {  get; set; }
        public string Modelo { get; set; }
        public string Año { get; set; }

        //constructor
        public Auto(string marca, string modelo, string año)
        {
            Marca = marca;
            Modelo = modelo;
            Año = año;
        }

        //mostrar informacion del auto
        public void Mostrarinfo()
        {
            Console.WriteLine($"Marca: {Marca}, Modelo: {Modelo}, Año: {Año}");
        }
        

    }
}
