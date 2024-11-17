using Microsoft.AspNetCore.Mvc;
using SistemaTienda.AccesoDatos.Repositorio.IRepositorio;
using SistemaTienda.Modelo.Models;
using SistemaTienda.Modelo.ViewsModels;

namespace SistemaTienda.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductosController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            this.unidadTrabajo = unidadTrabajo;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca"));
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = unidadTrabajo.Producto.ListaCategoriaMarca("Categoria"),
                MarcaLista = unidadTrabajo.Producto.ListaCategoriaMarca("Marca")
            };
            productoVM.Producto.Estado = true;
            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var archivo = HttpContext.Request.Form.Files;
                string rutaPrincipal = webHostEnvironment.WebRootPath;

                if (productoVM.Producto.Id == 0)
                {
                    //Subir Una Imagen
                    var subida = Path.Combine(rutaPrincipal, @"imagenes\producto");
                    var nombreArchivo = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(archivo[0].FileName);

                    using (var fileStrems = new FileStream(Path.Combine(subida, nombreArchivo + extension), FileMode.Create))
                    {
                        archivo[0].CopyTo(fileStrems);
                    }

                    productoVM.Producto.UrlImagen = @"\imagenes\producto\" + nombreArchivo + extension;

                    await unidadTrabajo.Producto.Agregar(productoVM.Producto);
                    TempData["success"] = "Producto Creado";
                    await unidadTrabajo.Guardar();
                    return RedirectToAction("Index", "Productos");
                }
            }
            productoVM.CategoriaLista = unidadTrabajo.Producto.ListaCategoriaMarca("Categoria");
            productoVM.MarcaLista = unidadTrabajo.Producto.ListaCategoriaMarca("Marca");
            return View(productoVM);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = unidadTrabajo.Producto.ListaCategoriaMarca("Categoria"),
                MarcaLista = unidadTrabajo.Producto.ListaCategoriaMarca("Marca")
            };

            productoVM.Producto = await unidadTrabajo.Producto.Obtener(id);

            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var archivo = HttpContext.Request.Form.Files;
                string rutaPrincipal = webHostEnvironment.WebRootPath;
                var productoBD = await unidadTrabajo.Producto.Obtener(productoVM.Producto.Id);

                if (archivo.Count() > 0)
                {
                    //Subir Una Imagen
                    var subida = Path.Combine(rutaPrincipal, @"imagenes\producto");
                    var nombreArchivo = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(archivo[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, productoBD.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    using (var fileStrems = new FileStream(Path.Combine(subida, nombreArchivo + extension), FileMode.Create))
                    {
                        archivo[0].CopyTo(fileStrems);
                    }

                    productoVM.Producto.UrlImagen = @"\imagenes\producto\" + nombreArchivo + extension;

                    await unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                    TempData["success"] = "Producto Actualizado";
                    await unidadTrabajo.Guardar();
                    return RedirectToAction("Index", "Productos");
                }
                else
                {
                    productoVM.Producto.UrlImagen = productoBD.UrlImagen;
                }

                await unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                TempData["success"] = "Producto Actualizado";
                await unidadTrabajo.Guardar();
                return RedirectToAction("Index", "Productos");
            }
            return View(productoVM);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Json(new { data = await unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades: "Categoria,Marca") });
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int? id)
        {
            var rutaPrincipal = webHostEnvironment.WebRootPath;
            var productoBD = await unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());

            if (id is null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            var rutaImagen = Path.Combine(rutaPrincipal, productoBD.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            unidadTrabajo.Producto.Remover(productoBD);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Producto eliminado" });
        }
        #endregion
    }
}
