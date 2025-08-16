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