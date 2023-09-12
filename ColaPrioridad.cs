using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ColaPrioridad
{
    private SortedSet<Solicitud> cola;

    public ColaPrioridad()
    {
        cola = new SortedSet<Solicitud>(Comparer<Solicitud>.Create((x, y) => y.NivelUrgencia.CompareTo(x.NivelUrgencia) != 0 ? y.NivelUrgencia.CompareTo(x.NivelUrgencia) : x.NumeroSolicitud.CompareTo(y.NumeroSolicitud)));
    }

    public void AgregarSolicitud(Solicitud solicitud)
    {
        cola.Add(solicitud);
    }

    public Solicitud AtenderSolicitud()
    {
        if (cola.Count == 0) return null;

        var solicitud = cola.Min;
        cola.Remove(solicitud);
        return solicitud;
    }

    public void VisualizarSolicitudes()
    {
        foreach (var solicitud in cola)
        {
            Console.WriteLine(solicitud);
        }
    }

    public void ActualizarUrgencia(int numeroSolicitud, int nuevoNivelUrgencia)
    {
        var solicitudExistente = cola.FirstOrDefault(s => s.NumeroSolicitud == numeroSolicitud);
        if (solicitudExistente != null)
        {
            cola.Remove(solicitudExistente);
            solicitudExistente.NivelUrgencia = nuevoNivelUrgencia;
            cola.Add(solicitudExistente);
        }
    }

    public void CargarSolicitudesDesdeArchivo(string path)
    {
        string[] lineas = File.ReadAllLines(path);

        foreach (string linea in lineas)
        {
            var datos = linea.Split(',');

            if (datos.Length == 4)
            {
                AgregarSolicitud(new Solicitud
                {
                    NumeroSolicitud = int.Parse(datos[0]),
                    NombreCliente = datos[1],
                    DescripcionProblema = datos[2],
                    NivelUrgencia = int.Parse(datos[3])
                });
            }
        }
    }

    public void GenerarArchivoDePrueba(string path, int numeroDeSolicitudes)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 1; i <= numeroDeSolicitudes; i++)
                {
                    sw.WriteLine($"{i},Cliente{i},Problema{i},{new Random().Next(1, 11)}");
                }
            }

            Console.WriteLine($"Archivo de prueba con {numeroDeSolicitudes} solicitudes generado exitosamente en {path}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error al generar el archivo de prueba: {ex.Message}");
        }
    }
}
