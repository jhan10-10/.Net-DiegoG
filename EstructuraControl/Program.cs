using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraControl
{
    internal class Program
    {
        static void Main(string[] args)
        {

            
            Console.Write("Ingrese el monto del préstamo: ");
            float monto = float.Parse(Console.ReadLine());

            float tasaAnual = 0.05f;
            int plazoAnios = 5;

            float interesAnual = monto * tasaAnual;
            float interesTrimestre3 = interesAnual * (3f / 12f);
            float interesPrimerMes = interesAnual / 12f;
            float totalAPagar = monto + (interesAnual * plazoAnios);

            
            Console.WriteLine("RESULTADOS");
            Console.WriteLine($"Interes pagado en un año: ${interesAnual}");
            Console.WriteLine($"Interes pagado en el tercer trimestre: ${interesTrimestre3}");
            Console.WriteLine($"Interes pagado en el primer mes: ${interesPrimerMes}");
            Console.WriteLine($"Total a pagar incluyendo intereses: ${totalAPagar}");

        }
    }
}
