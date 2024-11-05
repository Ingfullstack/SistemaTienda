using Microsoft.AspNetCore.Mvc;
using SistemaTienda.AccesoDatos.Repositorio.IRepositorio;
using SistemaTienda.Modelo.Models;

namespace SistemaTienda.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public CategoriasController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await unidadTrabajo.Categoria.ObtenerTodos());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            Categoria categoria = new Categoria()
            {
                Estado = true
            };

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            if (await unidadTrabajo.Categoria.ExisteNombre(categoria.Nombre))
            {
                TempData["error"] = "Ya existe este nombre";
                return View(categoria);
            }

            if (ModelState.IsValid)
            {
                await unidadTrabajo.Categoria.Agregar(categoria);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Categorias Creada";
                return RedirectToAction("Index", "Categorias", new { Area = "Admin" });
            }
            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var obj = await unidadTrabajo.Categoria.Obtener(id);

            if (obj is null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Categoria.Actualizar(categoria);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Categorias Actualizada";
                return RedirectToAction("Index", "Categorias", new { Area = "Admin" });
            }
            return View(categoria);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Json( new { data = await unidadTrabajo.Categoria.ObtenerTodos() } );
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var obj = await unidadTrabajo.Categoria.Obtener(id);

            if (obj is null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            unidadTrabajo.Categoria.Remover(obj);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categorias eliminada" });
        }
        #endregion
    }
}
