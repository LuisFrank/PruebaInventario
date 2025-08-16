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
            -- Devolver error si el registro ya existe
            DECLARE @ErrorMsg NVARCHAR(500) = 'Ya existe un registro con la clave primaria especificada: COD_CIA=' + @CodCia + 
                                             ', COMPANIA_VENTA_3=' + @CompaniaVenta3 + 
                                             ', ALMACEN_VENTA=' + @AlmacenVenta + 
                                             ', TIPO_MOVIMIENTO=' + @TipoMovimiento + 
                                             ', TIPO_DOCUMENTO=' + @TipoDocumento + 
                                             ', NRO_DOCUMENTO=' + @NroDocumento + 
                                             ', COD_ITEM_2=' + @CodItem2;
            
            RAISERROR(@ErrorMsg, 16, 1);
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