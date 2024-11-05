using Microsoft.AspNetCore.Mvc;
using SistemaTienda.AccesoDatos.Repositorio.IRepositorio;
using SistemaTienda.Modelo.Models;

namespace SistemaTienda.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlmacenController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public AlmacenController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await unidadTrabajo.Almacen.ObtenerTodos());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            Almacen almacen = new Almacen()
            {
                Estado = true
            };

            return View(almacen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Almacen almacen)
        {
            if (await unidadTrabajo.Almacen.ExisteNombre(almacen.Nombre))
            {
                TempData["error"] = "Ya existe este nombre";
                return View(almacen);
            }

            if (ModelState.IsValid)
            {
                await unidadTrabajo.Almacen.Agregar(almacen);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Almacen Creado";
                return RedirectToAction("Index", "Almacen", new { Area = "Admin" });
            }
            return View(almacen);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var obj = await unidadTrabajo.Almacen.Obtener(id);

            if (obj is null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Almacen.Actualizar(almacen);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Almacen Actualizado";
                return RedirectToAction("Index", "Almacen", new { Area = "Admin" });
            }
            return View(almacen);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Json( new { data = await unidadTrabajo.Almacen.ObtenerTodos() } );
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var obj = await unidadTrabajo.Almacen.Obtener(id);

            if (obj is null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            unidadTrabajo.Almacen.Remover(obj);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Almacen eliminado" });
        }
        #endregion
    }
}
