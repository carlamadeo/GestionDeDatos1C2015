using System;
using System.Windows.Forms;
using PagoElectronico.ABM_Rol;

namespace PagoElectronico.ABM_de_Usuario
{
    public partial class FormABMUsuario : Form
    {
        private Boolean fromLogin;

        public FormABMUsuario(Boolean fromLogin)
        {
            this.fromLogin = fromLogin;
            InitializeComponent();
        }

        public FormABMUsuario()
        {
            InitializeComponent();
        }

        private void FormABMUsuario_Load(object sender, EventArgs e)
        {

            if (!fromLogin)
            {
                this.ControlBox = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }

            else
            {
                buttonCreate.Enabled = false;
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            }

            Roles.fillComboBox(comboBoxRol);
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            TextBoxHelper.clean(textBoxName);
            ComboBoxHelper.clean(comboBoxRol);
            DataGridViewHelper.clean(dgvUser);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            UsuarioHelper.search(textBoxName.Text, comboBoxRol.SelectedValue.ToString(), dgvUser);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (fromLogin)
            {
                this.Close();
                Application.OpenForms[0].Show();
            }

            else
                this.Close();
        }

        private void buttonEnable_Click(object sender, EventArgs e)
        {
            if (dgvUser.CurrentRow != null)
            {
                UsuarioHelper.enable(Convert.ToString(dgvUser.CurrentRow.Cells[0].Value), Convert.ToInt16(dgvUser.CurrentRow.Cells[4].Value), true);
                buttonSearch.PerformClick();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario a habilitar");
            }
        }

        private void buttonDisable_Click(object sender, EventArgs e)
        {
            if (dgvUser.CurrentRow != null)
            {
                UsuarioHelper.enable(Convert.ToString(dgvUser.CurrentRow.Cells[0].Value), Convert.ToInt16(dgvUser.CurrentRow.Cells[4].Value), false);
                buttonSearch.PerformClick();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario a deshabilitar");
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            FormABMUsuarioModify formABMUsuarioModify = new FormABMUsuarioModify(false, null, false);
            formABMUsuarioModify.MdiParent = this.MdiParent;
            MdiParent.Size = formABMUsuarioModify.Size;
            formABMUsuarioModify.Show();
            this.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dgvUser.CurrentRow != null)
            {
                FormABMUsuarioModify formABMUsuarioModify = new FormABMUsuarioModify(true, dgvUser.CurrentRow.Cells[0].Value.ToString(), fromLogin);
                
                if (!fromLogin)
                {
                    formABMUsuarioModify.MdiParent = this.MdiParent;
                    MdiParent.Size = formABMUsuarioModify.Size;
                }

                formABMUsuarioModify.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario a modificar");
            }
        }
    }
}
