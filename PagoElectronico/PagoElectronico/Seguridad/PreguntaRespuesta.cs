using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Seguridad
{
    public class PreguntaRespuesta
    {
        public string pregunta { get; set; }
        public string respuesta { get; set; }

        public PreguntaRespuesta(string pregunta, string respuesta)
        {
            this.pregunta = pregunta;
            this.respuesta = respuesta;
        }
    }
}
