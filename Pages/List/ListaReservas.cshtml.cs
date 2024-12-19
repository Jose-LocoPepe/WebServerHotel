using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServerHotel;

namespace MyApp.Namespace
{
    public class ListaReservasModel : PageModel
    {
        public List<Reserva> Reservas { get; set; }
        public void OnGet()
        {
            ReservasContext context = new ReservasContext();
            Reservas = context.Reservas.ToList();
        }
    }
}
