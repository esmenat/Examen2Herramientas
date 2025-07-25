using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Modelos
{
    public class Usuario
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int ProyectoId { get; set; } 
        public Proyecto? Proyecto { get; set; } 




    }
}
