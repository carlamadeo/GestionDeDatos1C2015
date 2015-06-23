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
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Tarjeta.fillTarjetasByClientWhithout4LastDigits(comboBoxTarjetas, idCliente);
            labelImporte.Text = "Importe a Pagar: " + String.Format("{0:C}", FacturacionHelper.getImporteAPagar(facturacionesAPagar));    
        }

        private void buttonPagar_Click(object sender, EventArgs e)
        {
            String numberCard = comboBoxTarjetas.SelectedValue.ToString();
            if (Validaciones.requiredString(numberCard, "Debe seleccionar una tarjeta")
                && Validaciones.tarjetaNoVencida(numberCard))
            {
                FacturacionHelper.saveFacturacion(facturacionesAPagar, idCliente, numberCard);
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
