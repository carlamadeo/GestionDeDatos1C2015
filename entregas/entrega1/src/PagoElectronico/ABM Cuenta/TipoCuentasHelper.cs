using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace PagoElectronico.ABM_Cuenta
{
    public class TipoCuentasHelper
    {
        public static void search(string lastname, string idTypeAccount, DataGridView dgvAccount)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_account_search";

            command.Parameters.Add(new SqlParameter("@p_account_lastname", SqlDbType.VarChar, 255));
            if (lastname == string.Empty)
                command.Parameters["@p_account_lastname"].Value = null;
            else
                command.Parameters["@p_account_lastname"].Value = lastname;

            command.Parameters.Add(new SqlParameter("@p_account_type_id", SqlDbType.Int));
            if (idTypeAccount == string.Empty)
                command.Parameters["@p_account_type_id"].Value = null;
            else
                command.Parameters["@p_account_type_id"].Value = Convert.ToInt16(idTypeAccount);

            DataGridViewHelper.fill(command, dgvAccount);
        }

        public static void searchByClient(Int16 clientId, string idTypeAccount, DataGridView dgvAccount)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_account_search";

            command.Parameters.Add(new SqlParameter("@p_account_lastname", SqlDbType.VarChar, 255));
            command.Parameters["@p_account_lastname"].Value = null;

            command.Parameters.Add(new SqlParameter("@p_account_type_id", SqlDbType.Int));
            if (idTypeAccount == string.Empty)
                command.Parameters["@p_account_type_id"].Value = null;
            else
                command.Parameters["@p_account_type_id"].Value = Convert.ToInt16(idTypeAccount);

            command.Parameters.Add(new SqlParameter("@p_account_client_id", SqlDbType.Int));
            command.Parameters["@p_account_client_id"].Value = clientId;


            DataGridViewHelper.fill(command, dgvAccount);
        }

        public static void fillTypeAccount(ComboBox cmb)
        {
            ComboBoxHelper.fill(cmb, "SQL_SERVANT.Tipo_Cuenta tc",
                "tc.Id_Tipo_Cuenta", "tc.Descripcion", "", null);
        }

        public static void searchAccountEnabled(Int16 clientId, DataGridView dgvAccount)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_account_enabled_with_credit_search";

            command.Parameters.Add(new SqlParameter("@p_account_client_id", SqlDbType.Int));
            command.Parameters["@p_account_client_id"].Value = clientId;

            command.Parameters.Add(new SqlParameter("@p_account_today", SqlDbType.DateTime));
            command.Parameters["@p_account_today"].Value = DateHelper.getToday();

            DataGridViewHelper.fill(command, dgvAccount);
        }
    }
}
