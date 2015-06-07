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
        private Decimal importeMaximo { get; set; }

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

            isValid = Validaciones.requiredString(this.comboBoxCuentaOrigen.Text.ToString(), "Es necesario que seleccione una cuenta de origen");
            if (isValid)
                transferencia.cuentaOrigen = Convert.ToDecimal(this.comboBoxCuentaOrigen.Text.ToString());
            else
                return null;

            isValid = (Validaciones.validAndRequiredDoubleMoreThan0(this.textBoxImporte.Text.ToString(), "El monto a transferir debe ser mayor a 0")
                && (Convert.ToDecimal(this.textBoxImporte.Text.ToString()) <= importeMaximo));
            if (isValid)
                transferencia.monto = Convert.ToDecimal(this.textBoxImporte.Text.ToString());
            else
                return null;

            if ((this.comboBoxCuentaPropia.SelectedIndex == 0) && (this.comboBoxCuentaTercero.SelectedIndex == 0))
            {
                MessageBox.Show("Es necesario que seleccione una cuenta de destino", "Verifique los datos ingresados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            else if (this.comboBoxCuentaPropia.SelectedIndex != 0)
            {
                transferencia.cuentaDestino = Convert.ToDecimal(this.comboBoxCuentaPropia.Text.ToString());
                transferencia.mismoCliente = 1;
            }

            else
            {
                transferencia.cuentaDestino = Convert.ToDecimal(this.comboBoxCuentaTercero.Text.ToString());
                transferencia.mismoCliente = 0;
            }

            if (transferencia.cuentaOrigen == transferencia.cuentaDestino)
            {
                MessageBox.Show("Las cuentas Origen y Destino no pueden ser iguales", "Verifique los datos ingresados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return transferencia;
        }

        private void buttonTransferencia_Click(object sender, EventArgs e)
        {
            Transferencia transferencia = this.getDataFromForm();
            if (transferencia != null)
            {
                transferencia.fecha = DateHelper.getToday();
                TransferenciaHelper.saveTransferencia(transferencia);
                MessageBox.Show("Se ha realizado correctamente la tranferencia por un monto de " + String.Format("{0:C}", transferencia.monto), "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxCuentaOrigen.SelectedIndex != 0)
            {
                if (comboBoxCuentaPropia.SelectedIndex != 0)
                    importeMaximo = TransferenciaHelper.getImporteMaximo(Convert.ToDecimal(comboBoxCuentaOrigen.SelectedValue.ToString()), 1);

                if (comboBoxCuentaTercero.SelectedIndex != 0)
                    importeMaximo = TransferenciaHelper.getImporteMaximo(Convert.ToDecimal(comboBoxCuentaOrigen.SelectedValue.ToString()), 0);

                labelMax.Text = String.Format("Max. {0:C}", importeMaximo);
            }

            else
                labelMax.Text = "Max. $ 0,00";

        }

        private void comboBoxCuentaPropia_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxCuentaPropia.SelectedIndex != 0)
            {
                comboBoxCuentaTercero.Enabled = false;
                listBoxUsuario.Enabled = false;
            }

            else
            {
                comboBoxCuentaTercero.Enabled = true;
                listBoxUsuario.Enabled = true;
            }
        }

        private void comboBoxCuentaTercero_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxCuentaTercero.SelectedIndex != 0)
                comboBoxCuentaPropia.Enabled = false;

            else
                comboBoxCuentaPropia.Enabled = true;
        }
    }
}
