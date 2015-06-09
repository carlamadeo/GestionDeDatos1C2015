using System.Windows.Forms;

namespace PagoElectronico.Tarjetas
{
    public class Empresa
    {
        public static void fillEmpresa(ComboBox comboBoxEmpresa)
        {
            ComboBoxHelper.fill(comboBoxEmpresa, "SQL_SERVANT.Tarjeta_Empresa te",
                "te.Id_Tarjeta_Empresa", "te.Descripcion", "", null);
        }
    }
}
