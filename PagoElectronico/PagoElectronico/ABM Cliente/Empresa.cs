using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    class Empresa
    {
        public static void fillEmpresa(ComboBox comboBoxEmpresa)
        {
            ComboBoxHelper.fill(comboBoxEmpresa, "SQL_SERVANT.Tarjeta_Empresa te",
                "te.Id_Tarjeta_Empresa", "te.Descripcion", "", null);
        }
    }
}
