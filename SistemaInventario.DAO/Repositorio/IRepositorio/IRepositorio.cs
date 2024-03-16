using SistemaInventario.Modelos.Especificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.DAO.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        //DENTRO DE ESTA INTERFACE SE DEFINEN LOS METODOS QUE OCUPARAN TODOS LAS CLASES O INSTANCIAS DENTRO DEL PROYECTO
        //LA PROPIEDAD TASK PERMITE EJECUTAR LOS METODOS DE MANERA ASINCRONA

        Task<T> Obtener(int id);

        Task<IEnumerable<T>> obtenerTodos(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );

        PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );

        Task Agregar(T entidad);

        void Remover(T entidad);

        void RemoverRango(IEnumerable<T> entidad);



    }
}
