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
    public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public MarcaRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Marca marca)
        {
            var obj = await dbContext.Marcas.FirstOrDefaultAsync(x => x.Id == marca.Id);

            if (obj != null)
            {
                obj.Nombre = marca.Nombre;
                obj.Descripcion = marca.Descripcion;
                obj.Estado = marca.Estado;
            }
        }

        public async Task<bool> ExisteNombre(string nombre)
        {
            return await dbContext.Marcas.AnyAsync(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }
    }
}
