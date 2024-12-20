﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaTienda.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        Task Actualizar(Producto producto);
        IEnumerable<SelectListItem> ListaCategoriaMarca(string obj);
        Task<bool> ExisteCodigo(int codigo);
    }
}
