using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServerHotel;

namespace MyApp.Namespace
{
    public class HabitacionesDisponiblesModel : PageModel
    {
        public List<Habitacion> HabitacionesDisponibles { get; set; }
        public void OnGet()
        {
            ReservasContext context = new ReservasContext();
            //Verificar si esta reservada 
            var habitacionesReservadas = context.Reservas
                .Where(r => r.FechaInicio <= DateTime.Now && r.FechaFin >= DateTime.Now)
                .Select(r => r.IdHabitacion)
                .ToList();
            //Obtener habitaciones disponibles
            HabitacionesDisponibles = context.Habitacions
                .Where(h => !habitacionesReservadas.Contains(h.Id))
                .ToList();
                
        }
    }
}
