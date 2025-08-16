using Inventario.BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.BusinessLogicLayer.Services.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio para MovInventario
    /// </summary>
    public interface IMovInventarioService
    {
        /// <summary>
        /// Consulta movimientos de inventario con filtros y paginación
        /// </summary>
        /// <param name="searchDto">Criterios de búsqueda y paginación</param>
        /// <returns>Resultado paginado con movimientos de inventario</returns>
        Task<MovInventarioPagedResultBusinessDto> ConsultarMovInventarioAsync(MovInventarioSearchBusinessDto searchDto);

        /// <summary>
        /// Obtiene un movimiento de inventario específico por sus claves primarias
        /// </summary>
        /// <param name="codCia">Código de compañía</param>
        /// <param name="companiaVenta3">Compañía de venta</param>
        /// <param name="almacenVenta">Almacén de venta</param>
        /// <param name="tipoMovimiento">Tipo de movimiento</param>
        /// <param name="tipoDocumento">Tipo de documento</param>
        /// <param name="nroDocumento">Número de documento</param>
        /// <param name="codItem2">Código de item</param>
        /// <returns>Movimiento de inventario encontrado</returns>
        Task<MovInventarioResultBusinessDto> ObtenerMovInventarioPorIdAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2);

        /// <summary>
        /// Crea un nuevo movimiento de inventario
        /// </summary>
        /// <param name="movInventarioDto">Datos del movimiento a crear</param>
        /// <returns>Movimiento de inventario creado</returns>
        Task<MovInventarioResultBusinessDto> CrearMovInventarioAsync(MovInventarioBusinessDto movInventarioDto);

        /// <summary>
        /// Actualiza un movimiento de inventario existente
        /// </summary>
        /// <param name="codCia">Código de compañía (clave primaria)</param>
        /// <param name="companiaVenta3">Compañía de venta (clave primaria)</param>
        /// <param name="almacenVenta">Almacén de venta (clave primaria)</param>
        /// <param name="tipoMovimiento">Tipo de movimiento (clave primaria)</param>
        /// <param name="tipoDocumento">Tipo de documento (clave primaria)</param>
        /// <param name="nroDocumento">Número de documento (clave primaria)</param>
        /// <param name="codItem2">Código de item (clave primaria)</param>
        /// <param name="movInventarioDto">Nuevos datos del movimiento</param>
        /// <returns>Movimiento de inventario actualizado</returns>
        Task<MovInventarioResultBusinessDto> ActualizarMovInventarioAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2,
            MovInventarioBusinessDto movInventarioDto);

        /// <summary>
        /// Elimina un movimiento de inventario
        /// </summary>
        /// <param name="codCia">Código de compañía</param>
        /// <param name="companiaVenta3">Compañía de venta</param>
        /// <param name="almacenVenta">Almacén de venta</param>
        /// <param name="tipoMovimiento">Tipo de movimiento</param>
        /// <param name="tipoDocumento">Tipo de documento</param>
        /// <param name="nroDocumento">Número de documento</param>
        /// <param name="codItem2">Código de item</param>
        /// <returns>True si se eliminó correctamente</returns>
        Task<bool> EliminarMovInventarioAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2);

        /// <summary>
        /// Valida si un movimiento de inventario puede ser creado
        /// </summary>
        /// <param name="movInventarioDto">Datos del movimiento a validar</param>
        /// <returns>Lista de errores de validación (vacía si es válido)</returns>
        Task<List<string>> ValidarCreacionAsync(MovInventarioBusinessDto movInventarioDto);

        /// <summary>
        /// Valida si un movimiento de inventario puede ser actualizado
        /// </summary>
        /// <param name="codCia">Código de compañía</param>
        /// <param name="companiaVenta3">Compañía de venta</param>
        /// <param name="almacenVenta">Almacén de venta</param>
        /// <param name="tipoMovimiento">Tipo de movimiento</param>
        /// <param name="tipoDocumento">Tipo de documento</param>
        /// <param name="nroDocumento">Número de documento</param>
        /// <param name="codItem2">Código de ítem</param>
        /// <param name="movInventarioDto">Nuevos datos del movimiento</param>
        /// <returns>Lista de errores de validación (vacía si es válido)</returns>
        Task<List<string>> ValidarActualizacionAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2,
            MovInventarioBusinessDto movInventarioDto);

        /// <summary>
        /// Valida si un movimiento de inventario puede ser eliminado
        /// </summary>
        /// <param name="codCia">Código de compañía</param>
        /// <param name="companiaVenta3">Compañía de venta</param>
        /// <param name="almacenVenta">Almacén de venta</param>
        /// <param name="tipoMovimiento">Tipo de movimiento</param>
        /// <param name="tipoDocumento">Tipo de documento</param>
        /// <param name="nroDocumento">Número de documento</param>
        /// <param name="codItem2">Código de ítem</param>
        /// <returns>Lista de errores de validación (vacía si es válido)</returns>
        Task<List<string>> ValidarEliminacionAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2);

        /// <summary>
        /// Obtiene estadísticas de movimientos de inventario
        /// </summary>
        /// <param name="searchDto">Criterios de búsqueda para las estadísticas</param>
        /// <returns>Diccionario con estadísticas calculadas</returns>
        Task<Dictionary<string, object>> ObtenerEstadisticasAsync(MovInventarioSearchBusinessDto searchDto);

        /// <summary>
        /// Verifica si existe un movimiento de inventario con las claves especificadas
        /// </summary>
        /// <param name="codCia">Código de compañía</param>
        /// <param name="companiaVenta3">Compañía de venta</param>
        /// <param name="almacenVenta">Almacén de venta</param>
        /// <param name="tipoMovimiento">Tipo de movimiento</param>
        /// <param name="tipoDocumento">Tipo de documento</param>
        /// <param name="nroDocumento">Número de documento</param>
        /// <param name="codItem2">Código de item</param>
        /// <returns>True si existe el movimiento</returns>
        Task<bool> ExisteMovInventarioAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2);
    }
}