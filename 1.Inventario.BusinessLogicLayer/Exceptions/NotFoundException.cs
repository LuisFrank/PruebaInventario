using System;

namespace Inventario.BusinessLogicLayer.Exceptions
{
    /// <summary>
    /// Excepción específica para recursos no encontrados
    /// </summary>
    public class NotFoundException : BusinessException
    {
        public string? ResourceType { get; }
        public object? ResourceId { get; }

        public NotFoundException(string message) : base(message, "NOT_FOUND")
        {
        }

        public NotFoundException(string resourceType, object resourceId) 
            : base($"No se encontró el recurso '{resourceType}' con ID '{resourceId}'", "NOT_FOUND")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }

        public NotFoundException(string message, string resourceType, object resourceId) 
            : base(message, "NOT_FOUND")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }

        public NotFoundException(string message, Exception innerException) 
            : base(message, "NOT_FOUND", innerException)
        {
        }

        public NotFoundException(string resourceType, object resourceId, Exception innerException) 
            : base($"No se encontró el recurso '{resourceType}' con ID '{resourceId}'", "NOT_FOUND", innerException)
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }

        // Métodos estáticos de conveniencia para casos comunes
        public static NotFoundException ForMovInventario(string codigoProducto, string codigoAlmacen, DateTime fechaMovimiento)
        {
            var id = $"{codigoProducto}-{codigoAlmacen}-{fechaMovimiento:yyyy-MM-dd}";
            return new NotFoundException("MovInventario", id);
        }

        public static NotFoundException ForMovInventario(string codigoProducto, string codigoAlmacen)
        {
            var id = $"{codigoProducto}-{codigoAlmacen}";
            return new NotFoundException($"No se encontraron movimientos de inventario para el producto '{codigoProducto}' en el almacén '{codigoAlmacen}'", "MovInventario", id);
        }

        public static NotFoundException ForProducto(string codigoProducto)
        {
            return new NotFoundException($"No se encontró el producto con código '{codigoProducto}'", "Producto", codigoProducto);
        }

        public static NotFoundException ForAlmacen(string codigoAlmacen)
        {
            return new NotFoundException($"No se encontró el almacén con código '{codigoAlmacen}'", "Almacen", codigoAlmacen);
        }

        public override string ToString()
        {
            var result = $"[{ErrorCode}] {Message}";
            
            if (!string.IsNullOrEmpty(ResourceType))
            {
                result += $" | Resource: {ResourceType}";
            }
            
            if (ResourceId != null)
            {
                result += $" | ID: {ResourceId}";
            }
            
            if (InnerException != null)
            {
                result += $" | Inner: {InnerException.Message}";
            }
            
            return result;
        }
    }
}