using Microsoft.AspNetCore.Mvc;
using SistemaTienda.AccesoDatos.Repositorio.IRepositorio;
using SistemaTienda.Modelo.Models;
using System.Diagnostics;

namespace SistemaTienda.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    public class HomeController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public HomeController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Producto> productoLista = await unidadTrabajo.Producto.ObtenerTodos();
            return View(productoLista);
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
