using Microsoft.AspNetCore.Mvc;
using SistemaTienda.AccesoDatos.Repositorio.IRepositorio;
using SistemaTienda.Modelo.Especificaciones;
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
        public IActionResult Index(int pageNumber = 1, string busqueda = "", string busquedaActual = "")
        {

            if (!string.IsNullOrEmpty(busqueda))
            {
                pageNumber = 1;
            }
            else
            {
                busqueda = busquedaActual;
            }

            ViewData["BusquedaActual"] = busqueda;

            if (pageNumber < 1) { pageNumber = 1; }

            Parametros parametros = new Parametros()
            {
                PageNumber = pageNumber,
                PageSize = 4
            };

            var resultado = unidadTrabajo.Producto.ObtenerTodosPaginado(parametros);

            if (!string.IsNullOrEmpty(busqueda))
            {
                resultado = unidadTrabajo.Producto.ObtenerTodosPaginado(parametros, x => x.Descripcion.Contains(busqueda) || x.Codigo.ToString().Contains(busqueda));
            }

            ViewData["TotalPaginas"] = resultado.MetaData.TotalPages;
            ViewData["TotalRegistros"] = resultado.MetaData.TotalCount;
            ViewData["PageSize"] = resultado.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disabled";
            ViewData["Siguiente"] = "";

            if (pageNumber > 1) { ViewData["Previo"] = ""; }
            if(resultado.MetaData.TotalPages <= pageNumber) { ViewData["Siguiente"] = "disabled"; }

            return View(resultado);
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
