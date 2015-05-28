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
                TipoCuentasHelper.searchByClient(comboBoxTypeAccount.SelectedValue.ToString(), dgvAccount);
            }
            else
            {
                TipoCuentasHelper.search(textBoxLastname.Text, comboBoxTypeAccount.SelectedValue.ToString(), dgvAccount);
                ClienteHelper.searchAllClient(textBoxLastname.Text, dgvClient);
            }
        }
    }
}
