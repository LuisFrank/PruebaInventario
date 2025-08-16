using Microsoft.AspNetCore.Mvc;
using Inventario.BusinessLogicLayer.Services.Interfaces;
using Inventario.BusinessLogicLayer.DTOs;
using Inventario.BusinessLogicLayer.Exceptions;
using InventarioWeb.ViewModels;

namespace InventarioWeb.Controllers
{
    public class MovInventarioController : Controller
    {
        private readonly IMovInventarioService _movInventarioService;
        private readonly ILogger<MovInventarioController> _logger;

        public MovInventarioController(
            IMovInventarioService movInventarioService,
            ILogger<MovInventarioController> logger)
        {
            _movInventarioService = movInventarioService;
            _logger = logger;
        }

        // GET: MovInventario
        public async Task<IActionResult> Index(MovInventarioSearchViewModel? searchModel = null)
        {
            try
            {
                var searchDto = new MovInventarioSearchBusinessDto();
                
                if (searchModel != null)
                {
                    searchDto.FechaDesde = searchModel.FechaInicio;
                    searchDto.FechaHasta = searchModel.FechaFin;
                    searchDto.TipoMovimiento = searchModel.TipoMovimiento;
                    searchDto.CodigoProducto = searchModel.CodItem2;
                    searchDto.CodigoAlmacen = searchModel.AlmacenVenta;
                    searchDto.CodCia = searchModel.CodCia;
                    searchDto.CompaniaVenta3 = searchModel.CompaniaVenta3;
                    searchDto.TipoDocumento = searchModel.TipoDocumento;
                    searchDto.NroDocumento = searchModel.NroDocumento;
                    searchDto.Proveedor = searchModel.Proveedor;
                    searchDto.AlmacenDestino = searchModel.AlmacenDestino;
                    searchDto.DocRef1 = searchModel.DocRef1;
                    searchDto.DocRef2 = searchModel.DocRef2;
                    searchDto.DocRef3 = searchModel.DocRef3;
                    searchDto.DocRef4 = searchModel.DocRef4;
                    searchDto.DocRef5 = searchModel.DocRef5;
                }

                var result = await _movInventarioService.ConsultarMovInventarioAsync(searchDto);
                
                var viewModel = new MovInventarioIndexViewModel
                {
                    SearchModel = searchModel ?? new MovInventarioSearchViewModel(),
                    MovInventarios = result.Items?.Select(dto => new MovInventarioViewModel
                    {
                        CodCia = dto.CodCia,
                        CompaniaVenta3 = dto.CompaniaVenta3,
                        AlmacenVenta = dto.CodigoAlmacen,
                        TipoMovimiento = dto.TipoMovimiento,
                        TipoDocumento = dto.TipoDocumento,
                        NroDocumento = dto.NroDocumento,
                        CodItem2 = dto.CodigoProducto,
                        Cantidad = (int)(dto.Cantidad ?? 0),
                        FechaTransaccion = dto.FechaMovimiento,
                        DocRef1 = dto.Observaciones,
                        DocRef2 = dto.DocRef2,
                        DocRef3 = dto.DocRef3,
                        DocRef4 = dto.DocRef4,
                        DocRef5 = dto.DocRef5,
                        Proveedor = dto.Proveedor,
                        AlmacenDestino = dto.AlmacenDestino
                    }).ToList() ?? new List<MovInventarioViewModel>()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar movimientos de inventario");
                TempData["Error"] = "Error al cargar los movimientos de inventario";
                return View(new MovInventarioIndexViewModel
                {
                    SearchModel = new MovInventarioSearchViewModel(),
                    MovInventarios = new List<MovInventarioViewModel>()
                });
            }
        }

        // GET: MovInventario/Create
        public IActionResult Create()
        {
            return View(new MovInventarioViewModel());
        }

        // POST: MovInventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovInventarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new MovInventarioBusinessDto
                {
                    // Campos obligatorios
                    CodCia = model.CodCia,
                    CompaniaVenta3 = model.CompaniaVenta3,
                    CodigoAlmacen = model.AlmacenVenta,
                    TipoMovimiento = model.TipoMovimiento,
                    TipoDocumento = model.TipoDocumento,
                    NroDocumento = model.NroDocumento,
                    CodigoProducto = model.CodItem2,
                    // Campos opcionales
                    Proveedor = model.Proveedor,
                    AlmacenDestino = model.AlmacenDestino,
                    Cantidad = model.Cantidad,
                    Observaciones = model.DocRef1,
                    DocRef2 = model.DocRef2,
                    DocRef3 = model.DocRef3,
                    DocRef4 = model.DocRef4,
                    DocRef5 = model.DocRef5,
                    FechaMovimiento = model.FechaTransaccion
                };

                var result = await _movInventarioService.CrearMovInventarioAsync(dto);
                
                if (result != null)
                {
                    TempData["Success"] = "Movimiento de inventario creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Error al crear el movimiento");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear movimiento de inventario");
                ModelState.AddModelError("", $"Error al crear movimiento de inventario: {ex.Message}");
                return View(model);
            }
        }

        // GET: MovInventario/Edit
        public async Task<IActionResult> Edit(string codCia, string companiaVenta3, string almacenVenta, 
            string tipoMovimiento, string tipoDocumento, string nroDocumento, string codItem2)
        {
            try
            {
                var result = await _movInventarioService.ObtenerMovInventarioPorIdAsync(
                    codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);
                
                if (result == null)
                {
                    TempData["Error"] = "Movimiento de inventario no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                var viewModel = new MovInventarioViewModel
                {
                    CodItem2 = result.CodigoProducto,
                    AlmacenVenta = result.CodigoAlmacen,
                    TipoMovimiento = result.TipoMovimiento,
                    Cantidad = (int)(result.Cantidad ?? 0),
                    FechaTransaccion = result.FechaMovimiento,
                    DocRef1 = result.Observaciones,
                    DocRef2 = result.DocRef2,
                    DocRef3 = result.DocRef3,
                    DocRef4 = result.DocRef4,
                    DocRef5 = result.DocRef5,
                    Proveedor = result.Proveedor,
                    AlmacenDestino = result.AlmacenDestino,
                    // Usar los valores reales del registro
                    CodCia = codCia,
                    CompaniaVenta3 = companiaVenta3,
                    TipoDocumento = tipoDocumento,
                    NroDocumento = nroDocumento
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener movimiento de inventario para edición");
                TempData["Error"] = "Error al cargar el movimiento de inventario";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: MovInventario/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovInventarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var businessDto = new MovInventarioBusinessDto
                {
                    CodigoProducto = model.CodItem2,
                    CodigoAlmacen = model.AlmacenVenta,
                    TipoMovimiento = model.TipoMovimiento,
                    Cantidad = model.Cantidad ?? 0,
                    FechaMovimiento = model.FechaTransaccion ?? DateTime.Now,
                    Observaciones = model.DocRef1,
                    DocRef2 = model.DocRef2,
                    DocRef3 = model.DocRef3,
                    DocRef4 = model.DocRef4,
                    DocRef5 = model.DocRef5,
                    Proveedor = model.Proveedor,
                    AlmacenDestino = model.AlmacenDestino
                };

                var result = await _movInventarioService.ActualizarMovInventarioAsync(
                    model.CodCia, model.CompaniaVenta3, model.AlmacenVenta, 
                    model.TipoMovimiento, model.TipoDocumento, model.NroDocumento, 
                    model.CodItem2, businessDto);
                
                if (result != null)
                {
                    TempData["Success"] = "Movimiento de inventario actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Error al actualizar el movimiento");
                    return View(model);
                }
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Movimiento de inventario no encontrado para actualización");
                TempData["Error"] = "No se encontró el movimiento de inventario a actualizar";
                return RedirectToAction(nameof(Index));
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Error de validación al actualizar movimiento de inventario");
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar movimiento de inventario");
                ModelState.AddModelError("", "Error interno del servidor");
                return View(model);
            }
        }

        // POST: MovInventario/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string codCia, string companiaVenta3, string almacenVenta, 
            string tipoMovimiento, string tipoDocumento, string nroDocumento, string codItem2)
        {
            try
            {
                var result = await _movInventarioService.EliminarMovInventarioAsync(
                    codCia, companiaVenta3, almacenVenta, tipoMovimiento, tipoDocumento, nroDocumento, codItem2);
                
                if (result)
                {
                    TempData["Success"] = "Movimiento de inventario eliminado exitosamente";
                }
                else
                {
                    TempData["Error"] = "No se pudo eliminar el movimiento de inventario";
                }
            }
            catch (Inventario.BusinessLogicLayer.Exceptions.NotFoundException ex)
            {
                _logger.LogWarning(ex, "Movimiento no encontrado para eliminación");
                TempData["Error"] = "El movimiento de inventario no existe";
            }
            catch (Inventario.BusinessLogicLayer.Exceptions.ValidationException ex)
            {
                _logger.LogWarning(ex, "Error de validación al eliminar movimiento");
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al eliminar movimiento de inventario");
                TempData["Error"] = "Error interno del servidor";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}