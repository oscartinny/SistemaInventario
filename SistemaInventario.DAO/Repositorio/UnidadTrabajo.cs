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
        //La clase unidad de trabajo nos sirve para poder acceder desde cualquier momento
        //Y la unidad de trabajo nos cargará todos los repositorios de los diferentes modelos
        //Para que la clase este accesible en todo el proyecto la agregamos como un servicio en Program.cs
        //Al agregarlo como servicio ya se pueden utilizar en los controlladores

        private readonly ApplicationDbContext _bd;

        public IBodegaRepositorio Bodega { get; private set; }
        public ICategoriaRepositorio Categoria { get; private set; }  
        public IMarcaRepositorio Marca { get; private set; }
        public IProductoRepositorio Producto { get; private set; }

        //Se manda el DBContext para que todos los modelos puedan utilizar los diferentes repositorios 
        public UnidadTrabajo(ApplicationDbContext bd)
        {
            _bd = bd;
            //Implementamos la interface Bodega, con la clase BodegaRepositorio que ya contiene los metodos definidos
            Bodega = new BodegaRepositorio(_bd);
            Categoria = new CategoriaRepositorio(_bd);
            Marca = new MarcaRepositorio(_bd);
            Producto = new ProductoRepositorio(_bd);
        }

        public void Dispose()
        {
            //Liberación de memoria
            _bd.Dispose();
        }

        public async Task Guardar()
        {
            await _bd.SaveChangesAsync();
        }
    }
}
