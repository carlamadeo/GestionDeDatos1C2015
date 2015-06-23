--1
CREATE FUNCTION estadoArticuloPorDeposito(@prod_codigo char(8),@depo_codigo char(2))
RETURNS varchar(30)
AS
BEGIN
	Declare @stoc_cantidad decimal(12,2)
	Declare @stoc_stock_maximo decimal(12,2)
	
	SELECT @stoc_cantidad=stoc_cantidad, 
		@stoc_stock_maximo = stoc_stock_maximo 
		FROM gdd_practica.dbo.STOCK
		WHERE stoc_producto = @prod_codigo AND stoc_deposito = @depo_codigo
	IF @@ROWCOUNT > 0
	BEGIN
		IF (@stoc_cantidad >= @stoc_stock_maximo)
			RETURN 'DEPOSITO COMPLETO'
		ELSE
			RETURN 'OCUPACION DEL DEPOSITO ' +  LTRIM(STR((@stoc_cantidad / @stoc_stock_maximo) * 100)) + '%'
	END

	RETURN 'NO EXISTE STOCK DEL ARTICULO PARA EL DEPOSITO'
END

Declare @message varchar(30)
EXEC @message = estadoArticuloPorDeposito @prod_codigo='00000030',@depo_codigo='00'
PRINT @message

--2
/*
Declare @date date = '12-21-05'
Declare @datetime datetime = @date
@datetime '2005-12-21 00:00:00'
*/

CREATE FUNCTION stockProductoALaFecha(@prod_codigo char(8), @fecha datetime)
RETURNS decimal(12,2)
AS
BEGIN
	Declare @stock_actual decimal (12,2)
	Declare @vendido_desde_fecha decimal(12,2)

	SELECT @stock_actual = SUM(stoc_cantidad) FROM gdd_practica.dbo.STOCK
		WHERE stoc_producto = @prod_codigo

	SELECT @vendido_desde_fecha = SUM(item_cantidad) FROM gdd_practica.dbo.Item_Factura
		INNER JOIN gdd_practica.dbo.Item_Factura
		ON fa.fact_tipo = fi.item_tipo AND fa.fact_numero = fi.item_numero
			AND fa.fact_sucursal = fi.item_sucursal
		WHERE item_producto = @prod_codigo AND fact_fecha BETWEEN @fecha AND GETDATE()

	RETURN @stock_actual + @vendido_desde_fecha
END

Declare @stock decimal(12,2)
Declare @time date = '01-01-90'
Declare @fecha_a_buscar datetime = @time

EXEC @stock = stockProductoALaFecha @prod_codigo='00010220',@fecha=@fecha_a_buscar
PRINT @stock

--stock en deposito = 111
--total vendido = 180
--TOTAL STOCK ORIGINAL = 291

--3

--7
--opcion 1 para contar filas ROW_NUMBER() OVER (ORDER BY prod_codigo)
Declare @time_desde date = '01-01-90'
Declare @time_hasta date = '01-01-15'

Declare @fecha_desde datetime = @time_desde
Declare @fecha_hasta datetime = @time_hasta
Declare @counter numeric(18,0) = 0

INSERT INTO (codigo,detalle,cant_mov,precio_prom,renglon,ganancia)
SELECT prod_codigo,prod_detalle,SUM(item_cantidad) AS cantidad_movientos,
	AVG(item_precio) AS precio_promedio,ROW_NUMBER() OVER (ORDER BY prod_codigo) AS Reglon,
	(SUM(item_precio * item_cantidad)) - (SUM(item_cantidad) * prod_precio) AS Ganancia
	FROM gdd_practica.dbo.Item_Factura 
	INNER JOIN gdd_practica.dbo.Producto ON prod_codigo = item_producto
	INNER JOIN gdd_practica.dbo.Factura 
	ON fact_tipo = item_tipo AND fact_numero = item_numero
			AND fact_sucursal = item_sucursal
	WHERE fact_fecha BETWEEN @fecha_desde AND @fecha_hasta
	GROUP BY prod_codigo,prod_detalle,prod_precio	

--11
CREATE FUNCTION obtenerEmpleadosDe(@empl_codigo numeric(6))
RETURNS INT
AS
BEGIN
	Declare @jefe_nacimiento datetime
	Declare @cantidad_empleados int = 0
	Declare @sub_empleados int = 0
	SELECT @jefe_nacimiento = empl_nacimiento FROM gdd_practica.dbo.Empleado
		WHERE empl_codigo = @empl_codigo
	SELECT @sub_empleados= @sub_empleados + dbo.obtenerEmpleadosDe(empl_codigo) FROM gdd_practica.dbo.Empleado
	WHERE empl_jefe = @empl_codigo AND empl_nacimiento > @jefe_nacimiento
	
	SET @cantidad_empleados = @@ROWCOUNT
	
	RETURN @cantidad_empleados + @sub_empleados
END

--12
CREATE TRIGGER dbo.restriccion50empelados
ON gdd_practica.dbo.Empleado
INSTEAD OF INSERT,DELETE
AS
	Declare @empl_jefe numeric(6,0)
	Declare @empleados int
	SELECT @empl_jefe = empl_jefe FROM inserted
	EXEC @empleados = dbo.obtenerEmpleadosDe @empl_codigo = @empl_jefe
	PRINT @empleados


--SELECT * FROM gdd_practica.dbo.Empleado
INSERT INTO gdd_practica.dbo.Empleado (empl_codigo,empl_jefe,empl_departamento)
VALUES (100,1,1)

--DROP TRIGGER restriccion50empelados
DROP FUNCTION dbo.obtenerEmpleadosDe

