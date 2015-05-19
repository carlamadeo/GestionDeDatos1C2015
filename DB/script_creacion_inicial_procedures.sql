--PROCEDIMIENTOS PARA LOGIN USUARIO
GO
CREATE PROCEDURE [SQL_SERVANT].[sp_login_check_valid_user](
@p_id varchar(255) = null,
@p_is_valid bit = 0 OUTPUT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Usuario WHERE Id_Usuario = @p_id AND Habilitado = 1)
	BEGIN
		SET @p_is_valid = 1
	END
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_password_check_ok](
@p_id varchar(255) = null,
@p_pass varchar(255) = null,
@p_ok int = 0 OUTPUT
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM SQL_SERVANT.Usuario
		WHERE Id_Usuario = @p_id
		AND Password = @p_pass)
		SET @p_ok = 1
	ELSE
		SET @p_ok = 0
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_password_change](
@p_id varchar(255) = null,
@p_pass varchar(255) = null
)
AS
BEGIN
	UPDATE SQL_SERVANT.Usuario
		SET Password = @p_pass
	WHERE Id_Usuario = @p_id
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_login_check_password](
@p_id varchar(255) = null,
@p_pass varchar(255) = null,
@p_intentos int = 0 OUTPUT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Usuario WHERE Id_Usuario = @p_id AND Password = @p_pass AND Habilitado = 1)
	BEGIN
		UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = 0, Ultima_Fecha = getDate()
		SET @p_intentos = 0
	END
	ELSE
	BEGIN
		Declare @p_intentos_base int
		SELECT @p_intentos_base = Cantidad_Login FROM SQL_SERVANT.Usuario WHERE Id_Usuario = @p_id
		SET @p_intentos = @p_intentos_base + 1

		IF ( @p_intentos >= 3 )
			UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = @p_intentos, Ultima_Fecha = getDate(), Habilitado = 0
		ELSE
			UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = @p_intentos, Ultima_Fecha = getDate()

	END
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_rol_exist_one_by_user](
@p_id varchar(255) = null,
@p_count_rol int = 0 OUTPUT,
@p_id_rol int = 0 OUTPUT,
@p_rol_desc varchar(255) = null OUTPUT
)
AS
BEGIN
	Declare @count_rol int
	SELECT DISTINCT  Id_Usuario, Id_Rol FROM SQL_SERVANT.Usuario_Rol
		WHERE Id_Usuario = @p_id
		AND Habilitado = 1

	SET @count_rol = @@ROWCOUNT

	SET @p_count_rol = @count_rol

	IF ( @count_rol = 1 )
	BEGIN
		SELECT @p_id_rol = ur.Id_Rol, @p_rol_desc = r.Descripcion FROM LA_MAYORIA.Usuario_Rol ur
			INNER JOIN SQL_SERVANT.Rol r ON ur.Id_Rol = r.Id_Rol 
		WHERE ur.Id_Usuario = @p_id 
			AND r.Habilitado = 1
			AND urh.Habilitado = 1
	END
	ELSE
	BEGIN
		SET @p_id_rol = null
		SET @p_rol_desc = null
	END
END
GO

--PROCEDIMIENTO PARA LISTAR MENU
CREATE PROCEDURE [SQL_SERVANT].[sp_menu_list_functionality_by_user](
@p_id_rol int
)
AS
BEGIN
	SELECT fun.Descripcion, fun.Id_Funcionalidad FROM SQL_SERVANT.Funcionalidad fun
	INNER JOIN SQL_SERVANT.Rol_Funcionalidad funR ON fun.Id_Funcionalidad = funR.Id_Funcionalidad 
	WHERE @p_id_rol = funR.Id_Rol

END
GO


--PROCEDIMIENTOS PARA ROL
CREATE PROCEDURE [SQL_SERVANT].[sp_rol_search](
@p_rol_name varchar(255) = null
)
AS
BEGIN
	SELECT DISTINCT
				
		r.Id_Rol 'Id Rol',
		r.Descripcion 'Descripcion',
		r.Habilitado 'Habilitado'
		
		FROM SQL_SERVANT.Rol r

		WHERE
		((@p_rol_name IS NULL) OR (r.Descripcion like @p_rol_name + '%'))
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_rol_enable_disable](
@p_id_rol int,
@p_enable_disable int
)
AS
BEGIN
	UPDATE SQL_SERVANT.Rol SET Habilitado = @p_enable_disable
		WHERE Id_Rol = @p_id_rol
END
GO

CREATE PROCEDURE SQL_SERVANT.[sp_rol_create](
@p_rol_description varchar(255),
@p_id_rol int OUTPUT
)
AS
BEGIN
	IF (@p_id_rol = 0)
	BEGIN
		INSERT INTO SQL_SERVANT.Rol (Descripcion, Habilitado)
			VALUES(@p_rol_description, 1)
		SET @p_id_rol = @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE SQL_SERVANT.Rol SET Descripcion = @p_rol_description
			WHERE Id_Rol = @p_id_rol 
	END
	
END
GO


--PROCEDIMIENTOS PARA ROL Y FUNCIONALIDADES
CREATE PROCEDURE [SQL_SERVANT].[sp_rol_functionality_availability](
@p_id_rol int = null
)
AS
BEGIN
	SELECT DISTINCT
		f.Id_Funcionalidad 'Id Funcionalidad',
		f.Descripcion 'Descripcion'

		FROM SQL_SERVANT.Funcionalidad f
		WHERE NOT EXISTS (SELECT 1 FROM SQL_SERVANT.Rol_Funcionalidad rf
			WHERE f.Id_Funcionalidad = rf.Id_Funcionalidad
			AND rf.Id_Rol = @p_id_rol)
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_rol_functionality_enabled](
@p_id_rol int = null
)
AS
BEGIN
	SELECT DISTINCT
		f.Id_Funcionalidad 'Id Funcionalidad',
		f.Descripcion 'Descripcion'

		FROM SQL_SERVANT.Funcionalidad f
		WHERE EXISTS (SELECT 1 FROM SQL_SERVANT.Rol_Funcionalidad rf
			WHERE f.Id_Funcionalidad = rf.Id_Funcionalidad
			AND rf.Id_Rol = @p_id_rol)
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_rol_functionality_add](
@p_id_rol int = null,
@p_id_functionality int = null
)
AS
BEGIN
	INSERT INTO SQL_SERVANT.Rol_Funcionalidad (Id_Rol, Id_Funcionalidad)
		VALUES (@p_id_rol, @p_id_functionality)
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_rol_functionality_remove](
@p_id_rol int = null,
@p_id_functionality int = null
)
AS
BEGIN
	DELETE FROM SQL_SERVANT.Rol_Funcionalidad WHERE Id_Rol = @p_id_rol AND Id_Funcionalidad = @p_id_functionality
END
GO


--PROCEDIMIENTOS ESTADISTICOS
CREATE PROCEDURE [SQL_SERVANT].[sp_estadistic_top_5_country_movement](
@p_estadistic_from datetime,
@p_estadistic_to datetime
)
AS
BEGIN
	Declare @truncateFrom datetime = CAST(@p_estadistic_from AS DATE)
	Declare @truncateTo datetime = CAST(@p_estadistic_to AS DATE)

	SELECT TOP 5 p.Descripcion 'Pais', SUM(countTrans) 'Cantidad' FROM 
	(
		SELECT p1.Id_Pais, COUNT(p1.Id_Pais) as countTrans FROM SQL_SERVANT.Transferencia t1
			INNER JOIN SQL_SERVANT.Cuenta c1
			ON t1.Id_Cuenta_Origen = c1.Id_Cuenta
			INNER JOIN SQL_SERVANT.Pais p1
			ON c1.Id_Pais_Registro = p1.Id_Pais
			WHERE t1.Fecha_Transferencia BETWEEN @truncateFrom AND @truncateTo 
			GROUP BY p1.Id_Pais
		UNION
		(SELECT p2.Id_Pais, COUNT(p2.Id_Pais) as countTrans FROM SQL_SERVANT.Transferencia t2
			INNER JOIN SQL_SERVANT.Cuenta c2
			ON t2.Id_Cuenta_Origen = c2.Id_Cuenta
			INNER JOIN SQL_SERVANT.Pais p2
			ON c2.Id_Pais_Registro = p2.Id_Pais
			WHERE t2.Fecha_Transferencia BETWEEN @truncateFrom AND @truncateTo
			GROUP BY p2.Id_Pais)
	) t
	INNER JOIN SQL_SERVANT.Pais p
		ON p.Id_Pais = t.Id_Pais
	GROUP BY p.Id_Pais, p.Descripcion
	ORDER BY 2 DESC
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_estadistic_top_5_cli_more_pay_commission](
@p_estadistic_from datetime,
@p_estadistic_to datetime
)
AS
BEGIN
	Declare @truncateFrom datetime = CAST(@p_estadistic_from AS DATE)
	Declare @truncateTo datetime = CAST(@p_estadistic_to AS DATE)

	SELECT TOP 5 f.Id_Cliente 'Id cliente',
	 cd.Nombre 'Nombre', 
	 cd.Apellido 'Apellido', 
	 SUM(f.Importe) 'Facturado' 
	FROM SQL_SERVANT.Facturacion f
	INNER JOIN SQL_SERVANT.Cliente_Datos cd
		ON f.Id_Cliente = cd.Id_Cliente
	WHERE f.Fecha BETWEEN @truncateFrom AND @truncateTo 
	GROUP BY f.Id_Cliente, cd.Nombre, cd.Apellido
	ORDER BY 4 DESC
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_estadistic_top_5_cli_trans_own_count](
@p_estadistic_from datetime,
@p_estadistic_to datetime
)
AS
BEGIN
	Declare @truncateFrom datetime = CAST(@p_estadistic_from AS DATE)
	Declare @truncateTo datetime = CAST(@p_estadistic_to AS DATE)

	SELECT TOP 5 cc1.Id_Cliente 'Id Cliente', 
		cd.Nombre 'Nombre', 
		cd.Apellido 'Apellido', 
		COUNT(*) 'Transferencias'
	FROM SQL_SERVANT.Transferencia t
	INNER JOIN SQL_SERVANT.Cliente_Cuenta cc1
		ON cc1.Id_Cuenta = t.Id_Cuenta_Origen
	INNER JOIN SQL_SERVANT.Cliente_Cuenta cc2
		ON cc2.Id_Cuenta = t.Id_Cuenta_Destino
	INNER JOIN SQL_SERVANT.Cliente_Datos cd
		ON cc1.Id_Cliente = cd.Id_Cliente
	WHERE  cc1.Id_Cliente = cc2.Id_Cliente
		AND t.Fecha_Transferencia BETWEEN @truncateFrom AND @truncateTo 
	GROUP BY cc1.Id_Cliente, cd.Nombre, cd.Apellido
	ORDER BY 4 DESC
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_client_search](
@p_client_name varchar(255) = null,
@p_client_lastname varchar(255) = null,
@p_id_type_identification int = null,
@p_client_identification_number varchar(255) = null,
@p_client_mail varchar(255) = null
)

AS
BEGIN
	SELECT
		
		cd.Id_Cliente 'Id Cliente',
		cd.Nombre 'Nombre',
		cd.Apellido 'Apellido',
		cd.Fecha_Nacimiento 'Nacimiento',
		p.Descripcion 'Pais',
		cd.Mail 'Mail',
		ti.Descripcion 'Tipo Identificacion',
		cl.Nro_Identificacion 'Nro Identificacion',
		cl.Habilitado 'Habilitado'
		
		FROM SQL_SERVANT.Cliente_Datos cd
			INNER JOIN SQL_SERVANT.Cliente cl
				ON cl.Id_Cliente = cl.Id_Cliente
			INNER JOIN SQL_SERVANT.Tipo_Identificacion ti
				ON  cl.Id_Tipo_Identificacion = ti.Id_Tipo_Identificacion
			INNER JOIN SQL_SERVANT.Pais p
				ON cd.Id_Pais = p.Id_Pais

		WHERE
		( (@p_client_name IS NULL) OR (UPPER(cd.Nombre) like UPPER(@p_client_name) + '%'))
		AND ((@p_client_lastname IS NULL) OR (UPPER(cd.Apellido) like UPPER(@p_client_lastname) + '%'))
		AND ((@p_id_type_Identification IS NULL) OR (cl.Id_Tipo_Identificacion = @p_id_type_identification))
		AND ((@p_client_identification_number IS NULL) OR (LTRIM(RTRIM(STR(cl.Nro_Identificacion))) like @p_client_identification_number + '%'))
		AND ((@p_client_mail IS NULL) OR (UPPER(cd.Mail) like UPPER(@p_client_mail) + '%'))

END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_client_enable_disable](
@p_client_id int,
@p_enable_disable int
)
AS
BEGIN
	UPDATE SQL_SERVANT.Cliente SET Habilitado = @p_enable_disable
		WHERE Id_Cliente = @p_client_id
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_client_data_get_by_id_client](
@p_id_client varchar(255)
)
AS
BEGIN
	SELECT 
		c.Id_Cliente 'Id_Cliente',
		uc.Id_Usuario 'Username',
		u.Password 'Password',
		ur.Id_Rol 'Id_Rol',
		r.Descripcion 'Rol',
		u.Pregunta_Secreta 'Pregunta Secreta',
		u.Respuesta_Secreta 'Respuesta Secreta',
		cd.Nombre 'Nombre',
		cd.Apellido 'Apellido',
		ti.Id_Tipo_Identificacion 'Tipo_Identificacion',
		ti.Descripcion 'Identificacion_Descripcion',
		c.Nro_Identificacion 'Nro_Identificacion',
		cd.Mail 'Mail',
		cd.Calle 'Direccion',
		cd.Calle_Nro 'Calle_Nro',
		cd.Piso 'Piso',
		cd.Depto 'Depto',
		cd.Id_Nacionalidad 'Id_Nacionalidad',
		p.Descripcion 'Nacionalidad_Descripcion',
		cd.Fecha_Nacimiento 'Fecha_Nacimiento',
		cd.Localidad 'Localidad',
		cd.Id_Pais 'Id_Pais',
		p.Descripcion 'Pais',
		c.Habilitado 'Habilitado'

	 FROM SQL_SERVANT.Cliente c
		INNER JOIN SQL_SERVANT.Tipo_Identificacion ti
			ON ti.Id_Tipo_Identificacion = c.Id_Tipo_Identificacion
		INNER JOIN SQL_SERVANT.Cliente_Datos cd
			ON cd.Id_Cliente = c.Id_Cliente
		INNER JOIN SQL_SERVANT.Usuario_Cliente uc
			ON uc.Id_Cliente = c.Id_Cliente
		INNER JOIN SQL_SERVANT.Usuario u
			ON u.Id_Usuario = uc.Id_Usuario
		INNER JOIN SQL_SERVANT.Usuario_Rol ur
			ON ur.Id_Usuario = uc.Id_Usuario
		INNER JOIN SQL_SERVANT.Rol r
			ON r.Id_Rol = ur.Id_Rol
		INNER JOIN SQL_SERVANT.Pais p
			ON (p.Id_Pais = cd.Id_Pais) 
		WHERE c.Id_Cliente = @p_id_client
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_client_check_exist_identification](
@p_client_id int = 0,
@p_client_type_identification varchar(255),
@p_client_identification_number int,
@p_isValid bit = 0 OUTPUT
)
AS
BEGIN
	Declare @p_client_type_identification_id int

	SELECT @p_client_type_identification_id = Id_Tipo_Identificacion FROM SQL_SERVANT.Tipo_Identificacion
		WHERE UPPER(LTRIM(RTRIM(Descripcion))) = UPPER(LTRIM(RTRIM(@p_client_type_identification)))

	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Cliente
		WHERE Id_Tipo_Identificacion = @p_client_type_identification_id
			AND Nro_Identificacion = @p_client_identification_number
			AND Id_Cliente != @p_client_id)
		SET @p_isValid = 1
	ELSE
		SET @p_isValid = 0
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_client_check_exist_mail](
@p_client_id int = 0,
@p_client_mail varchar(255),
@p_isValid bit = 0 OUTPUT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Cliente_Datos
		WHERE Mail = @p_client_mail
			AND Id_Cliente != @p_client_id)
		SET @p_isValid = 1
	ELSE
		SET @p_isValid = 0
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_client_save_update](
@p_client_id int = 0 OUTPUT,
@p_client_name varchar(255),
@p_client_lastname varchar(255),
@p_client_type_identification varchar(255),
@p_client_identification_number int,
@p_client_country varchar (100),
@p_client_mail varchar(255),
@p_client_address_name varchar(255),
@p_client_address_number int,
@p_client_address_floor int = null,
@p_client_address_dept varchar(2) = null,
@p_client_localidad varchar(50) = null,
@p_client_nationality varchar(255),
@p_client_birthdate datetime,
@p_user_password varchar(64) = 0,
@p_user_username varchar (20) = 0,
@p_user_secret_question varchar(30) = '',
@p_user_secret_answer varchar(30) = ''
)

AS
BEGIN
	Declare @p_client_type_identification_id int
	Declare @p_client_nationality_id int
	Declare @p_client_country_id int

	SELECT @p_client_nationality_id = Id_Pais FROM SQL_SERVANT.Pais
		WHERE UPPER(LTRIM(RTRIM(Descripcion))) = UPPER(LTRIM(RTRIM(@p_client_nationality)))

	SELECT @p_client_type_identification_id = Id_Tipo_Identificacion FROM SQL_SERVANT.Tipo_Identificacion
		WHERE UPPER(LTRIM(RTRIM(Descripcion))) = UPPER(LTRIM(RTRIM(@p_client_type_identification)))
		
	SELECT @p_client_country_id = Id_Pais FROM SQL_SERVANT.Pais
		WHERE UPPER(LTRIM(RTRIM(Descripcion))) = UPPER(LTRIM(RTRIM(@p_client_country)))

	BEGIN TRANSACTION
		IF ( @p_client_id = 0)
		BEGIN
		
			INSERT INTO SQL_SERVANT.Cliente_Datos (Nombre, Apellido, Mail, Id_Pais, Calle,
				Calle_Nro, Piso, Depto, Localidad, Id_Nacionalidad, Fecha_Nacimiento)
			VALUES (@p_client_name, @p_client_lastname, @p_client_mail, @p_client_country_id, 
			@p_client_address_name, @p_client_address_number, @p_client_address_floor, @p_client_address_dept,
				@p_client_localidad, @p_client_nationality_id, @p_client_birthdate)
				
			SET @p_client_id = @@IDENTITY
				
			INSERT INTO SQL_SERVANT.Cliente (Id_Cliente, Nro_Identificacion, Id_Tipo_Identificacion,
				Habilitado, Fecha_Creacion, Fecha_Ultima_Modificacion)
			VALUES (@p_client_id, @p_client_identification_number, @p_client_type_identification_id,
			1, CAST(GETDATE() AS DATE), CAST(GETDATE() AS DATE))
			
			INSERT INTO SQL_SERVANT.Usuario (Id_Usuario, Password, Cantidad_Login, Pregunta_Secreta,
				Respuesta_Secreta, Habilitado)
			VALUES (@p_user_username, @p_user_password, 0, @p_user_secret_question, @p_user_secret_answer, 1)
		
			INSERT INTO SQL_SERVANT.Usuario_Cliente (Id_Usuario, Id_Cliente)
			VALUES (@p_user_username, @p_client_id)

			INSERT INTO SQL_SERVANT.Usuario_Rol (Id_Usuario, Id_Rol, Habilitado)
			VALUES (@p_user_username, 2, 1)
			
		END
		
		ELSE
		BEGIN
		
			UPDATE SQL_SERVANT.Cliente_Datos SET Nombre = @p_client_name, Apellido = @p_client_lastname, 
				Mail = @p_client_mail, Id_Pais = @p_client_country_id, Calle = @p_client_address_name,
				Calle_Nro = @p_client_address_number, Piso = @p_client_address_floor, Depto = @p_client_address_dept,
				Localidad = @p_client_localidad, Id_Nacionalidad = @p_client_nationality_id, Fecha_Nacimiento = @p_client_birthdate
			WHERE Id_Cliente = @p_client_id
			
			UPDATE SQL_SERVANT.Cliente SET Nro_Identificacion = @p_client_identification_number, 
				Id_Tipo_Identificacion = @p_client_type_identification_id, Fecha_Ultima_Modificacion = CAST(GETDATE() AS DATE)
			
		END
		
	COMMIT TRANSACTION
END
GO
