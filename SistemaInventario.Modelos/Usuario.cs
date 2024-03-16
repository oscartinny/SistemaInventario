using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Usuario : IdentityUser //Heredamos de esta clase que hace referencia a la tabla de usuarios que carga el framework
    {
        [Required( ErrorMessage ="Nombre es requerido")]
        [MaxLength(80)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido es requerido")]
        [MaxLength(80)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Dirección es requerido")]
        [MaxLength(200)]
        public string Dirección { get; set; }

        [Required(ErrorMessage = "Ciudad es requerido")]
        [MaxLength(60)]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "País es requerido")]
        [MaxLength(60)]
        public string País { get; set; }

        //Atributo para indicar que esta propiedad no se incluye como tabla en la BD
        [NotMapped]
        public string Role {  get; set; }

    }
}
