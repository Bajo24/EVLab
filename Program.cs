using System;

internal class Program
{
    static string[] codigos = { "AUTO-201", "AUTO-202", "AUTO-203", "AUTO-204", "AUTO-205", "AUTO-206"};
    static string[] modelos = { "Sedán Económico", "SUV Familiar", "Deportivo Premium", "Pick-up Trabajo", "Electric EV", "Van Carga" };
    static double[,] matrizDatos = {
        { 28000.00, 2 },
        { 18500.00, 4 },
        { 35000.00, 1 },
        { 14000.00, 5 },
        { 42000.00, 1 },
        { 22400.00, 3 }
    };

    static double[] montosNetos = new double[6];
    static double granTotalAcumulado = 0;
    static int contadorVentasPremium = 0;

    static public bool ValidorDatos(double precio, int cantidad)
    {
        if (precio > 0 && cantidad > 0)
        {
            return true;
        }
        else
        {
            Console.WriteLine("Error: El precio o la cantidad deben ser mayores a 0. Intente de nuevo.");
            return false;
        }
    }


    static public void ProcesarVentas()
    {
        granTotalAcumulado = 0;
        contadorVentasPremium = 0;

        for (int i = 0; i < codigos.Length; i++)
        {
            double precio = matrizDatos[i, 0];
            int cantidad = (int)matrizDatos[i, 1];

            if (ValidorDatos(precio, cantidad))
            {
                double neto = precio * cantidad;
                montosNetos[i] = neto;
                granTotalAcumulado += neto;

                if (neto > 50000.00)
                {
                    contadorVentasPremium++;
                }
            }
        }
    }


    static public void ImprimirMatrizDatos()
    {
        Console.WriteLine(" ----- MATRIZ BIDIMENSIONAL DE DATOS (PRECIO Y CANTIDAD) ----- ");
        Console.WriteLine(" FILA  | CODIGO   | COLUMNA 1 (PRECIO)     | COLUMNA 2 (CANTIDAD) ");
        Console.WriteLine(" ---------------------------------------------------------------- ");

        int filas = codigos.Length;

        for (int i = 0; i < filas; i++)
        {
            string codigoActual = codigos[i];
            double precioActual = matrizDatos[i, 0];
            double cantidadActual = matrizDatos[i, 1];

            Console.WriteLine($" [{i}]   | {codigoActual} | $ {precioActual} | {cantidadActual} ");
        }
    }


    public static void ResumenMetricas()
    {
        Console.WriteLine("=== RESUMEN DE MÉTRICAS ===");
        Console.WriteLine($"Gran Total Acumulado: ${granTotalAcumulado} USD");
        Console.WriteLine($"Cantidad de Ventas Premium (> $50,000.00): {contadorVentasPremium}");
    }


    public static void EjecutarOrdenamientoBurbuja()
    {
        int n = montosNetos.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (montosNetos[j] < montosNetos[j + 1])
                {
                    double tempMonto = montosNetos[j];
                    montosNetos[j] = montosNetos[j + 1];
                    montosNetos[j + 1] = tempMonto;

                    string tempCodigo = codigos[j];
                    codigos[j] = codigos[j + 1];
                    codigos[j + 1] = tempCodigo;

                    string tempModelo = modelos[j];
                    modelos[j] = modelos[j + 1];
                    modelos[j + 1] = tempModelo;

                    double tempPrecio = matrizDatos[j, 0];
                    double tempCantidad = matrizDatos[j, 1];

                    matrizDatos[j, 0] = matrizDatos[j + 1, 0];
                    matrizDatos[j, 1] = matrizDatos[j + 1, 1];

                    matrizDatos[j + 1, 0] = tempPrecio;
                    matrizDatos[j + 1, 1] = tempCantidad;
                }
            }
        }
        Console.WriteLine("[ÉXITO] Datos ordenados de mayor a menor según el Monto Neto.");
        ImprimirMatrizDatos(); 
    }


    public static void BuscarAuto()
    {
        Console.Write("Ingrese el código del producto a buscar (ej. AUTO-203): ");
        string codigoBuscado = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(codigoBuscado))
        {
            Console.WriteLine("[ERROR de Búsqueda]: El código ingresado no puede estar vacío.");
            return;
        }

        for (int i = 0; i < codigos.Length; i++)
        {
            if (codigos[i].Equals(codigoBuscado, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\n[Resultado Encontrado]");
                Console.WriteLine($"Código: {codigos[i]}");
                Console.WriteLine($"Modelo: {modelos[i]}");
                Console.WriteLine($"Monto Neto: ${montosNetos[i]:N2} USD");
                return;
            }
        }

        Console.WriteLine($"[ERROR de Búsqueda]: El código \"{codigoBuscado}\" no está registrado.");
    }

    static void Main(string[] args)
    {
        ProcesarVentas();

        int opc;
        bool valor = true;
        while (valor)
        {
            Console.WriteLine("===================================================================");
            Console.WriteLine("                Control de ventas de Automotrices                  ");
            Console.WriteLine("===================================================================");
            Console.WriteLine("[1] Matriz de datos ");
            Console.WriteLine("[2] Reporte por Monto neto (Decreciente)");
            Console.WriteLine("[3] Resumen de Metricas");
            Console.WriteLine("[4] Buscar por medio de codigo");
            Console.WriteLine("[5] Salir");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out opc))
            {
                opc = 0;
            }

            switch (opc)
            {
                case 1:
                    ImprimirMatrizDatos();
                    break;
                case 2:

                    EjecutarOrdenamientoBurbuja();
                    break;
                case 3:

                    ResumenMetricas();
                    break;
                case 4:
                    BuscarAuto();
                    break;
                case 5:
                    valor = false;
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("[ERROR] Opción no válida. Intente de nuevo.");
                    break;
            }

            if (valor)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
