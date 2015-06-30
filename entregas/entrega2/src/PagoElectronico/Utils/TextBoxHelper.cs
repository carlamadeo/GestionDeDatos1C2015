using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System;
namespace PagoElectronico
{
    public class TextBoxHelper
    {
        public static void clean(Control parent)
        {
            TextBox t;
            foreach (Control c in parent.Controls)
            {
                t = c as TextBox;
                if (t != null)
                {
                    t.Clear();
                }
                if (c.Controls.Count > 0)
                {
                    clean(c);
                }
            }
        }

        public static TextBox fill(SqlCommand command, TextBox txtBox)
        {
            SqlConnection conn = Connection.getConnection();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

 
            DataTable  dt = new DataTable ();
            sqlDataAdapter.Fill(dt);
            DataRow row = dt.Rows[0];
            txtBox.Text = Convert.ToString(row["Importe"]);

            Connection.close(conn);
            return txtBox;

            /*
             SqlConnection myConnection = new SqlConnection("your connection string
here");
myCommand.Connection = myConnection;
SqlDataAdapter da = new SqlDataAdapter(myCommand);

DataSet data = new DataSet();
da.Fill(data);


foreach(DataRow Row in Data.Tables[0].Rows)
{
txtNombre.Text = Row.cells["Nombre"].value.ToString();
             */
        }

    }
}