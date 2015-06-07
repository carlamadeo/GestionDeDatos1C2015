using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    public class PaisHelper
    {
        public static void fillComboBox(ComboBox comboBox)
        {
            ComboBoxHelper.fill(comboBox, "SQL_SERVANT.Pais p",
                "p.Id_Pais", "p.Descripcion", "", null);
        }
    }
}
