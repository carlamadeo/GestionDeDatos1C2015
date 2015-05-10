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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            Login.procLogin(textBoxUser.Text, textBoxPassword.Text);
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
