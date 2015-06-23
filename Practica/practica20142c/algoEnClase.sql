-- 36 a) SELECT c1.customer_num, c1.fname, c1.lname FROM customer c1 where (SELECT COUNT(1) FROM orders o1 WHERE o1.customer_num = c1.customer_num) > 1

/*SELECT c1.customer_num, c1.fname, c1.lname FROM customer c1 (36 c)
INNER JOIN orders o1 
	ON c1.customer_num = o1.customer_num
GROUP BY c1.customer_num, c1.fname, c1.lname
HAVING COUNT(1) > 1
*/

DROP TABLE #temp_compras_clientes

SELECT c1.customer_num, c1.fname, c1.lname INTO #temp_compras_clientes FROM customer c1
INNER JOIN orders o1 
	ON c1.customer_num = o1.customer_num
	
SELECT * FROM #temp_compras_clientes c1 
GROUP BY c1.customer_num, c1.fname, c1.lname
HAVING COUNT(1) > 1 

--37)
/*
Encontrar todas las Órdenes de compra con el Monto total (Suma del total_price de sus
items) menor que el precio total promedio (total_price) de todos los ítems de todas las
ordenes.
*/
SELECT o.order_num, SUM(total_price) FROM orders o 
INNER JOIN items i 
	ON o.order_num = i.order_num
GROUP BY o.order_num
HAVING SUM(total_price) < (SELECT AVG(i2.total_price) FROM items i2)