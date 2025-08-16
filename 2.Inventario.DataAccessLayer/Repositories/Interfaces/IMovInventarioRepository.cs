using _2.Inventario.DataAccessLayer.Models;
using _2.Inventario.DataAccessLayer.Models.DTOs;

namespace _2.Inventario.DataAccessLayer.Repositories.Interfaces
{
    public interface IMovInventarioRepository
    {
        /// <summary>
        /// Consulta movimientos de inventario con filtros opcionales
        /// </summary>
        /// <param name="filtros">Filtros de búsqueda opcionales</param>
        /// <returns>Lista de movimientos de inventario</returns>
        Task<IEnumerable<MovInventarioDto>> ConsultarMovInventarioAsync(MovInventarioFilterDto filtros);

        /// <summary>
        /// Inserta un nuevo movimiento de inventario
        /// </summary>
        /// <param name="movInventario">Datos del movimiento a insertar</param>
        /// <returns>True si se insertó correctamente</returns>
        Task<bool> InsertarMovInventarioAsync(MovInventarioDto movInventario);

        /// <summary>
        /// Actualiza un movimiento de inventario existente
        /// </summary>
        /// <param name="movInventario">Datos del movimiento a actualizar</param>
        /// <returns>True si se actualizó correctamente</returns>
        Task<bool> ActualizarMovInventarioAsync(MovInventarioDto movInventario);

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
        Task<bool> EliminarMovInventarioAsync(string codCia, string companiaVenta3, string almacenVenta, 
            string tipoMovimiento, string tipoDocumento, string nroDocumento, string codItem2);

        /// <summary>
        /// Obtiene un movimiento de inventario por su clave primaria
        /// </summary>
        /// <param name="codCia">Código de compañía</param>
        /// <param name="companiaVenta3">Compañía de venta</param>
        /// <param name="almacenVenta">Almacén de venta</param>
        /// <param name="tipoMovimiento">Tipo de movimiento</param>
        /// <param name="tipoDocumento">Tipo de documento</param>
        /// <param name="nroDocumento">Número de documento</param>
        /// <param name="codItem2">Código de item</param>
        /// <returns>Movimiento de inventario o null si no existe</returns>
        Task<MovInventarioDto?> ObtenerMovInventarioPorIdAsync(string codCia, string companiaVenta3, string almacenVenta, 
            string tipoMovimiento, string tipoDocumento, string nroDocumento, string codItem2);
    }
}