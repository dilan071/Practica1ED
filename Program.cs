using System;
using System.Diagnostics;
class Program
{
    static void Main(string[] args)
    {
        ColaPrioridad colaPrioridad = new ColaPrioridad();
        Stopwatch stopwatch = new Stopwatch();

        while (true)
        {
            Console.WriteLine("1. Agregar solicitud");
            Console.WriteLine("2. Atender solicitud");
            Console.WriteLine("3. Visualizar solicitudes");
            Console.WriteLine("4. Actualizar urgencia");
            Console.WriteLine("5. Cargar solicitudes desde archivo");
            Console.WriteLine("6. Medir y Comparar Complejidades");
            Console.WriteLine("7. Generar archivo de prueba");
            Console.WriteLine("8. Salir");

            int opcion = int.Parse(Console.ReadLine());
            if (opcion == 8) break;

            switch (opcion)
            {


                case 1:
                    Console.WriteLine("Ingrese el número de la solicitud:");
                    int numeroSolicitud = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese el nombre del cliente:");
                    string nombreCliente = Console.ReadLine();

                    Console.WriteLine("Ingrese la descripción del problema:");
                    string descripcionProblema = Console.ReadLine();

                    Console.WriteLine("Ingrese el nivel de urgencia (1-10):");
                    int nivelUrgencia = int.Parse(Console.ReadLine());

                    colaPrioridad.AgregarSolicitud(new Solicitud
                    {
                        NumeroSolicitud = numeroSolicitud,
                        NombreCliente = nombreCliente,
                        DescripcionProblema = descripcionProblema,
                        NivelUrgencia = nivelUrgencia
                    });

                    Console.WriteLine("Solicitud agregada exitosamente.");
                    break;
                case 2:
                    var solicitudAtendida = colaPrioridad.AtenderSolicitud();
                    if (solicitudAtendida != null)
                    {
                        Console.WriteLine("Solicitud atendida: " + solicitudAtendida);
                    }
                    else
                    {
                        Console.WriteLine("No hay solicitudes para atender.");
                    }
                    break;
                case 3:
                    Console.WriteLine("Visualizando solicitudes...");
                    colaPrioridad.VisualizarSolicitudes();
                    break;
                case 4:
                    Console.WriteLine("Ingrese el número de la solicitud para actualizar:");
                    int numeroSolicitudActualizar = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese el nuevo nivel de urgencia:");
                    int nuevoNivelUrgencia = int.Parse(Console.ReadLine());

                    colaPrioridad.ActualizarUrgencia(numeroSolicitudActualizar, nuevoNivelUrgencia);
                    Console.WriteLine("Urgencia actualizada exitosamente.");
                    break;
                case 5:
                    Console.WriteLine("Ingrese la ruta del archivo para cargar las solicitudes:");
                    string path = Console.ReadLine();

                    try
                    {
                        colaPrioridad.CargarSolicitudesDesdeArchivo(path);
                        Console.WriteLine("Solicitudes cargadas exitosamente desde el archivo.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ocurrió un error al cargar las solicitudes desde el archivo: {ex.Message}");
                    }
                    break;
                case 6:
                    var numerosDePruebas = new List<int> { 10, 100, 1000, 10000 };
                    var reporte = new List<string>
                    {
                        "Número de Solicitudes,Tiempo de Inserción (ms),Tiempo de Visualización (ms),Tiempo de Actualización (ms),Tiempo de Atención (ms)"
                    };

                    foreach (var numero in numerosDePruebas)
                    {
                        ColaPrioridad colaPrueba = new ColaPrioridad();
                        var tiempos = new List<long>();

                        // Medir el tiempo de inserción de un número de elementos
                        stopwatch.Start();
                        for (int i = 0; i < numero; i++)
                        {
                            colaPrueba.AgregarSolicitud(new Solicitud
                            {
                                NumeroSolicitud = i,
                                NombreCliente = "Cliente" + i,
                                DescripcionProblema = "Problema" + i,
                                NivelUrgencia = i % 10
                            });
                        }
                        stopwatch.Stop();
                        tiempos.Add(stopwatch.ElapsedMilliseconds);
                        stopwatch.Reset();

                        // Medir el tiempo de visualización
                        stopwatch.Start();
                        colaPrueba.VisualizarSolicitudes();
                        stopwatch.Stop();
                        tiempos.Add(stopwatch.ElapsedMilliseconds);
                        stopwatch.Reset();

                        // Medir el tiempo de actualización
                        stopwatch.Start();
                        for (int i = 0; i < numero; i++)
                        {
                            colaPrueba.ActualizarUrgencia(i, new Random().Next(1, 11));
                        }
                        stopwatch.Stop();
                        tiempos.Add(stopwatch.ElapsedMilliseconds);
                        stopwatch.Reset();

                        // Medir el tiempo de atención
                        stopwatch.Start();
                        for (int i = 0; i < numero; i++)
                        {
                            colaPrueba.AtenderSolicitud();
                        }
                        stopwatch.Stop();
                        tiempos.Add(stopwatch.ElapsedMilliseconds);
                        stopwatch.Reset();

                        reporte.Add($"{numero},{string.Join(",", tiempos)}");
                    }

                    try
                    {
                        File.WriteAllLines("reporte_complejidades.csv", reporte);
                        Console.WriteLine("Reporte de complejidades generado exitosamente en 'reporte_complejidades.csv'.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ocurrió un error al generar el reporte: {ex.Message}");
                    }
                    break;

                case 7:
                    Console.WriteLine("Ingrese la ruta del archivo y el número de solicitudes (por ejemplo: test.csv,1000):");
                    string[] detallesArchivo = Console.ReadLine().Split(',');

                    if (detallesArchivo.Length == 2)
                    {
                        colaPrioridad.GenerarArchivoDePrueba(detallesArchivo[0], int.Parse(detallesArchivo[1]));
                    }
                    else
                    {
                        Console.WriteLine("Detalles del archivo incorrectos.");
                    }
                    break;
            }
        }
    }
}