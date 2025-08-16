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