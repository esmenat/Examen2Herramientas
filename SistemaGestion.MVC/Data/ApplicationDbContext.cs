using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaGestion.Modelos;

namespace SistemaGestion.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SistemaGestion.Modelos.Proyecto> Proyecto { get; set; } = default!;
        public DbSet<SistemaGestion.Modelos.Tarea> Tarea { get; set; } = default!;
        public DbSet<SistemaGestion.Modelos.Usuario> Usuario { get; set; } = default!;
    }
}
