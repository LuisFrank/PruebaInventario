using System;
using System.Collections.Generic;

namespace Inventario.BusinessLogicLayer.DTOs
{
    /// <summary>
    /// DTO de resultado de negocio para operaciones que devuelven información enriquecida
    /// </summary>
    public class MovInventarioResultBusinessDto
    {
        // Campos de la clave primaria
        public string CodCia { get; set; } = string.Empty;
        public string CompaniaVenta3 { get; set; } = string.Empty;
        public string CodigoAlmacen { get; set; } = string.Empty;
        public string TipoMovimiento { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NroDocumento { get; set; } = string.Empty;
        public string CodigoProducto { get; set; } = string.Empty;
        
        // Campos opcionales
        public string? Proveedor { get; set; }
        public string? AlmacenDestino { get; set; }
        public decimal? Cantidad { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public string? Observaciones { get; set; }
        public string? DocRef2 { get; set; }
        public string? DocRef3 { get; set; }
        public string? DocRef4 { get; set; }
        public string? DocRef5 { get; set; }

        // Propiedades calculadas y enriquecidas
        
        public string TipoMovimientoDescripcion => TipoMovimiento switch
        {
            "E" => "Entrada",
            "I" => "Inventario", 
            "S" => "Salida",
            _ => "Desconocido"
        };

        public string FechaMovimientoFormateada => FechaMovimiento?.ToString("dd/MM/yyyy HH:mm") ?? "Sin fecha";
        
        public string EstadoMovimiento => "Activo";
        
        public string CantidadFormateada => Cantidad?.ToString("N2") ?? "0.00";

        // Información adicional de contexto
        public DateTime FechaConsulta { get; set; } = DateTime.Now;
        
        public int DiasDesdeMovimiento => FechaMovimiento.HasValue ? (DateTime.Now - FechaMovimiento.Value).Days : 0;
        
        public string CategoriaTiempo
        {
            get
            {
                return DiasDesdeMovimiento switch
                {
                    <= 1 => "Hoy",
                    <= 7 => "Esta semana",
                    <= 30 => "Este mes",
                    <= 90 => "Últimos 3 meses",
                    <= 365 => "Este año",
                    _ => "Más de un año"
                };
            }
        }

        // Métodos de utilidad
        public Dictionary<string, object> ToSummary()
        {
            return new Dictionary<string, object>
            {
                { "CodigoProducto", CodigoProducto },
                { "CodigoAlmacen", CodigoAlmacen },
                { "FechaMovimiento", FechaMovimientoFormateada },
                { "TipoMovimiento", TipoMovimientoDescripcion },
                { "Cantidad", CantidadFormateada },
                { "Estado", EstadoMovimiento },
                { "Categoria", CategoriaTiempo }
            };
        }
    }

    /// <summary>
    /// DTO para resultados paginados de MovInventario
    /// </summary>
    public class MovInventarioPagedResultBusinessDto
    {
        public List<MovInventarioResultBusinessDto> Items { get; set; } = new List<MovInventarioResultBusinessDto>();
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / TamanoPagina);
        public bool TienePaginaAnterior => PaginaActual > 1;
        public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
        
        // Estadísticas del resultado
        public decimal TotalCantidad => Items.Sum(x => x.Cantidad ?? 0);
        
        public Dictionary<string, int> ResumenPorTipo => Items
            .GroupBy(x => x.TipoMovimientoDescripcion)
            .ToDictionary(g => g.Key, g => g.Count());
            

    }
}