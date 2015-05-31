using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;
using PagoElectronico.ABM_Cliente;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormABMCuentaModify : Form
    {
        String idAccount;
        Int16 idClient;
        Boolean isEdition;
        public FormABMCuentaModify(Boolean isEdition, Int16 idClient, String idAccount)
        {
            InitializeComponent();
            this.idAccount = idAccount;
            this.isEdition = isEdition;
            this.idClient = idClient;
        }

        private void FormABMCuentaModify_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            MonedaHelper.fillCurrencyComboBox(comboBoxCurrency);
            PaisHelper.fillComboBox(comboBoxCountry);
            TipoCuentasHelper.fillTypeAccount(comboBoxTypeAccount);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.closeWindow();
        }

        private void closeWindow()
        {
            FormABMCuenta formABMCuenta = new FormABMCuenta();
            formABMCuenta.MdiParent = this.MdiParent;
            MdiParent.Size = formABMCuenta.Size;
            this.Close();
            formABMCuenta.Show();
        }
    }
}
