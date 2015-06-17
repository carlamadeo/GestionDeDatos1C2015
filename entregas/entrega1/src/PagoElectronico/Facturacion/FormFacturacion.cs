using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.ABM_Cliente;

namespace PagoElectronico.Facturacion
{
    public partial class FormFacturacion : Form
    {
        public List<Facturacion> facturacionesPendientes;
        public List<Facturacion> facturacionesAPagar;
        public Int16 idCliente;

        public FormFacturacion()
        {
            InitializeComponent();
        }

        private void FormFacturacion_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            facturacionesPendientes = new List<Facturacion>();
            facturacionesAPagar = new List<Facturacion>();

            if (VarGlobal.usuario.clientId != 0)
            {
                idCliente = VarGlobal.usuario.clientId;
                facturacionesPendientes = FacturacionHelper.getListFacturacionesPendientes(idCliente);
                comboBoxUsuario.Visible = false;
                labelUsuario.Visible = false;
            }

            else
            {
                ComboBoxHelper.fill(this.comboBoxUsuario, "SQL_SERVANT.Usuario_Cliente uc",
                "uc.Id_Usuario", "uc.Id_Usuario", "", null);
            }

                FacturacionHelper.fillDGVFacturaciones(dgvPendientes, facturacionesPendientes);
        }

        private void buttonContinuar_Click(object sender, EventArgs e)
        {
            if (facturacionesAPagar.Count != 0)
            {
                FormSeleccionTarjeta formSeleccionTarjeta = new FormSeleccionTarjeta(idCliente, facturacionesAPagar);
                formSeleccionTarjeta.MdiParent = this.MdiParent;
                MdiParent.Size = formSeleccionTarjeta.Size;
                formSeleccionTarjeta.Show();
                this.Close();
            }
            else
                MessageBox.Show("Debe seleccionar al menos una transaccion a pagar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (dgvPendientes.Rows.Count != 0)
            {
                Facturacion facturacionElegida = (Facturacion)dgvPendientes.CurrentRow.DataBoundItem;
                if (this.isOpenOrChange(facturacionElegida))
                {
                    FormSeleccionSuscripciones formSeleccionSuscripciones = new FormSeleccionSuscripciones();
                    formSeleccionSuscripciones.ShowDialog();
                    string userEnteredText = formSeleccionSuscripciones.EnteredText;
                    facturacionElegida.suscripciones = Convert.ToInt16(userEnteredText);
                    formSeleccionSuscripciones.Dispose();
                }

                facturacionesPendientes.Remove(facturacionElegida);
                facturacionesAPagar.Add(facturacionElegida);
                refreshDgvs();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dgvAPagar.Rows.Count != 0)
            {
                Facturacion facturacionElegida = (Facturacion)dgvAPagar.CurrentRow.DataBoundItem;
                facturacionElegida.suscripciones = 1;
                facturacionesAPagar.Remove(facturacionElegida);
                facturacionesPendientes.Add(facturacionElegida);
                refreshDgvs();
            }
        }

        private void refreshDgvs()
        {
            dgvPendientes.DataSource = null;
            dgvAPagar.DataSource = null;
            FacturacionHelper.fillDGVFacturaciones(dgvPendientes, facturacionesPendientes);
            FacturacionHelper.fillDGVFacturaciones(dgvAPagar, facturacionesAPagar);
        }

        private void comboBoxUsuario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            idCliente = ClienteHelper.getClientIdByUserId(comboBoxUsuario.Text.ToString());
            facturacionesPendientes = FacturacionHelper.getListFacturacionesPendientes(idCliente);
            refreshDgvs();
        }

        private Boolean isOpenOrChange(Facturacion facturacion)
        {
            if ((facturacion.descripcionGasto == "Apertura de cuenta.") ||
                (facturacion.descripcionGasto == "Modificacion de cuenta."))
                return true;
            else
                return false;
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
