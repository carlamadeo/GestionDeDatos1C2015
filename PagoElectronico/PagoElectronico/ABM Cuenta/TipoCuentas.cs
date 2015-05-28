using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cuenta
{
    public class TipoCuentas
    {
        public static void fillComboBox(ComboBox comboBox)
        {
            ComboBoxHelper.fill(comboBox, "SQL_SERVANT.Tipo_Cuenta tc",
                "tc.Id_Tipo_Cuenta", "tc.Descripcion", "", null);
        }
    }
}
