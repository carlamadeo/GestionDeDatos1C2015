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

CREATE PROCEDURE [SQL_SERVANT].[sp_login_get_answer_question_secret](
@p_id as varchar(255) = null,
@p_question as varchar(255) = null OUTPUT,
@p_answer as varchar(255) = null OUTPUT
)
AS
BEGIN
	SELECT @p_question = Pregunta_Secreta, @p_answer = Respuesta_Secreta
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
	UPDATE SQL_SERVANT.Usuario
		SET Password = @p_pass,
		Pregunta_Secreta = @p_question,
		Respuesta_Secreta = @p_answer
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
		WHERE Id_Usuario = @p_id
		SET @p_intentos = 0
	END
	ELSE
	BEGIN
		Declare @p_intentos_base int
		SELECT @p_intentos_base = Cantidad_Login FROM SQL_SERVANT.Usuario WHERE Id_Usuario = @p_id
		SET @p_intentos = @p_intentos_base + 1

		IF ( @p_intentos >= 3 )
			UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = @p_intentos, Ultima_Fecha = getDate(), Habilitado = 0
			WHERE Id_Usuario = @p_id
		ELSE
			UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = @p_intentos, Ultima_Fecha = getDate()
			WHERE Id_Usuario = @p_id
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

--PROCEDIMIENTOS DE USUARIOS
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
		ur.Habilitado 'Habilitado'
		
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
	SELECT * FROM SQL_SERVANT.Usuario u
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
			Respuesta_Secreta = @p_user_answer_secret,
			Habilitado = @p_enabled
			WHERE Id_Usuario = @p_user_name
		ELSE
			UPDATE SQL_SERVANT.Usuario SET Password = @p_password,
			Fecha_Creacion = @p_user_creation_date,
			Ultima_Modificacion = @p_user_modify_date,
			Pregunta_Secreta = @p_user_question_secret,
			Respuesta_Secreta = @p_user_answer_secret,
			Habilitado = @p_enabled
			WHERE Id_Usuario = @p_user_name
	END
	ELSE
	BEGIN
		INSERT INTO SQL_SERVANT.Usuario (Id_Usuario, Password, Cantidad_Login, Ultima_Fecha, Fecha_Creacion, Ultima_Modificacion,
		Pregunta_Secreta, Respuesta_Secreta, Habilitado)
		VALUES (@p_user_name, @p_password, 0, null, @p_user_creation_date, @p_user_modify_date, @p_user_question_secret, @p_user_answer_secret,
		@p_enabled)
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

--PROCEDIMIENTOS CONSULTA DE SALDO

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
	select top 5 t1.Id_Deposito, t1.Importe, t1.Id_Tarjeta, t1.Fecha_Deposito
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


CREATE PROCEDURE [SQL_SERVANT].[sp_account_search](
@p_account_lastname varchar(255) = null,
@p_account_type_id int = null,
@p_account_client_id int = null
)
AS
BEGIN
	SELECT DISTINCT 
	cd.Id_Cliente 'Id Cliente',
	cd.Nombre 'Nombre',
	cd.Apellido 'Apellido',
	cc.Id_Cuenta 'Cuenta Numero Identificacion',
	tp.Descripcion 'Tipo Cuenta',
	cu.Fecha_Creacion 'Fecha Creacion',
	cu.Fecha_Vencimiento 'Fecha Vencimiento',
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
				UPDATE SQL_SERVANT.Cuenta 
					SET Id_Pais_Registro = @p_account_country_id,
						Id_Moneda = @p_account_currency_id,
						Id_Tipo_Cuenta = @p_account_type_account_id,
						Fecha_Vencimiento = DATEADD(DAY, @type_account_day, Fecha_Creacion)
					WHERE	Id_Cuenta = @p_account_id
		END
		ELSE
		BEGIN
			
			INSERT INTO SQL_SERVANT.Cuenta (Id_Pais_Registro, Id_Moneda, Fecha_Creacion, Fecha_Vencimiento, Importe,
				Id_Tipo_Cuenta, Id_Estado_Cuenta)
				VALUES(@p_account_country_id, @p_account_currency_id, @p_account_date, DATEADD(DAY, @type_account_day, @p_account_date),
				0.00, @p_account_type_account_id, 4)
			Declare @account_id numeric(18,0)
			SET @account_id = @@IDENTITY
			INSERT INTO SQL_SERVANT.Cliente_Cuenta (Id_Cliente, Id_Cuenta)
				VALUES (@p_account_client_id, @account_id)
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
	AND cu.Fecha_Creacion <= @p_account_today
	AND cu.Fecha_Vencimiento <= @p_account_today
	AND cu.Importe > 0.00
	AND UPPER(mo.Descripcion) = UPPER('USD')
END
GO

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
			VALUES (@p_user_username, @p_user_password, 0, CAST(GETDATE() AS DATE), @p_user_secret_question, @p_user_secret_answer, 1)
		
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
				Localidad = @p_client_localidad, Id_Nacionalidad = @p_client_nationality_id, Fecha_Nacimiento = @p_client_birthdate
			WHERE Id_Cliente = @p_client_id
			
			UPDATE SQL_SERVANT.Cliente SET Nro_Identificacion = @p_client_identification_number, 
				Id_Tipo_Identificacion = @p_client_type_identification_id, Fecha_Ultima_Modificacion = CAST(GETDATE() AS DATE)
			
		END
		
	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_client_tarjeta_disable](
@p_client_id int,
@p_tarjeta_id varchar(16)
)
AS
BEGIN
	UPDATE SQL_SERVANT.Cliente_Tarjeta SET Habilitada = 0
		WHERE Id_Cliente = @p_client_id AND Id_Tarjeta = @p_tarjeta_id
END
GO

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
			ON ct.Id_Tarjeta = t.Id_tarjeta
		INNER JOIN SQL_SERVANT.Tarjeta_Empresa te
			ON t.Id_Tarjeta_Empresa = te.Id_Tarjeta_Empresa
		
	WHERE ct.Id_Cliente = @p_id_client AND
	ct.Id_Tarjeta = @p_Id_tarjeta
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_card_get_data](
@p_card_id varchar(16)
)
AS
BEGIN
	SELECT
		t.Id_Tarjeta,
		te.Descripcion "Tarjeta_Descripcion",
		t.Id_Tarjeta_Empresa,
		t.Fecha_Emision,
		t.Fecha_Vencimiento,
		t.Codigo_Seguridad

	FROM SQL_SERVANT.Tarjeta t
		INNER JOIN SQL_SERVANT.Tarjeta_Empresa te
			ON t.Id_Tarjeta_Empresa = te.Id_Tarjeta_Empresa
	WHERE t.Id_Tarjeta = @p_card_id
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_card_by_client_id](
@p_card_client_id int
)
AS
BEGIN
	SELECT
		t.Id_Tarjeta "Numero",
		te.Descripcion 'Empresa',
		t.Fecha_Emision "Fecha Emision",
		t.Fecha_Vencimiento "Fecha Vencimiento",
		CASE ct.Habilitada WHEN 1 THEN 'ASOCIADA' ELSE 'DESASOCIADA' END AS "Estado"

		FROM SQL_SERVANT.Cliente_Tarjeta ct
			INNER JOIN SQL_SERVANT.Tarjeta t
				ON ct.Id_Tarjeta = t.Id_Tarjeta
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
	WHERE Id_Cliente = @p_card_client_id AND Id_Tarjeta = @p_card_id
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
		INSERT INTO SQL_SERVANT.Tarjeta (Id_Tarjeta, Fecha_Emision, Fecha_Vencimiento, Id_Tarjeta_Empresa, Codigo_Seguridad)
		VALUES (@p_card_id, @p_card_creation_date, @p_card_expiration_date, @p_card_company_id, @p_card_security_code)

		INSERT INTO SQL_SERVANT.Cliente_Tarjeta (Id_Cliente, Id_Tarjeta, Habilitada)
		VALUES (@p_card_client_id, @p_card_id, SQL_SERVANT.Validar_Tarjeta_Habilitacion(@p_card_id, @p_card_expiration_date, @p_card_date_to_considerate))
	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_card_exist](
@p_card_id varchar(16),
@p_is_valid bit = 0 OUTPUT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM SQL_SERVANT.Tarjeta WHERE Id_Tarjeta = @p_card_id)
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
			WHERE Id_Tarjeta = @p_tarjeta_id
		
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
		t.Id_Tarjeta 'Nro Tarjeta', 
		t.Fecha_Vencimiento 'Fecha Vencimiento',
		te.Descripcion 'Empresa'
	FROM SQL_SERVANT.Tarjeta t
	INNER JOIN SQL_SERVANT.Cliente_Tarjeta ct
		ON t.Id_Tarjeta = ct.Id_Tarjeta
	INNER JOIN SQL_SERVANT.Tarjeta_Empresa te
		ON t.Id_Tarjeta_Empresa = te.Id_Tarjeta_Empresa
	WHERE ct.Id_Cliente = @p_card_client_id
		AND	t.Fecha_Emision <= @p_card_today
		AND t.Fecha_Vencimiento <= @p_card_today
		AND ct.Habilitada = 1
END
GO

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

CREATE PROCEDURE [SQL_SERVANT].[sp_tarjeta_not_expired](
@p_tarjeta_id varchar(16) = 0,
@p_notExpired bit = 1 OUTPUT
)
AS
BEGIN

	Declare @p_fecha_vencimiento datetime

	SELECT @p_fecha_vencimiento = Fecha_Vencimiento FROM SQL_SERVANT.Tarjeta
	WHERE Id_Tarjeta = @p_tarjeta_id
	
	IF (@p_fecha_vencimiento < CAST(GETDATE() AS DATE))
		SET @p_notExpired = 0
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_save_deposito](
@p_deposito_cuenta numeric(18,0),
@p_deposito_importe numeric(10,2),
@p_deposito_moneda int,
@p_deposito_tarjeta numeric(16,0),
@p_deposito_fecha datetime
)

AS
BEGIN

	BEGIN TRANSACTION

		Declare @importe_actual numeric(18,2)
		
		SELECT @importe_actual = Importe FROM SQL_SERVANT.Cuenta c
		WHERE @p_deposito_cuenta = c.Id_Cuenta
		
		INSERT INTO SQL_SERVANT.Deposito (Id_Cuenta, Importe, Id_Moneda, Id_Tarjeta, Fecha_Deposito)
		VALUES (@p_deposito_cuenta, @p_deposito_importe, @p_deposito_moneda, @p_deposito_tarjeta, @p_deposito_fecha)
		
		UPDATE SQL_SERVANT.Cuenta SET Importe = (@p_deposito_importe + @importe_actual)
		WHERE @p_deposito_cuenta = SQL_SERVANT.Cuenta.Id_Cuenta
		
	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE [SQL_SERVANT].[sp_get_importe_maximo_por_cuenta](
@p_cuenta_id numeric(18,0) = 0,
@p_cuenta_propia bit = 0,
@p_importe_maximo numeric(18,2) = 0 OUTPUT
)
AS
BEGIN

	Declare @importe_cuenta numeric(18,2)
	Declare @id_tipo_cuenta int

	SELECT @importe_cuenta = Importe, @id_tipo_cuenta = Id_Tipo_Cuenta FROM SQL_SERVANT.Cuenta
	WHERE Id_Cuenta = @p_cuenta_id
	
	SET @p_importe_maximo = @importe_cuenta
	
	IF (@p_importe_maximo < 0)
		SET @p_importe_maximo = 0
	
END
GO

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
		Declare @p_id_tipo_cuenta int
		Declare @p_mismo_cliente bit
		
		SELECT @p_importe_actual_origen = Importe FROM SQL_SERVANT.Cuenta c
		WHERE @p_transferencia_origen = c.Id_Cuenta
		
		SELECT @p_importe_actual_destino = Importe FROM SQL_SERVANT.Cuenta c
		WHERE @p_transferencia_destino = c.Id_Cuenta
		
		SELECT @p_transferencia_costo = Costo FROM SQL_SERVANT.Costo_Tipo_Cuenta ctc
		INNER JOIN SQL_SERVANT.Cuenta c ON ctc.Id_Tipo_Cuenta = c.Id_Tipo_Cuenta
		WHERE c.Id_Cuenta = @p_transferencia_origen
		
		SELECT @p_id_tipo_cuenta = Id_Tipo_Cuenta FROM SQL_SERVANT.Cuenta c
		WHERE c.Id_Cuenta = @p_transferencia_origen
		
		INSERT INTO SQL_SERVANT.Transferencia (Id_Cuenta_Origen, Id_Cuenta_Destino, Id_Moneda, 
		Importe, Costo,	Fecha_Transferencia)
		VALUES (@p_transferencia_origen, @p_transferencia_destino, @p_transferencia_moneda, @p_transferencia_monto, 
		@p_transferencia_costo, @p_tranferencia_fecha)
		
		SET @p_id_transferencia = @@IDENTITY
		
		SET @p_importe_final_origen = @p_importe_actual_origen - @p_transferencia_monto
		SET @p_importe_final_destino = @p_importe_actual_destino + @p_transferencia_monto
		
		IF (@p_tranferencia_mismo_cliente = 0)
			SET @p_importe_final_origen = @p_importe_final_origen -  @p_transferencia_costo
		
		UPDATE SQL_SERVANT.Cuenta SET Importe = (@p_importe_final_origen)
		WHERE @p_transferencia_origen = SQL_SERVANT.Cuenta.Id_Cuenta
		
		UPDATE SQL_SERVANT.Cuenta SET Importe = (@p_importe_final_destino)
		WHERE @p_transferencia_destino = SQL_SERVANT.Cuenta.Id_Cuenta
		
		INSERT INTO SQL_SERVANT.Facturacion_Pendiente (Id_Cuenta, Id_Tipo_Cuenta, Id_Moneda, Fecha,
		Importe, Id_Referencia, Descripcion)
		VALUES (@p_transferencia_origen, @p_id_tipo_cuenta, @p_transferencia_moneda, @p_tranferencia_fecha, 
		@p_transferencia_costo, @p_id_transferencia, 'Comisión por transferencia.')
		
	COMMIT TRANSACTION
END
GO



