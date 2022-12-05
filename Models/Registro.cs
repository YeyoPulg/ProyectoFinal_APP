using System;
using System.Collections.Generic;

namespace ProyectoFinal_APP.Models;

public partial class Registro
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Identificacion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Edad { get; set; }

    public string? PesoCorporal { get; set; }

    public string? PlanEntreno { get; set; }

    public string? Usuario { get; set; }

    public string? Contrasena { get; set; }
}
