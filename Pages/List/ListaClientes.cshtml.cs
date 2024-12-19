using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServerHotel;

namespace MyApp.Namespace
{
    public class ListaClientesModel : PageModel
    {
        public List<Cliente> Clientes { get; set; }
        public void OnGet()
        {
            ReservasContext context = new ReservasContext();
            Clientes = context.Clientes.ToList();
        }
    }
}
