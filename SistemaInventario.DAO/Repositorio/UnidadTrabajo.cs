using SistemaInventario.DAO.Data;
using SistemaInventario.DAO.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.DAO.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _bd;

        public IBodegaRepositorio Bodega { get; private set; }

        public UnidadTrabajo(ApplicationDbContext bd)
        {
            _bd = bd;
            Bodega = new BodegaRepositorio(_bd);
        }

        public void Dispose()
        {
            _bd.Dispose();
        }

        public async Task Guardar()
        {
            await _bd.SaveChangesAsync();
        }
    }
}
