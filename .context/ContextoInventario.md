PRUEBA: CASO PRÁCTICO (CRUD CON ASP.NET MVC
FRAMEWORK)
Objetivo:

Evaluar las habilidades del candidato en el desarrollo de una aplicación web utilizando ASP.NET
MVC Framework con C#. El proyecto incluye la implementación de un CRUD (Consulta,
Inserción, Actualización y Eliminación) basado en procedimientos almacenados (Store
Procedures) y organizado bajo un enfoque modular y escalable. (8 puntos)
Instrucciones:

1. Base de Datos – Crear la Tabla MOV_INVENTARIO
Debes crear la tabla MOV_INVENTARIO en tu base de datos utilizando el siguiente script SQL:
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
) ON PRIMARY
GO

2. Crear Store Procedures
• Consulta, este Store Procedure debe permitir la consulta de registros.
Debe incluir filtros opcionales: Fecha Inicio, Fecha Fin, Tipo de Movimiento, Número de
Documento, es decir, el Store Procedure debe poder ejecutarse con todos, algunos o
ninguno de ellos.
• Inserción, permitir la inserción de nuevos registros.
• Actualización, permitir la actualización de registros.
• Eliminación, permitir la eliminación de registros.
3. Desarrollo de una Aplicación Web
• Desarrolla una aplicación en ASP.NET MVC Framework con C# que incluya una pantalla
principal para gestionar las operaciones CRUD de la tabla MOV_INVENTARIO.
• La aplicación debe estar distribuida en capas (proyecto distribuido), es decir, separandolas diferentes responsabilidades del código.

• La arquitectura específica queda a tu elección, pero debe ser organizada y facilitar elmantenimiento. Intentemos esta vez la arquitectura N-capas

• El CRUD debe consumir los Store Procedures creados anteriormente para realizar las
operaciones de consulta, inserción, actualización y eliminación.

* Se puede usar entity framework para llamar a los stored procedures