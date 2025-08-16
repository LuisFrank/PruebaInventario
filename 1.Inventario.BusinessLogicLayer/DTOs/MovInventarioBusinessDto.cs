using System;
using System.ComponentModel.DataAnnotations;

namespace Inventario.BusinessLogicLayer.DTOs
{
    /// <summary>
    /// DTO de negocio para MovInventario con validaciones y lógica empresarial
    /// Basado en la estructura de la tabla MOV_INVENTARIO
    /// </summary>
    public class MovInventarioBusinessDto
    {
        // Campos obligatorios (NOT NULL en la tabla)
        [Required(ErrorMessage = "El código de compañía es requerido")]
        [StringLength(5, ErrorMessage = "El código de compañía no puede exceder 5 caracteres")]
        public string CodCia { get; set; } = string.Empty;

        [Required(ErrorMessage = "La compañía de venta es requerida")]
        [StringLength(5, ErrorMessage = "La compañía de venta no puede exceder 5 caracteres")]
        public string CompaniaVenta3 { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código de almacén es requerido")]
        [StringLength(10, ErrorMessage = "El código de almacén no puede exceder 10 caracteres")]
        public string CodigoAlmacen { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de movimiento es requerido")]
        [StringLength(2, ErrorMessage = "El tipo de movimiento no puede exceder 2 caracteres")]
        public string TipoMovimiento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de documento es requerido")]
        [StringLength(2, ErrorMessage = "El tipo de documento no puede exceder 2 caracteres")]
        public string TipoDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de documento es requerido")]
        [StringLength(50, ErrorMessage = "El número de documento no puede exceder 50 caracteres")]
        public string NroDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código de producto es requerido")]
        [StringLength(50, ErrorMessage = "El código de producto no puede exceder 50 caracteres")]
        public string CodigoProducto { get; set; } = string.Empty;

        // Campos opcionales (NULL permitido en la tabla)
        [StringLength(100, ErrorMessage = "El proveedor no puede exceder 100 caracteres")]
        public string? Proveedor { get; set; }

        [StringLength(50, ErrorMessage = "El almacén destino no puede exceder 50 caracteres")]
        public string? AlmacenDestino { get; set; }

        public int? Cantidad { get; set; }

        [StringLength(50, ErrorMessage = "Las observaciones no pueden exceder 50 caracteres")]
        public string? Observaciones { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 2 no puede exceder 50 caracteres")]
        public string? DocRef2 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 3 no puede exceder 50 caracteres")]
        public string? DocRef3 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 4 no puede exceder 50 caracteres")]
        public string? DocRef4 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 5 no puede exceder 50 caracteres")]
        public string? DocRef5 { get; set; }

        public DateTime? FechaMovimiento { get; set; }

        // Propiedades calculadas de negocio
        public string TipoMovimientoDescripcion => TipoMovimiento switch
        {
            "EN" => "Entrada",
            "SA" => "Salida",
            "IN" => "Inventario",
            _ => "Desconocido"
        };



        // Validaciones de negocio
        public bool EsValido(out List<string> errores)
        {
            errores = new List<string>();

            if (string.IsNullOrWhiteSpace(CodCia))
                errores.Add("El código de compañía es requerido");

            if (string.IsNullOrWhiteSpace(CompaniaVenta3))
                errores.Add("La compañía de venta es requerida");

            if (string.IsNullOrWhiteSpace(CodigoAlmacen))
                errores.Add("El código de almacén es requerido");

            if (string.IsNullOrWhiteSpace(TipoMovimiento))
                errores.Add("El tipo de movimiento es requerido");

            if (string.IsNullOrWhiteSpace(TipoDocumento))
                errores.Add("El tipo de documento es requerido");

            if (string.IsNullOrWhiteSpace(NroDocumento))
                errores.Add("El número de documento es requerido");

            if (string.IsNullOrWhiteSpace(CodigoProducto))
                errores.Add("El código de producto es requerido");

            if (FechaMovimiento.HasValue && FechaMovimiento > DateTime.Now)
                errores.Add("La fecha de movimiento no puede ser futura");

            if (Cantidad.HasValue && Cantidad <= 0)
                errores.Add("La cantidad debe ser mayor a 0");

            return errores.Count == 0;
        }
    }
}