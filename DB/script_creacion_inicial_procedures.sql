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

