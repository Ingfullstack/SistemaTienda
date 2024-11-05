using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.Modelo.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de categoria es requerida")]
        [Display(Name = "Categoria")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion de categoria es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado de categoria es requerido")]
        public bool Estado { get; set; }
    }
}
