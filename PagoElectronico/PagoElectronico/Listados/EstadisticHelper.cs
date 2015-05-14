using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PagoElectronico.Listados
{
    public class EstadisticHelper
    {
        public static void fillYear(ComboBox comboBoxYear)
        {
            ComboBoxHelper.fill(comboBoxYear, "SQL_SERVANT.Ano a",
                "a.Ano", "a.Ano", "", null);
        }

        public static void fillEstadistic(ComboBox comboBoxEstadistic)
        {
            ComboBoxHelper.fill(comboBoxEstadistic, "SQL_SERVANT.Estadistica e",
                "e.Store_Procedure", "e.Descripcion", "", null);
        }

        public static void fillQuater(ComboBox comboBoxQuater)
        {
            ComboBoxHelper.fill(comboBoxQuater, "SQL_SERVANT.Trimestre t",
                "t.Fechas", "t.Descripcion", "", null);
        }

        public static void loadEstadistic(String storeProcedure, DateTime from, DateTime to, DataGridView dgv)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT." + storeProcedure;
            command.Parameters.AddWithValue("@p_estadistic_from", from);
            command.Parameters.AddWithValue("@p_estadistic_to", to);

            DataGridViewHelper.fill(command, dgv);
        }
    }
}
