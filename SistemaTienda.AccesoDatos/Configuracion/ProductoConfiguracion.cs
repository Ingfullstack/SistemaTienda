using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaTienda.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.AccesoDatos.Configuracion
{
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.Descripcion).IsRequired();
            builder.Property(x => x.Precio).IsRequired();
            builder.Property(x => x.Costo).IsRequired();
            builder.Property(x => x.UrlImagen).IsRequired(false);
            builder.Property(x => x.CategoriaId).IsRequired();
            builder.Property(x => x.MarcaId).IsRequired();
            builder.Property(x => x.Estado).IsRequired();

            //Relaciones

            builder.HasOne(x => x.Categoria).WithMany()
                   .HasForeignKey(x => x.CategoriaId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Marca).WithMany()
                   .HasForeignKey(x => x.MarcaId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
