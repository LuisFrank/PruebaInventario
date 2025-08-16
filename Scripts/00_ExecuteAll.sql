-- Script maestro para ejecutar todos los scripts de configuración
-- Base de datos: InventarioDB
-- Orden de ejecución: Tabla -> Stored Procedures

PRINT '=== INICIANDO CONFIGURACIÓN DE BASE DE DATOS INVENTARIODB ===';
PRINT 'Fecha y hora: ' + CONVERT(VARCHAR, GETDATE(), 120);
PRINT '';

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'InventarioDB')
BEGIN
    CREATE DATABASE InventarioDB;
    PRINT 'Base de datos InventarioDB creada exitosamente';
END
ELSE
    PRINT 'Base de datos InventarioDB ya existe';
GO

PRINT '';
PRINT '=== PASO 1: CREANDO TABLA MOV_INVENTARIO ===';
PRINT '';

-- ========================================
-- CONTENIDO DE: 01_CreateTable_MOV_INVENTARIO.sql
-- ========================================

-- Script para crear la tabla MOV_INVENTARIO
-- Base de datos: InventarioDB

USE InventarioDB;
GO

-- Crear la tabla MOV_INVENTARIO
CREATE TABLE dbo.MOV_INVENTARIO(
    COD_CIA varchar(5) NOT NULL,
    COMPANIA_VENTA_3 varchar(5) NOT NULL,
    ALMACEN_VENTA varchar(10) NOT NULL,
    TIPO_MOVIMIENTO varchar(2) NOT NULL,
    TIPO_DOCUMENTO varchar(2) NOT NULL,
    NRO_DOCUMENTO varchar(50) NOT NULL,
    COD_ITEM_2 varchar(50) NOT NULL,
    PROVEEDOR varchar(100) NULL,
    ALMACEN_DESTINO varchar(50) NULL,
    CANTIDAD int NULL,
    DOC_REF_1 varchar(50) NULL,
    DOC_REF_2 varchar(50) NULL,
    DOC_REF_3 varchar(50) NULL,
    DOC_REF_4 varchar(50) NULL,
    DOC_REF_5 varchar(50) NULL,
    FECHA_TRANSACCION DATE NULL,
    CONSTRAINT PK_MOV_INVENTARIO PRIMARY KEY CLUSTERED
    (
        COD_CIA ASC,
        COMPANIA_VENTA_3 ASC,
        ALMACEN_VENTA ASC,
        TIPO_MOVIMIENTO ASC,
        TIPO_DOCUMENTO ASC,
        NRO_DOCUMENTO ASC,
        COD_ITEM_2 ASC
    ) ON [PRIMARY]
) ON [PRIMARY];
GO

PRINT 'Tabla MOV_INVENTARIO creada exitosamente';
GO

PRINT '';
PRINT '=== PASO 2: CREANDO STORED PROCEDURES ===';
PRINT '';

-- ========================================
-- CONTENIDO DE: 02_SP_ConsultarMovInventario.sql
-- ========================================

-- Stored Procedure para consultar registros de MOV_INVENTARIO
-- Incluye filtros opcionales: Fecha Inicio, Fecha Fin, Tipo de Movimiento, Número de Documento

USE InventarioDB;
GO

CREATE OR ALTER PROCEDURE dbo.SP_ConsultarMovInventario
    @FechaInicio DATE = NULL,
    @FechaFin DATE = NULL,
    @TipoMovimiento VARCHAR(2) = NULL,
    @NroDocumento VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        COD_CIA,
        COMPANIA_VENTA_3,
        ALMACEN_VENTA,
        TIPO_MOVIMIENTO,
        TIPO_DOCUMENTO,
        NRO_DOCUMENTO,
        COD_ITEM_2,
        PROVEEDOR,
        ALMACEN_DESTINO,
        CANTIDAD,
        DOC_REF_1,
        DOC_REF_2,
        DOC_REF_3,
        DOC_REF_4,
        DOC_REF_5,
        FECHA_TRANSACCION
    FROM dbo.MOV_INVENTARIO
    WHERE 
        (@FechaInicio IS NULL OR FECHA_TRANSACCION >= @FechaInicio)
        AND (@FechaFin IS NULL OR FECHA_TRANSACCION <= @FechaFin)
        AND (@TipoMovimiento IS NULL OR TIPO_MOVIMIENTO = @TipoMovimiento)
        AND (@NroDocumento IS NULL OR NRO_DOCUMENTO LIKE '%' + @NroDocumento + '%')
    ORDER BY FECHA_TRANSACCION DESC, COD_CIA, NRO_DOCUMENTO;
END;
GO

PRINT 'Stored Procedure SP_ConsultarMovInventario creado exitosamente';
GO

-- ========================================
-- CONTENIDO DE: 03_SP_InsertarMovInventario.sql
-- ========================================

-- Stored Procedure para insertar nuevos registros en MOV_INVENTARIO

USE InventarioDB;
GO

CREATE OR ALTER PROCEDURE dbo.SP_InsertarMovInventario
    @CodCia VARCHAR(5),
    @CompaniaVenta3 VARCHAR(5),
    @AlmacenVenta VARCHAR(10),
    @TipoMovimiento VARCHAR(2),
    @TipoDocumento VARCHAR(2),
    @NroDocumento VARCHAR(50),
    @CodItem2 VARCHAR(50),
    @Proveedor VARCHAR(100) = NULL,
    @AlmacenDestino VARCHAR(50) = NULL,
    @Cantidad INT = NULL,
    @DocRef1 VARCHAR(50) = NULL,
    @DocRef2 VARCHAR(50) = NULL,
    @DocRef3 VARCHAR(50) = NULL,
    @DocRef4 VARCHAR(50) = NULL,
    @DocRef5 VARCHAR(50) = NULL,
    @FechaTransaccion DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el registro ya existe
        IF EXISTS (
            SELECT 1 FROM dbo.MOV_INVENTARIO 
            WHERE COD_CIA = @CodCia 
                AND COMPANIA_VENTA_3 = @CompaniaVenta3
                AND ALMACEN_VENTA = @AlmacenVenta
                AND TIPO_MOVIMIENTO = @TipoMovimiento
                AND TIPO_DOCUMENTO = @TipoDocumento
                AND NRO_DOCUMENTO = @NroDocumento
                AND COD_ITEM_2 = @CodItem2
        )
        BEGIN
            RAISERROR('El registro ya existe en la tabla MOV_INVENTARIO', 16, 1);
            RETURN;
        END
        
        -- Insertar el nuevo registro
        INSERT INTO dbo.MOV_INVENTARIO (
            COD_CIA,
            COMPANIA_VENTA_3,
            ALMACEN_VENTA,
            TIPO_MOVIMIENTO,
            TIPO_DOCUMENTO,
            NRO_DOCUMENTO,
            COD_ITEM_2,
            PROVEEDOR,
            ALMACEN_DESTINO,
            CANTIDAD,
            DOC_REF_1,
            DOC_REF_2,
            DOC_REF_3,
            DOC_REF_4,
            DOC_REF_5,
            FECHA_TRANSACCION
        )
        VALUES (
            @CodCia,
            @CompaniaVenta3,
            @AlmacenVenta,
            @TipoMovimiento,
            @TipoDocumento,
            @NroDocumento,
            @CodItem2,
            @Proveedor,
            @AlmacenDestino,
            @Cantidad,
            @DocRef1,
            @DocRef2,
            @DocRef3,
            @DocRef4,
            @DocRef5,
            ISNULL(@FechaTransaccion, GETDATE())
        );
        
        PRINT 'Registro insertado exitosamente en MOV_INVENTARIO';
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

PRINT 'Stored Procedure SP_InsertarMovInventario creado exitosamente';
GO

-- ========================================
-- CONTENIDO DE: 04_SP_ActualizarMovInventario.sql
-- ========================================

-- Stored Procedure para actualizar registros en MOV_INVENTARIO

USE InventarioDB;
GO

CREATE OR ALTER PROCEDURE dbo.SP_ActualizarMovInventario
    @CodCia VARCHAR(5),
    @CompaniaVenta3 VARCHAR(5),
    @AlmacenVenta VARCHAR(10),
    @TipoMovimiento VARCHAR(2),
    @TipoDocumento VARCHAR(2),
    @NroDocumento VARCHAR(50),
    @CodItem2 VARCHAR(50),
    @Proveedor VARCHAR(100) = NULL,
    @AlmacenDestino VARCHAR(50) = NULL,
    @Cantidad INT = NULL,
    @DocRef1 VARCHAR(50) = NULL,
    @DocRef2 VARCHAR(50) = NULL,
    @DocRef3 VARCHAR(50) = NULL,
    @DocRef4 VARCHAR(50) = NULL,
    @DocRef5 VARCHAR(50) = NULL,
    @FechaTransaccion DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el registro existe
        IF NOT EXISTS (
            SELECT 1 FROM dbo.MOV_INVENTARIO 
            WHERE COD_CIA = @CodCia 
                AND COMPANIA_VENTA_3 = @CompaniaVenta3
                AND ALMACEN_VENTA = @AlmacenVenta
                AND TIPO_MOVIMIENTO = @TipoMovimiento
                AND TIPO_DOCUMENTO = @TipoDocumento
                AND NRO_DOCUMENTO = @NroDocumento
                AND COD_ITEM_2 = @CodItem2
        )
        BEGIN
            RAISERROR('El registro no existe en la tabla MOV_INVENTARIO', 16, 1);
            RETURN;
        END
        
        -- Actualizar el registro
        UPDATE dbo.MOV_INVENTARIO 
        SET 
            PROVEEDOR = @Proveedor,
            ALMACEN_DESTINO = @AlmacenDestino,
            CANTIDAD = @Cantidad,
            DOC_REF_1 = @DocRef1,
            DOC_REF_2 = @DocRef2,
            DOC_REF_3 = @DocRef3,
            DOC_REF_4 = @DocRef4,
            DOC_REF_5 = @DocRef5,
            FECHA_TRANSACCION = ISNULL(@FechaTransaccion, FECHA_TRANSACCION)
        WHERE 
            COD_CIA = @CodCia 
            AND COMPANIA_VENTA_3 = @CompaniaVenta3
            AND ALMACEN_VENTA = @AlmacenVenta
            AND TIPO_MOVIMIENTO = @TipoMovimiento
            AND TIPO_DOCUMENTO = @TipoDocumento
            AND NRO_DOCUMENTO = @NroDocumento
            AND COD_ITEM_2 = @CodItem2;
        
        IF @@ROWCOUNT > 0
            PRINT 'Registro actualizado exitosamente en MOV_INVENTARIO';
        ELSE
            PRINT 'No se pudo actualizar el registro en MOV_INVENTARIO';
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

PRINT 'Stored Procedure SP_ActualizarMovInventario creado exitosamente';
GO

-- ========================================
-- CONTENIDO DE: 05_SP_EliminarMovInventario.sql
-- ========================================

-- Stored Procedure para eliminar registros de MOV_INVENTARIO

USE InventarioDB;
GO

CREATE OR ALTER PROCEDURE dbo.SP_EliminarMovInventario
    @CodCia VARCHAR(5),
    @CompaniaVenta3 VARCHAR(5),
    @AlmacenVenta VARCHAR(10),
    @TipoMovimiento VARCHAR(2),
    @TipoDocumento VARCHAR(2),
    @NroDocumento VARCHAR(50),
    @CodItem2 VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el registro existe
        IF NOT EXISTS (
            SELECT 1 FROM dbo.MOV_INVENTARIO 
            WHERE COD_CIA = @CodCia 
                AND COMPANIA_VENTA_3 = @CompaniaVenta3
                AND ALMACEN_VENTA = @AlmacenVenta
                AND TIPO_MOVIMIENTO = @TipoMovimiento
                AND TIPO_DOCUMENTO = @TipoDocumento
                AND NRO_DOCUMENTO = @NroDocumento
                AND COD_ITEM_2 = @CodItem2
        )
        BEGIN
            RAISERROR('El registro no existe en la tabla MOV_INVENTARIO', 16, 1);
            RETURN;
        END
        
        -- Eliminar el registro
        DELETE FROM dbo.MOV_INVENTARIO 
        WHERE 
            COD_CIA = @CodCia 
            AND COMPANIA_VENTA_3 = @CompaniaVenta3
            AND ALMACEN_VENTA = @AlmacenVenta
            AND TIPO_MOVIMIENTO = @TipoMovimiento
            AND TIPO_DOCUMENTO = @TipoDocumento
            AND NRO_DOCUMENTO = @NroDocumento
            AND COD_ITEM_2 = @CodItem2;
        
        IF @@ROWCOUNT > 0
            PRINT 'Registro eliminado exitosamente de MOV_INVENTARIO';
        ELSE
            PRINT 'No se pudo eliminar el registro de MOV_INVENTARIO';
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

PRINT 'Stored Procedure SP_EliminarMovInventario creado exitosamente';
GO

PRINT '';
PRINT '=== CONFIGURACIÓN COMPLETADA EXITOSAMENTE ===';
PRINT 'Fecha y hora: ' + CONVERT(VARCHAR, GETDATE(), 120);
PRINT '';
PRINT 'Objetos creados:';
PRINT '- Tabla: MOV_INVENTARIO';
PRINT '- SP: SP_ConsultarMovInventario';
PRINT '- SP: SP_InsertarMovInventario';
PRINT '- SP: SP_ActualizarMovInventario';
PRINT '- SP: SP_EliminarMovInventario';
PRINT '';
PRINT '¡La base de datos está lista para usar!';
GO