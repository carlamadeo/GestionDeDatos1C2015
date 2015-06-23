CREATE FUNCTION [dbo].[obtenerEsJefeDe](@empl_jefe numeric(6,0), @empl_codigo numeric(6,0))
RETURNS INT
AS
BEGIN
	Declare @loTieneDeEmpleado int = 0
	Declare @loTienenLosSubEmpleados int = 0
	
	SELECT @loTieneDeEmpleado = 1 FROM gdd_practica.dbo.Empleado
		WHERE empl_jefe = @empl_jefe AND @empl_codigo = empl_codigo

	IF (@loTieneDeEmpleado >= 1)
		RETURN 1
	ELSE
		SELECT @loTienenLosSubEmpleados= @loTienenLosSubEmpleados + dbo.obtenerEsJefeDe(empl_codigo, @empl_codigo)
			FROM gdd_practica.dbo.Empleado
			WHERE empl_jefe = @empl_jefe
		IF (@loTienenLosSubEmpleados >= 1)
			RETURN 1

		RETURN 0
END

GO

CREATE TRIGGER [dbo].[restriccionBucleEmpleado]
ON [dbo].[Empleado]
AFTER INSERT, UPDATE
AS
BEGIN	
	IF EXISTS(SELECT 1 FROM inserted ins 
			WHERE dbo.obtenerEsJefeDe(ins.empl_jefe, ins.empl_codigo) > 0)
		BEGIN 	
			RAISERROR('ERROR: Alguno de los empleados que se trata de agregar/modificar no puede ser jefe 
				de uno de sus jefes actuales', 5, 1)
			ROLLBACK
		END
	END
GO