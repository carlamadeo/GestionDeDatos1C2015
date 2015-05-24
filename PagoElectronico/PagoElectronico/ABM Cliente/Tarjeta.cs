using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Seguridad;

namespace PagoElectronico.ABM_Cliente
{
    class Tarjeta
    {
        public Decimal id { get; set; }
        public string empresa { get; set; }
        public DateTime fechaEmision { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int codSeguridad { get; set; }

        public static void fillTarjetaByClient(ComboBox comboBoxTarjetas, Int32 idClient)
        {
            ComboBoxHelper.fill(comboBoxTarjetas, "SQL_SERVANT.Cliente_Tarjeta ct",
                "ct.Id_Tarjeta", "ct.Id_Tarjeta", "ct.Id_Cliente = '" + idClient + "' AND ct.Habilitada = 1", null);

        }

        public static string tc4UltimosDigitos(Decimal unDecimal)
        {
            string unString = unDecimal.ToString();
            char[] splitString;
            string cenpw = null;
            int NtoShow;

            splitString = new char[unString.Length];
            splitString = unString.ToCharArray();
            NtoShow = 4;
            for (int i = 0; i < unString.Length; i++)
            {
                if (i < unString.Length - NtoShow)
                    cenpw += "*";
                else
                    cenpw += splitString[i];
            }

            return unString;
        }

    }
}
