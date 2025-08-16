-- Script para insertar datos de ejemplo en la tabla MOV_INVENTARIO
-- Base de datos: InventarioDB

USE InventarioDB;
GO

-- Insertar registros de ejemplo en MOV_INVENTARIO
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
) VALUES 
(
    '001',                    -- COD_CIA
    '001',                    -- COMPANIA_VENTA_3
    'ALM001',                 -- ALMACEN_VENTA
    'EN',                     -- TIPO_MOVIMIENTO (Entrada)
    'FC',                     -- TIPO_DOCUMENTO (Factura)
    'FC-2024-001',            -- NRO_DOCUMENTO
    'PROD001',                -- COD_ITEM_2
    'PROVEEDOR ABC S.A.',     -- PROVEEDOR
    NULL,                     -- ALMACEN_DESTINO
    100,                      -- CANTIDAD
    'REF001',                 -- DOC_REF_1
    NULL,                     -- DOC_REF_2
    NULL,                     -- DOC_REF_3
    NULL,                     -- DOC_REF_4
    NULL,                     -- DOC_REF_5
    '2024-01-15'              -- FECHA_TRANSACCION
),
(
    '001',                    -- COD_CIA
    '001',                    -- COMPANIA_VENTA_3
    'ALM001',                 -- ALMACEN_VENTA
    'SA',                     -- TIPO_MOVIMIENTO (Salida)
    'GD',                     -- TIPO_DOCUMENTO (Guía de Despacho)
    'GD-2024-001',            -- NRO_DOCUMENTO
    'PROD002',                -- COD_ITEM_2
    NULL,                     -- PROVEEDOR
    'ALM002',                 -- ALMACEN_DESTINO
    50,                       -- CANTIDAD
    'REF002',                 -- DOC_REF_1
    'CLIENTE XYZ',            -- DOC_REF_2
    NULL,                     -- DOC_REF_3
    NULL,                     -- DOC_REF_4
    NULL,                     -- DOC_REF_5
    '2024-01-16'              -- FECHA_TRANSACCION
);
GO

PRINT 'Se insertaron 2 registros de ejemplo en MOV_INVENTARIO exitosamente';
GO

-- Verificar los registros insertados
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
    FECHA_TRANSACCION
FROM dbo.MOV_INVENTARIO
ORDER BY FECHA_TRANSACCION;
GO