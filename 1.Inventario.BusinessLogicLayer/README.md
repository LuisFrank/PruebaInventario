# Inventario Business Logic Layer

## Descripción
Esta capa contiene toda la lógica de negocio para el sistema de inventario, incluyendo validaciones, reglas de negocio, mapeo de datos y servicios empresariales.

## Estructura del Proyecto

```
1.Inventario.BusinessLogicLayer/
├── Configuration/
│   └── BusinessLogicLayerConfiguration.cs    # Configuración de DI
├── DTOs/
│   ├── MovInventarioBusinessDto.cs           # DTO para operaciones CRUD
│   ├── MovInventarioSearchBusinessDto.cs     # DTO para búsquedas
│   └── MovInventarioResultBusinessDto.cs     # DTO para resultados
├── Exceptions/
│   ├── BusinessException.cs                  # Excepción base de negocio
│   ├── NotFoundException.cs                  # Excepción para recursos no encontrados
│   └── ValidationException.cs                # Excepción para errores de validación
├── Mappers/
│   ├── IMovInventarioMapper.cs               # Interfaz del mapper
│   └── MovInventarioMapper.cs                # Implementación del mapper
├── Services/
│   ├── Interfaces/
│   │   └── IMovInventarioService.cs          # Interfaz del servicio
│   └── MovInventarioService.cs               # Implementación del servicio
└── Validators/
    ├── MovInventarioBusinessValidator.cs     # Validador para DTOs de negocio
    └── MovInventarioSearchValidator.cs       # Validador para DTOs de búsqueda
```

## Componentes Principales

### 1. DTOs (Data Transfer Objects)
- **MovInventarioBusinessDto**: DTO principal para operaciones CRUD con validaciones y propiedades calculadas
- **MovInventarioSearchBusinessDto**: DTO para criterios de búsqueda con paginación y ordenamiento
- **MovInventarioResultBusinessDto**: DTO para resultados enriquecidos con información adicional

### 2. Servicios de Negocio
- **IMovInventarioService**: Define las operaciones de negocio disponibles
- **MovInventarioService**: Implementa la lógica de negocio, validaciones y manejo de excepciones

### 3. Validadores
- **MovInventarioBusinessValidator**: Validaciones usando FluentValidation para DTOs de negocio
- **MovInventarioSearchValidator**: Validaciones para criterios de búsqueda

### 4. Mappers
- **IMovInventarioMapper**: Define las operaciones de mapeo entre DTOs
- **MovInventarioMapper**: Implementa el mapeo entre DTOs de datos y DTOs de negocio

### 5. Excepciones Personalizadas
- **BusinessException**: Excepción base para errores de lógica de negocio
- **ValidationException**: Para errores de validación de datos
- **NotFoundException**: Para recursos no encontrados

## Configuración

### Registro de Dependencias

En `Program.cs` o `Startup.cs`:

```csharp
using Inventario.BusinessLogicLayer.Configuration;

// Registrar todos los servicios del Business Logic Layer
builder.Services.AddBusinessLogicLayer();

// O registrar componentes específicos
builder.Services.AddBusinessLogicLayerCore();
builder.Services.AddBusinessLogicLayerValidators();
```

## Uso del Servicio

### Inyección de Dependencias

```csharp
public class InventarioController : ControllerBase
{
    private readonly IMovInventarioService _movInventarioService;

    public InventarioController(IMovInventarioService movInventarioService)
    {
        _movInventarioService = movInventarioService;
    }
}
```

### Ejemplos de Uso

#### Consultar Movimientos

```csharp
var searchDto = new MovInventarioSearchBusinessDto
{
    CodigoProducto = "PROD001",
    FechaDesde = DateTime.Now.AddDays(-30),
    FechaHasta = DateTime.Now,
    Pagina = 1,
    TamanoPagina = 10
};

var resultado = await _movInventarioService.ConsultarMovInventarioAsync(searchDto);
```

#### Crear Movimiento

```csharp
var nuevoMovimiento = new MovInventarioBusinessDto
{
    CodigoProducto = "PROD001",
    CodigoAlmacen = "ALM001",
    FechaMovimiento = DateTime.Now,
    TipoMovimiento = "E", // Entrada
    Cantidad = 100,
    Observaciones = "Compra inicial"
};

var resultado = await _movInventarioService.CrearMovInventarioAsync(nuevoMovimiento);
```

#### Obtener por ID

```csharp
var movimiento = await _movInventarioService.ObtenerMovInventarioPorIdAsync(
    "PROD001", "ALM001", DateTime.Today);
```

## Reglas de Negocio Implementadas

### Validaciones Generales
- Códigos de producto y almacén requeridos (máximo 20 caracteres)
- Fecha de movimiento requerida
- Tipo de movimiento debe ser 'E' (Entrada), 'I' (Inventario), o 'S' (Salida)
- Cantidad debe ser mayor a 0 y menor a 1,000,000

### Validaciones Específicas
- **Movimientos de Salida ('S')**: Cantidad máxima de 10,000 unidades
- **Movimientos de Inventario ('I')**: Observaciones obligatorias
- **Fechas**: No se permiten fechas futuras

### Propiedades Calculadas
- **TipoMovimientoDescripcion**: Descripción legible del tipo

## Manejo de Errores

El servicio maneja diferentes tipos de errores:

- **ValidationException**: Errores de validación de datos
- **NotFoundException**: Recursos no encontrados
- **BusinessException**: Errores de lógica de negocio
- **Exception**: Errores generales del sistema

Todos los errores se registran usando `ILogger` para facilitar el diagnóstico.

## Dependencias

- **FluentValidation**: Para validaciones declarativas
- **Microsoft.Extensions.Logging**: Para logging
- **Microsoft.Extensions.DependencyInjection**: Para inyección de dependencias
- **Inventario.DataAccessLayer**: Para acceso a datos

## Notas de Implementación

1. **Enfoque Pragmático**: Se reutilizan DTOs de la capa de datos cuando es apropiado
2. **Validaciones Robustas**: Uso de FluentValidation para validaciones declarativas
3. **Logging Completo**: Todas las operaciones se registran para auditoría
4. **Manejo de Excepciones**: Excepciones específicas para diferentes tipos de errores
5. **Mapeo Eficiente**: Mappers dedicados para conversión entre DTOs
6. **Configuración Flexible**: Múltiples opciones de registro de dependencias