using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.ABM_Cliente
{
    public class Cliente
    {
        public Int32 id { get; set; }
        public String name { get; set; }
        public String lastname { get; set; }
        public Int32 idTypeIdentification { get; set; }
        public String typeIdentification { get; set; }
        public Int32 identificationNumber { get; set; }
        public String mail { get; set; }
        public String addressName { get; set; }
        public Int32 addressNum { get; set; }
        public Int32 addressFloor { get; set; }
        public String adressDeptName { get; set; }
        public Int32 idNacionality { get; set; }
        public String nationality { get; set; }
        public String rol { get; set; }
        public DateTime birthdate { get; set; }
        public Int32 idCountry { get; set; }
        public String country { get; set; }
        public String localidad { get; set; }
        public Boolean enable { get; set; }
    }
}
