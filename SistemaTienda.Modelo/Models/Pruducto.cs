using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.Modelo.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Codigo de producto es requerido")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Descripcion de producto es requerido")]
        public string Descripcion { get; set; }

        [Display(Name = "Imagen")]
        [DataType(DataType.ImageUrl)]
        public string UrlImagen { get; set; }

        [Required(ErrorMessage = "Precio de producto es requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Costo de producto es requerido")]
        public double Costo { get; set; }

        [Required(ErrorMessage = "Categoria de producto es requerido")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Marca de producto es requerido")]
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }

        [Required(ErrorMessage = "Estado de producto es requerido")]
        public bool Estado { get; set; }
    }
}
