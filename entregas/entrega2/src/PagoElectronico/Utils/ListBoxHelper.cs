using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace PagoElectronico.Utils
{
    class ListBoxHelper
    {
        public static void fill(ListBox listBox, String dataSource, String valueMember, String displayMember, String whereMember, String orderMember)
        {
            SqlConnection conn = Connection.getConnection();
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(String.Format("SELECT {0} AS 'Value',{1} AS 'Display' FROM {2} {3} {4}", valueMember, displayMember, dataSource, String.IsNullOrEmpty(whereMember) ? "" : "WHERE " + whereMember, String.IsNullOrEmpty(orderMember) ? "" : "ORDER BY " + orderMember), conn);
            dataAdapter.Fill(dataSet, dataSource);
            listBox.DataSource = dataSet.Tables[0].DefaultView;
            listBox.ValueMember = "Value";
            listBox.DisplayMember = "Display";
            listBox.Text = "(Seleccione una Opcion)";

            Connection.close(conn);
        }
    }
}
