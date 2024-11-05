using Microsoft.AspNetCore.Mvc;
using SistemaTienda.AccesoDatos.Repositorio.IRepositorio;
using SistemaTienda.Modelo.Models;

namespace SistemaTienda.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcasController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public MarcasController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await unidadTrabajo.Marca.ObtenerTodos());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            Marca marca = new Marca()
            {
                Estado = true
            };

            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Marca marca)
        {
            if (await unidadTrabajo.Marca.ExisteNombre(marca.Nombre))
            {
                TempData["error"] = "Ya existe este nombre";
                return View(marca);
            }

            if (ModelState.IsValid)
            {
                await unidadTrabajo.Marca.Agregar(marca);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Marcas Creada";
                return RedirectToAction("Index", "Marcas", new { Area = "Admin" });
            }
            return View(marca);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var obj = await unidadTrabajo.Marca.Obtener(id);

            if (obj is null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Marca marca)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Marca.Actualizar(marca);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Marcas Actualizada";
                return RedirectToAction("Index", "Marcas", new { Area = "Admin" });
            }
            return View(marca);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Json( new { data = await unidadTrabajo.Marca.ObtenerTodos() } );
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var obj = await unidadTrabajo.Marca.Obtener(id);

            if (obj is null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            unidadTrabajo.Marca.Remover(obj);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marcas eliminada" });
        }
        #endregion
    }
}
