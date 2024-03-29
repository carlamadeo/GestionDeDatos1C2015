--**PROCEDIMIENTOS PARA LOGIN USUARIO**--

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

CREATE PROCEDURE [SQL_SERVANT].[sp_login_get_answer_question_secret](
@p_id as varchar(255) = null,
@p_question as varchar(255) = null OUTPUT,
@p_answer as varchar(255) = null OUTPUT
)
AS
BEGIN
	SELECT @p_question = Pregunta_Secreta, @p_answer = CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', Respuesta_Secreta))
	FROM SQL_SERVANT.Usuario
	WHERE Id_Usuario = @p_id
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
@p_pass varchar(255) = null,
@p_question varchar(255) = null,
@p_answer varchar(255) = null
)
AS
BEGIN
	IF @p_pass IS NOT NULL
	BEGIN
	UPDATE SQL_SERVANT.Usuario
		SET Password = @p_pass
	WHERE Id_Usuario = @p_id
	END
	
	UPDATE SQL_SERVANT.Usuario
		SET Pregunta_Secreta = @p_question,
		Respuesta_Secreta = EncryptByPassPhrase('SQL SERVANT', @p_answer)
	WHERE Id_Usuario = @p_id
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_login_check_password](
@p_id varchar(255) = null,
@p_pass varchar(255) = null,
@p_intentos int = 0 OUTPUT,
@p_correcto bit = 0 OUTPUT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Usuario WHERE Id_Usuario = @p_id AND Password = @p_pass AND Habilitado = 1)
	BEGIN
		UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = 0, Ultima_Fecha = getDate()
		WHERE Id_Usuario = @p_id
		SET @p_intentos = 0
	END
	ELSE
	BEGIN
		Declare @p_intentos_base int
		SET @p_correcto = 0
		SELECT @p_intentos_base = Cantidad_Login FROM SQL_SERVANT.Usuario WHERE Id_Usuario = @p_id
		SET @p_intentos = @p_intentos_base + 1

		IF ( @p_intentos = 3 )
			UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = @p_intentos, Ultima_Fecha = getDate(), Habilitado = 0
			WHERE Id_Usuario = @p_id
		ELSE IF ( @p_intentos < 3 )
			UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = @p_intentos, Ultima_Fecha = getDate()
			WHERE Id_Usuario = @p_id
	END
	
	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Usuario WHERE Id_Usuario = @p_id AND Password = @p_pass)
		SET @p_correcto = 1
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_save_auditoria_login](
@p_id varchar(20),
@p_is_correcto bit
)
AS
BEGIN

	Declare @cantidad_intentos_fallidos int

	SELECT @cantidad_intentos_fallidos = COUNT (Id_Usuario) FROM SQL_SERVANT.Auditoria_Login al
	WHERE al.Intento_Correcto = 0 AND al.Id_Usuario = @p_id
	
	IF (@p_is_correcto = 0)
		SET @cantidad_intentos_fallidos = @cantidad_intentos_fallidos + 1

	INSERT INTO SQL_SERVANT.Auditoria_Login (Id_Usuario, Fecha, Intento_Correcto, Cantidad_Fallidos)
	VALUES (@p_id, CAST(GETDATE() AS DATE), @p_is_correcto, @cantidad_intentos_fallidos)
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_check_user_is_admin](
@p_id_usuario varchar(20),
@p_is_admin int = 0 OUTPUT
)
AS
BEGIN

	SELECT @p_is_admin = COUNT (ur.Id_Rol) FROM SQL_SERVANT.Usuario_Rol ur
	WHERE ur.Id_Usuario = @p_id_usuario AND ur.Id_Rol = 1
	
END
GO

CREATE PROCEDURE [SQL_SERVANT].[check_and_update_accounts](
@p_today datetime
)
AS
BEGIN
	UPDATE SQL_SERVANT.Cuenta SET Id_Estado_Cuenta = 
	(SELECT Id_Estado_Cuenta FROM SQL_SERVANT.Estado_Cuenta WHERE Descripcion = 'Inhabilitada')
	WHERE (@p_today NOT BETWEEN Fecha_Creacion AND Fecha_Vencimiento)
	
	UPDATE SQL_SERVANT.Cuenta SET Id_Estado_Cuenta = 
	(SELECT Id_Estado_Cuenta FROM SQL_SERVANT.Estado_Cuenta WHERE Descripcion = 'Habilitada')
	WHERE (@p_today BETWEEN Fecha_Creacion AND Fecha_Vencimiento)
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
	SELECT DISTINCT  ur.Id_Usuario, ur.Id_Rol FROM SQL_SERVANT.Usuario_Rol ur
		INNER JOIN SQL_SERVANT.Rol r ON ur.Id_Rol = r.Id_Rol
		WHERE ur.Id_Usuario = @p_id
		AND r.Habilitado = 1

	SET @count_rol = @@ROWCOUNT

	SET @p_count_rol = @count_rol

	IF ( @count_rol = 1 )
	BEGIN
		SELECT @p_id_rol = ur.Id_Rol, @p_rol_desc = r.Descripcion FROM SQL_SERVANT.Usuario_Rol ur
			INNER JOIN SQL_SERVANT.Rol r ON ur.Id_Rol = r.Id_Rol 
		WHERE ur.Id_Usuario = @p_id 
			AND r.Habilitado = 1
			AND ur.Habilitado = 1
	END
	ELSE
	BEGIN
		SET @p_id_rol = null
		SET @p_rol_desc = null
	END
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_rol_is_enabled](
@p_rol_id int = 0,
@p_isEnabled bit = 0 OUTPUT
)
AS
BEGIN

	SELECT @p_isEnabled = Habilitado FROM SQL_SERVANT.Rol
	WHERE Id_Rol = @p_rol_id
END
GO

--**PROCEDIMIENTO PARA LISTAR MENU**--

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


--**PROCEDIMIENTOS PARA ROL**--

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

CREATE PROCEDURE SQL_SERVANT.[sp_rol_create](
@p_rol_description varchar(255),
@p_rol_habilitado bit,
@p_rol_funcionalidad varchar(40),
@p_id_rol int OUTPUT
)
AS
BEGIN	
	
	IF (@p_id_rol = 0)
	BEGIN
		Declare @p_id_funcionalidad int
		
		SELECT @p_id_funcionalidad = f.Id_Funcionalidad
		FROM SQL_SERVANT.Funcionalidad f
		WHERE f.Descripcion = @p_rol_funcionalidad
		
		INSERT INTO SQL_SERVANT.Rol (Descripcion, Habilitado)
			VALUES(@p_rol_description, @p_rol_habilitado)
		SET @p_id_rol = @@IDENTITY
		
		INSERT INTO SQL_SERVANT.Rol_Funcionalidad (Id_Rol, Id_Funcionalidad)
			VALUES (@p_id_rol, @p_id_funcionalidad)
		
	END
	ELSE
	BEGIN
		UPDATE SQL_SERVANT.Rol SET Descripcion = @p_rol_description, Habilitado = @p_rol_habilitado
			WHERE Id_Rol = @p_id_rol 
	END
	
END
GO


--**PROCEDIMIENTOS PARA ROL Y FUNCIONALIDADES**--
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

--**PROCEDIMIENTOS DE USUARIOS**--

CREATE PROCEDURE [SQL_SERVANT].[sp_user_search](
@p_user_name varchar(255) = null,
@p_id_rol int = null
)
AS
BEGIN
	SELECT DISTINCT
				
		u.Id_Usuario 'Usuario',
		u.Ultima_Fecha 'Ultimo Login',
		u.Fecha_Creacion 'Fecha Creacion',
		u.Ultima_Modificacion 'Ultima Modificacion',
		r.Id_Rol 'Id Rol',
		r.Descripcion 'Rol',
		u.Habilitado 'Habilitado'
		
		FROM SQL_SERVANT.Usuario u
			INNER JOIN SQL_SERVANT.Usuario_Rol ur
				ON u.Id_Usuario = ur.Id_Usuario
			INNER JOIN SQL_SERVANT.Rol r
				ON ur.Id_Rol = r.Id_Rol

		WHERE
		((@p_id_rol IS NULL) OR ( ur.Id_Rol = @p_id_rol))
		AND  ((@p_user_name IS NULL) OR (u.Id_Usuario like @p_user_name + '%'))
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_user_enable_disable](
@p_user_name varchar(255),
@p_id_rol int,
@p_enable_disable int,
@p_time datetime
)
AS
BEGIN
	UPDATE SQL_SERVANT.Usuario_Rol SET Habilitado = @p_enable_disable,
		Fecha_Ultima_Modificacion = @p_time
		WHERE Id_Usuario = @p_user_name
		AND Id_Rol = @p_id_rol
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_user_get_by_user](
@p_user_name varchar(255)
)
AS
BEGIN
	SELECT 
	Id_Usuario,
	Password,
	Cantidad_Login,
	Ultima_Fecha,
	Fecha_Creacion,
	Ultima_Modificacion,
	Pregunta_Secreta,
	CONVERT(varchar(50),DecryptByPassphrase ('SQL SERVANT', Respuesta_Secreta)),
	Habilitado 
	FROM SQL_SERVANT.Usuario u
		WHERE u.Id_Usuario = @p_user_name
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_user_clean_login](
@p_user_name varchar(255)
)
AS
BEGIN
	UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = 0
	WHERE Id_Usuario = @p_user_name
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_user_save_update](
@p_user_name varchar(255),
@p_password varchar(255) = null,
@p_user_question_secret varchar(255) = null,
@p_user_answer_secret varchar(255) = null,
@p_user_creation_date datetime,
@p_user_modify_date datetime,
@p_enabled bit
)
AS
BEGIN
	IF ( EXISTS(SELECT 1 FROM SQL_SERVANT.Usuario WHERE Id_Usuario = @p_user_name))
	BEGIN
		IF (@p_password IS NULL)		
			UPDATE SQL_SERVANT.Usuario SET 
			Fecha_Creacion = @p_user_creation_date,
			Ultima_Modificacion = @p_user_modify_date,
			Pregunta_Secreta = @p_user_question_secret,
			Respuesta_Secreta = EncryptByPassPhrase('SQL SERVANT', @p_user_answer_secret),
			Habilitado = @p_enabled
			WHERE Id_Usuario = @p_user_name
		ELSE
			UPDATE SQL_SERVANT.Usuario SET Password = @p_password,
			Fecha_Creacion = @p_user_creation_date,
			Ultima_Modificacion = @p_user_modify_date,
			Pregunta_Secreta = @p_user_question_secret,
			Respuesta_Secreta = EncryptByPassPhrase('SQL SERVANT', @p_user_answer_secret),
			Habilitado = @p_enabled
			WHERE Id_Usuario = @p_user_name
	END
	ELSE
	BEGIN
		INSERT INTO SQL_SERVANT.Usuario (Id_Usuario, Password, Cantidad_Login, Ultima_Fecha, Fecha_Creacion, Ultima_Modificacion,
		Pregunta_Secreta, Respuesta_Secreta, Habilitado)
		VALUES (@p_user_name, @p_password, 0, null, @p_user_creation_date, @p_user_modify_date, @p_user_question_secret, 
		EncryptByPassPhrase('SQL SERVANT', @p_user_answer_secret),@p_enabled)
	END
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_user_get_not_user_rol](
@p_user_name varchar(255) = null
)
AS
BEGIN
	SELECT r.Id_Rol, r.Descripcion FROM SQL_SERVANT.Rol r
	WHERE NOT EXISTS(SELECT 1 FROM SQL_SERVANT.Usuario_Rol ur
		WHERE ur.Id_Usuario = @p_user_name AND ur.Id_Rol = r.Id_Rol
		AND ur.Habilitado = 1)
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_user_get_user_rol](
@p_user_name varchar(255) = null
)
AS
BEGIN
	SELECT r.Id_Rol, r.Descripcion FROM SQL_SERVANT.Rol r
	WHERE EXISTS(SELECT 1 FROM SQL_SERVANT.Usuario_Rol ur
		WHERE ur.Id_Usuario = @p_user_name AND ur.Id_Rol = r.Id_Rol
		AND ur.Habilitado = 1)
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_user_rol_add](
@p_user_name varchar(255) = null,
@p_rol_id int,
@p_date datetime
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM SQL_SERVANT.Usuario_Rol ur WHERE ur.Id_Usuario = @p_user_name
		AND ur.Id_Rol = @p_rol_id)
		UPDATE SQL_SERVANT.Usuario_Rol SET Habilitado = 1, Fecha_Ultima_Modificacion = @p_date
		WHERE Id_Usuario = @p_user_name AND Id_Rol = @p_rol_id
	ELSE
		INSERT INTO SQL_SERVANT.Usuario_Rol (Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Ultima_Modificacion, Habilitado)
		VALUES (@p_user_name, @p_rol_id, @p_date, @p_date, 1)
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_user_rol_remove](
@p_user_name varchar(255) = null,
@p_rol_id int,
@p_date datetime
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM SQL_SERVANT.Usuario_Rol ur WHERE ur.Id_Usuario = @p_user_name
		AND ur.Id_Rol = @p_rol_id)
		UPDATE SQL_SERVANT.Usuario_Rol SET Habilitado = 0, Fecha_Ultima_Modificacion = @p_date
		WHERE Id_Usuario = @p_user_name AND Id_Rol = @p_rol_id
	ELSE
		INSERT INTO SQL_SERVANT.Usuario_Rol (Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Ultima_Modificacion, Habilitado)
		VALUES (@p_user_name, @p_rol_id, @p_date, @p_date, 0)
END
GO

--**PROCEDIMIENTOS CONSULTA DE SALDO**--

CREATE PROCEDURE [SQL_SERVANT].[sp_consulta_check_account](
@p_id_cuenta varchar(255) = null,
@p_is_valid bit = 0 OUTPUT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Cuenta WHERE Id_Cuenta = @p_id_cuenta )
	BEGIN
		SET @p_is_valid = 1
	END
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_consulta_saldo](
@p_consulta_cuenta varchar(255) = null
)
AS
BEGIN
	select Importe from SQL_SERVANT.Cuenta
	where Id_Cuenta = @p_consulta_cuenta
	
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_consulta_last_5_deposits](
@p_consulta_cuenta varchar(255) = null
)
AS
BEGIN
	select top 5 t1.Id_Deposito, t1.Importe, CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', t1.Id_Tarjeta)), t1.Fecha_Deposito
 	from SQL_SERVANT.Deposito t1
	where Id_Cuenta = @p_consulta_cuenta
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_consulta_last_5_withdrawal](
@p_consulta_cuenta varchar(255) = null
)
AS
BEGIN
	select top 5 t1.Id_Retiro, t1.Importe, t1.Id_Banco, t1.Fecha_Extraccion
 	from SQL_SERVANT.Retiro t1
	where Id_Cuenta = @p_consulta_cuenta
	
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_consulta_last_10_transfers](
@p_consulta_cuenta varchar(255) = null
)
AS
BEGIN
	select top 10 Id_Transferencia ,Id_Cuenta_Destino, Importe, Costo, Fecha_Transferencia
	from SQL_SERVANT.Transferencia
	where Id_Cuenta_Origen = @p_consulta_cuenta
	
END
GO

--**PROCEDIMIENTOS ESTADISTICOS**--

CREATE PROCEDURE [SQL_SERVANT].[sp_estadistic_top_5_client_disable_account](
@p_estadistic_from datetime,
@p_estadistic_to datetime
)
AS
BEGIN
	Declare @truncateFrom datetime = CAST(@p_estadistic_from AS DATE)
	Declare @truncateTo datetime = CAST(@p_estadistic_to AS DATE)

	SELECT TOP 5 
		l.Id_Cliente 'Id cliente',
	 	cd.Nombre 'Nombre', 
	 	cd.Apellido 'Apellido', 
	 	COUNT(*) 'Cantidad'
 	FROM SQL_SERVANT.Log l
 	INNER JOIN SQL_SERVANT.Motivo_Log ml
 		ON l.Id_Motivo = ml.Id_Motivo
	INNER JOIN SQL_SERVANT.Cliente_Datos cd
		ON l.Id_Cliente = cd.Id_Cliente
	WHERE l.Fecha BETWEEN @truncateFrom AND @truncateTo 
	AND ml.Descripcion = UPPER('INHABILITACION CUENTA POR NO PAGO')
	GROUP BY l.Id_Cliente, cd.Nombre, cd.Apellido
	ORDER BY 4 DESC
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_estadistic_top_5_count_type_payment](
@p_estadistic_from datetime,
@p_estadistic_to datetime
)
AS
BEGIN
	Declare @truncateFrom datetime = CAST(@p_estadistic_from AS DATE)
	Declare @truncateTo datetime = CAST(@p_estadistic_to AS DATE)

	SELECT TOP 5 
	tc.Id_Tipo_Cuenta 'Id Tipo Cuenta',
	tc.Descripcion 'Tipo Cuenta',
	SUM(fi.Importe) 'Monto Facturado'
	FROM SQL_SERVANT.Facturacion_Item fi
	INNER JOIN SQL_SERVANT.Facturacion f
		ON fi.Id_Factura = f.Id_Factura
	INNER JOIN SQL_SERVANT.Cuenta c
		ON c.Id_Cuenta = fi.Id_Cuenta
	INNER JOIN SQL_SERVANT.Tipo_Cuenta tc
		ON tc.Id_Tipo_Cuenta = c.Id_Tipo_Cuenta
	WHERE f.Fecha BETWEEN @truncateFrom AND @truncateTo
	GROUP BY tc.Id_Tipo_Cuenta, tc.Descripcion
	ORDER BY 3 DESC
END
GO

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

--**PROCEDIMIENTOS DE CUENTAS**--

CREATE PROCEDURE [SQL_SERVANT].[sp_account_search](
@p_account_lastname varchar(255) = null,
@p_account_type_id int = null,
@p_account_client_id int = null
)
AS
BEGIN
	SELECT DISTINCT 
	cd.Id_Cliente 'Id',
	cd.Nombre 'Nombre',
	cd.Apellido 'Apellido',
	cc.Id_Cuenta 'Cuenta',
	tp.Descripcion 'Tipo',
	cu.Fecha_Creacion 'Creacion',
	cu.Fecha_Vencimiento 'Vencimiento',
	ec.Descripcion 'Estado',
	mo.Descripcion 'Moneda',
	pa.Descripcion 'Pais'
	FROM SQL_SERVANT.Cliente c
		INNER JOIN SQL_SERVANT.Cliente_Datos cd
			ON cd.Id_Cliente = c.Id_Cliente
		INNER JOIN SQL_SERVANT.Cliente_Cuenta cc
			ON c.Id_Cliente = cc.Id_Cliente
		INNER JOIN SQL_SERVANT.Cuenta cu
			ON cc.Id_Cuenta = cu.Id_Cuenta
		INNER JOIN SQL_SERVANT.Tipo_Cuenta tp
			ON cu.Id_Tipo_Cuenta = tp.Id_Tipo_Cuenta
		INNER JOIN SQL_SERVANT.Estado_Cuenta ec
			ON cu.Id_Estado_Cuenta = ec.Id_Estado_Cuenta
		INNER JOIN SQL_SERVANT.Moneda mo
			ON mo.Id_Moneda = cu.Id_Moneda
		INNER JOIN SQL_SERVANT.Pais pa
			ON cu.Id_Pais_Registro = pa.Id_Pais
		WHERE
		((@p_account_type_id IS NULL) OR ( tp.Id_Tipo_Cuenta = @p_account_type_id))
		AND  ((@p_account_lastname IS NULL) OR (UPPER(cd.Apellido) like UPPER('%' + LTRIM(RTRIM(@p_account_lastname)) + '%')))
		AND ((@p_account_client_id IS NULL) OR (c.Id_Cliente = @p_account_client_id))
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_account_save_update](
@p_account_id numeric(18,0) = null,
@p_account_client_id int,
@p_account_country_id int,
@p_account_type_account_id int,
@p_account_currency_id int,
@p_account_date datetime
)
AS
BEGIN
	BEGIN TRANSACTION
		declare @type_account_day int
		SELECT @type_account_day = ptc.Dias FROM SQL_SERVANT.Periodo_Tipo_Cuenta ptc WHERE ptc.Id_Tipo_Cuenta = @p_account_type_account_id
		IF (@p_account_id IS NOT NULL)
		BEGIN
			declare @actual_type_account int
			SELECT @actual_type_account = c.Id_Tipo_Cuenta FROM SQL_SERVANT.Cuenta c WHERE c.Id_Cuenta = @p_account_id
			IF (@actual_type_account = @p_account_type_account_id)
				UPDATE SQL_SERVANT.Cuenta 
					SET Id_Pais_Registro = @p_account_country_id,
						Id_Moneda = @p_account_currency_id
					WHERE	Id_Cuenta = @p_account_id
			ELSE
			BEGIN
				UPDATE SQL_SERVANT.Cuenta 
					SET Id_Pais_Registro = @p_account_country_id,
						Id_Moneda = @p_account_currency_id,
						Id_Tipo_Cuenta = @p_account_type_account_id,
						Fecha_Vencimiento = DATEADD(DAY, @type_account_day, Fecha_Creacion)
					WHERE	Id_Cuenta = @p_account_id

				INSERT INTO SQL_SERVANT.Facturacion_Pendiente (Id_Cuenta, Fecha, Importe, Id_Tipo_Item)
				SELECT @p_account_id, @p_account_date, ctc.Costo_Apertura, ti.Id_Tipo_Item
				FROM SQL_SERVANT.Costo_Tipo_Cuenta ctc, SQL_SERVANT.Tipo_Item ti
					WHERE ctc.Id_Tipo_Cuenta = @p_account_type_account_id
					AND ti.Descripcion = 'Modificacion de cuenta.'
			END
		END
		ELSE
		BEGIN
			
			INSERT INTO SQL_SERVANT.Cuenta (Id_Pais_Registro, Id_Moneda, Fecha_Creacion, Fecha_Vencimiento, Importe,
			Id_Tipo_Cuenta, Id_Estado_Cuenta)
			VALUES(@p_account_country_id, @p_account_currency_id, @p_account_date, DATEADD(DAY, @type_account_day,									@p_account_date), 0.00, @p_account_type_account_id, 1)
			
			Declare @account_id numeric(18,0)
			SET @account_id = @@IDENTITY
			
			INSERT INTO SQL_SERVANT.Cliente_Cuenta (Id_Cliente, Id_Cuenta)
			VALUES (@p_account_client_id, @account_id)

			IF((SELECT Id_Tipo_Cuenta FROM Tipo_Cuenta WHERE Descripcion = 'Gratuita') <> @p_account_type_account_id)
			INSERT INTO SQL_SERVANT.Facturacion_Pendiente (Id_Cuenta, Fecha, Importe, Id_Tipo_Item)
			SELECT @account_id, @p_account_date, ctc.Costo_Apertura, ti.Id_Tipo_Item
			FROM SQL_SERVANT.Costo_Tipo_Cuenta ctc, SQL_SERVANT.Tipo_Item ti
				WHERE ctc.Id_Tipo_Cuenta = @p_account_type_account_id
				AND ti.Descripcion = 'Apertura de cuenta.'
		END
	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_account_get_data](
@p_account_id numeric(18,0),
@p_account_client_id varchar(255)
)
AS
BEGIN
	SELECT TOP 1
		pa.Id_Pais "Id_Pais",
		pa.Descripcion "Pais",
		mo.Id_Moneda "Id_Moneda",
		mo.Descripcion "Moneda",
		tc.Id_Tipo_Cuenta "Id_Tipo_Cuenta",
		tc.Descripcion "Tipo_Cuenta",
		cu.Fecha_Creacion "Fecha_Creacion",
		cu.Fecha_Vencimiento "Fecha_Vencimiento"

		FROM SQL_SERVANT.Cliente_Cuenta cc
			INNER JOIN SQL_SERVANT.Cuenta cu
				ON cc.Id_Cuenta = cu.Id_Cuenta
			INNER JOIN SQL_SERVANT.Pais pa
				ON pa.Id_Pais = cu.Id_Pais_Registro
			INNER JOIN SQL_SERVANT.Moneda mo
				ON mo.Id_Moneda = cu.Id_Moneda
			INNER JOIN SQL_SERVANT.Tipo_Cuenta tc
				ON tc.Id_Tipo_Cuenta = cu.Id_Tipo_Cuenta
			WHERE cc.Id_Cliente = @p_account_client_id
				AND cc.Id_Cuenta = @p_account_id
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_account_enabled_with_credit_search](
@p_account_client_id int,
@p_account_today datetime
)
AS
BEGIN
	SELECT 
		cc.Id_Cuenta 'Nro Cuenta',
		pa.Descripcion 'Pais',
		mo.Descripcion 'Moneda',
		cu.Importe 'Importe',
		cu.Fecha_Vencimiento 'Fecha Vencimiento'
	FROM SQL_SERVANT.Cuenta cu
	INNER JOIN SQL_SERVANT.Cliente_Cuenta cc
		ON cu.Id_Cuenta = cc.Id_Cuenta
	INNER JOIN SQL_SERVANT.Estado_Cuenta ec
		ON cu.Id_Estado_Cuenta = ec.Id_Estado_Cuenta
	INNER JOIN SQL_SERVANT.Pais pa
		ON cu.Id_Pais_Registro = pa.Id_Pais
	INNER JOIN SQL_SERVANT.Moneda mo
		ON mo.Id_Moneda = cu.Id_Moneda
	WHERE ec.Descripcion = 'Habilitada'
	AND cc.Id_Cliente = @p_account_client_id
	--AND cu.Fecha_Creacion <= @p_account_today
	--AND cu.Fecha_Vencimiento <= @p_account_today
	AND cu.Importe > 0.00
	AND UPPER(mo.Descripcion) = UPPER('USD')
END
GO

--**PROCEDIMIENTOS DE CLIENTES**--

--SE OBTIENEN LOS DATOS DEL CLIENTE A TRAVES DEL APELLIDO
CREATE PROCEDURE [SQL_SERVANT].[sp_client_search_by_lastname](
@p_client_lastname varchar(255) = null
)
AS
BEGIN
	SELECT
		cd.Id_Cliente 'Id Cliente',
		cd.Nombre 'Nombre',
		cd.Apellido 'Apellido'
		FROM SQL_SERVANT.Cliente_Datos cd
			INNER JOIN SQL_SERVANT.Cliente cl
				ON cd.Id_Cliente = cl.Id_Cliente

		WHERE
		((@p_client_lastname IS NULL) OR (UPPER(cd.Apellido) like UPPER(LTRIM(RTRIM(@p_client_lastname))) + '%'))
END
GO

--A TRAVES DE CIERTOS PARAMETROS SE OBTIENEN LOS DATOS DEL CLIENTE
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
				ON cd.Id_Cliente = cl.Id_Cliente
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

--SE ACTUALIZA EL ESTADO DEL CLIENTE EN HABILITADO O DESHABILITADO
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

--A TRAVES DEL ID DEL CLIENTE SE OBTIENEN SUS DATOS
CREATE PROCEDURE [SQL_SERVANT].[sp_client_data_get_by_id_client](
@p_id_client varchar(255)
)
AS
BEGIN
	SELECT 
		c.Id_Cliente 'Id_Cliente',
		uc.Id_Usuario 'Username',
		u.Password 'Password',
		u.Pregunta_Secreta 'Pregunta Secreta',
		CONVERT(varchar(50),DecryptByPassphrase ('SQL SERVANT', u.Respuesta_Secreta)) 'Respuesta Secreta',
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
		INNER JOIN SQL_SERVANT.Pais p
			ON (p.Id_Pais = cd.Id_Pais) 
		WHERE c.Id_Cliente = @p_id_client
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_client_nro_identity_is_valid](
@p_client_id int,
@p_client_identity_id int,
@p_is_valid bit = 0 OUTPUT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Cliente
	WHERE Id_Cliente = @p_client_id
		AND Nro_Identificacion = @p_client_identity_id)
		SET @p_is_valid = 1
	ELSE
		SET @p_is_valid = 0
END
GO

--SE VERIFICA QUE EL NUMERO Y TIPO DE INDENTIFICACION INGRESADO
--NO SE ENCUENTRA EN LA BASE DE DATOS 
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

--SE VERIFICA QUE EL MAIL INGRESADO NO SE ENCUENTRA EN LA
--BASE DE DATOS
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

--SE INGRESAN LOS DATOS DEL CLIENTE, EN CASO DE QUE EL ID DE CLIENTE SEA 0
--SE CREA UN NUEVO CLIENTE, SI ES DISTINTO DE 0 SE ACTUALIZAN LOS DATOS
--DEL CLIENTE 
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
@p_user_password varchar(64),
@p_user_username varchar (20),
@p_user_secret_question varchar(30),
@p_user_secret_answer varchar(30)
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
			
			INSERT INTO SQL_SERVANT.Usuario (Id_Usuario, Password, Cantidad_Login, Fecha_Creacion, Pregunta_Secreta,
				Respuesta_Secreta, Habilitado)
			VALUES (@p_user_username, @p_user_password, 0, CAST(GETDATE() AS DATE), @p_user_secret_question, 
			EncryptByPassPhrase('SQL SERVANT', @p_user_secret_answer), 1)
		
			INSERT INTO SQL_SERVANT.Usuario_Cliente (Id_Usuario, Id_Cliente)
			VALUES (@p_user_username, @p_client_id)

			INSERT INTO SQL_SERVANT.Usuario_Rol (Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Ultima_Modificacion, Habilitado)
			VALUES (@p_user_username, 2, CAST(GETDATE() AS DATE), CAST(GETDATE() AS DATE), 1)
			
		END
		
		ELSE
		BEGIN
		
			UPDATE SQL_SERVANT.Cliente_Datos SET Nombre = @p_client_name, Apellido = @p_client_lastname, 
				Mail = @p_client_mail, Id_Pais = @p_client_country_id, Calle = @p_client_address_name,
				Calle_Nro = @p_client_address_number, Piso = @p_client_address_floor, Depto = @p_client_address_dept,
				Localidad = @p_client_localidad, Id_Nacionalidad = @p_client_nationality_id, 
				Fecha_Nacimiento = @p_client_birthdate
			WHERE Id_Cliente = @p_client_id
			
			UPDATE SQL_SERVANT.Cliente SET Nro_Identificacion = @p_client_identification_number, 
				Id_Tipo_Identificacion = @p_client_type_identification_id, Fecha_Ultima_Modificacion = CAST(GETDATE() AS DATE)
			
		END
		
	COMMIT TRANSACTION
END
GO

--A TRAVES DEL ID DE CLIENTE SE OBTIENE SI EL MISMO SE ENCUENTRA HABILITADO
CREATE PROCEDURE [SQL_SERVANT].[sp_client_is_enabled](
@p_client_id int = 0,
@p_isEnabled bit = 0 OUTPUT
)
AS
BEGIN

	SELECT @p_isEnabled = Habilitado FROM SQL_SERVANT.Cliente
	WHERE Id_Cliente = @p_client_id
END
GO

--SE OBTIENE EL ID DE CLIENTE A PARTIR DEL ID DE USUARIO
CREATE PROCEDURE [SQL_SERVANT].[sp_client_get_by_user](
@p_client_id int = 0 OUTPUT,
@p_user_id varchar(20)
)

AS
BEGIN

	SELECT @p_client_id = Id_Cliente FROM Usuario_Cliente 
	WHERE Id_Usuario = @p_user_id
END
GO

--**PROCEDIMIENTOS DE TARJETAS**-

CREATE PROCEDURE [SQL_SERVANT].[sp_card_get_data](
@p_card_id varchar(16)
)
AS
BEGIN
	SELECT
		te.Descripcion 'Tarjeta_Descripcion',
		t.Id_Tarjeta_Empresa 'Id_Tarjeta_Empresa',
		t.Fecha_Emision 'Fecha_Emision',
		t.Fecha_Vencimiento 'Fecha_Vencimiento',
		t.Codigo_Seguridad 'Codigo_Seguridad'

	FROM SQL_SERVANT.Tarjeta t
		INNER JOIN SQL_SERVANT.Tarjeta_Empresa te
			ON t.Id_Tarjeta_Empresa = te.Id_Tarjeta_Empresa
	WHERE CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', t.Id_Tarjeta)) = @p_card_id
END
GO

--SE DESHABILITA LA TARJETA DE CREDITO INGRESADA
CREATE PROCEDURE [SQL_SERVANT].[sp_client_tarjeta_disable](
@p_client_id int,
@p_tarjeta_id varchar(16)
)
AS
BEGIN
	UPDATE SQL_SERVANT.Cliente_Tarjeta SET Habilitada = 0
		WHERE Id_Cliente = @p_client_id AND CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', Id_Tarjeta)) = @p_tarjeta_id
END
GO

--SE OBTIENEN LOS DATOS DE UNA TARJETA A TRAVES DEL ID DEL CLIENTE
CREATE PROCEDURE [SQL_SERVANT].[sp_client_tarjeta_get_by_id_client](
@p_id_client varchar(255),
@p_id_tarjeta varchar(16)
)
AS
BEGIN
	SELECT
		t.Id_Tarjeta_Empresa 'Id_Empresa',
		te.Descripcion 'Empresa',
		t.Fecha_Emision 'Fecha_Emision',
		t.Fecha_Vencimiento 'Fecha_Vencimiento',
		t.Codigo_Seguridad 'Codigo_Seguridad'

	 FROM SQL_SERVANT.Cliente_Tarjeta ct
		INNER JOIN SQL_SERVANT.Tarjeta t
			ON CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', ct.Id_Tarjeta)) = CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', t.Id_tarjeta))
		INNER JOIN SQL_SERVANT.Tarjeta_Empresa te
			ON t.Id_Tarjeta_Empresa = te.Id_Tarjeta_Empresa	
	WHERE ct.Id_Cliente = @p_id_client AND
	CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', ct.Id_Tarjeta)) = @p_Id_tarjeta
END
GO

--SE OBTIENEN LOS DATOS DE TODAS LAS TARJETAS QUE POSEA EL CLIENTE
CREATE PROCEDURE [SQL_SERVANT].[sp_card_by_client_id](
@p_card_client_id int
)
AS
BEGIN
	SELECT
		CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', t.Id_Tarjeta)) AS Numero,
		te.Descripcion 'Empresa',
		t.Fecha_Emision "Fecha Emision",
		t.Fecha_Vencimiento "Fecha Vencimiento",
		CASE ct.Habilitada WHEN 1 THEN 'ASOCIADA' ELSE 'DESASOCIADA' END AS "Estado"

		FROM SQL_SERVANT.Cliente_Tarjeta ct
			INNER JOIN SQL_SERVANT.Tarjeta t
				ON CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', ct.Id_Tarjeta)) = CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', t.Id_Tarjeta))
			INNER JOIN SQL_SERVANT.Tarjeta_Empresa te
				ON t.Id_Tarjeta_Empresa = te.Id_Tarjeta_Empresa
		WHERE ct.Id_Cliente = @p_card_client_id
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_card_associate](
@p_card_id varchar(16),
@p_card_client_id int,
@p_card_associate bit
)
AS
BEGIN
	UPDATE SQL_SERVANT.Cliente_Tarjeta 
		SET Habilitada = @p_card_associate
	WHERE Id_Cliente = @p_card_client_id AND CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', Id_Tarjeta)) = @p_card_id
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_card_save_with_association](
@p_card_id varchar(16),
@p_card_client_id int,
@p_card_company_id int,
@p_card_creation_date datetime,
@p_card_expiration_date datetime,
@p_card_security_code varchar(64),
@p_card_date_to_considerate datetime
)
AS
BEGIN
	BEGIN TRANSACTION
	
		Declare @encrypted_card varbinary(100)
		SET @encrypted_card = EncryptByPassPhrase('SQL SERVANT', @p_card_id)
		
		INSERT INTO SQL_SERVANT.Tarjeta (Id_Tarjeta, Fecha_Emision, Fecha_Vencimiento, Id_Tarjeta_Empresa, Codigo_Seguridad)
		VALUES (@encrypted_card, @p_card_creation_date, @p_card_expiration_date, @p_card_company_id, @p_card_security_code)

		INSERT INTO SQL_SERVANT.Cliente_Tarjeta (Id_Cliente, Id_Tarjeta, Habilitada)
		VALUES (@p_card_client_id, @encrypted_card, SQL_SERVANT.Validar_Tarjeta_Habilitacion(@p_card_id, @p_card_expiration_date, @p_card_date_to_considerate))
	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_card_exist](
@p_card_id varchar(16),
@p_is_valid bit = 0 OUTPUT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Tarjeta WHERE (CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', Id_Tarjeta)) = @p_card_id))
	BEGIN
		SET @p_is_valid = 1
	END
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_tarjeta_save](
@p_tarjeta_id varchar(16) OUTPUT,
@p_tarjeta_empresa varchar(50),
@p_tarjeta_fecha_vencimiento datetime,
@p_tarjeta_fecha_emision datetime,
@p_tarjeta_codigo_seguridad varchar(64)
)

AS
BEGIN
	Declare @p_tarjeta_empresa_id int

	SELECT @p_tarjeta_empresa_id = Id_Tarjeta_Empresa FROM SQL_SERVANT.Tarjeta_Empresa
		WHERE UPPER(LTRIM(RTRIM(Descripcion))) = UPPER(LTRIM(RTRIM(@p_tarjeta_empresa)))

	BEGIN TRANSACTION
		
			UPDATE SQL_SERVANT.Tarjeta SET Fecha_Emision = @p_tarjeta_fecha_emision, 
			Fecha_Vencimiento = @p_tarjeta_fecha_vencimiento, Id_Tarjeta_Empresa = @p_tarjeta_empresa_id,
			Codigo_Seguridad = @p_tarjeta_codigo_seguridad
			WHERE CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', Id_Tarjeta)) = @p_tarjeta_id
		
	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_card_enabled_search](
@p_card_client_id int,
@p_card_today datetime
)
AS
BEGIN
	SELECT 
		CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', t.Id_Tarjeta)) 'Nro Tarjeta', 
		t.Fecha_Vencimiento 'Fecha Vencimiento',
		te.Descripcion 'Empresa'
	FROM SQL_SERVANT.Tarjeta t
	INNER JOIN SQL_SERVANT.Cliente_Tarjeta ct
		ON CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', t.Id_Tarjeta)) = CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', ct.Id_Tarjeta))
	INNER JOIN SQL_SERVANT.Tarjeta_Empresa te
		ON t.Id_Tarjeta_Empresa = te.Id_Tarjeta_Empresa
	WHERE ct.Id_Cliente = @p_card_client_id
		AND	t.Fecha_Emision <= @p_card_today
		AND t.Fecha_Vencimiento <= @p_card_today
		AND ct.Habilitada = 1
END
GO

--SE VERIFICA SI LA TARJETA DE CREDITO SE ENCUENTRA VENCIDA
CREATE PROCEDURE [SQL_SERVANT].[sp_tarjeta_not_expired](
@p_tarjeta_id varchar(16) = 0,
@p_today datetime,
@p_notExpired bit = 1 OUTPUT
)
AS
BEGIN

	Declare @exist int
	
	SELECT @exist = 1 FROM SQL_SERVANT.Tarjeta t
	WHERE CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', Id_Tarjeta)) = @p_tarjeta_id
	AND (@p_today BETWEEN DATEADD(DAY, -1, t.Fecha_Emision) AND DATEADD(DAY, 1, t.Fecha_Vencimiento))

	IF (@exist IS NOT NULL)
		SET @p_notExpired = 1
	ELSE
		SET @p_notExpired = 0
END
GO

--**PROCEDIMIENTOS DE RETIROS**--

CREATE PROCEDURE [SQL_SERVANT].[sp_retirement_generate_extraction](
@p_retirement_client_id int,
@p_retirement_account_id numeric(18,0),
@p_retirement_amount numeric(18,2),
@p_retirement_bank_id int,
@p_retirement_today datetime,
@p_retirement_currency varchar(3),
@p_retirement_check_number numeric(18,0) = 0 OUTPUT
)
AS
BEGIN
	declare @Id_Cheque numeric(18,0)
	declare @Id_Retiro numeric(18,0)
	BEGIN TRANSACTION
		INSERT INTO SQL_SERVANT.Cheque (Id_Cuenta, Fecha, Importe, Id_Moneda, Id_Banco)
		SELECT @p_retirement_account_id, @p_retirement_today, @p_retirement_amount, mo.Id_Moneda, @p_retirement_bank_id
			FROM SQL_SERVANT.Cuenta cu
				INNER JOIN SQL_SERVANT.Moneda mo
					ON UPPER(mo.Descripcion) = UPPER(LTRIM(RTRIM(@p_retirement_currency)))
			WHERE cu.Id_Cuenta = @p_retirement_account_id

		SET @Id_Cheque = @@IDENTITY

		INSERT INTO SQL_SERVANT.Retiro(Id_Cuenta, Id_Moneda, Importe, Fecha_Extraccion, Id_Banco)
		SELECT @p_retirement_account_id, mo.Id_Moneda, @p_retirement_amount, @p_retirement_today, @p_retirement_bank_id
			FROM SQL_SERVANT.Moneda mo
			WHERE UPPER(mo.Descripcion) = UPPER(LTRIM(RTRIM(@p_retirement_currency)))
		SET @Id_Retiro = @@IDENTITY

		UPDATE SQL_SERVANT.Cuenta SET Importe = Importe - @p_retirement_amount
			WHERE Id_Cuenta = @p_retirement_account_id

		INSERT INTO SQL_SERVANT.Cheque_Retiro (Id_Retiro, Id_Cheque) VALUES (@Id_Retiro, @Id_Cheque)

		SET @p_retirement_check_number = @Id_Cheque
	COMMIT TRANSACTION
END
GO

--**PROCEDIMIENTOS DE DEPOSITOS**--

--SE GUARDA EL DEPOSITO REALIZADO Y SE ACTUALIZA EL IMPORTE DE LA CUENTA
--DONDE SE DEPOSITO
CREATE PROCEDURE [SQL_SERVANT].[sp_save_deposito](
@p_deposito_cuenta numeric(18,0),
@p_deposito_importe numeric(10,2),
@p_deposito_moneda int,
@p_deposito_tarjeta varchar(16),
@p_deposito_fecha datetime
)
AS
BEGIN

	BEGIN TRANSACTION

		Declare @importe_actual numeric(18,2)
		Declare @numero_tarjeta_encrypted varbinary(100)
		
		SELECT @importe_actual = Importe FROM SQL_SERVANT.Cuenta c
		WHERE @p_deposito_cuenta = c.Id_Cuenta
		
		SELECT @numero_tarjeta_encrypted = Id_Tarjeta FROM SQL_SERVANT.Tarjeta
		WHERE CONVERT(varchar(50),DecryptByPassphrase ('SQL SERVANT', Id_Tarjeta)) = @p_deposito_tarjeta
		
		INSERT INTO SQL_SERVANT.Deposito (Id_Cuenta, Importe, Id_Moneda, Id_Tarjeta, Fecha_Deposito)
		VALUES (@p_deposito_cuenta, @p_deposito_importe, @p_deposito_moneda, @numero_tarjeta_encrypted, @p_deposito_fecha)
		
		UPDATE SQL_SERVANT.Cuenta SET Importe = (@p_deposito_importe + @importe_actual)
		WHERE @p_deposito_cuenta = SQL_SERVANT.Cuenta.Id_Cuenta
		
	COMMIT TRANSACTION
END
GO

--**PROCEDIMIENTOS DE TRANSFERENCIAS**--

--SE VERIFICA CUAL ES EL IMPORTE MAXIMO QUE SE PUEDE
--TRANSFERIR/RETIRAR POR CUENTA
CREATE PROCEDURE [SQL_SERVANT].[sp_get_importe_maximo_por_cuenta](
@p_cuenta_id numeric(18,0) = 0,
@p_importe_maximo numeric(18,2) = 0 OUTPUT
)
AS
BEGIN

	SELECT @p_importe_maximo = Importe FROM SQL_SERVANT.Cuenta
	WHERE Id_Cuenta = @p_cuenta_id
	
	IF (@p_importe_maximo < 0)
		SET @p_importe_maximo = 0
	
END
GO

--SE GUARDA LA TRANSFERENCIA REALIZADA, SE ACTUALIZAN LOS IMPORTES
--DE AMBAS CUENTAS Y SE GENERA UNA FACTURACION PENDIENTE
CREATE PROCEDURE [SQL_SERVANT].[sp_save_transferencia](
@p_transferencia_origen numeric(18,0),
@p_transferencia_destino numeric(18,0),
@p_transferencia_monto numeric(18,2),
@p_transferencia_moneda int,
@p_tranferencia_fecha datetime,
@p_tranferencia_mismo_cliente bit
)
AS
BEGIN

	BEGIN TRANSACTION
		Declare @p_importe_actual_origen numeric(18,2)
		Declare @p_importe_actual_destino numeric(18,2)
		Declare @p_importe_final_origen numeric(18,2)
		Declare @p_importe_final_destino numeric(18,2)
		Declare @p_transferencia_costo numeric(10,2)
		Declare @p_id_transferencia int
		Declare @p_mismo_cliente bit
		
		SELECT @p_importe_actual_origen = Importe FROM SQL_SERVANT.Cuenta c
		WHERE @p_transferencia_origen = c.Id_Cuenta
		
		SELECT @p_importe_actual_destino = Importe FROM SQL_SERVANT.Cuenta c
		WHERE @p_transferencia_destino = c.Id_Cuenta
		
		SELECT @p_transferencia_costo = Costo_Transferencia FROM SQL_SERVANT.Costo_Tipo_Cuenta ctc
		INNER JOIN SQL_SERVANT.Cuenta c ON ctc.Id_Tipo_Cuenta = c.Id_Tipo_Cuenta
		WHERE c.Id_Cuenta = @p_transferencia_origen
		
		INSERT INTO SQL_SERVANT.Transferencia (Id_Cuenta_Origen, Id_Cuenta_Destino, Id_Moneda, 
		Importe, Costo,	Fecha_Transferencia)
		VALUES (@p_transferencia_origen, @p_transferencia_destino, @p_transferencia_moneda, @p_transferencia_monto, 
		@p_transferencia_costo, @p_tranferencia_fecha)
		
		SET @p_id_transferencia = @@IDENTITY
		
		SET @p_importe_final_origen = @p_importe_actual_origen - @p_transferencia_monto
		SET @p_importe_final_destino = @p_importe_actual_destino + @p_transferencia_monto
		
		UPDATE SQL_SERVANT.Cuenta SET Importe = (@p_importe_final_origen)
		WHERE @p_transferencia_origen = SQL_SERVANT.Cuenta.Id_Cuenta
		
		UPDATE SQL_SERVANT.Cuenta SET Importe = (@p_importe_final_destino)
		WHERE @p_transferencia_destino = SQL_SERVANT.Cuenta.Id_Cuenta
		
		IF (@p_tranferencia_mismo_cliente = 0)
		BEGIN
			Declare @tipo_item int
			
			SELECT @tipo_item = Id_Tipo_Item FROM SQL_SERVANT.Tipo_Item WHERE Descripcion = ('Comisi�n por transferencia.')
			
			INSERT INTO SQL_SERVANT.Facturacion_Pendiente (Id_Cuenta, Fecha, Importe, Id_Tipo_Item, Id_Referencia)
			VALUES (@p_transferencia_origen, @p_tranferencia_fecha, 
			@p_transferencia_costo, @tipo_item, @p_id_transferencia)
		END
	COMMIT TRANSACTION
END
GO

--SE INGRESAN 2 CUENTAS Y SE DEVUELVE 1 SI LAS MISMAS PERTENECEN
--AL MISMO CLIENTE, 0 SI NO
CREATE PROCEDURE [SQL_SERVANT].[sp_check_account_same_client](
@p_primer_cuenta numeric(18,0),
@p_segunda_cuenta numeric(18,0),
@p_isSameClient bit = 0 OUTPUT
)
AS
BEGIN

	Declare @p_primer_cliente int
	Declare @p_segundo_cliente int
	
	SELECT @p_primer_cliente = Id_Cliente FROM SQL_SERVANT.Cliente_Cuenta cc
	WHERE cc.Id_Cuenta = @p_primer_cuenta
	
	SELECT @p_segundo_cliente = Id_Cliente FROM SQL_SERVANT.Cliente_Cuenta cc
	WHERE cc.Id_Cuenta = @p_segunda_cuenta
	
	IF @p_primer_cliente = @p_segundo_cliente
	SET @p_isSameClient = 1
	
END
GO	

--**PROCEDIMIENTOS DE FACTURACION**--

CREATE PROCEDURE [SQL_SERVANT].[sp_remove_pending_facturacion](
@p_facturacion_pendiente_id int
)
AS
BEGIN
	DELETE FROM SQL_SERVANT.Facturacion_Pendiente 
	WHERE Id_Facturacion_Pendiente = @p_facturacion_pendiente_id
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_create_factura](
@p_id_cliente int,
@p_fecha datetime,
@p_id_tarjeta varchar(16),
@p_importe numeric (10,2),
@p_id_factura int = 0 OUTPUT
)
AS
BEGIN

	Declare @id_tarjeta_enc varbinary(100)
	
	SELECT @id_tarjeta_enc = Id_Tarjeta FROM SQL_SERVANT.Tarjeta
	WHERE @p_id_tarjeta = CONVERT(varchar(50),DecryptByPassphrase ('SQL SERVANT', Id_Tarjeta))
	
	INSERT INTO SQL_SERVANT.Facturacion VALUES
	(@p_id_cliente, @p_fecha, @id_tarjeta_enc, @p_importe)
	
	SET @p_id_factura = @@IDENTITY

END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_create_facturacion_item](
@p_id_factura int,
@p_id_cuenta numeric(18,0),
@p_descripcion_gasto varchar(255),
@p_importe numeric(10,2),
@p_cantidad_suscripciones int,
@p_id_referencia int
)
AS
BEGIN
	Declare @id_tipo_item int
	
	SELECT @id_tipo_item = Id_Tipo_Item FROM SQL_SERVANT.Tipo_Item
	WHERE @p_descripcion_gasto = Descripcion
	
	IF(@p_id_referencia = 0)
		INSERT INTO SQL_SERVANT.Facturacion_Item VALUES (@p_id_factura, @p_id_cuenta, @id_tipo_item, @p_importe, NULL)
	ELSE
		INSERT INTO SQL_SERVANT.Facturacion_Item VALUES (@p_id_factura, @p_id_cuenta, @id_tipo_item, @p_importe, @p_id_referencia)
	
	IF(@id_tipo_item = 2)
	BEGIN
		UPDATE SQL_SERVANT.Cuenta SET Id_Estado_Cuenta = 4
		WHERE Id_Cuenta = @p_id_cuenta
	END
	
	IF(@id_tipo_item = 2 or @id_tipo_item = 3)
			
		IF(@p_cantidad_suscripciones > 1)
		BEGIN
			Declare @fecha_vencimiento datetime
			Declare @dias_por_tipo_cuenta int
			Declare @cantidad_dias_aumentar int
			
			SELECT @dias_por_tipo_cuenta = ptc.Dias FROM SQL_SERVANT.Periodo_Tipo_Cuenta ptc
			INNER JOIN SQL_SERVANT.Cuenta c ON ptc.Id_Tipo_Cuenta = c.Id_Tipo_Cuenta
			
			SELECT @fecha_vencimiento = Fecha_Vencimiento FROM SQL_SERVANT.Cuenta
			WHERE Id_Cuenta = @p_id_cuenta
			
			SET @cantidad_dias_aumentar = (@p_cantidad_suscripciones - 1) * @dias_por_tipo_cuenta
			
			UPDATE SQL_SERVANT.Cuenta SET Fecha_Vencimiento = DATEADD(day, @cantidad_dias_aumentar, @fecha_vencimiento)
			WHERE Id_Cuenta = @p_id_cuenta
			
		END
	
END
GO

--CUANDO UNA CUENTA TIENE MAS DE 5 TRANSACCIONES PENDIENTES DE PAGO SE INHABILITA LA CUENTA
CREATE TRIGGER [SQL_SERVANT].[tr_inhabilitacion_cuenta_por_transacciones]
ON SQL_SERVANT.Facturacion_Pendiente
FOR INSERT
AS

Declare @id_cuenta numeric(18,0)

SELECT @id_cuenta = Id_Cuenta FROM inserted
IF ((SELECT COUNT (*) FROM SQL_SERVANT.Facturacion_Pendiente fp
	WHERE fp.Id_Cuenta = @id_cuenta) > 5)
BEGIN
	UPDATE SQL_SERVANT.Cuenta SET Id_Estado_Cuenta = 3
	WHERE Id_Cuenta = @id_cuenta
	
	INSERT INTO SQL_SERVANT.Log (Id_Cliente, Id_Cuenta, Fecha, Id_Motivo) 
	(SELECT cc.Id_Cliente, cc.Id_Cuenta, inserted.Fecha, ml.Id_Motivo
	FROM SQL_SERVANT.Cliente_Cuenta cc, inserted, SQL_SERVANT.Motivo_Log ml
	WHERE cc.Id_Cuenta = @id_cuenta
	AND ml.Descripcion = 'INHABILITACION CUENTA POR NO PAGO')
END
GO
