using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaGestion.Consumer;
using SistemaGestion.Modelos;

namespace SistemaGestion.MVC.Controllers
{
    public class ProyectosController : Controller
    {
        // GET: ProyectosController
        public ActionResult Index()
        {
            var proyectos = Crud<Proyecto>.GetAll();
            return View(proyectos);
        }

        // GET: ProyectosController/Details/5
        public ActionResult Details(int id)
        {
            var proyecto = Crud<Proyecto>.GetById(id);

            return View(proyecto);
        }

        // GET: ProyectosController/Create
        public ActionResult Create()
        {
            ViewBag.Usuarios = GetUsuarios();
            ViewBag.Tareas = GetTareas();
            return View();
        }
        // GET: ProyectosController/Create
        public List<SelectListItem> GetUsuarios()
        {
            var usuarios = Crud<Usuario>.GetAll();
            var usuariosSelect = usuarios.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.Nombre} {u.Apellido}"
            }).ToList();
            return usuariosSelect;
        }
        // GET: ProyectosController/Create
        public List<SelectListItem> GetTareas()
        {
            var tareas = Crud<Tarea>.GetAll();
            var tareasSelect = tareas.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nombre
            }).ToList();
            return tareasSelect;
        }

        // POST: ProyectosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Proyecto proyecto)
        {
            try
            {
                Crud<Proyecto>.Create(proyecto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProyectosController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Proyecto>.GetById(id);
            ViewBag.Usuarios = GetUsuarios();
            ViewBag.Tareas = GetTareas();
            return View(data);
        }

        // POST: ProyectosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Proyecto proyecto)
        {
            try
            {
                Crud<Proyecto>.Update(id, proyecto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProyectosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Proyecto>.GetById(id);
            return View(data);
        }

        // POST: ProyectosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Crud<Proyecto>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
