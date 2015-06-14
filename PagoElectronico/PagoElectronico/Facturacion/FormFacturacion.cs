using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Facturacion
{
    public partial class FormFacturacion : Form
    {
        public FormFacturacion()
        {
            InitializeComponent();
        }

        private void FormFacturacion_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

        }

        private void buttonContinuar_Click(object sender, EventArgs e)
        {
            FormSeleccionTarjeta formSeleccionTarjeta = new FormSeleccionTarjeta();
            formSeleccionTarjeta.MdiParent = this.MdiParent;
            MdiParent.Size = formSeleccionTarjeta.Size;
            formSeleccionTarjeta.Show();
            this.Close();
        }
    }
}
