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
            var query = "INSERT INTO Tarea (Nombre, Descripcion, Estado, UsuarioId, ProyectoId) " +
                          "VALUES (@Nombre, @Descripcion, @Estado, @UsuarioId, @ProyectoId)";
            connection.Execute( query, new
            {
                tarea.Nombre,
                tarea.Descripcion,
                tarea.Estado,
                tarea.UsuarioId,
                tarea.ProyectoId

            });
            return tarea;

        }

        // PUT api/<TareasController>/5
        [HttpPut("{id}")]
        public Tarea Put(int id, [FromBody] Tarea tarea)
        {
            var query = "UPDATE Tarea SET Nombre = @Nombre, Descripcion = @Descripcion, Estado = @Estado, UsuarioId = @UsuarioId, ProyectoId = @ProyectoId WHERE Id = @Id";
            connection.Execute(query, new
            {
                tarea.Nombre,
                tarea.Descripcion,
                tarea.Estado,
                tarea.UsuarioId,
                tarea.ProyectoId,
                Id = id
            });
            return tarea;
            
        }

        // DELETE api/<TareasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var query = "DELETE FROM Tarea WHERE Id = @Id";
            connection.Execute(query, new { Id = id });
        }
    }
}
