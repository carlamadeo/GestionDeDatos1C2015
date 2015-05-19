using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    class Paises
    {
        public static void fillComboBox(ComboBox comboBox)
        {
            ComboBoxHelper.fill(comboBox, "SQL_SERVANT.Pais p",
                "p.Id_Pais", "p.Descripcion", "", null);
        }
    }
}
