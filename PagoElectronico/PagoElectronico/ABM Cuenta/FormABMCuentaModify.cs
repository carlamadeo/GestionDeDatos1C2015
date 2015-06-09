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

            this.dtpCreationDate.Enabled = false;
            this.dtpExpirationDate.Enabled = false;

            if (isEdition)
            {
                Cuenta accountData = CuentaHelper.getAccountData(idClient, idAccount);
                this.comboBoxCountry.SelectedIndex = this.comboBoxCountry.FindStringExact(accountData.countryDescription);
                this.comboBoxCurrency.SelectedIndex = this.comboBoxCurrency.FindStringExact(accountData.currencyDescription);
                this.comboBoxTypeAccount.SelectedIndex = this.comboBoxTypeAccount.FindStringExact(accountData.typeAccountDescription);
                this.dtpCreationDate.Value = accountData.creationDate;
                this.dtpExpirationDate.Value = accountData.expirationDate;
            }
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

        private Cuenta getDataFromForm()
        {
            Cuenta account = new Cuenta();
            Boolean isValid;

            account.idClient = this.idClient;

            if (isEdition) account.id = this.idAccount;

            isValid = Validaciones.requiredString(comboBoxCountry.SelectedValue.ToString(), "Debe seleccionar un pais");
            if (isValid)
                account.idCountry = Convert.ToInt16(comboBoxCountry.SelectedValue.ToString());
            else
                return null;

            isValid = Validaciones.requiredString(comboBoxCurrency.SelectedValue.ToString(), "Debe seleccionar un moneda");
            if (isValid)
                account.idCurrency = Convert.ToInt16(comboBoxCurrency.SelectedValue.ToString());
            else
                return null;

            isValid = Validaciones.requiredString(comboBoxTypeAccount.SelectedValue.ToString(), "Debe seleccionar un tipo de cuenta");
            if (isValid)
                account.idTypeAccount = Convert.ToInt16(comboBoxTypeAccount.SelectedValue.ToString());
            else 
                return null;

            return account;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Cuenta accountData = this.getDataFromForm();
            if (accountData != null)
            {
                CuentaHelper.save(accountData);
                if (isEdition)
                {
                    MessageBox.Show("Modificacion de cuenta realizada con exito");
                    this.closeWindow();
                }
                else
                {
                    MessageBox.Show("Creacion de cuenta realizada con exito");
                    this.closeWindow();
                }
            }
        }
    }
}
