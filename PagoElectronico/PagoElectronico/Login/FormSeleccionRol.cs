using System;
using System.Windows.Forms;
using PagoElectronico.ABM_Rol;

namespace PagoElectronico.Login
{
    public partial class FormSeleccionRol : Form
    {
        private int countRol;
       
        public FormSeleccionRol(int countRol)
        {
            InitializeComponent();
            this.countRol = countRol;
            Usuario user = VarGlobal.usuario;
            Roles.fillComboBoxByUser(comboBox_Roles, user);
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
            Login.checkAccounts();
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
                MessageBox.Show("Debe seleccionar un tipo de rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
