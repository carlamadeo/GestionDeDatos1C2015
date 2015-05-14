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
    public partial class FormABMRolModify : Form
    {
        private Rol rol;
        private Boolean edit;

        public FormABMRolModify(Boolean edit, Rol rol)
        {
            InitializeComponent();
            this.edit = edit;
            this.rol = rol;
        }

        private void FormABMRolModify_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            reloadGrid();

            if (edit)
            {
                this.txtRolDescription.Text = this.rol.description;
                this.buttonSaveName.Text = "Editar nombre";
            }
        }

        private void reloadGrid()
        {
            RolFuncionalityHelper.geFunctionalityByRolAvailability(rol.id, this.dgvToAdd);
            RolFuncionalityHelper.getFunctionalityByRolEnabled(rol.id, this.dgvSelected);
        }

        private void buttonSaveName_Click(object sender, EventArgs e)
        {
            if (Validaciones.requiredString(txtRolDescription.Text, "Debe poner un rol valido"))
            {
                rol.description = txtRolDescription.Text;
                RolHelper.editRol(rol);
                MessageBox.Show("Se modifico/creo el rol correctamente");
                this.buttonSaveName.Text = "Editar nombre";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (rol.id != 0)
            {
                if (dgvToAdd.CurrentRow != null)
                {
                    Int32 idFunctionality = Convert.ToInt32(dgvToAdd.CurrentRow.Cells[0].Value);
                    RolFuncionalityHelper.setFunctionalityToRol(rol.id, idFunctionality);
                    MessageBox.Show("Se agrego funcionalidad al rol correctamente");
                    reloadGrid();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una funcionalidad a agregar al rol");
                }
            }
            else
            {
                MessageBox.Show("Debe primero guardar un nombre de rol");
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (rol.id != 0)
            {
                if (dgvSelected.CurrentRow != null)
                {
                    Int32 idFunctionality = Convert.ToInt32(dgvSelected.CurrentRow.Cells[0].Value);
                    RolFuncionalityHelper.removeFunctionalityToRol(rol.id, idFunctionality);
                    MessageBox.Show("Se quito funcionalidad al rol correctamente");
                    reloadGrid();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una funcionalidad a remover al rol");
                }
            }
            else
            {
                MessageBox.Show("Debe primero guardar un nombre de rol");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.closeWindow();
        }

        private void closeWindow()
        {
            FormABMRol formABMRol = new FormABMRol();
            formABMRol.MdiParent = this.MdiParent;
            MdiParent.Size = formABMRol.Size;
            this.Close();
            formABMRol.Show();
        }
    }
}
