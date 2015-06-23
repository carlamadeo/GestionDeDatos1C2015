--APELLIDO: GARCIA RONCA, FEDERICO
--LEGAJO: 1374412
--MAIL: FEDEMV@GMAIL.COM

--1
SELECT p1.prod_codigo,p1.prod_detalle, p1.prod_rubro, r1.rubr_detalle,
	p2.prod_rubro AS prod_rubro_sugerido,
	(SELECT rubr_detalle FROM gdd_practica.dbo.Rubro WHERE rubr_id = p2.prod_rubro) AS rubr_sugerido
FROM 
(gdd_practica.dbo.Producto p1 INNER JOIN 
gdd_practica.dbo.Rubro r1 ON p1.prod_rubro = r1.rubr_id)
INNER JOIN (gdd_practica.dbo.Producto p2 INNER JOIN
gdd_practica.dbo.Rubro r2 ON p2.prod_rubro = r2.rubr_id)
ON SUBSTRING(p1.prod_detalle,1,10) = SUBSTRING(p2.prod_detalle,1,10)
WHERE p2.prod_rubro = 
	(SELECT TOP 1 p4.prod_rubro FROM gdd_practica.dbo.Producto p4 INNER JOIN
			gdd_practica.dbo.Rubro r4 ON p4.prod_rubro = r4.rubr_id
		WHERE SUBSTRING(p1.prod_detalle,1,10) = SUBSTRING(p4.prod_detalle,1,10)
		GROUP BY p4.prod_codigo,p4.prod_rubro,r4.rubr_id
		ORDER BY COUNT(r4.rubr_id) DESC)
GROUP BY p1.prod_codigo, p1.prod_detalle, p1.prod_rubro, r1.rubr_id, r1.rubr_detalle, p2.prod_rubro
HAVING p1.prod_rubro != p2.prod_rubro
ORDER BY p1.prod_detalle ASC

--2
CREATE TRIGGER	dbo.restriccionLimiteDeCredito
ON gdd_practica.dbo.Factura
INSTEAD OF INSERT
AS
BEGIN
	Declare @fact_cliente numeric(6)
	Declare @suma_parcial_mes_facturado decimal(12,2)
	Declare @limite_credito decimal(12,2)
	Declare @total_factura decimal(12,2)
	
	SELECT @fact_cliente = fact_cliente, @total_factura = fact_total FROM inserted
	SELECT @limite_credito = clie_limite_credito from gdd_practica.dbo.Cliente
		WHERE clie_codigo = @fact_cliente
	
	SELECT @suma_parcial_mes_facturado = SUM(fact_total) FROM gdd_practica.dbo.Factura
	WHERE fact_cliente = @fact_cliente 
	AND DATEDIFF(MONTH,fact_fecha, GETDATE()) = 1
	
	IF (ISNULL(@suma_parcial_mes_facturado,0) + ISNULL(@total_factura,0) > @limite_credito)
		RAISERROR('El monto que se le puede facturar mensualmente a un cliente no puede superar su limite de credito',16,1)
	ELSE
		INSERT INTO gdd_practica.dbo.Factura SELECT * FROM inserted
END