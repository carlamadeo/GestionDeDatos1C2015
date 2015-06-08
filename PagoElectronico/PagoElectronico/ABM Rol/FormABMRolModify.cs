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
                this.checkBoxHabilitado.Checked = this.rol.habilitado;
                this.labelFuncionalidad.Enabled = false;
                this.comboBoxFuncionalidad.Enabled = false;
                this.groupBoxCrearRol.Text = "Editar Nombre";
                this.buttonSaveName.Text = "Editar";
            }

            else
            {
                RolFuncionalityHelper.fillComboBox(comboBoxFuncionalidad);
                this.checkBoxHabilitado.Checked = true;
            }
        }

        private void reloadGrid()
        {
            RolFuncionalityHelper.getFunctionalityByRolAvailability(rol.id, this.dgvToAdd);
            RolFuncionalityHelper.getFunctionalityByRolEnabled(rol.id, this.dgvSelected);
        }

        private void buttonSaveName_Click(object sender, EventArgs e)
        {
            if (Validaciones.requiredString(txtRolDescription.Text, "El nombre del rol es invalido"))
            {
                rol.habilitado = checkBoxHabilitado.Checked;
                rol.description = txtRolDescription.Text;

                if (edit)
                {
                    rol.primerFuncionalidad = "";
                    RolHelper.editRol(rol);
                    MessageBox.Show("Se modifico el rol \"" + rol.description + "\" correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    if (Validaciones.requiredString(comboBoxFuncionalidad.Text, "Debe seleccionar una funcionalidad"))
                    {
                        rol.primerFuncionalidad = comboBoxFuncionalidad.Text.ToString();
                        RolHelper.editRol(rol);
                        edit = true;
                        reloadGrid();
                        this.labelFuncionalidad.Enabled = false;
                        this.comboBoxFuncionalidad.Enabled = false;
                        MessageBox.Show("Se creo el rol \"" + rol.description + "\" correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.buttonSaveName.Text = "Editar nombre";
                    }
                }
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
                    MessageBox.Show("Se agrego funcionalidad al rol correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadGrid();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una funcionalidad a agregar al rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Primero debe crear el rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Se quito funcionalidad al rol correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadGrid();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una funcionalidad a remover del rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Primero debe crear el rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
