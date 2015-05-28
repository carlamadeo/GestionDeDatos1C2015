using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cuenta
{
    class Cuenta
    {
        public static void fillCuentasHabilitadasByClient(ComboBox comboBoxCuenta, Int32 idClient)
        {
            ComboBoxHelper.fill(comboBoxCuenta, "SQL_SERVANT.Cliente_Cuenta cc INNER JOIN SQL_SERVANT.Cuenta c ON cc.Id_Cuenta = c.Id_Cuenta",
                "c.Id_Cuenta", "c.Id_Cuenta", "cc.Id_Cliente = '" + idClient + "' AND c.Id_Estado_Cuenta = 4", null);
        }
    }
}
