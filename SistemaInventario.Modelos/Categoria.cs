using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre categoría requerido")]
        [MaxLength(60, ErrorMessage ="Nombre debe ser maximo 50 caracteres")]
        public string Nombre { get; set;}

        [Required(ErrorMessage = "Descripción categoría requerido")]
        [MaxLength(100, ErrorMessage = "Descripción debe ser maximo 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado de categoría requerido")]
        public bool Estado { get; set; }

    }
}
