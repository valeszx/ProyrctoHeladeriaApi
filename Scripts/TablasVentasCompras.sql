-- Tablas nuevas + columna Productos.Disponible. Ejecutar sobre la BD de la heladeria.
IF COL_LENGTH('Productos','Disponible') IS NULL
    ALTER TABLE Productos ADD Disponible bit NOT NULL CONSTRAINT DF_Productos_Disponible DEFAULT 1;

IF OBJECT_ID('Sabores') IS NULL
CREATE TABLE Sabores (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Nombre nvarchar(max) NOT NULL);

IF OBJECT_ID('Compra') IS NULL
CREATE TABLE Compra (
    id_compra int IDENTITY(1,1) PRIMARY KEY,
    id_usuario int NOT NULL REFERENCES Logins(Id),
    fecha datetime2 NOT NULL);

IF OBJECT_ID('Detalle_Compra') IS NULL
CREATE TABLE Detalle_Compra (
    id_detalle_compra int IDENTITY(1,1) PRIMARY KEY,
    id_compra int NOT NULL REFERENCES Compra(id_compra),
    id_producto int NOT NULL REFERENCES Productos(Id),
    cantidad int NOT NULL,
    costo_unitario decimal(18,2) NOT NULL);

IF OBJECT_ID('Venta') IS NULL
CREATE TABLE Venta (
    id_venta int IDENTITY(1,1) PRIMARY KEY,
    id_usuario int NOT NULL REFERENCES Logins(Id),
    fecha datetime2 NOT NULL,
    total decimal(18,2) NOT NULL);

IF OBJECT_ID('Detalle_Venta') IS NULL
CREATE TABLE Detalle_Venta (
    id_detalle_venta int IDENTITY(1,1) PRIMARY KEY,
    id_venta int NOT NULL REFERENCES Venta(id_venta),
    id_producto int NOT NULL REFERENCES Productos(Id),
    id_sabor int NULL REFERENCES Sabores(Id),
    cantidad int NOT NULL,
    precio_unitario decimal(18,2) NOT NULL);

IF OBJECT_ID('Perdida') IS NULL
CREATE TABLE Perdida (
    id_perdida int IDENTITY(1,1) PRIMARY KEY,
    id_usuario int NOT NULL REFERENCES Logins(Id),
    id_producto int NOT NULL REFERENCES Productos(Id),
    cantidad int NOT NULL,
    motivo nvarchar(max) NOT NULL,
    fecha datetime2 NOT NULL);

IF OBJECT_ID('Movimiento') IS NULL
CREATE TABLE Movimiento (
    id_movimiento int IDENTITY(1,1) PRIMARY KEY,
    id_producto int NOT NULL REFERENCES Productos(Id),
    id_venta int NULL REFERENCES Venta(id_venta),
    id_compra int NULL REFERENCES Compra(id_compra),
    id_perdida int NULL REFERENCES Perdida(id_perdida),
    tipo nvarchar(max) NOT NULL,
    cantidad int NOT NULL,
    fecha datetime2 NOT NULL);

IF OBJECT_ID('Notificacion') IS NULL
CREATE TABLE Notificacion (
    id_notificacion int IDENTITY(1,1) PRIMARY KEY,
    id_producto int NOT NULL REFERENCES Productos(Id),
    mensaje nvarchar(max) NOT NULL,
    fecha datetime2 NOT NULL,
    atendida bit NOT NULL);
