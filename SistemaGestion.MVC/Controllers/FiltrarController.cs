using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Consumer;
using SistemaGestion.Modelos;

namespace SistemaGestion.MVC.Controllers
{
    public class FiltrarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FiltrarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
 
        public async Task<IActionResult> Index(string estado)
        {
            if(estado == null)
            {
                var tareas = Crud<Tarea>.GetAll();
                return View(tareas); 
            }


            ViewData["estado"] = estado; 
            var client = _httpClientFactory.CreateClient("API");
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var response = await client.GetAsync($"Tareas/filtro?estado={estado}");

            if (response.IsSuccessStatusCode)
            {
                var tareas = await response.Content.ReadFromJsonAsync<List<Tarea>>();
                return View(tareas);  // Devuelve las tareas a la vista
            }

            return View("Error");
        }


    }
}

