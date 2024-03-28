using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaInventario.DAO.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]//CUANDO SE TRABAJEN CON AREAS SE DEBE REFERENCIAR A QUE CONTROLADOR PERTENECE CADA AREA
    //SE UTILIZA Authorize PARA OBLIGAR AL USUARIO A QUE SE LOGUEE PARA INGRESAR A ESTE CONTROLADOR
    //ADICIONAL SE AGREGA EL TIPO DE ROL QUE TENDRÁ ACCESO AL MODULO
    [Authorize(Roles = DS.Role_Admin)] 
    public class BodegaController : Controller
    {
        //Creamos una variable para nuestro servicio UnidadTrabajo
        private readonly IUnidadTrabajo _unidadTrabajo;

        //Se crea un constructor y se inicializa la variable de servicio para poder ser ocupada
        public BodegaController(IUnidadTrabajo unidadTrabajo)
        {
            //Ya inicializada, _unidadTrabajo cargará todos los repositorios de los modelos.
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async  Task<IActionResult> Upsert(int? id) //se coloca ? despues del tipo del dato si el parametro se puede recibir null
            //Para accerder al paremtro deberemos usar el tag asp-route-[nombre parametro], en este caso asp-route-ip
        {
            Bodega bodega = new Bodega();
            if (id==null)
            {
                //Crear una nueva bodega en blanco para poder realizar la insercion de uno nuevo
                bodega.Estado = true;
                return View(bodega); ;

            }
            //Actualizamos Bodega, obteniendo el modelo de bodega mediante el id recibido
            bodega = await _unidadTrabajo.Bodega.Obtener(id.GetValueOrDefault());
            if(bodega == null){
                return NotFound(); 
            }
            //Regresamos el modelo bodega en la vista para que obtenga los datos.
            return View(bodega);
        }

        [HttpPost] //Esta linea se utiliza para declarar que un metodo es de tipo post
        [ValidateAntiForgeryToken] //Sirve para evitar falsificación de solicitudes de un sitio cargado de otra pagina que pueda cargar datos a nuestra pagina
        public async Task<IActionResult> Upsert(Bodega bodega)
        {//El controlador tiene dos clases upsert, pero con medoto get y post 
            if(ModelState.IsValid) //Validamos si el modelo recibido dentro de metodo es valido para poder continuar
            {
                if (bodega.Id == 0)
                {
                    //Como el id es 0 refiere a un nuevo registro
                    await _unidadTrabajo.Bodega.Agregar(bodega);
                    TempData[DS.Exitosa] = "Exito al agregar nueva bodega";
                }
                else
                {
                    //Al recibir un valor diferente a 0 refiere a una actualización.
                    _unidadTrabajo.Bodega.Actualizar(bodega);
                    TempData[DS.Exitosa] = "Información de la bodega actualizada exitosamente";
                }

                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index)); //Linea para redireccionar a alguna acción terminando la ejecución
            }
            TempData[DS.Error] = "Error al grabar bodega";
            return View(bodega);
        }

        #region API

        [HttpGet] //Esta linea se utiliza para declarar que un metodo es de tipo get
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Bodega.obtenerTodos();
            return Json(new { data = todos }); //El nombre que le asignemos a la variable de retorno es como se diferenciara en el JS
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var bodegaDB = await _unidadTrabajo.Bodega.Obtener(id);
            if (bodegaDB == null)
            {
                //Regresamos un objeto JSON ya que este metodo es llamado por el js
                return Json(new { success = false, message = "Error al eliminar bodega" });

            }
            _unidadTrabajo.Bodega.Remover(bodegaDB);
            await _unidadTrabajo.Guardar();
            //Regresamos un objeto JSON ya que este metodo es llamado por el js
            return Json(new { success = true , message = "Bodega elimnada exitosamente"}) ;
        }


        //Se utiliza ActionName para poder invocar el metodo desde el JS
        [ActionName("ValidarNombre")]
        //Se reciben los parametros nombre y id, en caso de no recibir id este se inicia en 0
        public async Task<IActionResult> ValidarNombre(string nombre, int id =0)
        {
            bool valor = false;
            //Cargamos en un listado todas las bodegas existentes
            var lista = await _unidadTrabajo.Bodega.obtenerTodos();
            //
            if (id == 0)
            {
                //Buscamos en la lista con Any, en donde busque si hay alguna bodega con el nombre
                //que recibimos como parametro. En caso de encontrarlo regresa un true
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                //En caso de recibir el id, se agrega a la validación
                //Se busca que id sea diferente para que tome en cuenta otras bodegas y no el mismo nombre del registro que se esta validando
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                //Si la validación es correcta devolvemos un JSON para que sea leido por el JS
                return Json(new { data = true });
            }
            
            return Json(new { data = false });

        }

        #endregion
    }
}
