using System;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    public partial class FormABMCliente : Form
    {
        public FormABMCliente()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void FormABMCliente_Load (object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            TextBoxHelper.clean(this);

            TypeIdentification.fillComboBox(comboBoxTipoId);

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            Cliente clientToSearch = this.getDataToSearch();
            ClienteHelper.search(clientToSearch, dgvClient);
            checkClientEnableDisable();
        }

        private Cliente getDataToSearch()
        {
            Cliente cliente = new Cliente();
            cliente.name = textBoxNombre.Text;
            cliente.lastname = textBoxApellido.Text;

            if (comboBoxTipoId.SelectedValue.ToString() != String.Empty)
            {
                cliente.idTypeIdentification = Convert.ToInt32(comboBoxTipoId.SelectedValue.ToString());
            }
            else
            {
                cliente.idTypeIdentification = 0;
            }
            cliente.typeIdentification = comboBoxTipoId.SelectedText;
            if (textBoxNumeroId.Text != String.Empty)
            {
                if (Validaciones.validInt32(textBoxNumeroId.Text, "La identificacion a buscar no es valida, se realiza la busqueda sin filtro"))
                    cliente.identificationNumber = Convert.ToInt32(textBoxNumeroId.Text);
                else
                    cliente.identificationNumber = 0;
            }
            else
            {
                cliente.identificationNumber = 0;
            }

            cliente.mail = textBoxMail.Text;

            return cliente;
        }

        private void buttonDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvClient.CurrentRow != null)
            {
                ClienteHelper.enable(Convert.ToInt32(dgvClient.CurrentRow.Cells[0].Value), false);
                buttonBuscar.PerformClick();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario a deshabilitar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonHabilitar_Click(object sender, EventArgs e)
        {
            if (dgvClient.CurrentRow != null)
            {
                ClienteHelper.enable(Convert.ToInt32(dgvClient.CurrentRow.Cells[0].Value), true);
                buttonBuscar.PerformClick();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario a habilitar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            TextBoxHelper.clean(this);
            DataGridViewHelper.clean(dgvClient);
            ComboBoxHelper.fill(comboBoxTipoId, "SQL_SERVANT.TIPO_IDENTIFICACION ti",
                "ti.Id_Tipo_Identificacion", "ti.Descripcion", "", null);
            buttonDeshabilitar.Enabled = false;
            buttonHabilitar.Enabled = false;
        }

        private void buttonAlta_Click(object sender, EventArgs e)
        {
            FormAltaModificacionCliente formAltaModificacion = new FormAltaModificacionCliente(false, null, null);
            formAltaModificacion.MdiParent = this.MdiParent;
            MdiParent.Size = formAltaModificacion.Size;
            formAltaModificacion.Show();
            this.Close();
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            if (dgvClient.CurrentRow != null)
            {
                FormAltaModificacionCliente formAltaModificacion = new FormAltaModificacionCliente(true, dgvClient.CurrentRow.Cells[0].Value.ToString(), null);
                formAltaModificacion.MdiParent = this.MdiParent;
                MdiParent.Size = formAltaModificacion.Size;
                formAltaModificacion.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario a modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkClientEnableDisable()
        {
            Boolean enabled = ClienteHelper.isEnabled(Convert.ToInt32(dgvClient.CurrentRow.Cells[0].Value));
            if (enabled)
            {
                this.buttonDeshabilitar.Enabled = true;
                this.buttonHabilitar.Enabled = false;
            }
            else
            {
                this.buttonHabilitar.Enabled = true;
                this.buttonDeshabilitar.Enabled = false;
            }
        }

        private void dgvClient_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            checkClientEnableDisable();
        }

    }
}
