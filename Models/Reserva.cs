using System;
using System.Collections.Generic;

namespace WebServerHotel;

public partial class Reserva
{
    public int Id { get; set; }

    public int IdHabitacion { get; set; }

    public int IdCliente { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;
}
