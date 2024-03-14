using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.DAO.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable //--> Se deshace de los recursos que ya no se utilizan
    {
        IBodegaRepositorio Bodega { get; }
        Task Guardar();
    }
}
