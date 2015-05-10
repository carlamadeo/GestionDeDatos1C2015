--PROCEDIMIENTOS PARA LOGIN USUARIO

GO
CREATE PROCEDURE [SQL_SERVANT].[proc_isSuccessLogin](
	@User varchar(20),
	@Password varchar(64)
)
AS
BEGIN
	DECLARE @Correct bit = 0
	
	SELECT * FROM SQL_SERVANT.Usuario 
	WHERE 
	LTRIM(RTRIM(Id_Usuario)) = LTRIM(RTRIM(@User)) AND
	Password = @Password
	
	SET @Correct = @@ROWCOUNT
	
	RETURN @Correct
	
END
GO
	
	
GO
CREATE PROCEDURE [SQL_SERVANT].[proc_wrongLogin](
	@User varchar(20)
)
AS
BEGIN
	
	DECLARE @CountLogin Int
	
	SELECT @CountLogin = Cantidad_Login FROM SQL_SERVANT.Usuario 
	WHERE Id_Usuario = @User
	
	SET @CountLogin = @CountLogin + 1
	
	UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = @CountLogin
	WHERE Id_Usuario = @User
	
END
GO

GO
CREATE PROCEDURE [SQL_SERVANT].[proc_resetLogin](
	@User varchar(20)
)
AS
BEGIN
	
	UPDATE SQL_SERVANT.Usuario SET Cantidad_Login = 0
	WHERE Id_Usuario = @User
	
END
GO

GO
CREATE PROCEDURE [SQL_SERVANT].[proc_countRol](
	@User varchar(20)
)

AS
BEGIN

DECLARE @CountRol int = 0
	
	SELECT * FROM SQL_SERVANT.Usuario_Rol
	WHERE 
	LTRIM(RTRIM(Id_Usuario)) = LTRIM(RTRIM(@User))
	
	SET @CountRol = @@ROWCOUNT
	
	RETURN @CountRol
	
END
GO
	
GO
CREATE PROCEDURE [SQL_SERVANT].[proc_countWrongLogin](
	@User varchar(20)
)

AS
BEGIN

DECLARE @CountWrongLogin int = 0
	
	SELECT @CountWrongLogin = Cantidad_Login FROM SQL_SERVANT.Usuario 
	WHERE Id_Usuario = @User
	
	RETURN @CountWrongLogin
	
END
GO

GO
CREATE PROCEDURE [SQL_SERVANT].[proc_blockUser](
	@User varchar(20)
)

AS
BEGIN
	
	UPDATE SQL_SERVANT.Usuario SET Habilitado = 0
	WHERE Id_Usuario = @User
	
END
GO

GO
CREATE PROCEDURE [SQL_SERVANT].[proc_notBlockedUser](
	@User varchar(20)
)
AS
BEGIN
	DECLARE @notBlocked bit = 1
	
	SELECT @notBlocked = Habilitado FROM SQL_SERVANT.Usuario 
	WHERE LTRIM(RTRIM(Id_Usuario)) = LTRIM(RTRIM(@User))
	
	RETURN @notBlocked
	
END
GO

GO
CREATE PROCEDURE [SQL_SERVANT].[proc_isUser](
	@User varchar(20)
)

AS
BEGIN

DECLARE @exists int = 0
	
	SELECT * FROM SQL_SERVANT.Usuario
	WHERE 
	LTRIM(RTRIM(Id_Usuario)) = LTRIM(RTRIM(@User))
	
	SET @exists = @@ROWCOUNT
	
	RETURN @exists
	
END
GO

--