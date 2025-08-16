using _2.Inventario.DataAccessLayer.Repositories.Interfaces;
using FluentValidation;
using Inventario.BusinessLogicLayer.DTOs;
using Inventario.BusinessLogicLayer.Exceptions;
using Inventario.BusinessLogicLayer.Mappers;
using Inventario.BusinessLogicLayer.Services.Interfaces;
using Inventario.BusinessLogicLayer.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Inventario.BusinessLogicLayer.Services
{
    /// <summary>
    /// Implementación del servicio de negocio para MovInventario
    /// </summary>
    public class MovInventarioService : IMovInventarioService
    {
        private readonly IMovInventarioRepository _repository;
        private readonly IMovInventarioMapper _mapper;
        private readonly IValidator<MovInventarioBusinessDto> _businessValidator;
        private readonly IValidator<MovInventarioSearchBusinessDto> _searchValidator;
        private readonly ILogger<MovInventarioService> _logger;

        public MovInventarioService(
            IMovInventarioRepository repository,
            IMovInventarioMapper mapper,
            IValidator<MovInventarioBusinessDto> businessValidator,
            IValidator<MovInventarioSearchBusinessDto> searchValidator,
            ILogger<MovInventarioService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _businessValidator = businessValidator ?? throw new ArgumentNullException(nameof(businessValidator));
            _searchValidator = searchValidator ?? throw new ArgumentNullException(nameof(searchValidator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<MovInventarioPagedResultBusinessDto> ConsultarMovInventarioAsync(MovInventarioSearchBusinessDto searchDto)
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de movimientos de inventario");

                // Validar criterios de búsqueda
                await ValidarSearchDtoAsync(searchDto);

                // Mapear a DTO de datos
                var filterDto = _mapper.ToDataFilterDto(searchDto);

                // Consultar en repositorio
                var resultados = await _repository.ConsultarMovInventarioAsync(filterDto);

                // Calcular total de registros para paginación
                var totalRegistros = resultados.Count();

                // Aplicar paginación en memoria (idealmente esto debería hacerse en la base de datos)
                var registrosASaltar = searchDto.RegistrosASaltar;
                var resultadosPaginados = resultados
                    .Skip(registrosASaltar)
                    .Take(searchDto.TamanoPagina)
                    .ToList();

                // Mapear resultado
                var resultado = _mapper.ToPagedResultBusinessDto(
                    resultadosPaginados, 
                    totalRegistros, 
                    searchDto.Pagina, 
                    searchDto.TamanoPagina);

                _logger.LogInformation($"Consulta completada. Total registros: {totalRegistros}, Página: {searchDto.Pagina}");

                return resultado;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar movimientos de inventario");
                throw new BusinessException("Error al consultar movimientos de inventario", "CONSULTA_ERROR", ex);
            }
        }

        public async Task<MovInventarioResultBusinessDto> ObtenerMovInventarioPorIdAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2)
        {
            try
            {
                _logger.LogInformation($"Obteniendo movimiento: {codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");

                ValidarParametrosId(codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);

                var resultado = await _repository.ObtenerMovInventarioPorIdAsync(codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);

                if (resultado == null)
                {
                    throw new NotFoundException("MovInventario", $"{codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");
                }

                var resultadoMapeado = _mapper.ToResultBusinessDto(resultado);

                _logger.LogInformation($"Movimiento encontrado exitosamente");

                return resultadoMapeado;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener movimiento: {codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");
                throw new BusinessException("Error al obtener movimiento de inventario", "OBTENER_ERROR", ex);
            }
        }

        public async Task<MovInventarioResultBusinessDto> CrearMovInventarioAsync(MovInventarioBusinessDto movInventarioDto)
        {
            try
            {
                _logger.LogInformation($"Creando movimiento: {movInventarioDto.CodigoProducto}-{movInventarioDto.CodigoAlmacen}");

                // Validar datos de entrada
                await ValidarBusinessDtoAsync(movInventarioDto);

                // Validaciones específicas de creación
                var erroresCreacion = await ValidarCreacionAsync(movInventarioDto);
                if (erroresCreacion.Any())
                {
                    throw new ValidationException(string.Join("; ", erroresCreacion));
                }

                // Verificar que no exista el movimiento
                // Nota: Para verificar duplicados en creación, necesitaríamos los campos completos de la llave primaria
                // Por ahora, omitimos esta validación ya que el DTO de negocio no contiene todos los campos de la llave primaria

                // Mapear y crear
                var dataDto = _mapper.ToDataDtoForInsert(movInventarioDto);
                var insertado = await _repository.InsertarMovInventarioAsync(dataDto);

                if (!insertado)
                {
                    throw new BusinessException("No se pudo crear el movimiento de inventario", "CREAR_ERROR");
                }

                // Obtener el movimiento recién creado para devolverlo
                // Nota: Esto requiere que el DTO de negocio contenga los campos de la llave primaria
                // Por ahora, devolvemos un resultado básico
                var resultadoMapeado = _mapper.ToResultBusinessDto(dataDto);

                _logger.LogInformation($"Movimiento creado exitosamente");

                return resultadoMapeado;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear movimiento: {movInventarioDto.CodigoProducto}-{movInventarioDto.CodigoAlmacen}. Detalles: {ex.Message}");
                throw new BusinessException($"Error al crear movimiento de inventario: {ex.Message}", "CREAR_ERROR", ex);
            }
        }

        public async Task<MovInventarioResultBusinessDto> ActualizarMovInventarioAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2,
            MovInventarioBusinessDto movInventarioDto)
        {
            try
            {
                _logger.LogInformation($"Actualizando movimiento: {codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");

                ValidarParametrosId(codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);
                await ValidarBusinessDtoAsync(movInventarioDto);

                // Validaciones específicas de actualización
                var erroresActualizacion = await ValidarActualizacionAsync(
                    codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2, movInventarioDto);
                if (erroresActualizacion.Any())
                {
                    throw new ValidationException(string.Join("; ", erroresActualizacion));
                }

                // Verificar que existe el movimiento
                var movimientoExistente = await _repository.ObtenerMovInventarioPorIdAsync(
                    codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);

                if (movimientoExistente == null)
                {
                    throw new NotFoundException("MovInventario", $"{codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");
                }

                // Mapear para actualización
                var dataDto = _mapper.ToDataDtoForUpdate(movInventarioDto, movimientoExistente);
                var actualizado = await _repository.ActualizarMovInventarioAsync(dataDto);

                if (!actualizado)
                {
                    throw new BusinessException("No se pudo actualizar el movimiento de inventario", "ACTUALIZAR_ERROR");
                }

                // Obtener el movimiento actualizado
                var movimientoActualizado = await _repository.ObtenerMovInventarioPorIdAsync(
                    codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);
                
                var resultadoMapeado = _mapper.ToResultBusinessDto(movimientoActualizado);

                _logger.LogInformation($"Movimiento actualizado exitosamente");

                return resultadoMapeado;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar movimiento: {codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");
                throw new BusinessException("Error al actualizar movimiento de inventario", "ACTUALIZAR_ERROR", ex);
            }
        }

        public async Task<bool> EliminarMovInventarioAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2)
        {
            try
            {
                _logger.LogInformation($"Eliminando movimiento: {codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");

                ValidarParametrosId(codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);

                // Validaciones específicas de eliminación
                var erroresEliminacion = await ValidarEliminacionAsync(codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);
                if (erroresEliminacion.Any())
                {
                    throw new ValidationException(string.Join("; ", erroresEliminacion));
                }

                // Verificar que existe el movimiento antes de eliminar
                var movimientoExistente = await _repository.ObtenerMovInventarioPorIdAsync(
                    codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);
                
                if (movimientoExistente == null)
                {
                    throw new NotFoundException("MovInventario", $"{codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");
                }
                
                var resultado = await _repository.EliminarMovInventarioAsync(
                    codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);

                _logger.LogInformation($"Movimiento eliminado exitosamente");

                return resultado;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar movimiento: {codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");
                throw new BusinessException("Error al eliminar movimiento de inventario", "ELIMINAR_ERROR", ex);
            }
        }

        public async Task<List<string>> ValidarCreacionAsync(MovInventarioBusinessDto movInventarioDto)
        {
            var errores = new List<string>();

            // Validaciones específicas de creación
            if (movInventarioDto.FechaMovimiento > DateTime.Now)
            {
                errores.Add("La fecha de movimiento no puede ser futura");
            }

            if (movInventarioDto.TipoMovimiento == "S" && movInventarioDto.Cantidad > 10000)
            {
                errores.Add("Para movimientos de salida, la cantidad no puede exceder 10,000 unidades");
            }

            if (movInventarioDto.TipoMovimiento == "I" && string.IsNullOrWhiteSpace(movInventarioDto.Observaciones))
            {
                errores.Add("Los movimientos de inventario deben incluir observaciones");
            }

            return await Task.FromResult(errores);
        }

        public async Task<List<string>> ValidarActualizacionAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2,
            MovInventarioBusinessDto movInventarioDto)
        {
            var errores = new List<string>();

            // Validaciones específicas de actualización
            // Nota: Las validaciones de claves primarias se omiten ya que el DTO de negocio
            // no contiene todos los campos de la llave primaria compuesta
            
            // Validaciones de negocio
            // (Regla de 30 días eliminada según solicitud)

            return await Task.FromResult(errores);
        }

        public async Task<List<string>> ValidarEliminacionAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2)
        {
            var errores = new List<string>();

            // Validaciones específicas de eliminación
            // Nota: Para validar antigüedad necesitaríamos obtener la fecha del movimiento
            // desde la base de datos usando los parámetros de la llave primaria
            
            return await Task.FromResult(errores);
        }

        public async Task<Dictionary<string, object>> ObtenerEstadisticasAsync(MovInventarioSearchBusinessDto searchDto)
        {
            try
            {
                await ValidarSearchDtoAsync(searchDto);

                var filterDto = _mapper.ToDataFilterDto(searchDto);
                var movimientos = await _repository.ConsultarMovInventarioAsync(filterDto);

                var estadisticas = new Dictionary<string, object>
                {
                    { "TotalMovimientos", movimientos.Count() },
                    { "TotalCantidad", movimientos.Sum(m => m.Cantidad ?? 0) },
                    { "PromedioCantidad", movimientos.Any() ? movimientos.Average(m => (double)(m.Cantidad ?? 0)) : 0 },
                    { "MovimientosPorTipo", movimientos.GroupBy(m => m.TipoMovimiento).ToDictionary(g => g.Key, g => (object)g.Count()) },
                    { "CantidadPorTipo", movimientos.GroupBy(m => m.TipoMovimiento).ToDictionary(g => g.Key, g => (object)g.Sum(m => m.Cantidad ?? 0)) }
                };

                return estadisticas;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas");
                throw new BusinessException("Error al obtener estadísticas", "ESTADISTICAS_ERROR", ex);
            }
        }

        public async Task<bool> ExisteMovInventarioAsync(
            string codCia,
            string companiaVenta3,
            string almacenVenta,
            string tipoMovimiento,
            string tipoDocumento,
            string nroDocumento,
            string codItem2)
        {
            try
            {
                var movimiento = await _repository.ObtenerMovInventarioPorIdAsync(
                    codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);
                return movimiento != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al verificar existencia: {codCia}-{companiaVenta3}-{almacenVenta}-{tipoMovimiento}-{tipoDocumento}-{nroDocumento}-{codItem2}");
                return false;
            }
        }

        #region Métodos Privados

        private async Task ValidarBusinessDtoAsync(MovInventarioBusinessDto dto)
        {
            var validationResult = await _businessValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errores = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(string.Join("; ", errores));
            }
        }

        private async Task ValidarSearchDtoAsync(MovInventarioSearchBusinessDto dto)
        {
            var validationResult = await _searchValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errores = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(string.Join("; ", errores));
            }
        }

        private static void ValidarParametrosId(string codCia, string companiaVenta3, string almacenVenta, string tipoMovimiento, string tipoDocumento, string nroDocumento, string codItem2)
        {
            if (string.IsNullOrWhiteSpace(codCia))
                throw new ArgumentException("El código de compañía es requerido", nameof(codCia));

            if (string.IsNullOrWhiteSpace(companiaVenta3))
                throw new ArgumentException("La compañía de venta es requerida", nameof(companiaVenta3));

            if (string.IsNullOrWhiteSpace(almacenVenta))
                throw new ArgumentException("El almacén de venta es requerido", nameof(almacenVenta));

            if (string.IsNullOrWhiteSpace(tipoMovimiento))
                throw new ArgumentException("El tipo de movimiento es requerido", nameof(tipoMovimiento));

            if (string.IsNullOrWhiteSpace(tipoDocumento))
                throw new ArgumentException("El tipo de documento es requerido", nameof(tipoDocumento));

            if (string.IsNullOrWhiteSpace(nroDocumento))
                throw new ArgumentException("El número de documento es requerido", nameof(nroDocumento));

            if (string.IsNullOrWhiteSpace(codItem2))
                throw new ArgumentException("El código de ítem es requerido", nameof(codItem2));
        }



        #endregion
    }
}