using System;
using System.Collections.Generic;

namespace WebServerHotel;

public partial class Cliente
{
    public int Id { get; set; }

    public int Rut { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; } = new List<Reserva>();
}
