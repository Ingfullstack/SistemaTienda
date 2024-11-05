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
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriaRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Categoria categoria)
        {
            var obj = await dbContext.Categorias.FirstOrDefaultAsync(x => x.Id == categoria.Id);

            if (obj != null)
            {
                obj.Nombre = categoria.Nombre;
                obj.Descripcion = categoria.Descripcion;
                obj.Estado = categoria.Estado;
            }
        }

        public async Task<bool> ExisteNombre(string nombre)
        {
            return await dbContext.Categorias.AnyAsync(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }
    }
}
