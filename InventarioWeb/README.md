# Sistema de Inventario - ASP.NET MVC

## Descripción
Aplicación web desarrollada en ASP.NET MVC con C# que implementa un CRUD completo para la gestión de movimientos de inventario. El proyecto utiliza una arquitectura N-capas y procedimientos almacenados para las operaciones de base de datos.

## Estructura del Proyecto

### Capas de la Aplicación
- **InventarioWeb** (Capa de Presentación): Controladores MVC, vistas y ViewModels
- **1.Inventario.BusinessLogicLayer** (Capa de Negocio): Servicios, validadores y DTOs
- **2.Inventario.DataAccessLayer** (Capa de Acceso a Datos): Repositorios y contexto de Entity Framework

### Base de Datos
**Tabla Principal:** `MOV_INVENTARIO`
- Campos clave: COD_CIA, COMPANIA_VENTA_3, ALMACEN_VENTA, TIPO_MOVIMIENTO, etc.
- Clave primaria compuesta de 7 campos
- Incluye campos para referencias de documentos y fecha de transacción

### Procedimientos Almacenados
- **SP_ConsultarMovInventario**: Consulta con filtros opcionales (fecha inicio/fin, tipo movimiento, número documento)
- **SP_InsertarMovInventario**: Inserción de nuevos registros
- **SP_ActualizarMovInventario**: Actualización de registros existentes
- **SP_EliminarMovInventario**: Eliminación de registros

## Funcionalidades

### CRUD Completo
- ✅ **Consultar**: Búsqueda con filtros múltiples
- ✅ **Crear**: Inserción de nuevos movimientos
- ✅ **Editar**: Actualización de registros existentes
- ✅ **Eliminar**: Eliminación de movimientos

### Características Técnicas
- Entity Framework Core para acceso a datos
- Patrón Repository para abstracción de datos
- Validación de datos en capa de negocio
- Mapeo automático entre DTOs y entidades
- Manejo de excepciones personalizado

## Requisitos Técnicos

### Prerrequisitos
- **.NET 8.0 SDK** o superior
- **SQL Server** (Express, Developer o Standard)
- **Visual Studio 2022** o **VS Code** (opcional)
- **Git** para control de versiones

### Dependencias del Proyecto
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.8)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.8)
- Bootstrap 5.1.0
- jQuery 3.5.1

## Configuración e Instalación

### 1. Clonar el Repositorio
```bash
git clone https://github.com/LuisFrank/PruebaInventario.git
cd PruebaInventario
```

### 2. Configurar Base de Datos
1. **Ejecutar Scripts SQL** (en orden):
   ```sql
   -- Ubicados en carpeta Scripts/
   01_CreateTable_MOV_INVENTARIO.sql
   02_SP_ConsultarMovInventario.sql
   03_SP_InsertarMovInventario.sql
   04_SP_ActualizarMovInventario.sql
   05_SP_EliminarMovInventario.sql
   06_InsertarDatosEjemplo.sql
   ```

2. **Actualizar Cadena de Conexión** en `appsettings.json`:
   ```json
   "DefaultConnection": "Data Source=TU_SERVIDOR\\INSTANCIA;Initial Catalog=InventarioDB;User Id=UserInventario;Password=123456789;Integrated Security=False;TrustServerCertificate=True;"
   ```

### 3. Restaurar Dependencias
```bash
dotnet restore
```

### 4. Ejecutar la Aplicación

#### Opción A: Desde línea de comandos
```bash
dotnet run --project InventarioWeb
```
**URL de acceso:** `http://localhost:5180`

#### Opción B: Desde Visual Studio
- Abrir la solución `InventarioWeb.sln` en Visual Studio
- Presionar F5 o hacer clic en "Iniciar"
- **Nota:** Visual Studio puede generar una URL diferente (ej: `http://localhost:21514` para IIS Express o `https://localhost:7295` para HTTPS)

### 5. Verificar Funcionamiento
- Navegar a `/MovInventario` para acceder al CRUD
- Probar operaciones de consulta, inserción, edición y eliminación

## Arquitectura

```
InventarioWeb (MVC)
    ↓
1.Inventario.BusinessLogicLayer
    ↓
2.Inventario.DataAccessLayer
    ↓
SQL Server Database
```

## Scripts de Base de Datos
Todos los scripts SQL necesarios se encuentran en la carpeta `Scripts/`:
- Creación de tabla
- Procedimientos almacenados
- Datos de ejemplo

---
*Proyecto desarrollado como caso práctico de ASP.NET MVC Framework con arquitectura N-capas*