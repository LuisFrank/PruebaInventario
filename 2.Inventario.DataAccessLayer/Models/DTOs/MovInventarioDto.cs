using System.ComponentModel.DataAnnotations.Schema;

namespace _2.Inventario.DataAccessLayer.Models.DTOs
{
    public class MovInventarioDto
    {
        [Column("COD_CIA")]
        public string CodCia { get; set; } = string.Empty;
        
        [Column("COMPANIA_VENTA_3")]
        public string CompaniaVenta3 { get; set; } = string.Empty;
        
        [Column("ALMACEN_VENTA")]
        public string AlmacenVenta { get; set; } = string.Empty;
        
        [Column("TIPO_MOVIMIENTO")]
        public string TipoMovimiento { get; set; } = string.Empty;
        
        [Column("TIPO_DOCUMENTO")]
        public string TipoDocumento { get; set; } = string.Empty;
        
        [Column("NRO_DOCUMENTO")]
        public string NroDocumento { get; set; } = string.Empty;
        
        [Column("COD_ITEM_2")]
        public string CodItem2 { get; set; } = string.Empty;
        
        [Column("PROVEEDOR")]
        public string? Proveedor { get; set; }
        
        [Column("ALMACEN_DESTINO")]
        public string? AlmacenDestino { get; set; }
        
        [Column("CANTIDAD")]
        public int? Cantidad { get; set; }
        
        [Column("DOC_REF_1")]
        public string? DocRef1 { get; set; }
        
        [Column("DOC_REF_2")]
        public string? DocRef2 { get; set; }
        
        [Column("DOC_REF_3")]
        public string? DocRef3 { get; set; }
        
        [Column("DOC_REF_4")]
        public string? DocRef4 { get; set; }
        
        [Column("DOC_REF_5")]
        public string? DocRef5 { get; set; }
        
        [Column("FECHA_TRANSACCION")]
        public DateTime? FechaTransaccion { get; set; }
    }
}