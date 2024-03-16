using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Número de serie requerido")]
        [MaxLength(60)]
        public string NumeroSerie { get; set; }

        [Required(ErrorMessage = "Descripción requerida")]
        [MaxLength(60)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage ="Precio requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Costo requerido")]
        public double Costo { get; set; }

        public string ImagenUrl { get; set; }

        [Required(ErrorMessage ="Estado requerido")]
        public bool Estado { get; set;}

        /*Para realizar una relacion a otro modelo (Como en BD) se requiere tener una propiedad
         que sea del mismo tipo que el id a referenciar en este caso CategoriaId*/
        [Required(ErrorMessage ="Categoria requerida")]
        public int CategoriaId { get; set; }

        /*Adicional se requiere otra propiedad del modelo sobre el que se buscar "navegar".
         Para relacionar el modelo con nuestro id a relacionar 
        ocupamos el atributo ForeignKey("[NombrePropiedadId]")*/
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Marca es requerida")]
        public int MarcaId{ get; set; }

        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }

        /*Para que un campo pueda ser Null se deberá de incluir "?" al final del tipo de dato del
         la propiedad, si no se incluye y al ser un campo no requerido el sistema estaria guardando
         los registro con valor 0, lo cual puede traer problemas si se utiliza el campo como llave foranea*/
        public int? PadreId { get; set; }

        public virtual Producto Padre { get; set; }

    }
}
