using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios_POO
{
    internal class Empleado
    {
        public double salario;
        public double ahorro;

        public double CalcularSalud()
        {
            double salud = salario * 0.125;
            return salud;
        }

        public double CalcularPension()
        {
            double pension = salario * 0.16;
            return pension;
        }

        public double TotalRecibir()
        {
            double salud = CalcularSalud();
            double pension = CalcularPension();
            double total = salario - salud - pension - ahorro;
            return total;
        }
    }
}
