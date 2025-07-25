using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SistemaGestion.Consumer;
using SistemaGestion.Modelos;

namespace SistemaGestion.MVC.Controllers
{
    public class BusquedaPorGruposController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public BusquedaPorGruposController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> index(string proyectoNombre )
        {
            if (proyectoNombre == null)
            {
                var tareas = Crud<Tarea>.GetAll();
                return View(tareas);
            }


            ViewData["proyectoNombre"] = proyectoNombre;
            var client = _httpClientFactory.CreateClient("API");
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var response = await client.GetAsync($"Tareas/proyecto?proyectoNombre={proyectoNombre}");

            if (response.IsSuccessStatusCode)
            {
                var tareas = await response.Content.ReadFromJsonAsync<List<Tarea>>();
                return View(tareas);  
            }

            return View("Error");
        }

    }
}
