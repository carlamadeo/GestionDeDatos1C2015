using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.ABM_Cliente;
using PagoElectronico.ABM_Cuenta;

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
            String txtBoxCuenta = comboBoxAccount.SelectedValue.ToString();
            if (txtBoxCuenta == String.Empty)
            {
                MessageBox.Show("Debe ingresar una cuenta", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ConsultaSaldos.isValidAccount(txtBoxCuenta))
            {
                MessageBox.Show("Debe ingresar una cuenta válida", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_consulta_saldo";

            command.Parameters.Add(new SqlParameter("@p_consulta_cuenta", SqlDbType.VarChar, 255));
            command.Parameters["@p_consulta_cuenta"].Value = txtBoxCuenta;

            TextBoxHelper.fill(command, txtBoxSaldo);


            command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_consulta_last_5_deposits";

            command.Parameters.Add(new SqlParameter("@p_consulta_cuenta", SqlDbType.VarChar, 255));
            command.Parameters["@p_consulta_cuenta"].Value = txtBoxCuenta;

            DataGridViewHelper.fill(command, dgvDepositos);
            
            command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_consulta_last_5_withdrawal";

            command.Parameters.Add(new SqlParameter("@p_consulta_cuenta", SqlDbType.VarChar, 255));
            command.Parameters["@p_consulta_cuenta"].Value = txtBoxCuenta;

            DataGridViewHelper.fill(command, dgvRetiros);

            command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_consulta_last_10_transfers";

            command.Parameters.Add(new SqlParameter("@p_consulta_cuenta", SqlDbType.VarChar, 255));
            command.Parameters["@p_consulta_cuenta"].Value = txtBoxCuenta;

            DataGridViewHelper.fill(command, dgvTransferencias);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxSaldo.Clear();
            ComboBoxHelper.clean(comboBoxAccount);
            DataGridViewHelper.clean(dgvDepositos);
            DataGridViewHelper.clean(dgvRetiros);
            DataGridViewHelper.clean(dgvTransferencias);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormConsultaSaldos_Load(object sender, EventArgs e)
        {
            Int16 idClient = VarGlobal.usuario.clientId;

            if (idClient != 0)
            {
                dgvClient.Visible = false;
                CuentaHelper.fillCuentasByClient(comboBoxAccount, idClient);
            }
            else
            {
                ClienteHelper.searchAllClient("", dgvClient);
            }
            
        }

        private void dgvClient_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            Int16 idClient = Convert.ToInt16(dgvClient.CurrentRow.Cells[0].Value.ToString());
            ComboBoxHelper.clean(comboBoxAccount);
            CuentaHelper.fillCuentasByClient(comboBoxAccount, idClient);
        }
    }
}
