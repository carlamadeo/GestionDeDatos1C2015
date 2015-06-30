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
    public partial class FormSeleccionSuscripciones : Form
    {
        public FormSeleccionSuscripciones()
        {
            InitializeComponent();
        }

        private void FormSeleccionSuscripciones_Load(object sender, EventArgs e)
        {
            textBoxSuscripciones.Text = "1";
        }

        public string EnteredText
        {
            get
            {
                return (textBoxSuscripciones.Text);
            }
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            if(Validaciones.validAndRequiredInt32MoreThan0(textBoxSuscripciones.Text.ToString(),
                "El minimo de suscripciones a rendir es 1") &&
            Validaciones.validAndRequiredInt32LessThan13(textBoxSuscripciones.Text.ToString(),
                "El maximo de suscripciones a rendir es 12"))
            this.Close();
        }
    }
}
