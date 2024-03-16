using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaInventario.DAO.Migrations;
using SistemaInventario.DAO.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.ViewModels;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]//CUANDO SE TRABAJEN CON AREAS SE DEBE REFERENCIAR A QUE CONTROLADOR PERTENECE CADA AREA
    public class ProductoController : Controller
    {
        //Creamos una variable para nuestro servicio UnidadTrabajo
        private readonly IUnidadTrabajo _unidadTrabajo;

        private readonly IWebHostEnvironment _webHostEnvironment;

        //Se crea un constructor y se inicializa la variable de servicio para poder ser ocupada
        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            //Ya inicializada, _unidadTrabajo cargará todos los repositorios de los modelos.
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Para poder crear una vista podemos dar clic derecho en el nombre del metodo
        //Y posterior agregar > vista razor
        public async  Task<IActionResult> Upsert(int? id) //se coloca ? despues del tipo del dato si el parametro se puede recibir null
            //Para accerder al paremtro deberemos usar el tag asp-route-[nombre parametro], en este caso asp-route-ip
        {
            //Se crea una instancia del producto y se inicializan sus propiedades
            ProductoVM productoVM = new ProductoVM() {
                Producto = new Producto(),
                //Llenamos los catalogos llamando al metodo obtenerTodosDropdownLista en producto
                CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Categoria"),
                MarcaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Marca"),
                PadreLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Producto")
                };

            if (id == null)
            {
                //Crear un nuevo producto
                productoVM.Producto.Estado = true;
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = await _unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
                if (productoVM.Producto == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(productoVM);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productoVM.Producto.Id == 0)
                {
                    //Crear
                    string upload = webRootPath + DS.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create)) {
                        files[0].CopyTo(filestream);
                    }
                    productoVM.Producto.ImagenUrl = fileName + extension;
                    await _unidadTrabajo.Producto.Agregar(productoVM.Producto);

                }
                else
                {
                    //Actualizar
                    var objProducto = await _unidadTrabajo.Producto.ObtenerPrimero(p => p.Id == productoVM.Producto.Id, isTracking: false);
                    if (files.Count > 0)  //Verifica si se carga una nueva imagen para el producto existente
                    {
                        string upload = webRootPath + DS.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //Borrar imagen anterior
                        var anteriorFile = Path.Combine(upload, objProducto.ImagenUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create)) {
                            files[0].CopyTo(fileStream);
                        }
                        productoVM.Producto.ImagenUrl = fileName + extension;
                    }
                    else
                    {
                        productoVM.Producto.ImagenUrl = objProducto.ImagenUrl;
                    }
                    _unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                }
                TempData[DS.Exitosa] = "Transaccion exitosa";
                await _unidadTrabajo.Guardar();
                return View("Index");

            }//Si el modelo no es valido
            productoVM.CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Categoria");
            productoVM.MarcaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Marca");
            productoVM.PadreLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Producto");
            return View(productoVM);

        }


        

        #region API

        [HttpGet] //Esta linea se utiliza para declarar que un metodo es de tipo get
        public async Task<IActionResult> ObtenerTodos()
        {
            //Dentro de la consulta de incluyen las propiedades Categoria y Marca
            //Eso para que dentro de la consulta se pueda obtener a que categoria y marca pertenece el producto
            var todos = await _unidadTrabajo.Producto.obtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todos }); //El nombre que le asignemos a la variable de retorno es como se diferenciara en el JS
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var productoDB = await _unidadTrabajo.Producto.Obtener(id);
            if (productoDB == null)
            {
                //Regresamos un objeto JSON ya que este metodo es llamado por el js
                return Json(new { success = false, message = "Error al eliminar Producto" });

            }

            //Antes de eliminar el producto procedemos a eliminar la imagen
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRuta;
            var anteriorFile = Path.Combine(upload, productoDB.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);

            }

            _unidadTrabajo.Producto.Remover(productoDB);
            await _unidadTrabajo.Guardar();
            //Regresamos un objeto JSON ya que este metodo es llamado por el js
            return Json(new { success = true , message = "Producto eliminado exitosamente"}) ;
        }


        //Se utiliza ActionName para poder invocar el metodo desde el JS
        [ActionName("ValidarSerie")]
        //Se reciben los parametros nombre y id, en caso de no recibir id este se inicia en 0
        public async Task<IActionResult> ValidarSerie(string serie, int id =0)
        {
            bool valor = false;
            //Cargamos en un listado todas las Productos existentes
            var lista = await _unidadTrabajo.Producto.obtenerTodos();
            //
            if (id == 0)
            {
                //Buscamos en la lista con Any, en donde busque si hay alguna Producto con el nombre
                //que recibimos como parametro. En caso de encontrarlo regresa un true
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim());
            }
            else
            {
                //En caso de recibir el id, se agrega a la validación
                //Se busca que id sea diferente para que tome en cuenta otras Productos y no el mismo nombre del registro que se esta validando
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim() && b.Id != id);
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
