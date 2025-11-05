using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios_POO
{
    internal class Estudiante
    {
        public string programa;
        public string formaPago;
        public int creditos;
        public double descuento;
        public double valorCredito = 200000;

        public double CalcularValorSinDescuento()
        {
            double total = creditos * valorCredito;
            return total;
        }

        public double CalcularDescuento()
        {
            double total = CalcularValorSinDescuento();
            if (formaPago.ToLower() == "efectivo")
            {
                return total * descuento;
            }
            else
            {
                return 0;
            }
        }

        public double CalcularValorFinal()
        {
            double total = CalcularValorSinDescuento();
            double desc = CalcularDescuento();
            return total - desc;
        }
    }
}
