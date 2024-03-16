using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos.Especificaciones
{
    public class MetaData
    {
        //Total de paginas que va a contener la vista 
        public int TotalPaginas { get; set; }

        public int PageSize { get; set; }

        //Total de registros 
        public int TotalCount { get; set; }
    }
}
