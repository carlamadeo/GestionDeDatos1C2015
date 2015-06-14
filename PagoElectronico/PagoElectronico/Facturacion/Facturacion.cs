using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Facturacion
{
    class Facturacion
    {
        public Decimal idCuenta { get; set; }
        public String descripcionGasto { get; set; }
        public DateTime fecha { get; set; }
        public Decimal importe { get; set; }
    }
}
