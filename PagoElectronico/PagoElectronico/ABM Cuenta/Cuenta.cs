using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.ABM_Cuenta
{
    public class Cuenta
    {
        public String id { get; set; }
        public long idClient { get; set; }
        public int idTypeAccount { get; set; }
        public string typeAccountDescription { get; set; }
        public int idCountry { get; set; }
        public string countryDescription { get; set; }
        public int idCurrency { get; set; }
        public string currencyDescription { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime expirationDate { get; set; }
        public DateTime closeDate { get; set; }
    }
}
