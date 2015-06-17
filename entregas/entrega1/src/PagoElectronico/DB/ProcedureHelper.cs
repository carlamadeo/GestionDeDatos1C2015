using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace PagoElectronico.DB
{
    public class ProcedureHelper
    {
        public static object execute(SqlCommand command)
        {
            object result = null;
            try
            {
                SqlConnection conn = Connection.getConnection();
                command.Connection = conn;
                result = command.ExecuteScalar();
                Connection.close(conn);
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = sqlEx.Message;
                MessageBox.Show(errorMessage, "Ejecucion de Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                MessageBox.Show(errorMessage, "Ejecucion de Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        public static Decimal execute(SqlCommand command, String actionDescription, Boolean showMessage)
        {
            int affectedRows = -1;
            Decimal identity = -1;
            SqlConnection conn = Connection.getConnection();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            affectedRows = command.ExecuteNonQuery();

            if (affectedRows > 0)
            {
                string sqlIdentity = "SELECT @@IDENTITY";
                using (SqlCommand cmdIdentity = new SqlCommand(sqlIdentity, conn))
                {
                    if (!(cmdIdentity.ExecuteScalar() is DBNull))
                        identity = Convert.ToDecimal(cmdIdentity.ExecuteScalar());
                }
            }

            if (showMessage)
            {
                MessageBox.Show(actionDescription, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Connection.close(conn);
            return identity;
        }
    }
}
