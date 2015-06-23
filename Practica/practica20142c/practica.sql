--Practica 2014
--1
SELECT DISTINCT c.fname + ' ' + c.lname, c.address1, c.address2 FROM dbo.customer c

--2
SELECT * FROM customer WHERE state = 'CA'

--3
SELECT DISTINCT city FROM customer WHERE state = 'CA'

--4
SELECT DISTINCT city FROM customer WHERE state = 'CA' ORDER BY city ASC

--5
SELECT address1, address2 FROM customer WHERE customer_num = 103