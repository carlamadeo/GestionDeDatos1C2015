using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.ABM_Rol;

namespace PagoElectronico
{
    public class Usuario
    {
        public string id { get; set; }
        public Rol rol { get; set; }
        public int clientId { get; set; }
    }
}
