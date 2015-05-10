using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Login
{
    public partial class FormBlockedUser : Form
    {
        public FormBlockedUser()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
