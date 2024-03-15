using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre marca requerido")]
        [MaxLength(60, ErrorMessage = "Nombre debe ser maximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripción marca requerisdo")]
        [MaxLength(100, ErrorMessage = "Descripción debe ser maximo 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado de marca requerido")]
        public bool Estado { get; set; }
    }
}
