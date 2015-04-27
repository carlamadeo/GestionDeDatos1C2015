/****** Object:  Schema [SQL_SERVANT]    Script Date: 27/04/2015 23:39:06 ******/

/****************************************************************/
--						CREAR ESQUEMA
/****************************************************************/

CREATE SCHEMA [SQL_SERVANT] AUTHORIZATION [gd]
GO

/****************************************************************/
--				CREAR TABLAS E INSERTAR DATOS
/****************************************************************/

--TABLA Usuario
/*
	Tabla con los usuarios con responsabilidad en el sistema, ya se administrador
	o cliente
	Id_Usuario: Id_Usuario
	Password: password encriptada
	Cantidad_Login: cantidad de login incorrectos
	Ultima_Fecha: ultima fecha que se logueo
	Habilitado: Habilitado
*/
CREATE TABLE [SQL_SERVANT].[Usuario](
	[Id_Usuario][varchar](20) NOT NULL,
	[Password][varchar](64) NOT NULL,
	[Cantidad_Login][Int] NOT NULL,
	[Ultima_Fecha][datetime] NULL,
	[Pregunta_Secreta][varchar](30) NOT NULL,
	[Respuesta_Secreta][varchar](30) NOT NULL,
	[Habilitado][bit] NULL
	
	CONSTRAINT [PK_Usuario_Id_Usuario] PRIMARY KEY(Id_Usuario),
	CONSTRAINT UQ_Usuarios_Id_Usuario UNIQUE(Id_Usuario)
)

--Se agrega usuario admin con contraseña "shadea" w23e
INSERT INTO SQL_SERVANT.Usuario(Id_Usuario,Password, Cantidad_Login, Habilitado) 
VALUES ('admin','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 0, 1)

--TABLA ROL
/*
	Tabla que contiene los tipos de roles que existen
	-Id_Rol: es unica
	-Descripcion: descripción
	-Habilitado: Indica si el rol esta habilitado
*/
CREATE TABLE [SQL_SERVANT].[Rol](
	[Id_Rol][Int]IDENTITY(1,1) NOT NULL,
	[Descripcion][varchar](20) NOT NULL,
	[Habilitado][bit] NULL

	CONSTRAINT [PK_Rol_Id_Rol] PRIMARY KEY(Id_Rol),
	CONSTRAINT UQ_Rol_Id_Rol UNIQUE(Id_Rol)
)

INSERT INTO SQL_SERVANT.Rol(Descripcion,Habilitado) VALUES('administrador',1)
INSERT INTO SQL_SERVANT.Rol(Descripcion,Habilitado) VALUES('cliente',1)

--TABLA FUNCIONALIDAD
/*
	Tabla que contiene las funcionalidades del sistema a las que se pueden acceder
	-Id_Funcionalidad: es unica
	-Descripcion: Descripcion de la funcionalidad
*/
CREATE TABLE [SQL_SERVANT].[Funcionalidad](
	[Id_Funcionalidad][Int] NOT NULL,
	[Descripcion][varchar](40) NOT NULL

	CONSTRAINT [PK_Funcionalidad_Id_Funcionalidad] PRIMARY KEY(Id_Funcionalidad),
	CONSTRAINT UQ_Funcionalidad_Id_Funcionalidad UNIQUE(Id_Funcionalidad)
)

INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(1,'ABM de Rol')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(2,'Login y Seguridad')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(3,'ABM de Usuario')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(4,'ABM de Cliente')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(5,'ABM de Cuenta')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(6,'Depositos')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(7,'Retiro de Efectivo')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(8,'Transferencias entre cuentas')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(9,'Facturacion de Costos')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(10,'Consulta de saldos')
INSERT INTO SQL_SERVANT.Funcionalidad(Id_Funcionalidad,Descripcion) VALUES(11,'Listado Estadistico')

--TABLA ROL_FUNCIONALIDAD
/*
	Tabla que relaciona las funcionalidades del sistema, seguún el rol que se tenga
*/
CREATE TABLE [SQL_SERVANT].[Rol_Funcionalidad](
	[Id_Rol][Int] NOT NULL,
	[Id_Funcionalidad][Int] NOT NULL

	CONSTRAINT [PK_Rol_Funcionalidad] PRIMARY KEY (
		[Id_Rol] ASC,
		[Id_Funcionalidad] ASC
	)
	
	CONSTRAINT [FK_Rol_Funcionalidad_Funcionalidad_Id_Funcionalidad] FOREIGN KEY(Id_Funcionalidad)
		REFERENCES [SQL_SERVANT].[Funcionalidad] (Id_Funcionalidad),
	CONSTRAINT [FK_Rol_Funcionalidad_Rol_Id_Rol] FOREIGN KEY(Id_Rol)
		REFERENCES [SQL_SERVANT].[Rol] (Id_Rol),
	CONSTRAINT UQ_Rol_Funcionalidad_Id_Rol_Id_Funcionalidad UNIQUE(Id_Rol,Id_Funcionalidad)
)

INSERT INTO SQL_SERVANT.Rol_Funcionalidad(Id_Rol, Id_Funcionalidad) VALUES (1, 1)
INSERT INTO SQL_SERVANT.Rol_Funcionalidad(Id_Rol, Id_Funcionalidad) VALUES (1, 2)
INSERT INTO SQL_SERVANT.Rol_Funcionalidad(Id_Rol, Id_Funcionalidad) VALUES (1, 3)
INSERT INTO SQL_SERVANT.Rol_Funcionalidad(Id_Rol, Id_Funcionalidad) VALUES (1, 4)
INSERT INTO SQL_SERVANT.Rol_Funcionalidad(Id_Rol, Id_Funcionalidad) VALUES (1, 13)

--TABLA USUARIO_ROL
/*
	Tabla que relaciona a los usuarios con los roles asignados
*/

CREATE TABLE [SQL_SERVANT].[Usuario_Rol](
	[Id_Usuario][varchar](20) NOT NULL,
	[Id_Rol][Int] NOT NULL,
	[Fecha_Creacion][datetime] NULL,
	[Fecha_Ultima_Modificacion] NULL

	CONSTRAINT [PK_Usuario_Rol] PRIMARY KEY(
		[Id_Usuario] ASC,
		[Id_Rol] ASC
	)

	CONSTRAINT [FK_Usuario_Rol_Id_Usuario] FOREIGN KEY (Id_USuario)
		REFERENCES [SQL_SERVANT].[Usuario] (Id_Usuario),
	CONSTRAINT [FK_Usuario_Rol_Id_Rol] FOREIGN KEY (Id_Rol)
		REFERENCES [SQL_SERVANT].[Rol] (Id_Rol)
)

--TABLA CLIENTE
/*
	Tabla con los datos del cliente
*/
CREATE TABLE [SQL_SERVANT].[Cliente](
	[Id_Cliente][Int]IDENTITY(1,1),
	[Nro_Identificacion][Int] NOT NULL,
	[Tipo_Identificacion][Int] NOT NULL,
	[Nombre][varchar](30) NOT NULL,
	[Apellido][varchar](30) NOT NULL

	[Fecha_Creacion][datetime] NULL,
	[Fecha_Ultima_Modificacion] NULL

	CONSTRAINT [FK_Cliente_Tipo_Identificacion] FOREIGN KEY (Tipo_Identificacion)
		REFERENCES [SQL_SERVANT].[Cliente] (Tipo_Identificacion),
	CONSTRAINT [FK_Cliente_Pais] FOREIGN KEY (Id_Pais)
		REFERENCES [SQL_SERVANT].[Pais] (Id_Pais),
	CONSTRAINT UQ_Clieente_Nro_Tipo_Identificacion UNIQUE (Nro_Identificacion, Tipo_Identificacion)
)

--TABLA CLIENTE_DATOS
/*
	Tabla con los datos del cliente

*/
CREATE TABLE [SQL_SERVANT].[Cliente_Datos](
	[Id_Cliente][Int] NOT NULL,
	[Mail][varchar](30) NOT NULL,
	[Id_Pais][Int] NOT NULL,
	[Domicilio][varchar](20) NOT NULL,
	[Calle][varchar](20) NOT NULL,
	[Piso][Int] NULL,
	[Depto][varchar](1) NULL,
	[Localidad][varchar](20) NULL,
	[Id_Nacionalidad][Int] NOT NULL,
	[Fecha_Nacimiento][datetime] NOT NULL

	CONSTRAINT [PK_Cliente_Datos] PRIMARY KEY (
		[Id_Cliente] ASC
	)

	CONSTRAINT [FK_Cliente_Datos_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente),
	CONSTRAINT UQ_Cliente_Datos_Id_Cliente UNIQUE (Id_Cliente)
)

--TABLA USUARIO_CLIENTE
/*
	Tabla que relaciona a los usuarios con su numero de cliente y sus datos como cliente
*/
CREATE TABLE [SQL_SERVANT].[Usuario_Cliente](
	[Id_Usuario][varchar](20) NOT NULL,
	[Id_Cliente][Int] NOT NULL

	CONSTRAINT [PK_Usuario_Cliente] PRIMARY KEY (
		[Id_Usuario] ASC,
		[Id_Cliente] ASC
	)

	CONSTRAINT [FK_Usuario_Cliente_Id_Usuario] FOREIGN KEY (Id_Usuario)
		REFERENCES [SQL_SERVANT].[Usuario] (Id_Usuario),
	CONSTRAINT [FK_Usuario_Cliente_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente)
)

--TODO
--TABLA PAIS

--TODO
--TABLA TIPO-DOCUMENTO

/* NOTA 
	SEGUN EL TP: además de generar (durante el proceso de migración) todos los usuarios
pertenecientes a los clientes que se encuentran en la tabla maestra.
*/

