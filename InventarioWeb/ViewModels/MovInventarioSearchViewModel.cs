using System.ComponentModel.DataAnnotations;

namespace InventarioWeb.ViewModels
{
    public class MovInventarioSearchViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicio")]
        public DateTime? FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Fin")]
        public DateTime? FechaFin { get; set; }

        [StringLength(2, ErrorMessage = "El tipo de movimiento no puede exceder 2 caracteres")]
        [Display(Name = "Tipo Movimiento")]
        public string? TipoMovimiento { get; set; }

        [StringLength(50, ErrorMessage = "El número de documento no puede exceder 50 caracteres")]
        [Display(Name = "Número Documento")]
        public string? NroDocumento { get; set; }

        [StringLength(5, ErrorMessage = "El código de compañía no puede exceder 5 caracteres")]
        [Display(Name = "Código Compañía")]
        public string? CodCia { get; set; }

        [StringLength(5, ErrorMessage = "La compañía venta no puede exceder 5 caracteres")]
        [Display(Name = "Compañía Venta")]
        public string? CompaniaVenta3 { get; set; }

        [StringLength(10, ErrorMessage = "El almacén venta no puede exceder 10 caracteres")]
        [Display(Name = "Almacén Venta")]
        public string? AlmacenVenta { get; set; }

        [StringLength(2, ErrorMessage = "El tipo de documento no puede exceder 2 caracteres")]
        [Display(Name = "Tipo Documento")]
        public string? TipoDocumento { get; set; }

        [StringLength(50, ErrorMessage = "El código de item no puede exceder 50 caracteres")]
        [Display(Name = "Código Item")]
        public string? CodItem2 { get; set; }

        [StringLength(100, ErrorMessage = "El proveedor no puede exceder 100 caracteres")]
        [Display(Name = "Proveedor")]
        public string? Proveedor { get; set; }

        [StringLength(10, ErrorMessage = "El almacén destino no puede exceder 10 caracteres")]
        [Display(Name = "Almacén Destino")]
        public string? AlmacenDestino { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 1 no puede exceder 50 caracteres")]
        [Display(Name = "Referencia 1")]
        public string? DocRef1 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 2 no puede exceder 50 caracteres")]
        [Display(Name = "Referencia 2")]
        public string? DocRef2 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 3 no puede exceder 50 caracteres")]
        [Display(Name = "Referencia 3")]
        public string? DocRef3 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 4 no puede exceder 50 caracteres")]
        [Display(Name = "Referencia 4")]
        public string? DocRef4 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 5 no puede exceder 50 caracteres")]
        [Display(Name = "Referencia 5")]
        public string? DocRef5 { get; set; }
    }
}