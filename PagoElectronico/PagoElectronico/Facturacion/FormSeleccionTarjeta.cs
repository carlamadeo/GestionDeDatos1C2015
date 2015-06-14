using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Tarjetas;

namespace PagoElectronico.Facturacion
{
    public partial class FormSeleccionTarjeta : Form
    {
        public List<Facturacion> facturacionesAPagar;
        public Int16 idCliente;

        public FormSeleccionTarjeta(Int16 idCliente, List<Facturacion> facturacionesAPagar)
        {
            InitializeComponent();
            this.idCliente = idCliente;
            this.facturacionesAPagar = facturacionesAPagar;
        }

        private void FormSeleccionTarjeta_Load(object sender, EventArgs e)
        {
            Tarjeta.fillTarjetasByClientWhithout4LastDigits(comboBoxTarjetas, idCliente);
        }

        private void buttonPagar_Click(object sender, EventArgs e)
        {
            if (Validaciones.requiredString(comboBoxTarjetas.Text.ToString(), "Debe seleccionar una tarjeta"))
            {
                FacturacionHelper.saveFacturacion(facturacionesAPagar, idCliente, comboBoxTarjetas.SelectedValue.ToString());
                MessageBox.Show("Se han facturado los cargos correctamente", "Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.irAFormFacturacion();
            }

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.irAFormFacturacion();
        }

        private void irAFormFacturacion()
        {
            FormFacturacion formFacturacion = new FormFacturacion();
            formFacturacion.MdiParent = this.MdiParent;
            MdiParent.Size = formFacturacion.Size;
            this.Close();
            formFacturacion.Show();
        }
    }
}
