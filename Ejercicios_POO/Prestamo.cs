using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios_POO
{
    internal class Prestamo
    {

        public double monto;
        public double tasa = 0.05;
        public int plazoAños = 5;

        public double CalcularInteres(double tiempoAnios)
        {
            double interes = monto * tasa * tiempoAnios;
            return interes;
        }

        public double TotalPagar()
        {
            double interesTotal = CalcularInteres(plazoAños);
            double total = monto + interesTotal;
            return total;
        }
    }

}



