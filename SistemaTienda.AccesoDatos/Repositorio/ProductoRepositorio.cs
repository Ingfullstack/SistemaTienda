using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaTienda.AccesoDatos.Data;
using SistemaTienda.AccesoDatos.Repositorio.IRepositorio;
using SistemaTienda.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.AccesoDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public ProductoRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Producto producto)
        {
            var obj = await dbContext.Productos.FirstOrDefaultAsync(x => x.Id == producto.Id);

            if (obj != null)
            {
                obj.Codigo = producto.Codigo;
                obj.Descripcion = producto.Descripcion;
                obj.Precio = producto.Precio;
                obj.Costo = producto.Costo;
                obj.UrlImagen = producto.UrlImagen;
                obj.CategoriaId = producto.CategoriaId;
                obj.MarcaId = producto.MarcaId;
                obj.Estado = producto.Estado;
            }
        }

        public async Task<bool> ExisteCodigo(int codigo)
        {
            return await dbContext.Productos.AnyAsync(x => x.Codigo == codigo);
        }

        public IEnumerable<SelectListItem> ListaCategoriaMarca(string obj)
        {
            if (obj == "Categoria")
            {
                return dbContext.Categorias.Where(x => x.Estado == true).Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString()
                });
            }

            if (obj == "Marca")
            {
                return dbContext.Marcas.Where(x => x.Estado == true).Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString()
                });
            }

            return null;
        }
    }
}
