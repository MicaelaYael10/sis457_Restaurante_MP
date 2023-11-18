CREATE DATABASE LabRestauranteMP;
USE master
GO

CREATE LOGIN usrrestaurantemp WITH PASSWORD=N'123456',
	DEFAULT_DATABASE=LabRestauranteMP,
	CHECK_EXPIRATION=OFF,
	CHECK_POLICY=ON
GO

USE LabRestauranteMP
GO

CREATE USER usrrestaurantemp FOR LOGIN usrrestaurantemp
GO
ALTER ROLE db_owner ADD MEMBER usrrestaurantemp
GO

--ELIMINAR TABLAS 
DROP TABLE Cliente;
DROP TABLE Empleado;
DROP TABLE Usuario;
DROP TABLE Comida;
DROP TABLE Factura;
DROP TABLE Bebida;


--CREACION DE TABLAS 

CREATE TABLE Usuarios (
  id INT PRIMARY KEY IDENTITY (1,1),
  usuario VARCHAR(30) NOT NULL,
  clave VARCHAR(100) NOT NULL,
  idEmpleado INT NOT NULL,

  CONSTRAINT fk_Usuarios_Empleado FOREIGN KEY(idEmpleado) REFERENCES Empleado(id)

);


CREATE TABLE Cliente (
id INT PRIMARY KEY IDENTITY (1,1),
nombre VARCHAR (50) NOT NULL,
primerApellido VARCHAR (30) NOT NULL,
segundoApellido VARCHAR(30) NOT NULL,
cedulaIdentidad VARCHAR (10) NOT NULL,

);

CREATE TABLE Empleado (
id INT PRIMARY KEY IDENTITY(1,1),
nombre VARCHAR(30) NOT NULL,
primerApellido VARCHAR (30) NOT NULL,
segundoApellido VARCHAR(30) NOT NULL,
telefono VARCHAR(8) NOT NULL,
direccion VARCHAR(30) NOT NULL,
cargo VARCHAR(30) NOT NULL
);


CREATE TABLE Comida (
id INT PRIMARY KEY IDENTITY(1,1),
nombre VARCHAR (50) NOT NULL,
precio DECIMAL NOT NULL
);


CREATE TABLE Factura (
id INT PRIMARY KEY IDENTITY(1,1),
idCliente INT NOT NULL,
idEmpleado INT NOT NULL,
idComida INT NOT NULL,
idBebida INT NOT NULL,


 CONSTRAINT fk_Factura_Cliente FOREIGN KEY(idCliente) REFERENCES Cliente(id),
 CONSTRAINT fk_Factura_Empleado FOREIGN KEY(idEmpleado) REFERENCES Empleado(id),
 CONSTRAINT fk_Factura_Comida FOREIGN KEY(idComida) REFERENCES Comida(id),
 CONSTRAINT fk_Factura_Bebida FOREIGN KEY(idBebida) REFERENCES Bebida(id),

);


CREATE TABLE Bebida (
id INT PRIMARY KEY IDENTITY(1,1),
nombre VARCHAR (50) NOT NULL,
precio DECIMAL NOT NULL,
marca VARCHAR(50)
);

ALTER TABLE Usuarios  ADD usuarioRegistro VARCHAR (20) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Usuarios ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Usuarios ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: eliminacion logica, 0: inactivo, 1: activo


ALTER TABLE Cliente  ADD usuarioRegistro VARCHAR (20) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Cliente ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Cliente ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: eliminacion logica, 0: inactivo, 1: activo

ALTER TABLE Factura  ADD usuarioRegistro VARCHAR (20) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Factura ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Factura ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: eliminacion logica, 0: inactivo, 1: activo

ALTER TABLE Empleado  ADD usuarioRegistro VARCHAR (20) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Empleado ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Empleado ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: eliminacion logica, 0: inactivo, 1: activo


ALTER TABLE Comida  ADD usuarioRegistro VARCHAR (20) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Comida ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Comida ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: eliminacion logica, 0: inactivo, 1: activo

ALTER TABLE Bebida  ADD usuarioRegistro VARCHAR (20) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Bebida ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Bebida ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: eliminacion logica, 0: inactivo, 1: activo

--USUARIO
CREATE PROC paUsuariosListar @parametro VARCHAR(50)
AS
  SELECT us.id,idEmpleado,em.nombre,us.usuario,us.clave,us.usuarioRegistro,us.fechaRegistro,us.estado
  FROM Usuario us INNER JOIN Empleado em ON us.id = em.id
  WHERE us.estado <> -1 AND em.nombre LIKE '%'+ REPLACE (@parametro,' ','%')+'%';


  SELECT *FROM Usuario

  

--CLIENTES
CREATE PROC paClienteListar @parametro Varchar(50)
AS
  SELECT id,nombre,primerApellido,segundoApellido,cedulaIdentidad
  FROM Cliente
  WHERE estado <> -1 AND nombre LIKE '%'+ REPLACE (@parametro,' ','%')+'%';

INSERT INTO Cliente(nombre,primerApellido,segundoApellido,cedulaIdentidad,Telefono)
VALUES ('Luis','Perez','Mendoza','14181556','47852136');
INSERT INTO Cliente(nombre,primerApellido,segundoApellido,cedulaIdentidad,Telefono)
VALUES ('Pablo','Padilla','Arandia ','14181556','47852136');
INSERT INTO Cliente(nombre,primerApellido,segundoApellido,cedulaIdentidad,Telefono)
VALUES ('Mariana','Velazco','Gutierrez','7895411','16457845');

SELECT *FROM Cliente 

-- PLATOS
CREATE PROC paComidaListar @parametro VARCHAR(50) 
AS
  SELECT id,nombre,precio,usuarioRegistro,fechaRegistro, estado 
  FROM Comida
  WHERE estado <> -1 AND nombre LIKE '%'+ REPLACE (@parametro,' ','%')+'%';


  INSERT INTO Comida(nombre,precio)
  VALUES ('Lasaña',45);

  SELECT *FROM Comida 

  --EMPLEADO
CREATE PROC paEmpleadoListar @parametro VARCHAR(50) 
AS
  SELECT id,nombre,primerApellido,segundoApellido,telefono,direccion,cargo,usuarioRegistro,fechaRegistro, estado 
  FROM Empleado
  WHERE estado <> -1 AND nombre LIKE '%'+ REPLACE (@parametro,' ','%')+'%';


  INSERT INTO Empleado(nombre,primerApellido,segundoApellido,telefono,direccion,cargo)
  VALUES ('Paola','Duran',' ',78963254,'avaroa 47','Gerente');

  INSERT INTO Empleado(nombre,primerApellido,segundoApellido,telefono,direccion,cargo)
  VALUES ('Micaela','Mamani',' ',7892547,'mendizabal 7','Cajero');


  SELECT *FROM Empleado



  --DETALLE FACTURA
  CREATE PROC paFacturarListar @parametro VARCHAR(50) 
AS
  SELECT id,idCliente,idEmpleado,idComida,idBebida,usuarioRegistro,fechaRegistro, estado 
  FROM Factura
  WHERE estado <> -1 AND idCliente LIKE '%'+ REPLACE (@parametro,' ','%')+'%';




  SELECT *FROM DetalleFactura



--BEBIDA
CREATE PROC paBebidasListar @parametro VARCHAR(50)
AS
  SELECT id, nombre, precio, marca, usuarioRegistro,fechaRegistro, estado 
  FROM Bebida
  WHERE estado <> -1 AND nombre LIKE '%'+ REPLACE (@parametro,' ','%')+'%';



  SELECT *FROM Bebida 