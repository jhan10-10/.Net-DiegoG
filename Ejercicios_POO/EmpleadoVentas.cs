using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios_POO
{
    internal class EmpleadoVentas
    {
        public string nombre;
        public int cantidadVentas;
        public double totalVentas;
        public double pagoBasico = 500000;
        public double bonificacion;

        public void CalcularBonificacion()
        {
            if (totalVentas >= 800000)
            {
                bonificacion = totalVentas * 0.10;
            }
            else if (totalVentas >= 400001 && totalVentas < 800000)
            {
                bonificacion = totalVentas * 0.05;
            }
            else if (totalVentas >= 400000)
            {
                bonificacion = totalVentas * 0.03;
            }
            else
            {
                bonificacion = 0;
            }
        }

        public double TotalPagar()
        {
            return pagoBasico + bonificacion;
        }
    }
}
