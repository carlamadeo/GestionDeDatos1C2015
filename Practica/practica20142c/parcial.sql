/*
Alumno: García Ronca, Federico
Legajo: 137441-2
Curso: Martes a la noche
Mail: fedemv@gmail.com
*/

/*
ENUNCIADO
para el jugador con nick 1974class obtener en una consulta sql informacion de las preguntas
que habiandolas respondido bien, la respondio mal en una fecha posterior y nivel superior con otra combinatoria de respuesta
y mismo detalle de pregunta.
Ambas preguntas deben estar vigentes y no ser de un pais en particular

el formato de la salida de la consulta es:

pregunta | cta_1 | rta_dada | nivel | cta_2

la primera columna es el detalle de la pregunta
la segunda, la tercera y cuarta corresponde a la respuesta correcta, a la respuesta dada 
y al nivel de la pregunta mal respondida
la quinta columna es la respuesta bien respondida siempre y cuando varie, si no varia colocar null
si hay varias repeticiones mostrar un solo valor en la ultima columna
*/

CREATE FUNCTION sonLaMismaRespuesta(@rt1Detalle varchar(30), @rt2Detalle varchar(30))
RETURN varchar(30)
BEGIN
	IF UPPER(@rt1Detalle) != UPPER(@rt2Detalle)
		RETURN @rt1Detalle
	ELSE
		RETURN null
END
GO

SELECT preguntaNivelInferior.detalle, rt2correcta.detalle, rt2dada.detalle, nivelSuperior.detalle, 
	sonLaMismaRespuesta(rt1.detalle, rt2correcta.detalle)
FROM JUGADORES jugador, LOGS log1, LOGS log2, PREGUNTAS preguntaNivelInferior, PREGUNTAS preguntaNivelSuperior, RESPUESTAS rt1,
	RESPUESTAS rt2dada, RESPUESTAS rt2correcta, NIVELES nivelInferior, NIVELES nivelSuperior
WHERE

	jugador.nick = '1974class'
	AND log1.jugador = jugador.idJugador
	
	#verifico que la pregunta no exista en rel_pais_pregunta
	AND NOT EXISTS(SELECT 1 FROM REL_PAIS_PREGUNTA rpp1 where rpp1.idPregunta = log1.idPregunta)
	
	#Verifico que la respuesta de la pregunta de nivel inferior es la correcta
	AND rt1.idRespuesta = log1.respuesta
	AND rt1.esCorrecta = 'S'
	
	#Verifico que todavia la pregunta este vigente
	AND preguntaNivelInferior.idPregunta = log1.pregunta
	AND getDate() BETWEEN preguntaNivelInferior.fechaInicio AND preguntaNivelInferior.fechaFin

	#Busco el nivel de la pregunta
	AND nivelInferior.idNivel = preguntaNivelInferior.nivel
	
	#Busco la misma pregunta posterior
	AND	log2.jugador = jugador.idJugador
	AND log1.idLog != log2.idLog
	AND log1.pregunta != log2.pregunta
	AND log1.fechaHora < log2.fechaHora
	AND preguntaNivelSuperior.idPregunta = log2.pregunta
	AND UPPER(preguntaNivelInferior.detalle) = UPPER(preguntaNivelSuperior.detalle)
	AND nivelSuperior.idNivel = preguntaNivelSuperior.nivel
	AND nivelSuperior.detalle > nivelInferior.detalle
	
	#Verifico que todavia la pregunta este vigente
	AND getDate() BETWEEN preguntaNivelSuperior.fechaInicio AND preguntaNivelSuperior.fechaFin
	#verifico que la pregunta no exista en rel_pais_pregunta
	AND NOT EXISTS(SELECT 1 FROM REL_PAIS_PREGUNTA rpp2 where rpp2.idPregunta = log2.idPregunta)
	
	#Verifico que la respuesta que dio el usuario a la pregunta fuera incorrecta
	AND rt2dada.idRespuesta = log2.respuesta
	AND rt2dada.esCorrecta = 'N'
	
	#Busco la respuesta correcta para la pregunta de nivel superior
	AND rt2correcta.pregunta = preguntaNivelSuperior.idPregunta
	AND rt2correcta.esCorrecta = 'S'