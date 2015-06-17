using System;

namespace PagoElectronico.Depositos
{
    class Deposito
    {
        public Decimal importe { get; set; }
        public String tarjeta { get; set; }
        public int moneda { get; set; }
        public Decimal cuenta { get; set; }
        public DateTime fecha { get; set; }
    }
}
