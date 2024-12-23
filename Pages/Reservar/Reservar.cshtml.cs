using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServerHotel;
using Grpc.Net.Client;
using ServidorgRPC;

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
        private string? grpcClientUrl = Environment.GetEnvironmentVariable("GRPC_CLIENT_URL");


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

        public async Task OnPost(int habitacionId, DateTime fechaInicio, DateTime fechaFin, int clienteId)
        {
            try
            {
                if (string.IsNullOrEmpty(grpcClientUrl))
                {
                    TempData["Error"] = "No se ha configurado el servidor gRPC.";
                    return;
                }
                // Validar entrada del formulario
                if (fechaInicio >= fechaFin)
                {
                    // Mostrar mensaje de error en consola
                    Console.WriteLine("La fecha de inicio debe ser anterior a la fecha de fin.");
                    TempData["Error"] = "La fecha de inicio debe ser anterior a la fecha de fin.";
                    return;
                }

                int diasReserva = (fechaFin - fechaInicio).Days;
                if (diasReserva <= 0)
                {
                    // Mostrar mensaje de error en consola
                    Console.WriteLine("La reserva debe ser de al menos un día.");
                    TempData["Error"] = "La reserva debe ser de al menos un día.";
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
                    return;
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

                // TODO: The port number must match the port number of the gRPC Client server
                using var channel = GrpcChannel.ForAddress(grpcClientUrl);
                var client = new Greeter.GreeterClient(channel);
                var request = new ReservaRequest { IdCliente = clienteId, Dias = diasReserva};
                var reply = await client.ReservarAsync(request);

                Console.WriteLine("Reserva procesada.");
                Console.WriteLine($"Respuesta del servidor: {reply.Message}");

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
