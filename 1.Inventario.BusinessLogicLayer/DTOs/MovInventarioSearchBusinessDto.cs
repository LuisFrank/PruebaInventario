using System;
using System.ComponentModel.DataAnnotations;

namespace Inventario.BusinessLogicLayer.DTOs
{
    /// <summary>
    /// DTO de negocio para búsquedas y filtros de MovInventario
    /// </summary>
    public class MovInventarioSearchBusinessDto
    {
        [StringLength(20, ErrorMessage = "El código de producto no puede exceder 20 caracteres")]
        public string? CodigoProducto { get; set; }

        [StringLength(10, ErrorMessage = "El código de almacén no puede exceder 10 caracteres")]
        public string? CodigoAlmacen { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        [RegularExpression("^[EIS]$", ErrorMessage = "El tipo de movimiento debe ser E (Entrada), I (Inventario) o S (Salida)")]
        public string? TipoMovimiento { get; set; }

        [StringLength(5, ErrorMessage = "El código de compañía no puede exceder 5 caracteres")]
        public string? CodCia { get; set; }

        [StringLength(5, ErrorMessage = "La compañía venta no puede exceder 5 caracteres")]
        public string? CompaniaVenta3 { get; set; }

        [StringLength(2, ErrorMessage = "El tipo de documento no puede exceder 2 caracteres")]
        public string? TipoDocumento { get; set; }

        [StringLength(50, ErrorMessage = "El número de documento no puede exceder 50 caracteres")]
        public string? NroDocumento { get; set; }

        [StringLength(100, ErrorMessage = "El proveedor no puede exceder 100 caracteres")]
        public string? Proveedor { get; set; }

        [StringLength(10, ErrorMessage = "El almacén destino no puede exceder 10 caracteres")]
        public string? AlmacenDestino { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 1 no puede exceder 50 caracteres")]
        public string? DocRef1 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 2 no puede exceder 50 caracteres")]
        public string? DocRef2 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 3 no puede exceder 50 caracteres")]
        public string? DocRef3 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 4 no puede exceder 50 caracteres")]
        public string? DocRef4 { get; set; }

        [StringLength(50, ErrorMessage = "La referencia 5 no puede exceder 50 caracteres")]
        public string? DocRef5 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La cantidad mínima no puede ser negativa")]
        public decimal? CantidadMinima { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La cantidad máxima no puede ser negativa")]
        public decimal? CantidadMaxima { get; set; }

        // Propiedades de paginación
        [Range(1, int.MaxValue, ErrorMessage = "La página debe ser mayor a 0")]
        public int Pagina { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "El tamaño de página debe estar entre 1 y 100")]
        public int TamanoPagina { get; set; } = 10;

        // Propiedades de ordenamiento
        public string? CampoOrden { get; set; }
        public bool OrdenDescendente { get; set; } = false;

        // Validaciones de negocio específicas para búsquedas
        public bool EsValido(out List<string> errores)
        {
            errores = new List<string>();

            // Validar rango de fechas
            if (FechaDesde.HasValue && FechaHasta.HasValue && FechaDesde > FechaHasta)
                errores.Add("La fecha desde no puede ser mayor a la fecha hasta");

            // Validar rango de cantidades
            if (CantidadMinima.HasValue && CantidadMaxima.HasValue && CantidadMinima > CantidadMaxima)
                errores.Add("La cantidad mínima no puede ser mayor a la cantidad máxima");



            // Validar que las fechas no sean futuras
            if (FechaDesde.HasValue && FechaDesde > DateTime.Now)
                errores.Add("La fecha desde no puede ser futura");

            if (FechaHasta.HasValue && FechaHasta > DateTime.Now)
                errores.Add("La fecha hasta no puede ser futura");

            // Validar campos de ordenamiento válidos
            if (!string.IsNullOrEmpty(CampoOrden))
            {
                var camposValidos = new[] { "CodigoProducto", "CodigoAlmacen", "FechaMovimiento", "TipoMovimiento", "Cantidad" };
                if (!camposValidos.Contains(CampoOrden))
                    errores.Add($"El campo de orden '{CampoOrden}' no es válido");
            }

            return errores.Count == 0;
        }

        // Propiedades calculadas para facilitar el uso
        public bool TieneFiltros => !string.IsNullOrEmpty(CodigoProducto) ||
                                   !string.IsNullOrEmpty(CodigoAlmacen) ||
                                   FechaDesde.HasValue ||
                                   FechaHasta.HasValue ||
                                   !string.IsNullOrEmpty(TipoMovimiento) ||
                                   !string.IsNullOrEmpty(CodCia) ||
                                   !string.IsNullOrEmpty(CompaniaVenta3) ||
                                   !string.IsNullOrEmpty(TipoDocumento) ||
                                   !string.IsNullOrEmpty(NroDocumento) ||
                                   !string.IsNullOrEmpty(Proveedor) ||
                                   !string.IsNullOrEmpty(AlmacenDestino) ||
                                   !string.IsNullOrEmpty(DocRef1) ||
                                   !string.IsNullOrEmpty(DocRef2) ||
                                   !string.IsNullOrEmpty(DocRef3) ||
                                   !string.IsNullOrEmpty(DocRef4) ||
                                   !string.IsNullOrEmpty(DocRef5) ||
                                   CantidadMinima.HasValue ||
                                   CantidadMaxima.HasValue;

        public int RegistrosASaltar => (Pagina - 1) * TamanoPagina;
    }
}