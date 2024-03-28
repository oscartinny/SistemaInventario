using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.DAO.Data;
using SistemaInventario.DAO.Repositorio.IRepositorio;
using System.Collections.Immutable;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuarioController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ApplicationDbContext _db;

        /*Inyeccion de dependencias*/
        public UsuarioController(IUnidadTrabajo unidadTrabajo, ApplicationDbContext bd)
        {
            _unidadTrabajo = unidadTrabajo;
            _db = bd;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarioLista = await _unidadTrabajo.Usuario.obtenerTodos();
            var userRole = await _db.UserRoles.ToListAsync();
            var roles = await _db.Roles.ToListAsync();

            foreach (var usuario in usuarioLista)
            {
                //Por cada usuario deberemos de obtener el primer rol que tenga y ser asignado a nuestra propiedad rol
                var roleId = userRole.FirstOrDefault(u => u.UserId == usuario.Id).RoleId;
                usuario.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

            }
            return Json(new { data = usuarioLista });
        }

        [HttpPost]
        public async Task<IActionResult> BloquearDesbloquear([FromBody] string id)
        {
            var usuario = await _unidadTrabajo.Usuario.ObtenerPrimero(u => u.Id == id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Error el usuario no exste" });
            }
            if (usuario.LockoutEnd != null && usuario.LockoutEnd > DateTime.Today)
            {
                //EL USUARIO ESTA BLOQUEADO Y SE PRETENDE DESBLOQUEAR
                usuario.LockoutEnd = DateTime.Now;
            }
            else
            {
                //EL USUARIO ESTA DESBLOQUEADO Y SE REQUIERE BLOQUEAR
                usuario.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Operacion Exitosa" });
        }

        #endregion
    }
}
