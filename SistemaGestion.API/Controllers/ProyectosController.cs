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
    public class ProyectosController : ControllerBase
    {
        private readonly DbConnection connection;

        public ProyectosController(IConfiguration config)
        {
            var connString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connString);
            connection.Open();
        }
        // GET: api/<ProyectosController>
        [HttpGet]
        public IEnumerable<Proyecto> Get()
        {
            var proyectos = connection.Query<Proyecto>("SELECT * FROM Proyecto").ToList();
            foreach (var proyecto in proyectos)
            {
                // Cargar tareas asociadas
                proyecto.Tareas = connection.Query<Tarea>("SELECT * FROM Tarea WHERE ProyectoId = @ProyectoId", new { ProyectoId = proyecto.Id }).ToList();

                // Cargar usuarios asociados
                proyecto.Usuarios = connection.Query<Usuario>("SELECT * FROM Usuario WHERE ProyectoId = @ProyectoId", new { ProyectoId = proyecto.Id }).ToList();
            }
            return proyectos;

        }

        // GET api/<ProyectosController>/5
        [HttpGet("{id}")]
        public Proyecto Get(int id)
        {
           var proyecto = connection.QueryFirstOrDefault<Proyecto>("SELECT * FROM Proyecto WHERE Id = @Id", new { Id = id });
            if (proyecto != null)
            {
                // Cargar tareas asociadas
                proyecto.Tareas = connection.Query<Tarea>("SELECT * FROM Tarea WHERE ProyectoId = @ProyectoId", new { ProyectoId = proyecto.Id }).ToList();
                // Cargar usuarios asociados
                proyecto.Usuarios = connection.Query<Usuario>("SELECT * FROM Usuario WHERE ProyectoId = @ProyectoId", new { ProyectoId = proyecto.Id }).ToList();

            }
            return proyecto;
        }

        // POST api/<ProyectosController>
        [HttpPost]
        public Proyecto Post([FromBody] Proyecto proyecto)
        {
            var query = "INSERT INTO Proyecto (Nombre, Descripcion, FechaInicio, FechaFin, Estado) " +
                          "VALUES (@Nombre, @Descripcion, @FechaInicio, @FechaFin, @Estado)";
                          
            connection.Execute(query, new
            {
             
                Nombre = proyecto.Nombre,
                Descripcion = proyecto.Descripcion,
                FechaInicio = proyecto.FechaInicio,
                FechaFin = proyecto.FechaFin,
                Estado = proyecto.Estado
                

            });
            return proyecto; 
        }

        // PUT api/<ProyectosController>/5
        [HttpPut("{id}")]
        public Proyecto Put(int id, [FromBody] Proyecto proyecto)
        {
            var query = "UPDATE Proyecto SET Nombre = @Nombre, Descripcion = @Descripcion, " +
                          "FechaInicio = @FechaInicio, FechaFin = @FechaFin, Estado = @Estado " +
                          "WHERE Id = @Id";
            connection.Execute(query, new 
            {
                Nombre = proyecto.Nombre,
                Descripcion = proyecto.Descripcion,
                FechaInicio = proyecto.FechaInicio,
                FechaFin = proyecto.FechaFin,
                Estado = proyecto.Estado


            });
            return proyecto;
        }

        // DELETE api/<ProyectosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var query = "DELETE FROM Proyecto WHERE Id = @Id";
            connection.Execute(query, new { Id = id });
        }
       
    }
}
