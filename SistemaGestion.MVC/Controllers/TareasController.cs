using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaGestion.Consumer;
using SistemaGestion.Modelos;

namespace SistemaGestion.MVC.Controllers
{
    public class TareasController : Controller
    {
        // GET: TareasController
        public ActionResult Index()
        {
            var tareas = Crud<Tarea>.GetAll();
            return View(tareas);
        }

        // GET: TareasController/Details/5
        public ActionResult Details(int id)
        {
            var tarea = Crud<Tarea>.GetById(id);

            return View(tarea);
        }

        // GET: TareasController/Create
        public ActionResult Create()
        {
            ViewBag.Proyectos = GetProyectos();
            ViewBag.Usuarios = GetUsuario();
            return View();
        }
        public List<SelectListItem> GetProyectos()
        {
            var proyectos = Crud<Proyecto>.GetAll();
            var proyectosSelect = proyectos.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nombre
            }).ToList();
            return proyectosSelect;
        }
        public List<SelectListItem> GetUsuario()
        {
            var usuarios = Crud<Usuario>.GetAll();
            var usuariosSelect = usuarios.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Nombre
            }).ToList();
            return usuariosSelect;
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Create(tarea);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasController/Edit/5
        public ActionResult Edit(int id)
        {
            var tarea = Crud<Tarea>.GetById(id);
            ViewBag.Proyectos = GetProyectos();
            ViewBag.Usuarios = GetUsuario();

            return View(tarea);
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Update(id,tarea);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasController/Delete/5
        public ActionResult Delete(int id)
        {
            var tarea = Crud<Tarea>.GetById(id);
            return View(tarea);
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Crud<Tarea>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
