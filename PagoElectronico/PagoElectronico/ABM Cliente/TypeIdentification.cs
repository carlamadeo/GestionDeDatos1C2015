using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    class TypeIdentification
    {
        public static void fillComboBox(ComboBox comboBox)
        {
            ComboBoxHelper.fill(comboBox, "SQL_SERVANT.TIPO_IDENTIFICACION ti",
                "ti.Id_Tipo_Identificacion", "ti.Descripcion", "", null);
        }
    }
}
