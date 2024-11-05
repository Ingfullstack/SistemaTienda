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
    public class AlmacenRepositorio : Repositorio<Almacen>, IAlmacenRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public AlmacenRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Almacen almacen)
        {
            var obj = await dbContext.Almacen.FirstOrDefaultAsync(x => x.Id == almacen.Id);

            if (obj != null)
            {
                obj.Nombre = almacen.Nombre;
                obj.Descripcion = almacen.Descripcion;
                obj.Estado = almacen.Estado;
            }
        }

        public async Task<bool> ExisteNombre(string nombre)
        {
            return await dbContext.Almacen.AnyAsync(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }
    }
}
