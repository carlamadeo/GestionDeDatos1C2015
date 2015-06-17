using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Utils
{
    public class MonedaHelper
    {
        public static void fillCurrencyComboBox(ComboBox cmb)
        {
            ComboBoxHelper.fill(cmb, "SQL_SERVANT.Moneda mo",
                "mo.Id_Moneda", "mo.Descripcion", "", null);
        }
    }
}
