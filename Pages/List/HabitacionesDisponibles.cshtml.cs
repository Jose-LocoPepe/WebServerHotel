using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServerHotel;

namespace MyApp.Namespace
{
    public class HabitacionesDisponiblesModel : PageModel
    {
        public List<Habitacion> HabitacionesDisponibles { get; set; }
        public void OnGet(DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                ReservasContext context = new ReservasContext();
                //Verificar si esta reservada 
                var habitacionesReservadas = context.Reservas
                    .Where(r => r.FechaInicio <= fechaFin && r.FechaFin >= fechaInicio)
                    .Select(r => r.IdHabitacion)
                    .ToList();
                //Obtener habitaciones disponibles
                HabitacionesDisponibles = context.Habitacions
                    .Where(h => !habitacionesReservadas.Contains(h.Id))
                    .ToList();
            }    
        }
    }
}
