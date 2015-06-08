using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Consulta_Saldos
{
    public partial class FormConsultaSaldos : Form
    {
        public FormConsultaSaldos()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (txtBoxCuenta.Text == String.Empty)
            {
                MessageBox.Show("Debe ingresar una cuenta", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ConsultaSaldos.isValidAccount(txtBoxCuenta.Text))
            {
                MessageBox.Show("Debe ingresar una cuenta válida", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_consulta_saldo";

            command.Parameters.Add(new SqlParameter("@p_consulta_cuenta", SqlDbType.VarChar, 255));
            command.Parameters["@p_consulta_cuenta"].Value = txtBoxCuenta.Text;

            TextBoxHelper.fill(command, txtBoxSaldo);


            command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_consulta_last_5_deposits";

            command.Parameters.Add(new SqlParameter("@p_consulta_cuenta", SqlDbType.VarChar, 255));
            command.Parameters["@p_consulta_cuenta"].Value = txtBoxCuenta.Text;

            DataGridViewHelper.fill(command, dgvDepositos);
            
            command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_consulta_last_5_withdrawal";

            command.Parameters.Add(new SqlParameter("@p_consulta_cuenta", SqlDbType.VarChar, 255));
            command.Parameters["@p_consulta_cuenta"].Value = txtBoxCuenta.Text;

            DataGridViewHelper.fill(command, dgvRetiros);

            command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_consulta_last_10_transfers";

            command.Parameters.Add(new SqlParameter("@p_consulta_cuenta", SqlDbType.VarChar, 255));
            command.Parameters["@p_consulta_cuenta"].Value = txtBoxCuenta.Text;

            DataGridViewHelper.fill(command, dgvTransferencias);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxCuenta.Clear();
            txtBoxSaldo.Clear();
            DataGridViewHelper.clean(dgvDepositos);
            DataGridViewHelper.clean(dgvRetiros);
            DataGridViewHelper.clean(dgvTransferencias);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
