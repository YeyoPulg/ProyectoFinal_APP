using System;
using System.Collections.Generic;

namespace ProyectoFinal_APP.Models;

public partial class Reserva
{
    public int IdUser { get; set; }

    public string? Nombre { get; set; }

    public string? Identificacion { get; set; }

    public string? Reserva1 { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
