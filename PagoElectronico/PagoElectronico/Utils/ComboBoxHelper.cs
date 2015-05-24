using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System;
using PagoElectronico.ABM_Cliente;
namespace PagoElectronico
{
    public class ComboBoxHelper
    {
        public static void clean(Control parent)
        {
            ComboBox t;
            foreach (Control c in parent.Controls)
            {
                t = c as ComboBox;
                if (t != null)
                {
                    t.SelectedIndex = -1;
                }
                if (c.Controls.Count > 0)
                {
                    clean(c);
                }
            }
        }

        public static void fill(ComboBox comboBox, String dataSource, String valueMember, String displayMember, String whereMember, String orderMember)
        {
            SqlConnection conn = Connection.getConnection();
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(String.Format("SELECT {0} AS 'Value',{1} AS 'Display' FROM {2} {3} {4}", valueMember, displayMember, dataSource, String.IsNullOrEmpty(whereMember) ? "" : "WHERE " + whereMember, String.IsNullOrEmpty(orderMember) ? "" : "ORDER BY " + orderMember), conn);
            dataAdapter.Fill(dataSet, dataSource);
            DataRow row = dataSet.Tables[0].NewRow();
            dataSet.Tables[0].Rows.InsertAt(row, 0);
            comboBox.DataSource = dataSet.Tables[0].DefaultView;
            comboBox.ValueMember = "Value";
            comboBox.DisplayMember = "Display";
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Text = "(Seleccione una Opcion)";

            Connection.close(conn);
        }

        public static void fillFromProcedure(ComboBox comboBox, String dataSource, String valueMember, String displayMember, String whereMember, String orderMember)
        {
            SqlConnection conn = Connection.getConnection();
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("EXEC " + dataSource, conn);
            dataAdapter.Fill(dataSet, dataSource);
            DataRow row = dataSet.Tables[0].NewRow();
            dataSet.Tables[0].Rows.InsertAt(row, 0);
            comboBox.DataSource = dataSet.Tables[0].DefaultView;
            comboBox.ValueMember = valueMember;
            comboBox.DisplayMember = displayMember;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Text = "(Seleccione una Opcion)";

            Connection.close(conn);
        }

    }
}