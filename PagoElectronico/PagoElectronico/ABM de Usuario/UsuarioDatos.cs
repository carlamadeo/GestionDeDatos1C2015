﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Seguridad;

namespace PagoElectronico.ABM_de_Usuario
{
    public class UsuarioDatos
    {
        public string username { get; set; }
        public string password { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime modifyDate { get; set; }
        public Int16 idRol { get; set; }
        public Boolean enabled { get; set; }
        public PreguntaRespuesta questionAnswer { get; set; }
    }
}
