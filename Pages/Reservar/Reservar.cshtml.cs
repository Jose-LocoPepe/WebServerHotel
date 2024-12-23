using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServerHotel;

namespace MyApp.Namespace
{
    public class ReservarModel : PageModel
    {
        
        public List<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
        
        public void OnGet()
        {
            ReservasContext context = new ReservasContext();
            
            Habitaciones = context.Habitacions.ToList();
            Reservas = context.Reservas.ToList();
        }
        public void OnPost(){

ReservasContext context = new ReservasContext();
            //Validar que la habitación no esté ocupada
            if (!int.TryParse(Request.Form["habitacion"], out int habitacionId) || 
                !DateTime.TryParse(Request.Form["fechaInicio"], out DateTime fechaInicio) || 
                !DateTime.TryParse(Request.Form["fechaFin"], out DateTime fechaFin))
            {
                TempData["Error"] = "Datos de la reserva inválidos";
                return;
            }

            var habitacion = context.Habitacions.Find(habitacionId);
            if (habitacion == null)
            {
                TempData["Error"] = "Habitación no encontrada";
                return;
            }

            var reservas = context.Reservas.Where(r => r.IdHabitacion == habitacion.Id).ToList();
            bool ocupada = false;
            foreach(var reserva in reservas){
                if(fechaInicio >= reserva.FechaInicio && fechaInicio <= reserva.FechaFin){
                    ocupada = true;
                }
                if(fechaFin >= reserva.FechaInicio && fechaFin <= reserva.FechaFin){
                    ocupada = true;
                }
            }
            if(ocupada){
                TempData["Error"] = "La habitación ya está ocupada en esas fechas";
                return;
            }
        }
    }
}
