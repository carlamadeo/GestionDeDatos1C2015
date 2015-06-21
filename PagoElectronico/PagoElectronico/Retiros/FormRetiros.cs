using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.ABM_Cuenta;
using PagoElectronico.ABM_Cliente;

namespace PagoElectronico.Retiros
{
    public partial class FormRetiros : Form
    {
        private Decimal importeMaximo { get; set; }

        public FormRetiros()
        {
            InitializeComponent();
        }

        private void FormRetiros_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            BancoHelper.fillComboBox(comboBoxBank);
            reloadGrid();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reloadGrid()
        {
            TipoCuentasHelper.searchAccountEnabled(VarGlobal.usuario.clientId, dgvAccount);
        }

        public void reloadImporteMaximo()
        {
            this.importeMaximo = CuentaHelper.getImporteMaximo(Convert.ToDecimal(dgvAccount.CurrentRow.Cells[0].Value.ToString()));
            labelMax.Text = String.Format("Max. {0:C}", importeMaximo);
        }

        private void buttonRetire_Click(object sender, EventArgs e)
        {
            if (dgvAccount.CurrentRow != null)
            {
                if (validaciones())
                {
                    String accountId = dgvAccount.CurrentRow.Cells[0].Value.ToString();
                    Decimal amount = Convert.ToDecimal(textBoxCount.Text.ToString().Replace(".", ","));
                    Int32 bankId = Convert.ToInt16(comboBoxBank.SelectedValue.ToString());
                    String checkNumber = RetirosHelper.generateExtraction(accountId, amount, "USD", bankId);
                    MessageBox.Show("Se genero el retiro, nro de cheque: " + checkNumber, "Retiro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadGrid();
                    reloadImporteMaximo();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una cuenta de la cual generar un retiro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean validaciones() 
        {

            return Validaciones.validAndRequiredInt32(textBoxNroDoc.Text, "El nro de documento debe ser numerico")
            && ClienteHelper.checkIdentificationIsCorrect(Convert.ToInt32(textBoxNroDoc.Text))
            && Validaciones.condition(importeMaximo >= Convert.ToDecimal(textBoxCount.Text.ToString().Replace(".", ",")), "El importe maximo a retirar es de " + String.Format("{0:C}", importeMaximo))
            && Validaciones.validAndRequiredDecimalMoreThan0(textBoxCount.Text, "El importe debe ser numerico y mayor a 0")
            && Validaciones.requiredString(comboBoxBank.SelectedValue.ToString(), "Debe seleccionar un banco de donde extraer");
        }

        private void dgvAccount_SelectionChanged(object sender, EventArgs e)
        {
            this.reloadImporteMaximo();
        }
    }
}
