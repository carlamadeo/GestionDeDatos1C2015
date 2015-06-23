CREATE PROCEDURE [dbo].[actualizarHistorialVendedor](@empl_codigo numeric(6,0))
AS
BEGIN
	Declare @ventas_total_facturas numeric(12,2)
	
	SELECT @ventas_total_facturas = SUM(fact_total) FROM dbo.Factura WHERE fact_vendedor = @empl_codigo

	UPDATE dbo.empleado SET empl_ventas_historicas = @ventas_total_facturas WHERE empl_codigo = @empl_codigo
END

GO

CREATE TRIGGER [dbo].[actualizarHistorialVentas]
ON [dbo].[Factura]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	DECLARE @id_empleado numeric(12,0)
	DECLARE empleados_id CURSOR FOR SELECT DISTINCT(fact_vendedor) FROM inserted
		WHERE fact_vendedor IS NOT NULL
	
	OPEN empleados_id

	FETCH empleados_id INTO @id_empleado

	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC dbo.actualizarHistorialVendedor @empl_codigo = @id_empleado

		FETCH empleados_id INTO @id_empleado
	END

	CLOSE empleados_id
	DEALLOCATE empleados_id
END
GO