using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _2.Inventario.DataAccessLayer.Models;

namespace _2.Inventario.DataAccessLayer.Data.Configurations
{
    public class MovInventarioConfiguration : IEntityTypeConfiguration<MovInventario>
    {
        public void Configure(EntityTypeBuilder<MovInventario> builder)
        {
            // Configurar tabla
            builder.ToTable("MOV_INVENTARIO");

            // Configurar clave primaria compuesta
            builder.HasKey(x => new { 
                x.CodCia, 
                x.CompaniaVenta3, 
                x.AlmacenVenta, 
                x.TipoMovimiento, 
                x.TipoDocumento, 
                x.NroDocumento, 
                x.CodItem2 
            });

            // Configurar propiedades requeridas
            builder.Property(x => x.CodCia)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("COD_CIA");

            builder.Property(x => x.CompaniaVenta3)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("COMPANIA_VENTA_3");

            builder.Property(x => x.AlmacenVenta)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("ALMACEN_VENTA");

            builder.Property(x => x.TipoMovimiento)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnName("TIPO_MOVIMIENTO");

            builder.Property(x => x.TipoDocumento)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnName("TIPO_DOCUMENTO");

            builder.Property(x => x.NroDocumento)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("NRO_DOCUMENTO");

            builder.Property(x => x.CodItem2)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("COD_ITEM_2");

            // Configurar propiedades opcionales
            builder.Property(x => x.Proveedor)
                .HasMaxLength(100)
                .HasColumnName("PROVEEDOR");

            builder.Property(x => x.AlmacenDestino)
                .HasMaxLength(50)
                .HasColumnName("ALMACEN_DESTINO");

            builder.Property(x => x.Cantidad)
                .HasColumnName("CANTIDAD");

            builder.Property(x => x.DocRef1)
                .HasMaxLength(50)
                .HasColumnName("DOC_REF_1");

            builder.Property(x => x.DocRef2)
                .HasMaxLength(50)
                .HasColumnName("DOC_REF_2");

            builder.Property(x => x.DocRef3)
                .HasMaxLength(50)
                .HasColumnName("DOC_REF_3");

            builder.Property(x => x.DocRef4)
                .HasMaxLength(50)
                .HasColumnName("DOC_REF_4");

            builder.Property(x => x.DocRef5)
                .HasMaxLength(50)
                .HasColumnName("DOC_REF_5");

            builder.Property(x => x.FechaTransaccion)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_TRANSACCION");
        }
    }
}