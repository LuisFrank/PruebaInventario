-- Stored Procedure para consultar registros de MOV_INVENTARIO
-- Incluye filtros opcionales: Fecha Inicio, Fecha Fin, Tipo de Movimiento, Número de Documento

USE InventarioDB;
GO

CREATE OR ALTER PROCEDURE dbo.SP_ConsultarMovInventario
    @FechaInicio DATE = NULL,
    @FechaFin DATE = NULL,
    @TipoMovimiento VARCHAR(2) = NULL,
    @NroDocumento VARCHAR(50) = NULL,
    @CodCia VARCHAR(5) = NULL,
    @CompaniaVenta3 VARCHAR(5) = NULL,
    @AlmacenVenta VARCHAR(10) = NULL,
    @TipoDocumento VARCHAR(2) = NULL,
    @CodItem2 VARCHAR(50) = NULL,
    @Proveedor VARCHAR(100) = NULL,
    @AlmacenDestino VARCHAR(10) = NULL,
    @DocRef1 VARCHAR(50) = NULL,
    @DocRef2 VARCHAR(50) = NULL,
    @DocRef3 VARCHAR(50) = NULL,
    @DocRef4 VARCHAR(50) = NULL,
    @DocRef5 VARCHAR(50) = NULL
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
        AND (@CodCia IS NULL OR COD_CIA = @CodCia)
        AND (@CompaniaVenta3 IS NULL OR COMPANIA_VENTA_3 = @CompaniaVenta3)
        AND (@AlmacenVenta IS NULL OR ALMACEN_VENTA = @AlmacenVenta)
        AND (@TipoDocumento IS NULL OR TIPO_DOCUMENTO = @TipoDocumento)
        AND (@CodItem2 IS NULL OR COD_ITEM_2 LIKE '%' + @CodItem2 + '%')
        AND (@Proveedor IS NULL OR PROVEEDOR LIKE '%' + @Proveedor + '%')
        AND (@AlmacenDestino IS NULL OR ALMACEN_DESTINO LIKE '%' + @AlmacenDestino + '%')
        AND (@DocRef1 IS NULL OR DOC_REF_1 LIKE '%' + @DocRef1 + '%')
        AND (@DocRef2 IS NULL OR DOC_REF_2 LIKE '%' + @DocRef2 + '%')
        AND (@DocRef3 IS NULL OR DOC_REF_3 LIKE '%' + @DocRef3 + '%')
        AND (@DocRef4 IS NULL OR DOC_REF_4 LIKE '%' + @DocRef4 + '%')
        AND (@DocRef5 IS NULL OR DOC_REF_5 LIKE '%' + @DocRef5 + '%')
    ORDER BY FECHA_TRANSACCION DESC, COD_CIA, NRO_DOCUMENTO;
END;
GO

PRINT 'Stored Procedure SP_ConsultarMovInventario creado exitosamente';
GO