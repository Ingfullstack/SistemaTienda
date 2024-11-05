using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.Modelo.Models
{
    public class Almacen
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de almacen es requerido")]
        [Display(Name = "Almacen")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion de almacen es requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado de almacen es requerido")]
        public bool Estado { get; set; }
    }
}
