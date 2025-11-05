using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios_POO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ejercicio 1: Clase Prestamo
            //Prestamo p = new Prestamo();

            //Console.WriteLine("Calculo de prestamo");
            //Console.Write("Ingrese el monto del prestamo: ");
            //p.monto = Convert.ToDouble(Console.ReadLine());

            //double interesAnual = p.CalcularInteres(1);
            //double interesTrimestre = p.CalcularInteres(0.25);
            //double interesMes = p.CalcularInteres(1.0 / 12.0);
            //double total = p.TotalPagar();

            //Console.WriteLine();
            //Console.WriteLine("Monto del prestamo: $" + p.monto);
            //Console.WriteLine("Tasa de interes anual: 5%");
            //Console.WriteLine("Plazo: " + p.plazoAños + " años");
            //Console.WriteLine();
            //Console.WriteLine("Interes pagado en un anio: $" + interesAnual);
            //Console.WriteLine("Interes pagado en el tercer trimestre: $" + interesTrimestre);
            //Console.WriteLine("Interes pagado en el primer mes: $" + interesMes);
            //Console.WriteLine("Total a pagar: $" + total);

            //Console.WriteLine("Fin del programa");

            //Ejercicio 2: Clase Empleado
            //Empleado e = new Empleado();

            //Console.WriteLine("Colilla de pago");
            //Console.Write("Ingrese el salario del empleado: ");
            //e.salario = Convert.ToDouble(Console.ReadLine());

            //Console.Write("Ingrese el valor de ahorro mensual: ");
            //e.ahorro = Convert.ToDouble(Console.ReadLine());

            //double salud = e.CalcularSalud();
            //double pension = e.CalcularPension();
            //double total = e.TotalRecibir();

            //Console.WriteLine();
            //Console.WriteLine("Salario del empleado: $" + e.salario);
            //Console.WriteLine("Ahorro mensual: $" + e.ahorro);
            //Console.WriteLine("Descuento por salud (12.5%): $" + salud);
            //Console.WriteLine("Descuento por pension (16%): $" + pension);
            //Console.WriteLine("Total a recibir: $" + total);

            //Console.WriteLine("Fin del programa");

            //Ejercicio 3: Clase Persona
            //Console.WriteLine("Programa agenda");
            //Console.Write("Ingrese el nombre: ");
            //string nombre = Console.ReadLine();

            //Console.Write("Ingrese la edad: ");
            //int edad = Convert.ToInt32(Console.ReadLine());

            //Console.Write("Ingrese el genero (F o M): ");
            //string genero = Console.ReadLine();

            //Console.Write("Ingrese el telefono: ");
            //string telefono = Console.ReadLine();

            //Persona persona = new Persona(nombre, edad, genero, telefono);

            //int opcion = 0;
            //while (opcion != 3)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Menu de opciones:");
            //    Console.WriteLine("1. Imprimir detalles");
            //    Console.WriteLine("2. Calcular edad en dias");
            //    Console.WriteLine("3. Salir");
            //    Console.Write("Seleccione una opcion: ");
            //    opcion = Convert.ToInt32(Console.ReadLine());

            //    switch (opcion)
            //    {
            //        case 1:
            //            persona.ImprimirDetalles();
            //            break;
            //        case 2:
            //            persona.CalcularEdadEnDias();
            //            break;
            //        case 3:
            //            Console.WriteLine("Saliendo del programa...");
            //            break;
            //        default:
            //            Console.WriteLine("Opcion no valida");
            //            break;
            //    }
            //}

            //Ejercicio 4: Clase Libro
            //Biblioteca biblioteca = new Biblioteca();
            //int opcion = 0;

            //while (opcion != 4)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Menu biblioteca:");
            //    Console.WriteLine("1. Agregar libro");
            //    Console.WriteLine("2. Listar libros");
            //    Console.WriteLine("3. Buscar libro por nombre");
            //    Console.WriteLine("4. Salir");
            //    Console.Write("Seleccione una opcion: ");
            //    opcion = Convert.ToInt32(Console.ReadLine());

            //    switch (opcion)
            //    {
            //        case 1:
            //            Console.Write("Titulo: ");
            //            string titulo = Console.ReadLine();
            //            Console.Write("Autor: ");
            //            string autor = Console.ReadLine();
            //            Console.Write("Editorial: ");
            //            string editorial = Console.ReadLine();
            //            Console.Write("Año de publicacion: ");
            //            int anio = Convert.ToInt32(Console.ReadLine());

            //            Libro nuevo = new Libro(titulo, autor, editorial, anio);
            //            biblioteca.AgregarLibro(nuevo);
            //            break;

            //        case 2:
            //            biblioteca.ListarLibros();
            //            break;

            //        case 3:
            //            Console.Write("Ingrese el nombre del libro a buscar: ");
            //            string nombre = Console.ReadLine();
            //            biblioteca.BuscarLibro(nombre);
            //            break;

            //        case 4:
            //            Console.WriteLine("Saliendo del programa...");
            //            break;

            //        default:
            //            Console.WriteLine("Opcion no valida.");
            //            break;
            //    }
            //}

            //Ejercicio 5: Clase CuentaBancaria
            //int cantSistemas = 0;
            //int cantPsicologia = 0;
            //int cantEconomia = 0;
            //int cantComunicacion = 0;
            //int cantAdministracion = 0;

            //double totalCreditos = 0;
            //double totalSinDescuento = 0;
            //double totalDescuentos = 0;
            //double totalFinal = 0;

            //Console.Write("Cuantos estudiantes va a registrar: ");
            //int n = Convert.ToInt32(Console.ReadLine());

            //for (int i = 0; i < n; i++)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Estudiante " + (i + 1));
            //    Estudiante e = new Estudiante();

            //    Console.WriteLine("Programas:");
            //    Console.WriteLine("1. Ingenieria de sistemas");
            //    Console.WriteLine("2. Psicologia");
            //    Console.WriteLine("3. Economia");
            //    Console.WriteLine("4. Comunicacion social");
            //    Console.WriteLine("5. Administracion de empresas");
            //    Console.Write("Seleccione el numero del programa: ");
            //    int opcion = Convert.ToInt32(Console.ReadLine());

            //    if (opcion == 1)
            //    {
            //        e.programa = "Ingenieria de sistemas";
            //        e.creditos = 20;
            //        e.descuento = 0.18;
            //        cantSistemas++;
            //    }
            //    else if (opcion == 2)
            //    {
            //        e.programa = "Psicologia";
            //        e.creditos = 16;
            //        e.descuento = 0.12;
            //        cantPsicologia++;
            //    }
            //    else if (opcion == 3)
            //    {
            //        e.programa = "Economia";
            //        e.creditos = 18;
            //        e.descuento = 0.10;
            //        cantEconomia++;
            //    }
            //    else if (opcion == 4)
            //    {
            //        e.programa = "Comunicacion social";
            //        e.creditos = 18;
            //        e.descuento = 0.05;
            //        cantComunicacion++;
            //    }
            //    else if (opcion == 5)
            //    {
            //        e.programa = "Administracion de empresas";
            //        e.creditos = 20;
            //        e.descuento = 0.15;
            //        cantAdministracion++;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Opcion no valida");
            //        i--;
            //        continue;
            //    }

            //    Console.Write("Forma de pago (efectivo o linea): ");
            //    e.formaPago = Console.ReadLine();

            //    double valorSinDesc = e.CalcularValorSinDescuento();
            //    double valorDesc = e.CalcularDescuento();
            //    double valorFinal = e.CalcularValorFinal();

            //    totalCreditos += e.creditos;
            //    totalSinDescuento += valorSinDesc;
            //    totalDescuentos += valorDesc;
            //    totalFinal += valorFinal;

            //    Console.WriteLine();
            //    Console.WriteLine("Resumen del estudiante:");
            //    Console.WriteLine("Programa: " + e.programa);
            //    Console.WriteLine("Creditos: " + e.creditos);
            //    Console.WriteLine("Valor sin descuento: $" + valorSinDesc);
            //    Console.WriteLine("Descuento aplicado: $" + valorDesc);
            //    Console.WriteLine("Valor final a pagar: $" + valorFinal);
            //}

            //Console.WriteLine();
            //Console.WriteLine("---- RESULTADOS FINALES ----");
            //Console.WriteLine("Estudiantes por programa:");
            //Console.WriteLine("Ingenieria de sistemas: " + cantSistemas);
            //Console.WriteLine("Psicologia: " + cantPsicologia);
            //Console.WriteLine("Economia: " + cantEconomia);
            //Console.WriteLine("Comunicacion social: " + cantComunicacion);
            //Console.WriteLine("Administracion de empresas: " + cantAdministracion);
            //Console.WriteLine();
            //Console.WriteLine("Total de creditos inscritos: " + totalCreditos);
            //Console.WriteLine("Valor total sin descuentos: $" + totalSinDescuento);
            //Console.WriteLine("Valor total de descuentos: $" + totalDescuentos);
            //Console.WriteLine("Valor neto (final) de inscripciones: $" + totalFinal);

            //Ejercicio 6: Clase EmpleadoVentas
            //Console.Write("Cuantos empleados hay: ");
            //int n = Convert.ToInt32(Console.ReadLine());

            //for (int i = 0; i < n; i++)
            //{
            //    Console.WriteLine();
            //    EmpleadoVentas e = new EmpleadoVentas();

            //    Console.Write("Nombre del empleado: ");
            //    e.nombre = Console.ReadLine();

            //    Console.Write("Cuantas ventas realizo hoy: ");
            //    e.cantidadVentas = Convert.ToInt32(Console.ReadLine());

            //    int ventasBajas = 0;
            //    int ventasMedias = 0;
            //    int ventasAltas = 0;
            //    e.totalVentas = 0;

            //    for (int j = 0; j < e.cantidadVentas; j++)
            //    {
            //        Console.Write("Valor de la venta " + (j + 1) + ": ");
            //        double valor = Convert.ToDouble(Console.ReadLine());

            //        if (valor <= 300000)
            //        {
            //            ventasBajas++;
            //        }
            //        else if (valor > 300000 && valor < 800000)
            //        {
            //            ventasMedias++;
            //        }
            //        else
            //        {
            //            ventasAltas++;
            //        }

            //        e.totalVentas += valor;
            //    }

            //    e.CalcularBonificacion();

            //    Console.WriteLine();
            //    Console.WriteLine("Resumen del empleado " + e.nombre + ":");
            //    Console.WriteLine("Ventas menores o iguales a 300000: " + ventasBajas);
            //    Console.WriteLine("Ventas entre 300001 y 799999: " + ventasMedias);
            //    Console.WriteLine("Ventas mayores o iguales a 800000: " + ventasAltas);
            //    Console.WriteLine("Total de ventas: $" + e.totalVentas);
            //    Console.WriteLine("Pago basico: $" + e.pagoBasico);
            //    Console.WriteLine("Bonificacion: $" + e.bonificacion);
            //    Console.WriteLine("Total a pagar: $" + e.TotalPagar());
            //}

            //Console.WriteLine();
            //Console.WriteLine("Fin del programa");

            //Ejercicio 7: Clase Producto
            //int actual = 2025;
            //Console.Write("Cuantos conductores desea ingresar: ");
            //int n = Convert.ToInt32(Console.ReadLine());

            //int menores30 = 0;
            //int totalFemenino = 0;
            //int totalMasculino = 0;
            //int masculinos12a30 = 0;
            //int registradosFuera = 0;

            //for (int i = 0; i < n; i++)
            //{
            //    Conductor c = new Conductor();

            //    Console.WriteLine();
            //    Console.WriteLine("Conductor " + (i + 1));

            //    Console.Write("Año de nacimiento: ");
            //    c.anioNacimiento = Convert.ToInt32(Console.ReadLine());

            //    Console.Write("Sexo (1: Femenino, 2: Masculino): ");
            //    c.sexo = Convert.ToInt32(Console.ReadLine());

            //    Console.Write("Registro del carro (1: Bogota, 2: Otras ciudades): ");
            //    c.registro = Convert.ToInt32(Console.ReadLine());

            //    int edad = actual - c.anioNacimiento;

            //    if (edad < 30)
            //    {
            //        menores30++;
            //    }

            //    if (c.sexo == 1)
            //    {
            //        totalFemenino++;
            //    }
            //    else if (c.sexo == 2)
            //    {
            //        totalMasculino++;

            //        if (edad >= 12 && edad <= 30)
            //        {
            //            masculinos12a30++;
            //        }
            //    }

            //    if (c.registro == 2)
            //    {
            //        registradosFuera++;
            //    }
            //}

            //Console.WriteLine();
            //Console.WriteLine("---- RESULTADOS ----");
            //Console.WriteLine("Total de conductores: " + n);

            //double porcMenores30 = (double)menores30 / n * 100;
            //double porcFemenino = (double)totalFemenino / n * 100;
            //double porcMasculino = (double)totalMasculino / n * 100;
            //double porcMasculinos12a30 = (double)masculinos12a30 / n * 100;
            //double porcFueraBogota = (double)registradosFuera / n * 100;

            //Console.WriteLine("Porcentaje menores de 30 años: " + porcMenores30 + "%");
            //Console.WriteLine("Porcentaje femenino: " + porcFemenino + "%");
            //Console.WriteLine("Porcentaje masculino: " + porcMasculino + "%");
            //Console.WriteLine("Porcentaje masculinos entre 12 y 30 años: " + porcMasculinos12a30 + "%");
            //Console.WriteLine("Porcentaje registrados fuera de Bogota: " + porcFueraBogota + "%");

            //Console.WriteLine();
            //Console.WriteLine("Fin del programa");

            //Ejercicio 8: Clase EmpleadoTikTok
            //int actual = 2025;
            //Console.Write("Cuantos empleados va a registrar: ");
            //int n = Convert.ToInt32(Console.ReadLine());

            //EmpleadoTikTok[] empleados = new EmpleadoTikTok[n];

            //int[] empleadosPorMes = new int[12];
            //int sumaEdades = 0;
            //int totalConBono = 0;

            //for (int i = 0; i < n; i++)
            //{
            //    empleados[i] = new EmpleadoTikTok();

            //    Console.WriteLine();
            //    Console.WriteLine("Empleado " + (i + 1));
            //    Console.Write("Nombre: ");
            //    empleados[i].nombre = Console.ReadLine();

            //    Console.Write("Año de nacimiento: ");
            //    empleados[i].añoNacimiento = Convert.ToInt32(Console.ReadLine());

            //    Console.Write("Mes de nacimiento (1-12): ");
            //    empleados[i].mesNacimiento = Convert.ToInt32(Console.ReadLine());

            //    empleados[i].edad = actual - empleados[i].añoNacimiento;
            //    sumaEdades += empleados[i].edad;

            //    if (empleados[i].edad > 18 && empleados[i].edad < 50)
            //    {
            //        empleadosPorMes[empleados[i].mesNacimiento - 1]++;
            //        totalConBono++;
            //    }
            //}

            //double promedio = (double)sumaEdades / n;
            //int bono = 150000;
            //int totalDinero = totalConBono * bono;

            //Console.WriteLine();
            //Console.WriteLine("---- RESULTADOS ----");
            //Console.WriteLine("Promedio de edades: " + promedio);
            //Console.WriteLine();

            //string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            //for (int i = 0; i < 12; i++)
            //{
            //    if (empleadosPorMes[i] > 0)
            //    {
            //        int dineroMes = empleadosPorMes[i] * bono;
            //        Console.WriteLine(meses[i] + ": " + empleadosPorMes[i] + " empleados - Dinero en bonos $" + dineroMes);
            //    }
            //}

            //Console.WriteLine();
            //Console.WriteLine("Total de dinero en bonos para toda la empresa: $" + totalDinero);
            //Console.WriteLine("Fin del programa");

            //Ejercicio 9: Clase Camion
            Console.WriteLine("Control de carga de camiones (20 camiones maximo)");
            int camionesCargados = 0;

            while (camionesCargados < 20)
            {
                Console.WriteLine();
                Console.Write("Capacidad del camion (entre 18000 y 28000): ");
                double capacidad = Convert.ToDouble(Console.ReadLine());

                if (capacidad < 18000 || capacidad > 28000)
                {
                    Console.WriteLine("Capacidad fuera del rango permitido.");
                    continue;
                }

                Camion camion = new Camion(capacidad);

                bool continuar = true;
                while (continuar)
                {
                    Console.Write("Ingrese cantidad de litros de la siguiente saca (entre 3000 y 9000, o 0 para terminar): ");
                    double litros = Convert.ToDouble(Console.ReadLine());

                    if (litros == 0)
                    {
                        camion.Despachar();
                        continuar = false;
                        camionesCargados++;
                    }
                    else if (litros < 3000 || litros > 9000)
                    {
                        Console.WriteLine("Cantidad fuera del rango permitido.");
                    }
                    else
                    {
                        bool cargado = camion.Cargar(litros);
                        if (!cargado)
                        {
                            camion.Despachar();
                            camionesCargados++;
                            continuar = false;
                        }
                    }

                    if (camionesCargados == 20)
                    {
                        Console.WriteLine("Se han cargado los 20 camiones del dia.");
                        break;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Fin del programa. Todos los camiones han sido despachados.");
        }
    }
}
