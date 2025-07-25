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
    public class UsuariosController : ControllerBase
    {
        private readonly DbConnection connection;

        public UsuariosController(IConfiguration config)
        {
            var connString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connString);
            connection.Open();
        }
        // GET: api/<UsuariosController>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            var usuarios = connection.Query<Usuario>("SELECT Nombre FROM Usuario").ToList();
            return usuarios;

        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            var usuario = connection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuario WHERE Id = @Id", new { Id = id });
            return usuario;
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public Usuario Post([FromBody] Usuario usuario)
        {
            var query = "INSERT INTO Usuario (Nombre, Apellido, ProyectoId) " +
                        "VALUES (@Nombre, @Apellido, @ProyectoId)";
            connection.Execute(query, new
            {
                usuario.Nombre,
                usuario.Apellido,
                usuario.ProyectoId
            });
            return usuario;
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public Usuario Put(int id, [FromBody] Usuario usuario)
        {
            var query = "UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, ProyectoId = @ProyectoId WHERE Id = @Id";
            connection.Execute(query, new
            {
                usuario.Nombre,
                usuario.Apellido,
                usuario.ProyectoId,
                Id = id
            });
            return usuario;
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var query = "DELETE FROM Usuario WHERE Id = @Id";
            connection.Execute(query, new { Id = id });
        }
    }
}
