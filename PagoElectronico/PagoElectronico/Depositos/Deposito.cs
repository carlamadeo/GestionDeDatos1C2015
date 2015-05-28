using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Depositos
{
    class Deposito
    {
        public Decimal importe { get; set; }
        public Decimal tarjeta { get; set; }
        public int moneda { get; set; }
        public Decimal cuenta { get; set; }
        public DateTime fecha { get; set; }
    }
}
