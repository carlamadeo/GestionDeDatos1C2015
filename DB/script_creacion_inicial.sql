/****** Object:  Schema [SQL_SERVANT]    Script Date: 27/04/2015 23:39:06 ******/

/****************************************************************/
--						CREAR ESQUEMA
/****************************************************************/

CREATE SCHEMA [SQL_SERVANT] AUTHORIZATION [gd]
GO

--FUNCIONES COMPLEMENTARIAS

CREATE FUNCTION SQL_SERVANT.Crear_Nombre_Usuario(
@p_cli_name as varchar(30),
@p_cli_lastname as varchar(30)
)
RETURNS varchar(30)
BEGIN
	declare @p_username varchar(30)
	SET @p_username = SUBSTRING(@p_cli_name,1,1)+@p_cli_lastname
	SET @p_username = LOWER(@p_username)
	SET @p_username = REPLACE(@p_username,'á','a')
	SET @p_username = REPLACE(@p_username,'é','e')
	SET @p_username = REPLACE(@p_username,'í','i')
	SET @p_username = REPLACE(@p_username,'ó','o')
	SET @p_username = REPLACE(@p_username,'ú','u')
	RETURN @p_username
END
GO

CREATE FUNCTION SQL_SERVANT.Es_Fecha_Anterior(
@p_fecha_a_evaluar as datetime,
@p_fecha_a_tener_en_cuenta as datetime
)
RETURNS bit
BEGIN
	declare @truncate_fecha datetime
	declare @retorno_choto bit
	SET @truncate_fecha = CAST(@p_fecha_a_evaluar AS DATE)
	IF(@truncate_fecha < @p_fecha_a_tener_en_cuenta)
		SET @retorno_choto = 0
	ELSE
		SET @retorno_choto = 1
	
	RETURN @retorno_choto
END
GO

/****************************************************************/
-- TABLAS A PRECARGAR


/****************************************************************/

--TABLA TIPO_IDENTIFICACION
/*
	Tabla con la tipificacion de los tipos de documentos validos
*/
CREATE TABLE [SQL_SERVANT].[Tipo_Identificacion](
	[Id_Tipo_Identificacion][Int]IDENTITY(1,1),
	[Descripcion][varchar](20) NOT NULL

	CONSTRAINT [PK_Tipo_Identificacion] PRIMARY KEY (
		[Id_Tipo_Identificacion] ASC
	)
)

SET IDENTITY_INSERT [SQL_SERVANT].Tipo_Identificacion ON
--LLENAR TABLA TIPO_IDENTIFICACION
INSERT INTO SQL_SERVANT.Tipo_Identificacion (Id_Tipo_Identificacion, Descripcion)
SELECT DISTINCT Cli_Tipo_Doc_Cod, Cli_Tipo_Doc_Desc FROM gd_esquema.Maestra
--
SET IDENTITY_INSERT [SQL_SERVANT].Tipo_Identificacion OFF


--TABLA PAIS
/*
	Tabla con la tipificacion de los pais validos para le sistema
*/
CREATE TABLE [SQL_SERVANT].[Pais](
	[Id_Pais][Int]IDENTITY(1,1) NOT NULL,
	[Descripcion][varchar](100) NOT NULL

	CONSTRAINT [PK_Pais] PRIMARY KEY (Id_Pais)
)
--LLENO LA TABLA PAIS

SET IDENTITY_INSERT [SQL_SERVANT].Pais ON

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

SET IDENTITY_INSERT [SQL_SERVANT].Pais OFF

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

	CONSTRAINT [PK_Banco] PRIMARY KEY (Id_Banco)
)

SET IDENTITY_INSERT [SQL_SERVANT].Banco ON

INSERT INTO SQL_SERVANT.Banco (Id_Banco, Nombre, Direccion)
SELECT DISTINCT Banco_Cogido, Banco_Nombre, Banco_Direccion 
	FROM gd_esquema.Maestra WHERE Banco_Nombre IS NOT NULL

SET IDENTITY_INSERT [SQL_SERVANT].Banco OFF

--TABLA TARJETA_EMPRESA
/*
	Tabla con la tipificacion de las empresas que dan tarjetas
*/
CREATE TABLE [SQL_SERVANT].[Tarjeta_Empresa](
	[Id_Tarjeta_Empresa][Int]IDENTITY(1,1) NOT NULL,
	[Descripcion][varchar](50) NOT NULL

	CONSTRAINT [PK_Tarjeta_Empresa] PRIMARY KEY(Id_Tarjeta_Empresa),
	CONSTRAINT UQ_Tarjeta_Empresa_Descripcion UNIQUE (Descripcion)
)
INSERT INTO SQL_SERVANT.Tarjeta_Empresa (Descripcion)
SELECT DISTINCT Tarjeta_Emisor_Descripcion FROM gd_esquema.Maestra WHERE Tarjeta_Emisor_Descripcion IS NOT NULL

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

INSERT INTO SQL_SERVANT.Usuario(Id_Usuario, Password, Cantidad_Login, Pregunta_Secreta, Respuesta_Secreta, Habilitado)
SELECT DISTINCT SQL_SERVANT.Crear_Nombre_Usuario(m.Cli_Nombre, m.Cli_Apellido), 
'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 0, 'default', 'default', 1
FROM gd_esquema.Maestra m

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
	[Fecha_Ultima_Modificacion][datetime] NULL,
	[Habilitado][bit] NOT NULL DEFAULT 1

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
INSERT INTO SQL_SERVANT.Usuario_Rol(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Ultima_Modificacion)
SELECT u.Id_Usuario, r.Id_Rol, CAST(GETDATE() AS DATE), CAST(GETDATE() AS DATE)
FROM SQL_SERVANT.Usuario u
	INNER JOIN SQL_SERVANT.Rol r ON r.Descripcion = 'cliente'

INSERT INTO SQL_SERVANT.Usuario_Rol(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Ultima_Modificacion)
SELECT 'admin', r.Id_Rol, CAST(GETDATE() AS DATE), CAST(GETDATE() AS DATE)
FROM SQL_SERVANT.Rol r
	WHERE r.Descripcion = 'administrador'

--TABLA CLIENTE_DATOS
/*
	Tabla con los datos del cliente

*/
CREATE TABLE [SQL_SERVANT].[Cliente_Datos](
	[Id_Cliente][Int]IDENTITY(1,1) NOT NULL,
	[Nombre][varchar](40) NOT NULL,
	[Apellido][varchar](40) NOT NULL,
	[Mail][varchar](50) NOT NULL,
	[Id_Pais][Int] NOT NULL,
	[Calle][varchar](50) NOT NULL,
	[Calle_Nro][Int] NOT NULL,
	[Piso][Int] NULL,
	[Depto][varchar](3) NULL,
	[Localidad][varchar](50) NULL,
	[Id_Nacionalidad][Int] NOT NULL,
	[Fecha_Nacimiento][datetime] NOT NULL

	CONSTRAINT [PK_Cliente_Datos] PRIMARY KEY (
		[Id_Cliente] ASC
	),
	CONSTRAINT UQ_Cliente_Datos_Mail UNIQUE (Mail)
)
INSERT INTO SQL_SERVANT.Cliente_Datos (Nombre, Apellido, Mail, Id_Pais, Calle, Calle_Nro, Piso, Depto, Localidad, Id_Nacionalidad, Fecha_Nacimiento)
SELECT DISTINCT LTRIM(RTRIM(m.Cli_Nombre)), LTRIM(RTRIM(m.Cli_Apellido)), 
	LTRIM(RTRIM(m.Cli_Mail)), m.Cli_Pais_Codigo, LTRIM(RTRIM(m.Cli_Dom_Calle)), m.Cli_Dom_Nro, m.Cli_Dom_Piso, LTRIM(RTRIM(m.Cli_Dom_Depto)),
null, m.Cli_Pais_Codigo, CAST(m.Cli_Fecha_Nac AS DATE)
FROM gd_esquema.Maestra m

--TABLA CLIENTE
/*
	Tabla con los datos del cliente
*/
CREATE TABLE [SQL_SERVANT].[Cliente](
	[Id_Cliente][Int] NOT NULL,
	--CAMBIAR CUANDO SE LES CANTE A LOS AYUDANTES LARGAR LA DATA
	[Nro_Identificacion][Int] NULL,
	[Id_Tipo_Identificacion][Int] NOT NULL,
	[Habilitado][bit] NOT NULL,
	[Fecha_Creacion][datetime] NULL,
	[Fecha_Ultima_Modificacion][datetime] NULL

	CONSTRAINT [PK_Cliente] PRIMARY KEY (
		[Id_Cliente] ASC
	),
	CONSTRAINT [FK_Cliente_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente_Datos] (Id_Cliente),
	CONSTRAINT UQ_Cliente_Id_Cliente UNIQUE (Id_Cliente),
	CONSTRAINT [FK_Cliente_Tipo_Identificacion] FOREIGN KEY (Id_Tipo_Identificacion)
		REFERENCES [SQL_SERVANT].[Tipo_Identificacion] (Id_Tipo_Identificacion)
)
INSERT INTO SQL_SERVANT.Cliente (Id_Cliente,Id_Tipo_Identificacion, Habilitado, Fecha_Creacion, Fecha_Ultima_Modificacion)
SELECT DISTINCT cd.Id_Cliente, m.Cli_Tipo_Doc_Cod, 1, CAST(GETDATE() AS DATE), CAST(GETDATE() AS DATE)
FROM gd_esquema.Maestra m
	INNER JOIN SQL_SERVANT.Cliente_Datos cd ON 
		cd.Nombre = LTRIM(RTRIM(m.Cli_Nombre))
		AND cd.Apellido = LTRIM(RTRIM(m.Cli_Apellido))
		AND cd.Mail = LTRIM(RTRIM(m.Cli_Mail))

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

INSERT INTO SQL_SERVANT.Usuario_Cliente (Id_Usuario, Id_Cliente)
SELECT u.Id_Usuario, cd.Id_Cliente 
FROM SQL_SERVANT.Cliente_Datos cd
	INNER JOIN SQL_SERVANT.Usuario u
	ON u.Id_Usuario = LTRIM(RTRIM(SQL_SERVANT.Crear_Nombre_Usuario(cd.Nombre, cd.Apellido)))

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
INSERT INTO SQL_SERVANT.Costo_Tipo_Cuenta(Id_Tipo_Cuenta, Costo)
SELECT tc.Id_Tipo_Cuenta, 0.00
FROM SQL_SERVANT.Tipo_Cuenta tc

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
--NOTA VER DE AGREGAR EL VALOR ACTUAL DE CADA CUENTA
CREATE TABLE [SQL_SERVANT].[Cuenta](
	[Id_Cuenta][numeric](18,0) NOT NULL,
	[Id_Pais_Registro][Int] NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Fecha_Creacion][datetime] NOT NULL,
	--Fecha vencimiento valida el periodo de habilitacion de la tarjeta, esto me parece
	--mas acorde que poner un campo habilitado. Igual entra en juego el estado de la cuenta
	--que puede estar habilitada o deshabilitida, pero me da a entender que esto puede ser
	--porque las cuentas se pueden deshabilitar si no se paga su facturacion
	[Fecha_Vencimiento][datetime],
	[Fecha_Cierre][datetime] NULL,
	[Importe][numeric](18,2) NOT NULL DEFAULT 0.00,
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
		REFERENCES [SQL_SERVANT].[Tipo_Cuenta] (Id_Tipo_Cuenta),
	CONSTRAINT [FK_Cuenta_Id_Estado_Cuenta] FOREIGN KEY (Id_Estado_Cuenta)
		REFERENCES [SQL_SERVANT].[Estado_Cuenta] (Id_Estado_Cuenta)
)
INSERT INTO SQL_SERVANT.Cuenta (Id_Cuenta, Id_Pais_Registro, Id_Moneda, Fecha_Creacion, Fecha_Vencimiento, Id_Tipo_Cuenta, Id_Estado_Cuenta)
SELECT DISTINCT m.Cuenta_Numero, m.Cuenta_Pais_Codigo, 
mo.Id_Moneda, m.Cuenta_Fecha_Creacion, 
m.Cuenta_Fecha_Cierre, tc.Id_Tipo_Cuenta, ec.Id_Estado_Cuenta
FROM gd_esquema.Maestra m 
	INNER JOIN SQL_SERVANT.Moneda mo ON 'USD' = mo.Descripcion 
	INNER JOIN SQL_SERVANT.Tipo_Cuenta tc ON 'Gratuita' = tc.Descripcion
	INNER JOIN SQL_SERVANT.Estado_Cuenta ec ON 'Habilitada' = ec.Descripcion
WHERE m.Cuenta_Numero IS NOT NULL

--TABLA CLIENTE_CUENTA
CREATE TABLE [SQL_SERVANT].[Cliente_Cuenta](
	[Id_Cliente][Int] NOT NULL,
	[Id_Cuenta][numeric](18,0) NOT NULL

	CONSTRAINT UQ_Cliente_Cuenta_Id_Cliente_Id_Cuente UNIQUE (Id_Cliente, Id_Cuenta),
	CONSTRAINT [FK_Cliente_Cuenta_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente),
	CONSTRAINT [FK_Cliente_Cuenta_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta)
)
INSERT INTO SQL_SERVANT.Cliente_Cuenta (Id_Cliente, Id_Cuenta)
SELECT DISTINCT uc.Id_Cliente, m.Cuenta_Numero 
FROM gd_esquema.Maestra m
INNER JOIN SQL_SERVANT.Usuario_Cliente uc
	ON LTRIM(RTRIM(SQL_SERVANT.Crear_Nombre_Usuario(m.Cli_Nombre, m.Cli_Apellido))) = uc.Id_Usuario
WHERE m.Cuenta_Numero IS NOT NULL

--TABLA TARJETA
/*
	Tabla con los datos de cada tarjeta
	PD: Si se desvincula una tarjeta, se borra del sistema(?)
*/
CREATE TABLE [SQL_SERVANT].[Tarjeta](
	[Id_Tarjeta][numeric](16,0) NOT NULL,
	[Fecha_Emision][datetime] NOT NULL,
	[Fecha_Vencimiento][datetime] NOT NULL,
	[Id_Tarjeta_Empresa][Int] NOT NULL,
	--es el mismo formato que el de la password de usuario, como tiene que estar encriptado
	--lo vamos a encriptar con el mismo algoritmo de SHA256
	[Codigo_Seguridad][varchar](64) NOT NULL

	CONSTRAINT [PK_Tarjeta] PRIMARY KEY(
		[Id_Tarjeta] ASC
	),
	CONSTRAINT [FK_Tarjeta_Id_Tarjeta_Empresa] FOREIGN KEY (Id_Tarjeta_Empresa)
	REFERENCES [SQL_SERVANT].[Tarjeta_Empresa](Id_Tarjeta_Empresa)
)
INSERT INTO SQL_SERVANT.Tarjeta (Id_Tarjeta, Fecha_Emision, Fecha_Vencimiento, Id_Tarjeta_Empresa, Codigo_Seguridad)
SELECT DISTINCT m.Tarjeta_Numero, m.Tarjeta_Fecha_Emision, m.Tarjeta_Fecha_Vencimiento, te.Id_Tarjeta_Empresa, m.Tarjeta_Codigo_Seg 
FROM gd_esquema.Maestra m
	INNER JOIN SQL_SERVANT.Tarjeta_Empresa te
		ON UPPER(LTRIM(RTRIM(m.Tarjeta_Emisor_Descripcion))) = UPPER(RTRIM(LTRIM(te.Descripcion)))
	WHERE m.Tarjeta_Numero IS NOT NULL

--TABLA CLIENTE_TARJETA
/*
	Tabla con la relacion cliente tarjeta
*/
--NOTA PARA ESTA TARJETA 575702838885428 APARECEN DOS NUMEROS DE CLIENTES DISTINTOS
--POR LAS DUDAS ACA TE PEGO LA QUERY
/*
SELECT DISTINCT uc.Id_Cliente, m.Tarjeta_Numero
FROM gd_esquema.Maestra m
INNER JOIN SQL_SERVANT.Usuario_Cliente uc
	ON LTRIM(RTRIM(SQL_SERVANT.Crear_Nombre_Usuario(m.Cli_Nombre, m.Cli_Apellido))) = uc.Id_Usuario
WHERE m.Tarjeta_Numero IS NOT NULL AND CAST(m.Tarjeta_Numero AS NUMERIC(16,0)) = CAST(575702838885428 AS NUMERIC (16,0))
GROUP BY uc.Id_Cliente, m.Tarjeta_Numero
*/
CREATE TABLE [SQL_SERVANT].[Cliente_Tarjeta](
	[Id_Cliente][Int] NOT NULL,
	[Id_Tarjeta][numeric](16,0) NOT NULL,
	--VER SI HAY QUE MOVER ESTA PROPERTY A LA TABLA DE LA TARJETA MISMA
	[Habilitada][bit] NOT NULL DEFAULT 1

	CONSTRAINT [PK_Cliente_Tarjeta] PRIMARY KEY(
		[Id_Cliente] ASC,
		[Id_Tarjeta] ASC
	),
	CONSTRAINT [FK_Cliente_Tarjeta_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente),
	CONSTRAINT [FK_Cliente_Tarjeta_Id_Tarjeta] FOREIGN KEY (Id_Tarjeta)
		REFERENCES [SQL_SERVANT].[Tarjeta] (Id_Tarjeta)
)
INSERT INTO SQL_SERVANT.Cliente_Tarjeta (Id_Cliente, Id_Tarjeta, Habilitada)
SELECT DISTINCT uc.Id_Cliente, m.Tarjeta_Numero, SQL_SERVANT.Es_Fecha_Anterior(m.Tarjeta_Fecha_Vencimiento, GETDATE())
FROM gd_esquema.Maestra m
INNER JOIN SQL_SERVANT.Usuario_Cliente uc
	ON LTRIM(RTRIM(SQL_SERVANT.Crear_Nombre_Usuario(m.Cli_Nombre, m.Cli_Apellido))) = uc.Id_Usuario
WHERE m.Tarjeta_Numero IS NOT NULL


--TABLA DEPOSITO
/*
	Tabla con los registros de depositos a las cuentas
*/
CREATE TABLE [SQL_SERVANT].[Deposito](
	[Id_Deposito][numeric](18,0)IDENTITY(1,1) NOT NULL,
	--Ver si el id de cuenta tiene el tipo de cuenta
	[Id_Cuenta][numeric](18,0) NOT NULL,
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
SET IDENTITY_INSERT [SQL_SERVANT].Deposito ON

INSERT INTO SQL_SERVANT.Deposito (Id_Deposito, Id_Cuenta, Importe, Id_Moneda, Id_Tarjeta, Fecha_Deposito)
SELECT m.Deposito_Codigo, m.Cuenta_Numero, m.Deposito_Importe, mo.Id_Moneda, m.Tarjeta_Numero, m.Deposito_Fecha 
FROM gd_esquema.Maestra m 
	INNER JOIN SQL_SERVANT.Moneda mo ON 'USD' = mo.Descripcion
WHERE m.Deposito_Codigo IS NOT NULL

SET IDENTITY_INSERT [SQL_SERVANT].Deposito OFF

--UPDATEO LOS DEPOSITOS EN LAS CUENTAS
--MORE INFO : http://stackoverflow.com/questions/11990534/select-aggregate-and-update-sql-server
UPDATE  cuenta
SET     cuenta.Importe = cuenta.Importe + deposito.dep_importe
FROM    SQL_SERVANT.Cuenta as cuenta
JOIN    (
        SELECT  Id_Cuenta, SUM(Importe) as dep_importe
        FROM    SQL_SERVANT.Deposito
        GROUP BY
                Id_Cuenta
        ) AS deposito
ON      cuenta.Id_Cuenta = deposito.Id_Cuenta

--TABLA CHEQUE
/*
	Tabla con los cheques emitidos
*/
CREATE TABLE [SQL_SERVANT].[Cheque](
	[Id_Cheque][Numeric](18,0)IDENTITY(1,1) NOT NULL,
	[Id_Cuenta][Numeric](18,0) NOT NULL,
	[Fecha][datetime] NOT NULL,
	[Importe][Numeric](10,2) NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Id_Banco][Int] NOT NULL

	CONSTRAINT [PK_Cheque] PRIMARY KEY (Id_Cheque),
	CONSTRAINT [FK_Cheque_Id_Banco] FOREIGN KEY (Id_Banco)
		REFERENCES [SQL_SERVANT].[Banco] (Id_Banco),
	CONSTRAINT [FK_Cheque_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Cheque_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda)
)
SET IDENTITY_INSERT [SQL_SERVANT].Cheque ON

INSERT INTO SQL_SERVANT.Cheque (Id_Cheque, Id_Cuenta, Fecha, Importe, Id_Moneda, Id_Banco)
SELECT m.Cheque_Numero, m.Cuenta_Numero, m.Cheque_Fecha, m.Cheque_Importe, mo.Id_Moneda, m.Banco_Cogido 
FROM gd_esquema.Maestra m 
	INNER JOIN SQL_SERVANT.Moneda mo ON 'USD' = mo.Descripcion
WHERE Cheque_Numero IS NOT NULL

SET IDENTITY_INSERT [SQL_SERVANT].Cheque OFF

--TABLA RETIRO
/*
	Tabla con los retiros realizados
*/
CREATE TABLE [SQL_SERVANT].[Retiro](
	[Id_Retiro][Numeric](18,0)IDENTITY(1,1) NOT NULL,
	[Id_Cuenta][Numeric](18,0) NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Importe][Numeric](10,2) NOT NULL,
	[Fecha_Extraccion][datetime] NOT NULL,
	[Id_Banco][Int] NOT NULL

	CONSTRAINT [PK_Retiro] PRIMARY KEY (Id_Retiro),
	CONSTRAINT [FK_Retiro_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Retiro_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda),
	CONSTRAINT [FK_Retiro_Id_Banco] FOREIGN KEY (Id_Banco)
		REFERENCES [SQL_SERVANT].[Banco] (Id_Banco)
)
SET IDENTITY_INSERT [SQL_SERVANT].Retiro ON

INSERT INTO SQL_SERVANT.Retiro (Id_Retiro, Id_Cuenta, Id_Moneda, Importe, Fecha_Extraccion, Id_Banco)
SELECT m.Retiro_Codigo, m.Cuenta_Numero, mo.Id_Moneda, m.Retiro_Importe, m.Retiro_Fecha, m.Banco_Cogido 
FROM gd_esquema.Maestra m 
	INNER JOIN SQL_SERVANT.Moneda mo ON 'USD' = mo.Descripcion
WHERE m.Retiro_Codigo IS NOT NULL

SET IDENTITY_INSERT [SQL_SERVANT].Retiro OFF

--UPDATEO LOS IMPORTES RETIRADOS DE LA CUENTA
UPDATE  cuenta
SET     cuenta.Importe = cuenta.Importe - retiro.ret_importe
FROM    SQL_SERVANT.Cuenta as cuenta
JOIN    (
        SELECT  Id_Cuenta, SUM(Importe) as ret_importe
        FROM    SQL_SERVANT.Retiro
        GROUP BY
                Id_Cuenta
        ) AS retiro
ON      cuenta.Id_Cuenta = retiro.Id_Cuenta

--TABLA CHEQUE-RETIRO
/*
	Tabla con la relacion retiro -> cheque
*/
CREATE TABLE [SQL_SERVANT].[Cheque_Retiro](
	[Id_Retiro][Numeric](18,0) NOT NULL,
	[Id_Cheque][Numeric](18,0) NOT NULL

	CONSTRAINT [PK_Cheque_Retiro] PRIMARY KEY (Id_Retiro, Id_Cheque),
	CONSTRAINT [FK_Cheque_Retiro_Id_Cheque] FOREIGN KEY (Id_Cheque)
		REFERENCES [SQL_SERVANT].[Cheque] (Id_Cheque),
	CONSTRAINT [FK_Cheque_Retiro_Retiro] FOREIGN KEY (Id_Retiro)
		REFERENCES [SQL_SERVANT].[Retiro] (Id_Retiro)
)

INSERT INTO SQL_SERVANT.Cheque_Retiro (Id_Retiro, Id_Cheque)
SELECT DISTINCT m.Retiro_Codigo, m.Cheque_Numero
FROM gd_esquema.Maestra m
WHERE m.Retiro_Codigo IS NOT NULL
	AND m.Cheque_Numero IS NOT NULL


--TABLA TRANSFERENCIAS
/*
	Tabla con todas las transferencias realizadas
*/
CREATE TABLE [SQL_SERVANT].[Transferencia](
	[Id_Transferencia][Int]IDENTITY(1,1) NOT NULL,
	[Id_Cuenta_Origen][numeric](18,0) NOT NULL,
	[Id_Cuenta_Destino][numeric](18,0) NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Importe][Numeric](10,2) NOT NULL,
	[Costo][Numeric](10,2) NOT NULL,
	[Fecha_Transferencia][datetime] NOT NULL

	CONSTRAINT [PK_Transferencia] PRIMARY KEY ([Id_Transferencia] ASC),
	CONSTRAINT [FK_Transferencia_Id_Cuenta_Origen] FOREIGN KEY (Id_Cuenta_Origen)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Transferencia_Id_Cuenta_Destino] FOREIGN KEY (Id_Cuenta_Destino)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Transferencia_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda)
)
INSERT INTO SQL_SERVANT.Transferencia(Id_Cuenta_Origen, Id_Cuenta_Destino, Id_Moneda, Importe, Costo, Fecha_Transferencia)
SELECT m.Cuenta_Numero, m.Cuenta_Dest_Numero, mo.Id_Moneda, m.Trans_Importe, m.Trans_Costo_Trans, m.Transf_Fecha
FROM gd_esquema.Maestra m 
	INNER JOIN SQL_SERVANT.Moneda mo ON 'USD' = mo.Descripcion
WHERE m.Transf_Fecha IS NOT NULL 
	AND m.Factura_Numero IS NULL

--UPDATEO TRANSFERENCIAS A SUS RESPECTIVAS CUENTAS
--ENVIANTE
UPDATE  cuenta
SET     cuenta.Importe = cuenta.Importe - enviante.env_importe
FROM    SQL_SERVANT.Cuenta as cuenta
JOIN    (
        SELECT  Id_Cuenta_Origen, SUM(Importe) as env_importe
        FROM    SQL_SERVANT.Transferencia
        GROUP BY
                Id_Cuenta_Origen
        ) AS enviante
ON      cuenta.Id_Cuenta = enviante.Id_Cuenta_Origen
--RECEPTOR
UPDATE  cuenta
SET     cuenta.Importe = cuenta.Importe + receptor.rep_importe
FROM    SQL_SERVANT.Cuenta as cuenta
JOIN    (
        SELECT  Id_Cuenta_Destino, SUM(Importe) as rep_importe
        FROM    SQL_SERVANT.Transferencia
        GROUP BY
                Id_Cuenta_Destino
        ) AS receptor
ON      cuenta.Id_Cuenta = receptor.Id_Cuenta_Destino

--TABLA FACTURACION_PENDIENTE
/*
	Tabla donde se almacenan temporalmente las facturaciones
*/
CREATE TABLE [SQL_SERVANT].[Facturacion_Pendiente](
	[Id_Facturacion_Pendiente][Int]IDENTITY(1,1) NOT NULL,
	[Id_Cuenta][numeric](18,0) NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Fecha][datetime] NOT NULL,
	[Importe][Numeric](10,2) NOT NULL,
	[Id_Referencia][Int] NOT NULL,
	[Descripcion][varchar](30) NOT NULL

	CONSTRAINT [PK_Facturacion_Pendiente] PRIMARY KEY(Id_Facturacion_Pendiente),
	CONSTRAINT [FK_Facturacion_Pendiente_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Facturacion_Pendiente_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda)
)
INSERT INTO SQL_SERVANT.Facturacion_Pendiente (Id_Cuenta, Id_Moneda, Fecha, Importe, Id_Referencia, Descripcion)
SELECT m.Cuenta_Numero, mo.Id_Moneda, m.Transf_Fecha, m.Trans_Importe, 0, 'Comisión por transferencia.' 
FROM gd_esquema.Maestra m 
INNER JOIN SQL_SERVANT.Moneda mo
	ON mo.Descripcion = 'USD'
WHERE m.Transf_Fecha IS NOT NULL 
	AND m.Factura_Numero IS NULL 
	AND NOT EXISTS(SELECT 1 FROM gd_esquema.Maestra m1
		WHERE
		m.Cuenta_Numero = m1.Cuenta_Numero
		AND m.Cuenta_Dest_Numero = m1.Cuenta_Dest_Numero
		AND m.Transf_Fecha = m1.Transf_Fecha
		AND m.Trans_Importe = m1.Trans_Importe
		AND m1.Factura_Numero IS NOT NULL
	)

CREATE TABLE [SQL_SERVANT].[Facturacion](
	[Id_Factura][Int]IDENTITY(1,1) NOT NULL,
	[Fecha][datetime] NOT NULL,	
	[Id_Cliente][Int] NOT NULL,
	[Id_Cuenta][numeric](18,0) NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Importe][numeric](10,2) NOT NULL

	CONSTRAINT [PK_Facturacion] PRIMARY KEY(Id_Factura),
	CONSTRAINT [FK_Facturacion_Id_Cliente] FOREIGN KEY (Id_Cliente)
		REFERENCES [SQL_SERVANT].[Cliente] (Id_Cliente),
	CONSTRAINT [FK_Facturacion_Id_Cuenta] FOREIGN KEY (Id_Cuenta)
		REFERENCES [SQL_SERVANT].[Cuenta] (Id_Cuenta),
	CONSTRAINT [FK_Facturacion_Id_Moneda] FOREIGN KEY (Id_Moneda)
		REFERENCES [SQL_SERVANT].[Moneda] (Id_Moneda)
)

SET IDENTITY_INSERT SQL_SERVANT.Facturacion ON

INSERT INTO SQL_SERVANT.Facturacion (Id_Factura, Fecha, Id_Cliente, Id_Cuenta, Id_Moneda, Importe)
SELECT m.Factura_Numero, m.Factura_Fecha, cd.Id_Cliente, cc.Id_Cuenta, mo.Id_Moneda, SUM(Item_Factura_Importe)
FROM gd_esquema.Maestra m
	INNER JOIN SQL_SERVANT.Cliente_Datos cd
		ON UPPER(LTRIM(RTRIM(m.Cli_Nombre))) = UPPER(LTRIM(RTRIM(cd.Nombre)))
		AND UPPER(LTRIM(RTRIM(m.Cli_Apellido))) = UPPER(LTRIM(RTRIM(cd.Apellido)))
	INNER JOIN SQL_SERVANT.Moneda mo
		ON mo.Descripcion = 'USD'
	INNER JOIN SQL_SERVANT.Cliente_Cuenta cc
		ON cc.Id_Cuenta = m.Cuenta_Numero
		AND cc.Id_Cliente = cd.Id_Cliente
WHERE m.Factura_Numero IS NOT NULL
GROUP BY m.Factura_Numero, m.Factura_Fecha, cd.Id_Cliente, cc.Id_Cuenta, mo.Id_Moneda

SET IDENTITY_INSERT SQL_SERVANT.Facturacion OFF

CREATE TABLE [SQL_SERVANT].[Facturacion_Item](
	[Id_Factura][Int] NOT NULL,
	[Id_Factura_Item][Int] NOT NULL,
	[Id_Referencia][Int] NOT NULL,
	[Descripcion][varchar](30) NOT NULL,
	[Id_Moneda][Int] NOT NULL,
	[Importe][numeric](10,2) NOT NULL

	CONSTRAINT [FK_Facturacion_Item_Id_Factura] FOREIGN KEY (Id_Factura)
	REFERENCES [SQL_SERVANT].[Facturacion] (Id_Factura)
)

INSERT INTO SQL_SERVANT.Facturacion_Item (Id_Factura, Id_Factura_Item, Id_Referencia, Descripcion, Id_Moneda, Importe)
SELECT m.Factura_Numero, 1, 
t.Id_Transferencia, m.Item_Factura_Descr, mo.Id_Moneda, m.Item_Factura_Importe
FROM gd_esquema.Maestra m
	INNER JOIN SQL_SERVANT.Moneda mo
	ON mo.Descripcion = 'USD'
	INNER JOIN SQL_SERVANT.Transferencia t
	ON t.Id_Cuenta_Origen = m.Cuenta_Numero
	AND t.Id_Cuenta_Destino = m.Cuenta_Dest_Numero
	AND t.Fecha_Transferencia = m.Transf_Fecha
	AND t.Importe = m.Trans_Importe
WHERE Factura_Numero IS NOT NULL

--SE ELIMINAN TRANSACCIONES QUE SE REALIZARON EL MISMO DIA ENTRE MISMO CLIENTES
--QUE VAN A APARECER DOS VECES COMO TRANSFERENCIAS
--Y EN EL INNER JOIN SE DUPLICAN
DELETE FROM SQL_SERVANT.Facturacion_Item WHERE Id_Factura = 15230528 AND Id_Referencia = 33384
DELETE FROM SQL_SERVANT.Facturacion_Item WHERE Id_Factura = 15230529 AND Id_Referencia = 33383
DELETE FROM SQL_SERVANT.Facturacion_Item WHERE Id_Factura = 15231781 AND Id_Referencia = 34338
DELETE FROM SQL_SERVANT.Facturacion_Item WHERE Id_Factura = 15231782 AND Id_Referencia = 34337
DELETE FROM SQL_SERVANT.Facturacion_Item WHERE Id_Factura = 15238589 AND Id_Referencia = 39586
DELETE FROM SQL_SERVANT.Facturacion_Item WHERE Id_Factura = 15238590 AND Id_Referencia = 39585
DELETE FROM SQL_SERVANT.Facturacion_Item WHERE Id_Factura = 15331954 AND Id_Referencia = 111352
DELETE FROM SQL_SERVANT.Facturacion_Item WHERE Id_Factura = 15331955 AND Id_Referencia = 111351

CREATE TABLE [SQL_SERVANT].[Estadistica](
	[Id_Estadistica][int]IDENTITY(1,1) NOT NULL,
	[Store_Procedure][varchar](100) NOT NULL,
	[Descripcion][varchar](100) NOT NULL
)

INSERT INTO SQL_SERVANT.Estadistica (Store_Procedure, Descripcion) VALUES ('sp_estadistic_top_5_cli_count_disabled', 
	'TOP 5 CLIENTES CUE. INH. POR NO PAGAR')
INSERT INTO SQL_SERVANT.Estadistica (Store_Procedure, Descripcion) VALUES ('sp_estadistic_top_5_cli_more_pay_commission', 
	'TOP 5 CLIENTES PAGAR MAS COMISIONES')
INSERT INTO SQL_SERVANT.Estadistica (Store_Procedure, Descripcion) VALUES ('sp_estadistic_top_5_cli_trans_own_count', 
	'TOP 5 CLIENTES TRANSF. CUENTAS PROPIAS')
INSERT INTO SQL_SERVANT.Estadistica (Store_Procedure, Descripcion) VALUES ('sp_estadistic_top_5_country_movement', 
	'TOP 5 PAISES MAYOR MOV. ING. EGR.')
INSERT INTO SQL_SERVANT.Estadistica (Store_Procedure, Descripcion) VALUES ('sp_estadistic_top_5_count_type_payment', 
	'TOP 5 TIPOS DE CUENTAS FACTURADOS')

CREATE TABLE [SQL_SERVANT].[Trimestre](
	[Id_Trimestre][Int]IDENTITY(1,1) NOT NULL,
	[Fechas][varchar](20) NOT NULL,
	[Descripcion][varchar](30) NOT NULL
)

INSERT INTO SQL_SERVANT.Trimestre (Fechas, Descripcion) VALUES ('1,1;31,3','1° TRIMESTRE')
INSERT INTO SQL_SERVANT.Trimestre (Fechas, Descripcion) VALUES ('1,4;30,6','2° TRIMESTRE')
INSERT INTO SQL_SERVANT.Trimestre (Fechas, Descripcion) VALUES ('1,7;30,9','3° TRIMESTRE')
INSERT INTO SQL_SERVANT.Trimestre (Fechas, Descripcion) VALUES ('1,10;31,12','4° TRIMESTRE')

CREATE TABLE [SQL_SERVANT].[Ano](
	[Id_Ano][INT]IDENTITY(1,1) NOT NULL,
	[Ano][Int] NOT NULL

	CONSTRAINT [UQ_Ano_Ano] UNIQUE (Ano)
)

INSERT INTO SQL_SERVANT.Ano (Ano) VALUES (2015)
INSERT INTO SQL_SERVANT.Ano (Ano) VALUES (2016)
INSERT INTO SQL_SERVANT.Ano (Ano) VALUES (2017)

/* NOTA 
	SEGUN EL TP: 
	-además de generar (durante el proceso de migración) todos los usuarios
pertenecientes a los clientes que se encuentran en la tabla maestra.

	-Al momento de generar la factura se deben facturar el total de transacciones pendientes,
teniendo en cuenta el costo de transacción puede variar en el tiempo.
*/

