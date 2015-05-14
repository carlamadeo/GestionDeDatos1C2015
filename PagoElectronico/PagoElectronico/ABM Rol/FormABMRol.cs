using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Rol
{
    public partial class FormABMRol : Form
    {
        public FormABMRol()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            RolHelper.search(textBoxNameRol.Text, dgvRol);
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            TextBoxHelper.clean(this);
            DataGridViewHelper.clean(dgvRol);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Rol rolToCreate = new Rol();
            rolToCreate.description = "";
            rolToCreate.id = 0;
            FormABMRolModify formABMRolModify = new FormABMRolModify(false, rolToCreate);
            formABMRolModify.MdiParent = this.MdiParent;
            MdiParent.Size = formABMRolModify.Size;
            formABMRolModify.Show();
            this.Close();
        }

        private void buttonEnabled_Click(object sender, EventArgs e)
        {
            if (dgvRol.CurrentRow != null)
            {
                RolHelper.enable(Convert.ToInt32(dgvRol.CurrentRow.Cells[0].Value), true);
                buttonSearch.PerformClick();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un rol habilitar");
            }
        }

        private void buttonDisable_Click(object sender, EventArgs e)
        {
            if (dgvRol.CurrentRow != null)
            {
                RolHelper.enable(Convert.ToInt32(dgvRol.CurrentRow.Cells[0].Value), false);
                buttonSearch.PerformClick();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un rol habilitar");
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dgvRol.CurrentRow != null)
            {
                Rol rolToModify = new Rol();
                rolToModify.id = Convert.ToInt32(dgvRol.CurrentRow.Cells[0].Value.ToString());
                rolToModify.description = dgvRol.CurrentRow.Cells[1].Value.ToString();
                FormABMRolModify formABMRolModify = new FormABMRolModify(true, rolToModify);
                formABMRolModify.MdiParent = this.MdiParent;
                MdiParent.Size = formABMRolModify.Size;
                formABMRolModify.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un rol a modificar");
            }
        }

        private void FormABMRol_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
