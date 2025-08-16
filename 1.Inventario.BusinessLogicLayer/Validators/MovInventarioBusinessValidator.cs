using FluentValidation;
using Inventario.BusinessLogicLayer.DTOs;
using System;

namespace Inventario.BusinessLogicLayer.Validators
{
    /// <summary>
    /// Validador para MovInventarioBusinessDto usando FluentValidation
    /// </summary>
    public class MovInventarioBusinessValidator : AbstractValidator<MovInventarioBusinessDto>
    {
        public MovInventarioBusinessValidator()
        {
            // Validaciones para CodigoProducto
            RuleFor(x => x.CodigoProducto)
                .NotEmpty()
                .WithMessage("El código de producto es requerido")
                .MaximumLength(20)
                .WithMessage("El código de producto no puede exceder 20 caracteres")
                .Matches(@"^[A-Za-z0-9-_]+$")
                .WithMessage("El código de producto solo puede contener letras, números, guiones y guiones bajos");

            // Validaciones para CodigoAlmacen
            RuleFor(x => x.CodigoAlmacen)
                .NotEmpty()
                .WithMessage("El código de almacén es requerido")
                .MaximumLength(10)
                .WithMessage("El código de almacén no puede exceder 10 caracteres")
                .Matches(@"^[A-Za-z0-9-_]+$")
                .WithMessage("El código de almacén solo puede contener letras, números, guiones y guiones bajos");

            // Validaciones para FechaMovimiento
            RuleFor(x => x.FechaMovimiento)
                .NotEmpty()
                .WithMessage("La fecha de movimiento es requerida")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La fecha de movimiento no puede ser futura")
                .GreaterThan(new DateTime(2000, 1, 1))
                .WithMessage("La fecha de movimiento debe ser posterior al año 2000");

            // Validaciones para TipoMovimiento
            RuleFor(x => x.TipoMovimiento)
                .NotEmpty()
                .WithMessage("El tipo de movimiento es requerido");

            // Validaciones para Cantidad
            RuleFor(x => x.Cantidad)
                .Must(x => x == null || x > 0)
                .WithMessage("La cantidad debe ser mayor a 0")
                .Must(x => x == null || x <= 999999.99m)
                .WithMessage("La cantidad no puede exceder 999,999.99");



            // Validaciones para Observaciones (opcional)
            RuleFor(x => x.Observaciones)
                .MaximumLength(200)
                .WithMessage("Las observaciones no pueden exceder 200 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Observaciones));



            // Validación específica para movimientos de salida
            RuleFor(x => x.Cantidad)
                .LessThanOrEqualTo(10000)
                .WithMessage("Para movimientos de salida, la cantidad no puede exceder 10,000 unidades")
                .When(x => x.TipoMovimiento == "S");

            // Validación para movimientos de inventario (deben tener observaciones)
            RuleFor(x => x.Observaciones)
                .NotEmpty()
                .WithMessage("Los movimientos de inventario deben incluir observaciones")
                .When(x => x.TipoMovimiento == "I");
        }




    }
}