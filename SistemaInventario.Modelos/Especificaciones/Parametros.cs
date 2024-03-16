using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos.Especificaciones
{
    public class Parametros
    {
        //Numero de paginas a utilizar
        public int PageNumber { get; set; } = 1;

        //Numero de registros por cada pagina 
        public int PageSize { get; set; } = 4;
    }
}
