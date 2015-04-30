Empleados
Id	Nombre	Sector 	Sueldo
1	Dante	1		12000
2	Gina	1 		10000
3	Paula	2		9000
4	Marcero	2		9000

SELECT SECTOR, SUELDO, Nombre
FROM Empleados F1
WHERE SECTOR IS NOT NULL
	AND SUELDO = (
			SELECT MAX(SUELDO)
			FROM Empleados F2
			WHERE F2.SECTOR =  = F1.SECTOR
		)