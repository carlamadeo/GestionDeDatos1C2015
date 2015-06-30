using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cuenta
{
    public class BancoHelper
    {
        public static void fillComboBox(ComboBox comboBox)
        {
            ComboBoxHelper.fill(comboBox, "SQL_SERVANT.Banco ba",
                "ba.Id_Banco", "ba.Nombre", "", null);
        }
    }
}
