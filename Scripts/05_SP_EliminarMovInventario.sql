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