using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.ABM_Cuenta;
using PagoElectronico.Utils;
using PagoElectronico.ABM_Cliente;

namespace PagoElectronico.Transferencias
{
    public partial class FormTransferencias : Form
    {
        public FormTransferencias()
        {
            InitializeComponent();
        }

        private void FormTransferencias_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            CuentaHelper.fillCuentasHabilitadasByClient(comboBoxCuentaOrigen, VarGlobal.usuario.clientId);
            CuentaHelper.fillCuentasHabilitadasDeshabilitadasByClient(comboBoxCuentaPropia, VarGlobal.usuario.clientId);
            ListBoxHelper.fill(this.listBoxUsuario, "SQL_SERVANT.Usuario_Cliente uc",
                "uc.Id_Usuario", "uc.Id_Usuario", "", null);

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBoxUsuario_SelectedValueChanged(object sender, EventArgs e)
        {
            CuentaHelper.fillCuentasHabilitadasDeshabilitadasByClient(comboBoxCuentaTercero, ClienteHelper.getClientIdByUserId(listBoxUsuario.SelectedValue.ToString()));
        }

        private Transferencia getDataFromForm()
        {
            Transferencia transferencia = new Transferencia();

            Boolean isValid;

            isValid = Validaciones.validAndRequiredDoubleMoreThan0(this.textBoxImporte.Text.ToString(), "El importe a transferir debe ser mayor a 0");
            if (isValid)
                transferencia.importe = Convert.ToDecimal(this.textBoxImporte.Text.ToString());
            else
                return null;

            return transferencia;
        }

        private void buttonTransferencia_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBoxCuentaOrigen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Decimal importe = TransferenciaHelper.getImporteMaximo(Convert.ToDecimal(comboBoxCuentaOrigen.SelectedValue.ToString()));
            labelMax.Text = String.Format("Max. {0:C}", importe);
        }
    }
}
