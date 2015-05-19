using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.ABM_Rol;
using System.Data.SqlClient;

namespace PagoElectronico.Login
{
    public partial class FormSeleccionRol : Form
    {
        private int countRol;

        public FormSeleccionRol()
        {
            InitializeComponent();

            Usuario user = VarGlobal.usuario;

            countRol = Roles.fillRolByUser(user);

            if (countRol > 0)
            {
                Roles.fillComboBoxByUser(comboBox_Roles, user);
            }
            else
            {
                MessageBox.Show("El usuario no tiene ningun rol habilitado");
            }
        }


        private void goToMenu()
        {
            FormMenu formMenu = new FormMenu();
            this.Hide();
            formMenu.ShowDialog();
            this.Close();
        }

        private void FormSeleccionRol_Load(object sender, EventArgs e)
        {
            if (countRol == 1)
            {
                goToMenu();
            }
        }

        private void button_accept_Click(object sender, EventArgs e)
        {
            Usuario user = VarGlobal.usuario;

            if (comboBox_Roles.Text != "")
            {
                string rolDescription = comboBox_Roles.Text;
                int idRol = Convert.ToInt16(comboBox_Roles.SelectedValue.ToString());

                user.rol = new Rol(idRol, rolDescription);

                goToMenu();
            }

            else
            {
                MessageBox.Show("Debe ser seleccionar un tipo de rol");
            }
        }
    }
}
