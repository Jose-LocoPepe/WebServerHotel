using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebServerHotel;

namespace MyApp.Namespace
{
    public class ListaReservasModel : PageModel
    {
        public List<Reserva> Reservas { get; set; }
        public void OnGet()
        {
            ReservasContext context = new ReservasContext();
            Reservas = context.Reservas
                            .Include(r => r.IdClienteNavigation)
                            .OrderBy(r => r.Id)
                            .ToList();
        }
    }
}
