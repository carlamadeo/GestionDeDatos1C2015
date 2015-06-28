using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Facturacion
{
    public class Facturacion
    {
        public Int32 id { get; set; }
        public int? idReferencia { get; set; }
        public Decimal idCuenta { get; set; }
        public String descripcionGasto { get; set; }
        public DateTime fecha { get; set; }
        public Decimal importe { get; set; }
        public Int16 suscripciones { get; set; }
    }
}
