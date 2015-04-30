/****** Object:  Schema [SQL_SERVANT]    Script Date: 27/04/2015 23:39:06 ******/

/****************************************************************/
--						CREAR ESQUEMA
/****************************************************************/

CREATE SCHEMA [SQL_SERVANT] AUTHORIZATION [gd]
GO

/****************************************************************/
-- TABLAS A PRECARGAR


/****************************************************************/

--TABLA TIPO_IDENTIFICACION
/*
	Tabla con la tipificacion de los tipos de documentos validos
*/
CREATE TABLE [SQL_SERVANT].[Tipo_Identificacion](
	[Id_Tipo_Identificacion][Int],
	[Descripcion][varchar](20) NOT NULL

	CONSTRAINT [PK_Tipo_Identificacion] PRIMARY KEY (
		[Id_Tipo_Identificacion] ASC
	)
)

--LLENAR TABLA TIPO_IDENTIFICACION
INSERT INTO SQL_SERVANT.Tipo_Identificacion (Id_Tipo_Identificacion, Descripcion)
SELECT DISTINCT Cli_Tipo_Doc_Cod, Cli_Tipo_Doc_Desc FROM gd_esquema.Maestra
--

--TABLA PAIS
/*
	Tabla con la tipificacion de los pais validos para le sistema
*/
CREATE TABLE [SQL_SERVANT].[Pais](
	[Id_Pais][Int],
	[Descripcion][varchar](100) NOT NULL

	CONSTRAINT [PK_Pais] PRIMARY KEY (Id_Pais)
)
--LLENO LA TABLA PAIS
--Inserto solo de cliente pais
INSERT INTO SQL_SERVANT.Pais (Id_Pais, Descripcion)
SELECT DISTINCT Cli_Pais_Codigo, UPPER(LTRIM(RTRIM(Cli_Pais_Desc)))
	FROM gd_esquema.Maestra

--verifico los de cuentas en paises
INSERT INTO SQL_SERVANT.Pais (Id_Pais, Descripcion)
SELECT DISTINCT Cuenta_Dest_Pais_Codigo, UPPER(LTRIM(RTRIM(Cuenta_Dest_Pais_Desc)))
	FROM 
		gd_esquema.Maestra m
	WHERE 
		m.Cuenta_Dest_Pais_Codigo IS NOT NULL
		AND m.Cuenta_Dest_Pais_Desc IS NOT NULL
		AND NOT EXISTS (
			SELECT 1 FROM SQL_SERVANT.Pais p 
			WHERE 
				m.Cuenta_Dest_Pais_Codigo = p.Id_Pais
				AND UPPER(LTRIM(RTRIM(m.Cuenta_Dest_Pais_Desc))) = p.Descripcion
			)

--TABLA MONEDA
/*
	Tabla con la tipificacion de las monedas que utilizara el sistema
*/
CREATE TABLE [SQL_SERVANT].[Moneda](
	[Id_Moneda][Int]IDENTITY(1,1) NOT NULL,
	[Descripcion][varchar](20) NOT NULL

	CONSTRAINT [PK_Moneda] PRIMARY KEY (Id_Moneda)
)

INSERT INTO SQL_SERVANT.Moneda(Descripcion) VALUES('USD')

--TABLA BANCO
/*
	Tabla con la tipificacion de los bancos
*/
CREATE TABLE [SQL_SERVANT].[Banco](
	[Id_Banco][Int]IDENTITY(1,1) NOT NULL,
	[Nombre][varchar](60) NOT NULL,
	[Direccion][varchar](100) NOT NULL

	CONSTRAINT [PK_Banco] PRIMARY KEY (Id_Banco),
	CONSTRAINT UQ_Banco_Descripcion UNIQUE (Nombre)
)
--NO HAY BANCOS CARGADOS EN EL SISTEMA A MIGRAR

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
INSERT INTO SQL_SERVANT.Usuario(Id_Usuario,Password, Cantidad_Login, Pregunta_Secreta, Respuesta_Secreta, Habilitado) 
VALUES ('admin','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 0, 'la default', 'la default', 1)

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
	[Habilitado][bit] NOT NULL

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
	),
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
INSERT INTO SQL_SERVANT.Rol_Funcionalidad(Id_Rol, Id_Funcionalidad) VALUES (1, 11)

--TABLA USUARIO_ROL
/*
	Tabla que relaciona a los usuarios con los roles asignados
*/

CREATE TABLE [SQL_SERVANT].[Usuario_Rol](
	[Id_Usuario][varchar](20) NOT NULL,
	[Id_Rol][Int] NOT NULL,
	[Fecha_Creacion][datetime] NULL,
	[Fecha_Ultima_Modificacion][datetime] NULL

	CONSTRAINT [PK_Usuario_Rol] PRIMARY KEY(
		[Id_Usuario] ASC,
		[Id_Rol] ASC
	),
	CONSTRAINT [FK_Usuario_Rol_Id_Usuario] FOREIGN KEY (Id_USuario)
		REFERENCES [SQL_SERVANT].[Usuario] (Id_Usuario),
	CONSTRAINT [FK_Usuario_Rol_Id_Rol] FOREIGN KEY (Id_Rol)
		REFERENCES [SQL_SERVANT].[Rol] (Id_Rol)
)

--CREACION DE USUARIO_ROL para el usuario admin tanto como administrador
--como para cliente
INSERT INTO SQL_SERVANT.Usuario_Rol(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Ultima_Modificacion)
	VALUES ('admin',1,GETDATE(),GETDATE())
INSERT INTO SQL_SERVANT.Usuario_Rol(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Ultima_Modificacion)
	VALUES ('admin',2,GETDATE(),GETDATE())


--TABLA CLIENTE
/*
	Tabla con los datos del cliente
*/
CREATE TABLE [SQL_SERVANT].[Cliente](
	[Id_Cliente][Int]IDENTITY(1,1),
	[Nro_Identificacion][Int] NOT NULL,
	[Id_Tipo_Identificacion][Int] NOT NULL,
	[Habilitado][bit] NOT NULL,
	[Fecha_Creacion][datetime] NULL,
	[Fecha_Ultima_Modificacion][datetime] NULL

	CONSTRAINT [PK_Cliente] PRIMARY KEY (
		[Id_Cliente] ASC
	),
	CONSTRAINT [FK_Cliente_Tipo_Identificacion] FOREIGN KEY (Id_Tipo_Identificacion)
		REFERENCES [SQL_SERVANT].[Tipo_Identificacion] (Id_Tipo_Identificacion),
	CONSTRAINT UQ_Clieente_Nro_Tipo_Identificacion UNIQUE (Nro_Identificacion, Id_Tipo_Identificacion)
)

--TABLA CLIENTE_DATOS
/*
	Tabla con los datos del cliente

*/
CREATE TABLE [SQL_SERVANT].[Cliente_Datos](
	[Id_Cliente][Int] NOT NULL,
	[Nombre][varchar](30) NOT NULL,
	[Apellido][varchar](30) NOT NULL,
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
	),
	CONSTRAINT [FK_Cliente_Datos_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente),
	CONSTRAINT UQ_Cliente_Datos_Id_Cliente UNIQUE (Id_Cliente),
	CONSTRAINT UQ_Cliente_Datos_Mail UNIQUE (Mail)
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
	),
	CONSTRAINT [FK_Usuario_Cliente_Id_Usuario] FOREIGN KEY (Id_Usuario)
		REFERENCES [SQL_SERVANT].[Usuario] (Id_Usuario),
	CONSTRAINT [FK_Usuario_Cliente_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente)
)

--TABLA TIPO_CUENTA
/*
	Tabla con la tipificacion de los tipos de cuentas
*/
CREATE TABLE [SQL_SERVANT].[Tipo_Cuenta](
	[Id_Tipo_Cuenta][Int]IDENTITY(1,1) NOT NULL,
	[Descripcion][varchar](30) NOT NULL

	CONSTRAINT [PK_Tipo_Cuenta] PRIMARY KEY (Id_Tipo_Cuenta)
)

INSERT INTO SQL_SERVANT.Tipo_Cuenta(Descripcion) VALUES('Oro')
INSERT INTO SQL_SERVANT.Tipo_Cuenta(Descripcion) VALUES('Plata')
INSERT INTO SQL_SERVANT.Tipo_Cuenta(Descripcion) VALUES('Bronce')
INSERT INTO SQL_SERVANT.Tipo_Cuenta(Descripcion) VALUES('Gratuita')

--TABLA COSTO_TIPO_CUENTA
--VER SI HACE FALTA AGREGAR LA DE GRATUITA
CREATE TABLE [SQL_SERVANT].[Costo_Tipo_Cuenta](
	[Id_Tipo_Cuenta][Int] NOT NULL,
	[Costo][Numeric](10,2) NOT NULL

	CONSTRAINT UQ_Costo_Tipo_Cuenta UNIQUE (Id_Tipo_Cuenta),
	CONSTRAINT [FK_Costo_Tipo_Cuenta] FOREIGN KEY (Id_Tipo_Cuenta)
		REFERENCES [SQL_SERVANT].[Tipo_Cuenta] (Id_Tipo_Cuenta)
)

--TABLA ESTADO_CUENTA
CREATE TABLE [SQL_SERVANT].[Estado_Cuenta](
	[Id_Estado_Cuenta][Int]IDENTITY(1,1) NOT NULL,
	[Descripcion][varchar](30) NOT NULL

	CONSTRAINT [PK_Estado_Cuenta] PRIMARY KEY (Id_Estado_Cuenta)
)

INSERT INTO SQL_SERVANT.Estado_Cuenta(Descripcion) VALUES('Pendiente de activacion')
INSERT INTO SQL_SERVANT.Estado_Cuenta(Descripcion) VALUES('Cerrada')
INSERT INTO SQL_SERVANT.Estado_Cuenta(Descripcion) VALUES('Inhabilitada')
INSERT INTO SQL_SERVANT.Estado_Cuenta(Descripcion) VALUES('Habilitada')

--TABLA CUENTA
/*
	Tabla que posea los datos de las cuentas de dinero electronico
*/
CREATE TABLE [SQL_SERVANT].[Cuenta](
	[Id_Cuenta][Int] NOT NULL,
	[Id_Pais_Registro][Int] NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Fecha_Apertura][datetime] NOT NULL,
	--Fecha vencimiento valida el periodo de habilitacion de la tarjeta, esto me parece
	--mas acorde que poner un campo habilitado. Igual entra en juego el estado de la cuenta
	--que puede estar habilitada o deshabilitida, pero me da a entender que esto puede ser
	--porque las cuentas se pueden deshabilitar si no se paga su facturacion
	[Fecha_Vencimiento][datetime] NOT NULL,
	[Id_Tipo_Cuenta][Int] NOT NULL,
	[Id_Estado_Cuenta][Int] NOT NULL

	CONSTRAINT [PK_Cuenta] PRIMARY KEY(
		[Id_Cuenta] ASC
	),
	CONSTRAINT [FK_Cuenta_Id_Pais_Registro] FOREIGN KEY (Id_Pais_Registro)
		REFERENCES [SQL_SERVANT].[Pais] (Id_Pais),
	CONSTRAINT [FK_Cuenta_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda),
	CONSTRAINT [FK_Cuenta_Tipo_Cuenta] FOREIGN KEY (Id_Tipo_Cuenta)
		REFERENCES [SQL_SERVANT].[Tipo_Cuenta] (Id_Tipo_Cuenta)
)

--TABLA CLIENTE_CUENTA
CREATE TABLE [SQL_SERVANT].[Cliente_Cuenta](
	[Id_Cliente][Int] NOT NULL,
	[Id_Cuenta][Int] NOT NULL

	CONSTRAINT UQ_Cliente_Cuenta_Id_Cliente_Id_Cuente UNIQUE (Id_Cliente, Id_Cuenta),
	CONSTRAINT [FK_Cliente_Cuenta_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente),
	CONSTRAINT [FK_Cliente_Cuenta_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta)
)

--TABLA TARJETA
/*
	Tabla con los datos de cada tarjeta
	PD: Si se desvincula una tarjeta, se borra del sistema(?)
*/
CREATE TABLE [SQL_SERVANT].[Tarjeta](
	[Id_Tarjeta][numeric](16,0) NOT NULL,
	[Fecha_Emision][datetime] NOT NULL,
	[Fecha_Vencimiento][datetime] NOT NULL,
	--es el mismo formato que el de la password de usuario, como tiene que estar encriptado
	--lo vamos a encriptar con el mismo algoritmo de SHA256
	[Codigo_Seguridad][varchar](64) NOT NULL

	CONSTRAINT [PK_Tarjeta] PRIMARY KEY(
		[Id_Tarjeta] ASC
	)
)

--TABLA CLIENTE_TARJETA
/*
	Tabla con la relacion cliente tarjeta
*/
CREATE TABLE [SQL_SERVANT].[Cliente_Tarjeta](
	[Id_Cliente][Int] NOT NULL,
	[Id_Tarjeta][numeric](16,0) NOT NULL,
	[Habilitada][bit] NOT NULL DEFAULT 1

	CONSTRAINT [PK_Cliente_Tarjeta] PRIMARY KEY(
		[Id_Cliente] ASC,
		[Id_Tarjeta] ASC
	),
	CONSTRAINT UQ_Cliente_Tarjeta_Id_Tarjeta UNIQUE (Id_Tarjeta),
	CONSTRAINT [FK_Cliente_Tarjeta_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente),
	CONSTRAINT [FK_Cliente_Tarjeta_Id_Tarjeta] FOREIGN KEY (Id_Tarjeta)
		REFERENCES [SQL_SERVANT].[Tarjeta] (Id_Tarjeta)
)

--TABLA DEPOSITO
/*
	Tabla con los registros de depositos a las cuentas
*/
CREATE TABLE [SQL_SERVANT].[Deposito](
	[Id_Deposito][Int]IDENTITY(1,1) NOT NULL,
	--Ver si el id de cuenta tiene el tipo de cuenta
	[Id_Cuenta][Int] NOT NULL,
	[Importe][Numeric](10,2) NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Id_Tarjeta][numeric](16,0) NOT NULL,
	[Fecha_Deposito][datetime] NOT NULL

	CONSTRAINT [PK_Deposito] PRIMARY KEY (Id_Deposito),
	CONSTRAINT [FK_Deposito_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Deposito_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda),
	CONSTRAINT [FK_Deposito_Id_Tarjeta] FOREIGN KEY (Id_Tarjeta)
		REFERENCES [SQL_SERVANT].[Tarjeta] (Id_Tarjeta)
)

--TABLA CHEQUE-RETIRO
/*
	Tabla con los retiros de efectivo
*/
CREATE TABLE [SQL_SERVANT].[Cheque_Retiro](
	[Id_Cheque][Int]IDENTITY(1,1) NOT NULL,
	[Id_Cuenta][Int] NOT NULL,
	--EN TEORIA SIEMPRE ES DOLARES PERO POR LAS DUDAS
	[Id_Moneda][Int] NOT NULL,
	[Importe][Numeric](10,2) NOT NULL,
	[Fecha_Extraccion][datetime] NOT NULL,
	[Banco][varchar](30) NOT NULL

	CONSTRAINT [PK_Cheque_Retiro] PRIMARY KEY (Id_Cheque),
	CONSTRAINT [FK_Cheque_Retiro_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Cheque_Retiro_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda)
)

--TABLA TRANSFERENCIAS
/*
	Tabla con todas las transferencias realizadas
*/
CREATE TABLE [SQL_SERVANT].[Transferencia](
	[Id_Transferencia][Int]IDENTITY(1,1) NOT NULL,
	[Id_Cuenta_Origen][Int] NOT NULL,
	[Id_Cuenta_Destino][Int] NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Importe][Numeric](10,2) NOT NULL,
	[Fecha_Transferencia][datetime] NOT NULL

	CONSTRAINT [PK_Transferencia] PRIMARY KEY ([Id_Transferencia] ASC),
	CONSTRAINT [FK_Transferencia_Id_Cuenta_Origen] FOREIGN KEY (Id_Cuenta_Origen)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Transferencia_Id_Cuenta_Destino] FOREIGN KEY (Id_Cuenta_Destino)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Transferencia_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda)
)

--TABLA FACTURACION_PENDIENTE
/*
	Tabla donde se almacenan temporalmente las facturaciones
*/
CREATE TABLE [SQL_SERVANT].[Facturacion_Pendiente](
	[Id_Facturacion_Pendiente][Int]IDENTITY(1,1) NOT NULL,
	[Id_Cuenta][Int] NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Fecha][datetime] NOT NULL,
	[Importe][Numeric](10,2) NOT NULL,
	[Descripcion][varchar](20) NOT NULL

	CONSTRAINT [PK_Facturacion_Pendiente] PRIMARY KEY(Id_Facturacion_Pendiente),
	CONSTRAINT [FK_Facturacion_Pendiente_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Facturacion_Pendiente_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda)
)

CREATE TABLE [SQL_SERVANT].[Facturacion](
	[Id_Factura][Int]IDENTITY(1,1) NOT NULL,
	[Fecha][datetime] NOT NULL,
	[Id_Cliente][Int] NOT NULL,
	[Id_Cuenta][Int] NOT NULL,
	[Id_Referencia][Int] NOT NULL,
	[Descripcion][Int] NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Importe][Int] NOT NULL

	CONSTRAINT [PK_Facturacion] PRIMARY KEY(Id_Factura),
	CONSTRAINT [FK_Facturacion_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente),
	CONSTRAINT [FK_Facturacion_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Facturacion_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda)
)

/* NOTA 
	SEGUN EL TP: 
	-además de generar (durante el proceso de migración) todos los usuarios
pertenecientes a los clientes que se encuentran en la tabla maestra.

	-Al momento de generar la factura se deben facturar el total de transacciones pendientes,
teniendo en cuenta el costo de transacción puede variar en el tiempo.
*/

