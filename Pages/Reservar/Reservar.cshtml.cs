using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServerHotel;

namespace MyApp.Namespace
{
    public class ReservarModel : PageModel
    {
        private ReservasContext _context = new ReservasContext();
        public List<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public void OnGet()
        {
            try
            {
                // Cargar habitaciones y reservas desde la base de datos
                Habitaciones = _context.Habitacions?.ToList() ?? new List<Habitacion>();
                Reservas = _context.Reservas?.ToList() ?? new List<Reserva>();
                Clientes = _context.Clientes?.ToList() ?? new List<Cliente>();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar los datos: {ex.Message}";
            }
        }

        public void OnPost(int habitacionId, DateTime fechaInicio, DateTime fechaFin, int clienteId)
        {
            try
            {
                // Validar entrada del formulario
                if (fechaInicio >= fechaFin)
                {
                    // Mostrar mensaje de error en consola
                    Console.WriteLine("La fecha de inicio debe ser anterior a la fecha de fin.");
                    TempData["Error"] = "La fecha de inicio debe ser anterior a la fecha de fin.";
                    return;
                }


                // Verificar que la habitación exista
                var habitacion = _context.Habitacions.Find(habitacionId);
                if (habitacion == null)
                {
                    // Mostrar mensaje de error en consola
                    Console.WriteLine("Habitación no encontrada.");
                    TempData["Error"] = "Habitación no encontrada.";
                    return;
                }
                // Verificar disponibilidad de la habitación
                var reservas = _context.Reservas.Where(r => r.IdHabitacion == habitacionId).ToList();
                bool ocupada = reservas.Any(r =>
                    (fechaInicio >= r.FechaInicio && fechaInicio <= r.FechaFin) ||
                    (fechaFin >= r.FechaInicio && fechaFin <= r.FechaFin));

                if (ocupada)
                {
                    TempData["Error"] = "La habitación ya está ocupada en esas fechas.";
                    return ;
                }
                Console.WriteLine("Habitación disponible.");

                // Crear la reserva
                var nuevaReserva = new Reserva
                {
                    IdHabitacion = habitacionId,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    IdCliente = clienteId
                };

                Console.WriteLine("Reserva creada.");
                // Guardar la reserva en la base de datos

                _context.Reservas.Add(nuevaReserva);
                _context.SaveChanges();

                TempData["Success"] = "Reserva creada exitosamente.";
                return;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al procesar la reserva: {ex.Message}";
                return;
            }
        }
    }
}