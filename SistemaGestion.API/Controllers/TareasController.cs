using System.Data.Common;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SistemaGestion.Modelos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaGestion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly DbConnection connection;

        public TareasController(IConfiguration config)
        {
            var connString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connString);
            connection.Open();
        }
        // GET: api/<TareasController>
        [HttpGet]
        public IEnumerable<Tarea> Get()
        {
            var tareas = connection.Query<Tarea>("SELECT * FROM Tarea").ToList();
            return tareas;
        }

        // GET api/<TareasController>/5
        [HttpGet("{id}")]
        public  Tarea Get(int id)
        {
           var tarea = connection.QueryFirstOrDefault<Tarea>("SELECT * FROM Tarea WHERE Id = @Id", new { Id = id });
            return tarea;
        }

        // POST api/<TareasController>
        [HttpPost]
        public Tarea Post([FromBody] Tarea tarea)
        {
       
            var query = "INSERT INTO Tarea (Nombre, Descripcion, Estado, ProyectoId) " +
                          "VALUES (@Nombre, @Descripcion, @Estado, @ProyectoId)";
            connection.Execute( query, new
            {
                Nombre = tarea.Nombre,
                Descripcion = tarea.Descripcion,
                Estado = tarea.Estado,
                ProyectoId = tarea.ProyectoId,

            });
            return tarea;

        }

        // PUT api/<TareasController>/5
        [HttpPut("{id}")]
        public Tarea Put(int id, [FromBody] Tarea tarea)
        {
            var query = "UPDATE Tarea SET Nombre = @Nombre, Descripcion = @Descripcion, Estado = @Estado, ProyectoId = @ProyectoId WHERE Id = @Id";
            connection.Execute(query, new
            {
               Nombre =  tarea.Nombre,
                Descripcion = tarea.Descripcion,
                Estado = tarea.Estado,
                ProyectoId = tarea.ProyectoId,
                
                    
            });
            return tarea;
            
        }
        [HttpGet("filtro")]
        public IEnumerable<Tarea> GetTareasFiltradas(string estado)
        {
           
            var query = "SELECT * FROM Tarea WHERE Estado = @Estado";
            var tareas = connection.Query<Tarea>(query, new { Estado = estado }).ToList();

           
            return tareas;
        }
        [HttpGet("proyecto")]
        public IEnumerable<Tarea> GetTareasPorProyecto(string proyectoNombre)
        {
            var query = "SELECT * FROM Tarea WHERE ProyectoId = (SELECT Id FROM Proyecto WHERE Nombre = @Nombre)";
            var tareas = connection.Query<Tarea>(query, new { Nombre = proyectoNombre }).ToList();
            foreach (var tarea in tareas)
            {
                var proyecto = connection.QueryFirstOrDefault<Proyecto>("SELECT * FROM Proyecto WHERE Id = @ProyectoId", new { ProyectoId = tarea.ProyectoId });
               
            }
          
            return tareas;
        }
       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tarea = connection.QueryFirstOrDefault<Tarea>("SELECT * FROM Tarea WHERE Id = @Id", new { Id = id });

            if (tarea == null)
            {
                return NotFound();
            }

            var query = "DELETE FROM Tarea WHERE Id = @Id";
            connection.Execute(query, new { Id = id });

            return Ok(tarea); 
        }
       
    }
}
