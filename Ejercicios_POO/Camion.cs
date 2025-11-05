using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios_POO
{
    internal class Camion
    {
        public double capacidad;
        public double cargaActual;

        public Camion(double cap)
        {
            capacidad = cap;
            cargaActual = 0;
        }

        public bool Cargar(double litros)
        {
            if (cargaActual + litros <= capacidad)
            {
                cargaActual += litros;
                Console.WriteLine("Se cargaron " + litros + " litros. Carga actual: " + cargaActual);
                return true;
            }
            else
            {
                Console.WriteLine("No se puede cargar " + litros + " litros, se pasaria de la capacidad.");
                Console.WriteLine("Debe despachar el camion.");
                return false;
            }
        }

        public void Despachar()
        {
            Console.WriteLine("Camion despachado con " + cargaActual + " litros cargados.");
            cargaActual = 0;
        }
    }
}
