using System;
using System.Windows.Forms;
using PagoElectronico.ABM_Cliente;
using PagoElectronico.ABM_Cuenta;

namespace PagoElectronico.Consulta_Saldos
{
    public partial class FormConsultaSaldos : Form
    {
        private Int16 idClient;

        public FormConsultaSaldos()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void buttonMovimientos_Click(object sender, EventArgs e)
        {
            if (comboBoxAccount.Text.ToString() != String.Empty)
            {
                String txtBoxCuenta = comboBoxAccount.SelectedValue.ToString();

                if (txtBoxCuenta == String.Empty)
                {
                    MessageBox.Show("Debe ingresar una cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!ConsultaSaldosHelper.isValidAccount(txtBoxCuenta))
                {
                    MessageBox.Show("Debe ingresar una cuenta válida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FormSaldos formSaldos = new FormSaldos(idClient, txtBoxCuenta);
                formSaldos.MdiParent = this.MdiParent;
                MdiParent.Size = formSaldos.Size;
                formSaldos.Show();
                this.Close();
            }

            else
                MessageBox.Show("Debe seleccionar un cliente y numero de cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormConsultaSaldos_Load(object sender, EventArgs e)
        {
            idClient = VarGlobal.usuario.clientId;

            if (idClient != 0)
            {
                dgvClient.Visible = false;
                CuentaHelper.fillCuentasByClient(comboBoxAccount, idClient);
            }
            else
            {
                ClienteHelper.searchAllClient("", dgvClient);
                idClient = Convert.ToInt16(dgvClient.CurrentRow.Cells[0].Value.ToString());
                CuentaHelper.fillCuentasByClient(comboBoxAccount, idClient);
            }
            
        }

        private void dgvClient_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            idClient = Convert.ToInt16(dgvClient.CurrentRow.Cells[0].Value.ToString());
            CuentaHelper.fillCuentasByClient(comboBoxAccount, idClient);
        }

        private void comboBoxAccount_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAccount.SelectedIndex != 0)
                ConsultaSaldosHelper.getSaldo(this.txtBoxSaldo, comboBoxAccount.SelectedValue.ToString());
            else
                this.txtBoxSaldo.Text = "";
        }
    }
}
