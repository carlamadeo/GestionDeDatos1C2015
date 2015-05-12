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