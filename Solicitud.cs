using System;

public class Solicitud
{
    public int NumeroSolicitud { get; set; }
    public string NombreCliente { get; set; }
    public string DescripcionProblema { get; set; }
    public int NivelUrgencia { get; set; }

    public override string ToString()
    {
        return $"#{NumeroSolicitud} - {NombreCliente} - {DescripcionProblema} - Urgencia: {NivelUrgencia}";
    }
}
