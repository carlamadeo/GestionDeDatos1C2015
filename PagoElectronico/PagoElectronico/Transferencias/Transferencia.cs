using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Transferencias
{
    class Transferencia
    {
        public Decimal monto { get; set; }
        public Decimal cuentaOrigen { get; set; }
        public Decimal cuentaDestino { get; set; }
        public DateTime fecha { get; set; }
        public int mismoCliente { get; set; }
        public int moneda = 1;
    }
}
