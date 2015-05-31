using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System;
namespace PagoElectronico
{
    public class DataGridViewHelper
    {
        public static void clean(DataGridView dgv)
        {
            if (dgv.CurrentCell != null)
            {
                DataTable dt = (DataTable)dgv.DataSource;
                dt.Clear();
            }
        }

        public static Boolean hasElementSelected(DataGridView dgv)
        {
            return dgv.SelectedRows.Count > 0;
        }

        public static DataTable fill(SqlCommand command, DataGridView dataGridView)
        {
            SqlConnection conn = Connection.getConnection();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            dataGridView.DataSource = dataTable;

            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AllowUserToAddRows = false;

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (!column.Name.Equals("checkbox") && !column.Name.Equals("chk"))
                    column.ReadOnly = true;
            }

            Connection.close(conn);
            return dataTable;
        }


    }
}