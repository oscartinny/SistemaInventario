using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos.ViewModels
{
    public class ProductoVM
    {
        //Los ViewModels nos ayudan a poder enviar más de un modelo en la vista.
        //En el caso de los productos instanciamos un objeto producto con sus diferentes relaciones de categoria y marca
        public Producto Producto { get; set; }

        public IEnumerable<SelectListItem> CategoriaLista { get; set; }

        public IEnumerable<SelectListItem> MarcaLista { get; set; }

        public IEnumerable<SelectListItem> PadreLista { get; set; }

    }
}
