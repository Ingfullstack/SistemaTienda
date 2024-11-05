using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.Modelo.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de marca es requerida")]
        [Display(Name = "Marca")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion de marca es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado de marca es requerido")]
        public bool Estado { get; set; }
    }
}
