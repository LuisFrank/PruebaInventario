using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2.Inventario.DataAccessLayer.Models
{
    [Table("MOV_INVENTARIO")]
    public class MovInventario
    {
        [Key]
        [Column("COD_CIA")]
        [StringLength(5)]
        public string CodCia { get; set; } = string.Empty;

        [Key]
        [Column("COMPANIA_VENTA_3")]
        [StringLength(5)]
        public string CompaniaVenta3 { get; set; } = string.Empty;

        [Key]
        [Column("ALMACEN_VENTA")]
        [StringLength(10)]
        public string AlmacenVenta { get; set; } = string.Empty;

        [Key]
        [Column("TIPO_MOVIMIENTO")]
        [StringLength(2)]
        public string TipoMovimiento { get; set; } = string.Empty;

        [Key]
        [Column("TIPO_DOCUMENTO")]
        [StringLength(2)]
        public string TipoDocumento { get; set; } = string.Empty;

        [Key]
        [Column("NRO_DOCUMENTO")]
        [StringLength(50)]
        public string NroDocumento { get; set; } = string.Empty;

        [Key]
        [Column("COD_ITEM_2")]
        [StringLength(50)]
        public string CodItem2 { get; set; } = string.Empty;

        [Column("PROVEEDOR")]
        [StringLength(100)]
        public string? Proveedor { get; set; }

        [Column("ALMACEN_DESTINO")]
        [StringLength(50)]
        public string? AlmacenDestino { get; set; }

        [Column("CANTIDAD")]
        public int? Cantidad { get; set; }

        [Column("DOC_REF_1")]
        [StringLength(50)]
        public string? DocRef1 { get; set; }

        [Column("DOC_REF_2")]
        [StringLength(50)]
        public string? DocRef2 { get; set; }

        [Column("DOC_REF_3")]
        [StringLength(50)]
        public string? DocRef3 { get; set; }

        [Column("DOC_REF_4")]
        [StringLength(50)]
        public string? DocRef4 { get; set; }

        [Column("DOC_REF_5")]
        [StringLength(50)]
        public string? DocRef5 { get; set; }

        [Column("FECHA_TRANSACCION")]
        public DateTime? FechaTransaccion { get; set; }
    }
}