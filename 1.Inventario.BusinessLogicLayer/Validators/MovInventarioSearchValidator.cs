using FluentValidation;
using Inventario.BusinessLogicLayer.DTOs;
using System;
using System.Linq;

namespace Inventario.BusinessLogicLayer.Validators
{
    /// <summary>
    /// Validador para MovInventarioSearchBusinessDto usando FluentValidation
    /// </summary>
    public class MovInventarioSearchValidator : AbstractValidator<MovInventarioSearchBusinessDto>
    {
        private static readonly string[] CamposOrdenValidos = 
        {
            "CodigoProducto", "CodigoAlmacen", "FechaMovimiento", 
            "TipoMovimiento", "Cantidad"
        };

        public MovInventarioSearchValidator()
        {
            // Validaciones para CodigoProducto (opcional)
            RuleFor(x => x.CodigoProducto)
                .MaximumLength(20)
                .WithMessage("El código de producto no puede exceder 20 caracteres")
                .Matches(@"^[A-Za-z0-9-_*%]+$")
                .WithMessage("El código de producto solo puede contener letras, números, guiones, guiones bajos y comodines (*, %)")
                .When(x => !string.IsNullOrEmpty(x.CodigoProducto));

            // Validaciones para CodigoAlmacen (opcional)
            RuleFor(x => x.CodigoAlmacen)
                .MaximumLength(10)
                .WithMessage("El código de almacén no puede exceder 10 caracteres")
                .Matches(@"^[A-Za-z0-9-_*%]+$")
                .WithMessage("El código de almacén solo puede contener letras, números, guiones, guiones bajos y comodines (*, %)")
                .When(x => !string.IsNullOrEmpty(x.CodigoAlmacen));

            // Validaciones para rango de fechas
            RuleFor(x => x.FechaDesde)
                .GreaterThan(new DateTime(2000, 1, 1))
                .WithMessage("La fecha desde debe ser posterior al año 2000")
                .When(x => x.FechaDesde.HasValue);

            RuleFor(x => x.FechaHasta)
                .GreaterThan(new DateTime(2000, 1, 1))
                .WithMessage("La fecha hasta debe ser posterior al año 2000")
                .When(x => x.FechaHasta.HasValue);

            // Validación de coherencia entre fechas
            RuleFor(x => x.FechaHasta)
                .GreaterThanOrEqualTo(x => x.FechaDesde)
                .WithMessage("La fecha hasta debe ser mayor o igual a la fecha desde")
                .When(x => x.FechaDesde.HasValue && x.FechaHasta.HasValue);

            // Validación para evitar rangos de fechas muy amplios
            RuleFor(x => x)
                .Must(HaveReasonableDateRange)
                .WithMessage("El rango de fechas no puede exceder 5 años")
                .When(x => x.FechaDesde.HasValue && x.FechaHasta.HasValue);

            // TipoMovimiento sin validaciones - el usuario puede filtrar según los datos mostrados

            // Validaciones para rangos de cantidad
            RuleFor(x => x.CantidadMinima)
                .GreaterThanOrEqualTo(0)
                .WithMessage("La cantidad mínima no puede ser negativa")
                .LessThanOrEqualTo(999999.99m)
                .WithMessage("La cantidad mínima no puede exceder 999,999.99")
                .When(x => x.CantidadMinima.HasValue);

            RuleFor(x => x.CantidadMaxima)
                .GreaterThanOrEqualTo(0)
                .WithMessage("La cantidad máxima no puede ser negativa")
                .LessThanOrEqualTo(999999.99m)
                .WithMessage("La cantidad máxima no puede exceder 999,999.99")
                .GreaterThanOrEqualTo(x => x.CantidadMinima)
                .WithMessage("La cantidad máxima debe ser mayor o igual a la cantidad mínima")
                .When(x => x.CantidadMaxima.HasValue);



            // Validaciones para paginación
            RuleFor(x => x.Pagina)
                .GreaterThan(0)
                .WithMessage("La página debe ser mayor a 0")
                .LessThanOrEqualTo(10000)
                .WithMessage("La página no puede exceder 10,000");

            RuleFor(x => x.TamanoPagina)
                .InclusiveBetween(1, 100)
                .WithMessage("El tamaño de página debe estar entre 1 y 100");

            // Validaciones para ordenamiento
            RuleFor(x => x.CampoOrden)
                .Must(BeValidOrderField)
                .WithMessage($"El campo de orden debe ser uno de: {string.Join(", ", CamposOrdenValidos)}")
                .When(x => !string.IsNullOrEmpty(x.CampoOrden));

            // Validaciones para los nuevos campos de filtro
            RuleFor(x => x.CodCia)
                .MaximumLength(5)
                .WithMessage("El código de compañía no puede exceder 5 caracteres")
                .When(x => !string.IsNullOrEmpty(x.CodCia));

            RuleFor(x => x.CompaniaVenta3)
                .MaximumLength(5)
                .WithMessage("La compañía de venta no puede exceder 5 caracteres")
                .When(x => !string.IsNullOrEmpty(x.CompaniaVenta3));

            RuleFor(x => x.TipoDocumento)
                .MaximumLength(2)
                .WithMessage("El tipo de documento no puede exceder 2 caracteres")
                .When(x => !string.IsNullOrEmpty(x.TipoDocumento));

            RuleFor(x => x.NroDocumento)
                .MaximumLength(50)
                .WithMessage("El número de documento no puede exceder 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.NroDocumento));

            RuleFor(x => x.Proveedor)
                .MaximumLength(100)
                .WithMessage("El proveedor no puede exceder 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Proveedor));

            // Validación de que al menos un filtro esté presente para búsquedas amplias
            RuleFor(x => x)
                .Must(HaveAtLeastOneFilter)
                .WithMessage("Debe especificar al menos un filtro de búsqueda cuando el tamaño de página es mayor a 50")
                .When(x => x.TamanoPagina > 50);
        }



        private static bool BeValidOrderField(string campoOrden)
        {
            return !string.IsNullOrEmpty(campoOrden) && 
                   CamposOrdenValidos.Contains(campoOrden);
        }

        private static bool HaveReasonableDateRange(MovInventarioSearchBusinessDto dto)
        {
            if (!dto.FechaDesde.HasValue || !dto.FechaHasta.HasValue)
                return true;

            var diferencia = dto.FechaHasta.Value - dto.FechaDesde.Value;
            return diferencia.TotalDays <= 365 * 5; // Máximo 5 años
        }

        private static bool HaveAtLeastOneFilter(MovInventarioSearchBusinessDto dto)
        {
            return !string.IsNullOrEmpty(dto.CodigoProducto) ||
                   !string.IsNullOrEmpty(dto.CodigoAlmacen) ||
                   dto.FechaDesde.HasValue ||
                   dto.FechaHasta.HasValue ||
                   !string.IsNullOrEmpty(dto.TipoMovimiento) ||
                   dto.CantidadMinima.HasValue ||
                   dto.CantidadMaxima.HasValue ||
                   !string.IsNullOrEmpty(dto.CodCia) ||
                   !string.IsNullOrEmpty(dto.CompaniaVenta3) ||
                   !string.IsNullOrEmpty(dto.TipoDocumento) ||
                   !string.IsNullOrEmpty(dto.NroDocumento) ||
                   !string.IsNullOrEmpty(dto.Proveedor);
        }
    }
}