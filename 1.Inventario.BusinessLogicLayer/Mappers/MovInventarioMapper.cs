using Inventario.BusinessLogicLayer.DTOs;
using _2.Inventario.DataAccessLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventario.BusinessLogicLayer.Mappers
{
    /// <summary>
    /// Implementación del mapper para MovInventario
    /// </summary>
    public class MovInventarioMapper : IMovInventarioMapper
    {
        public MovInventarioDto ToDataDto(MovInventarioBusinessDto businessDto)
        {
            if (businessDto == null)
                throw new ArgumentNullException(nameof(businessDto));

            return new MovInventarioDto
            {
                CodItem2 = businessDto.CodigoProducto?.Trim(),
                AlmacenVenta = businessDto.CodigoAlmacen?.Trim(),
                FechaTransaccion = businessDto.FechaMovimiento,
                TipoMovimiento = businessDto.TipoMovimiento?.Trim().ToUpper(),
                Cantidad = (int?)businessDto.Cantidad,
                DocRef1 = string.IsNullOrWhiteSpace(businessDto.Observaciones) ? null : businessDto.Observaciones.Trim(),
                DocRef2 = string.IsNullOrWhiteSpace(businessDto.DocRef2) ? null : businessDto.DocRef2.Trim(),
                DocRef3 = string.IsNullOrWhiteSpace(businessDto.DocRef3) ? null : businessDto.DocRef3.Trim(),
                DocRef4 = string.IsNullOrWhiteSpace(businessDto.DocRef4) ? null : businessDto.DocRef4.Trim(),
                DocRef5 = string.IsNullOrWhiteSpace(businessDto.DocRef5) ? null : businessDto.DocRef5.Trim(),
                // Campos requeridos con valores por defecto
                CodCia = "001",
                CompaniaVenta3 = "001",
                TipoDocumento = "01",
                NroDocumento = Guid.NewGuid().ToString()[..20]
            };
        }

        public MovInventarioFilterDto ToDataFilterDto(MovInventarioSearchBusinessDto searchDto)
        {
            if (searchDto == null)
                throw new ArgumentNullException(nameof(searchDto));

            return new MovInventarioFilterDto
            {
                CodItem2 = string.IsNullOrWhiteSpace(searchDto.CodigoProducto) ? null : searchDto.CodigoProducto.Trim(),
                AlmacenVenta = string.IsNullOrWhiteSpace(searchDto.CodigoAlmacen) ? null : searchDto.CodigoAlmacen.Trim(),
                FechaInicio = searchDto.FechaDesde,
                FechaFin = searchDto.FechaHasta,
                TipoMovimiento = string.IsNullOrWhiteSpace(searchDto.TipoMovimiento) ? null : searchDto.TipoMovimiento.Trim().ToUpper(),
                CodCia = string.IsNullOrWhiteSpace(searchDto.CodCia) ? null : searchDto.CodCia.Trim(),
                CompaniaVenta3 = string.IsNullOrWhiteSpace(searchDto.CompaniaVenta3) ? null : searchDto.CompaniaVenta3.Trim(),
                TipoDocumento = string.IsNullOrWhiteSpace(searchDto.TipoDocumento) ? null : searchDto.TipoDocumento.Trim(),
                NroDocumento = string.IsNullOrWhiteSpace(searchDto.NroDocumento) ? null : searchDto.NroDocumento.Trim(),
                Proveedor = string.IsNullOrWhiteSpace(searchDto.Proveedor) ? null : searchDto.Proveedor.Trim(),
                AlmacenDestino = string.IsNullOrWhiteSpace(searchDto.AlmacenDestino) ? null : searchDto.AlmacenDestino.Trim(),
                DocRef1 = string.IsNullOrWhiteSpace(searchDto.DocRef1) ? null : searchDto.DocRef1.Trim(),
                DocRef2 = string.IsNullOrWhiteSpace(searchDto.DocRef2) ? null : searchDto.DocRef2.Trim(),
                DocRef3 = string.IsNullOrWhiteSpace(searchDto.DocRef3) ? null : searchDto.DocRef3.Trim(),
                DocRef4 = string.IsNullOrWhiteSpace(searchDto.DocRef4) ? null : searchDto.DocRef4.Trim(),
                DocRef5 = string.IsNullOrWhiteSpace(searchDto.DocRef5) ? null : searchDto.DocRef5.Trim()
            };
        }

        public MovInventarioBusinessDto ToBusinessDto(MovInventarioDto dataDto)
        {
            if (dataDto == null)
                throw new ArgumentNullException(nameof(dataDto));

            return new MovInventarioBusinessDto
            {
                CodigoProducto = dataDto.CodItem2 ?? string.Empty,
                CodigoAlmacen = dataDto.AlmacenVenta ?? string.Empty,
                FechaMovimiento = dataDto.FechaTransaccion ?? DateTime.MinValue,
                TipoMovimiento = dataDto.TipoMovimiento ?? string.Empty,
                Cantidad = dataDto.Cantidad ?? 0,
                Observaciones = dataDto.DocRef1, // Usamos DocRef1 como observaciones
                DocRef2 = dataDto.DocRef2,
                DocRef3 = dataDto.DocRef3,
                DocRef4 = dataDto.DocRef4,
                DocRef5 = dataDto.DocRef5,
                Proveedor = dataDto.Proveedor,
                AlmacenDestino = dataDto.AlmacenDestino
            };
        }

        public MovInventarioResultBusinessDto ToResultBusinessDto(MovInventarioDto dataDto)
        {
            if (dataDto == null)
                throw new ArgumentNullException(nameof(dataDto));

            return new MovInventarioResultBusinessDto
            {
                // Campos de la clave primaria
                CodCia = dataDto.CodCia ?? string.Empty,
                CompaniaVenta3 = dataDto.CompaniaVenta3 ?? string.Empty,
                CodigoAlmacen = dataDto.AlmacenVenta ?? string.Empty,
                TipoMovimiento = dataDto.TipoMovimiento ?? string.Empty,
                TipoDocumento = dataDto.TipoDocumento ?? string.Empty,
                NroDocumento = dataDto.NroDocumento ?? string.Empty,
                CodigoProducto = dataDto.CodItem2 ?? string.Empty,
                // Campos opcionales
                Proveedor = dataDto.Proveedor,
                AlmacenDestino = dataDto.AlmacenDestino,
                Cantidad = dataDto.Cantidad,
                FechaMovimiento = dataDto.FechaTransaccion,
                Observaciones = dataDto.DocRef1,
                DocRef2 = dataDto.DocRef2,
                DocRef3 = dataDto.DocRef3,
                DocRef4 = dataDto.DocRef4,
                DocRef5 = dataDto.DocRef5
            };
        }

        public List<MovInventarioResultBusinessDto> ToResultBusinessDtoList(List<MovInventarioDto> dataDtoList)
        {
            if (dataDtoList == null)
                return new List<MovInventarioResultBusinessDto>();

            return dataDtoList.Select(ToResultBusinessDto).ToList();
        }

        public List<MovInventarioDto> ToDataDtoList(List<MovInventarioBusinessDto> businessDtoList)
        {
            if (businessDtoList == null)
                return new List<MovInventarioDto>();

            return businessDtoList.Select(ToDataDto).ToList();
        }

        public MovInventarioPagedResultBusinessDto ToPagedResultBusinessDto(
            List<MovInventarioDto> dataDtoList, 
            int totalRegistros, 
            int paginaActual, 
            int tamanoPagina)
        {
            var items = ToResultBusinessDtoList(dataDtoList);

            return new MovInventarioPagedResultBusinessDto
            {
                Items = items,
                TotalRegistros = totalRegistros,
                PaginaActual = paginaActual,
                TamanoPagina = tamanoPagina
            };
        }

        public MovInventarioDto ToDataDtoForUpdate(MovInventarioBusinessDto businessDto, MovInventarioDto existingDataDto)
        {
            if (businessDto == null)
                throw new ArgumentNullException(nameof(businessDto));
            if (existingDataDto == null)
                throw new ArgumentNullException(nameof(existingDataDto));

            // Para actualización, preservamos las claves primarias del registro existente
            // y solo actualizamos los campos modificables
            return new MovInventarioDto
            {
                CodCia = existingDataDto.CodCia, // Clave primaria - no se modifica
                CompaniaVenta3 = existingDataDto.CompaniaVenta3, // Clave primaria - no se modifica
                AlmacenVenta = existingDataDto.AlmacenVenta, // Clave primaria - no se modifica
                TipoDocumento = existingDataDto.TipoDocumento, // Clave primaria - no se modifica
                NroDocumento = existingDataDto.NroDocumento, // Clave primaria - no se modifica
                CodItem2 = existingDataDto.CodItem2, // Clave primaria - no se modifica
                TipoMovimiento = businessDto.TipoMovimiento?.Trim().ToUpper(),
                Cantidad = (int?)businessDto.Cantidad,
                FechaTransaccion = businessDto.FechaMovimiento,
                DocRef1 = string.IsNullOrWhiteSpace(businessDto.Observaciones) ? null : businessDto.Observaciones.Trim(),
                Proveedor = string.IsNullOrWhiteSpace(businessDto.Proveedor) ? null : businessDto.Proveedor.Trim(),
                AlmacenDestino = string.IsNullOrWhiteSpace(businessDto.AlmacenDestino) ? null : businessDto.AlmacenDestino.Trim(),
                DocRef2 = string.IsNullOrWhiteSpace(businessDto.DocRef2) ? null : businessDto.DocRef2.Trim(),
                DocRef3 = string.IsNullOrWhiteSpace(businessDto.DocRef3) ? null : businessDto.DocRef3.Trim(),
                DocRef4 = string.IsNullOrWhiteSpace(businessDto.DocRef4) ? null : businessDto.DocRef4.Trim(),
                DocRef5 = string.IsNullOrWhiteSpace(businessDto.DocRef5) ? null : businessDto.DocRef5.Trim()
            };
        }

        public MovInventarioDto ToDataDtoForInsert(MovInventarioBusinessDto businessDto)
        {
            if (businessDto == null)
                throw new ArgumentNullException(nameof(businessDto));

            // Para inserción, validamos que todos los campos requeridos estén presentes
            if (string.IsNullOrWhiteSpace(businessDto.CodigoProducto))
                throw new ArgumentException("El código de producto es requerido para inserción", nameof(businessDto));
            
            if (string.IsNullOrWhiteSpace(businessDto.CodigoAlmacen))
                throw new ArgumentException("El código de almacén es requerido para inserción", nameof(businessDto));
            
            if (string.IsNullOrWhiteSpace(businessDto.TipoMovimiento))
                throw new ArgumentException("El tipo de movimiento es requerido para inserción", nameof(businessDto));

            return new MovInventarioDto
            {
                // Campos obligatorios (NOT NULL)
                CodCia = businessDto.CodCia.Trim(),
                CompaniaVenta3 = businessDto.CompaniaVenta3.Trim(),
                AlmacenVenta = businessDto.CodigoAlmacen.Trim(),
                TipoMovimiento = businessDto.TipoMovimiento.Trim().ToUpper(),
                TipoDocumento = businessDto.TipoDocumento.Trim(),
                NroDocumento = businessDto.NroDocumento.Trim(),
                CodItem2 = businessDto.CodigoProducto.Trim(),
                // Campos opcionales
                Proveedor = string.IsNullOrWhiteSpace(businessDto.Proveedor) ? null : businessDto.Proveedor.Trim(),
                AlmacenDestino = string.IsNullOrWhiteSpace(businessDto.AlmacenDestino) ? null : businessDto.AlmacenDestino.Trim(),
                Cantidad = businessDto.Cantidad,
                DocRef1 = string.IsNullOrWhiteSpace(businessDto.Observaciones) ? null : businessDto.Observaciones.Trim(),
                DocRef2 = string.IsNullOrWhiteSpace(businessDto.DocRef2) ? null : businessDto.DocRef2.Trim(),
                DocRef3 = string.IsNullOrWhiteSpace(businessDto.DocRef3) ? null : businessDto.DocRef3.Trim(),
                DocRef4 = string.IsNullOrWhiteSpace(businessDto.DocRef4) ? null : businessDto.DocRef4.Trim(),
                DocRef5 = string.IsNullOrWhiteSpace(businessDto.DocRef5) ? null : businessDto.DocRef5.Trim(),
                FechaTransaccion = businessDto.FechaMovimiento
            };
        }
    }
}