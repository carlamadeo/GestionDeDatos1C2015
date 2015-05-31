using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.ABM_Cliente;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormABMCuenta : Form
    {
        Boolean isClient;
        public FormABMCuenta()
        {
            InitializeComponent();
            isClient = VarGlobal.usuario.clientId != 0;
        }

        private void FormABMCuenta_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            TipoCuentas.fillComboBox(comboBoxTypeAccount);

            if (this.isClient)
            {
                textBoxLastname.Enabled = false;
                dgvClient.Visible = false;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            TextBoxHelper.clean(textBoxLastname);
            ComboBoxHelper.clean(comboBoxTypeAccount);
            DataGridViewHelper.clean(dgvAccount);

            TipoCuentas.fillComboBox(comboBoxTypeAccount);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (isClient)
            {
                TipoCuentasHelper.searchByClient(VarGlobal.usuario.clientId, comboBoxTypeAccount.SelectedValue.ToString(), dgvAccount);
            }
            else
            {
                ClienteHelper.searchAllClient(textBoxLastname.Text, dgvClient);
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            String idCuenta;
            Int16 idCliente;
            if (isClient)
            {
                if (dgvAccount.CurrentRow != null)
                {
                    idCuenta = dgvAccount.CurrentRow.Cells[3].Value.ToString();
                    idCliente = VarGlobal.usuario.clientId;
                    FormABMCuentaModify formABMCuentaModify = new FormABMCuentaModify(true, idCliente, idCuenta);
                    formABMCuentaModify.MdiParent = this.MdiParent;
                    MdiParent.Size = formABMCuentaModify.Size;
                    formABMCuentaModify.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una cuenta a modificar");
                }
            }
            else
            {
                Boolean dgvAccountSelected = DataGridViewHelper.hasElementSelected(dgvAccount);
                Boolean dgvClientSelected = DataGridViewHelper.hasElementSelected(dgvClient);
                if (dgvAccountSelected && dgvClientSelected)
                {
                    idCuenta = dgvAccount.CurrentRow.Cells[3].Value.ToString();
                    idCliente = Convert.ToInt16(dgvAccount.CurrentRow.Cells[0].Value.ToString());
                    FormABMCuentaModify formABMCuentaModify = new FormABMCuentaModify(true, idCliente, idCuenta);
                    formABMCuentaModify.MdiParent = this.MdiParent;
                    MdiParent.Size = formABMCuentaModify.Size;
                    formABMCuentaModify.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un cliente y una cuenta ha modificar");
                }
            }
        }

        private void dgvClient_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            Int16 clientId = Convert.ToInt16(dgvClient.CurrentRow.Cells[0].Value.ToString());
            TipoCuentasHelper.searchByClient(clientId, comboBoxTypeAccount.SelectedValue.ToString(), dgvAccount);
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Boolean dgvClientSelected = DataGridViewHelper.hasElementSelected(dgvClient);
            if (dgvClientSelected)
            {
                Int16 idCliente = Convert.ToInt16(dgvClient.CurrentRow.Cells[0].Value.ToString());
                FormABMCuentaModify formABMCuentaModify = new FormABMCuentaModify(false, idCliente, "");
                formABMCuentaModify.MdiParent = this.MdiParent;
                MdiParent.Size = formABMCuentaModify.Size;
                formABMCuentaModify.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente ha crear cuenta");
            }
        }
    }
}
