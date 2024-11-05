using SistemaTienda.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.AccesoDatos.Repositorio.IRepositorio
{
    public interface IAlmacenRepositorio : IRepositorio<Almacen>
    {
        Task Actualizar(Almacen almacen);
        Task<bool> ExisteNombre(string nombre);
    }
}
