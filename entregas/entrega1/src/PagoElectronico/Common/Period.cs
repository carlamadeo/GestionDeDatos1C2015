using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Common
{
    public class Period
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public String motive { get; set; }
    }
}
