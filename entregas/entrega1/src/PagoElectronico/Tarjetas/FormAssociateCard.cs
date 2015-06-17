using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Tarjetas
{
    public partial class FormAssociateCard : Form
    {
        public FormAssociateCard()
        {
            InitializeComponent();
        }

        private void FormAssociateCard_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            reloadGrid();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            reloadGrid();
        }

        private void reloadGrid()
        {
            TarjetaHelper.getClientCard(VarGlobal.usuario.clientId, dgvCard);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAssociate_Click(object sender, EventArgs e)
        {
            if (dgvCard.CurrentRow != null)
            {
                Boolean associate;
                if (dgvCard.CurrentRow.Cells[4].Value.ToString().Equals("ASOCIADA"))
                    associate = true;
                else
                    associate = false;
                String cardId = dgvCard.CurrentRow.Cells[0].Value.ToString();
                Int16 idCliente = VarGlobal.usuario.clientId;
                TarjetaHelper.associate(cardId, idCliente, !associate);
                reloadGrid();
                MessageBox.Show("Se asocio/desasocio la tarjeta correctamente");                
            }
            else
            {
                MessageBox.Show("Debe seleccionar una cuenta a modificar");
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            FormAssociateCardModify formABMAssociateCardModify = new FormAssociateCardModify(false, "");
            formABMAssociateCardModify.MdiParent = this.MdiParent;
            MdiParent.Size = formABMAssociateCardModify.Size;
            formABMAssociateCardModify.Show();
            this.Close();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            Boolean dgvCardSelected = DataGridViewHelper.hasElementSelected(dgvCard);
            if (dgvCardSelected)
            {
                String idCard = dgvCard.CurrentRow.Cells[0].Value.ToString();
                FormAssociateCardModify formABMAssociateCardModify = new FormAssociateCardModify(true, idCard);
                formABMAssociateCardModify.MdiParent = this.MdiParent;
                MdiParent.Size = formABMAssociateCardModify.Size;
                formABMAssociateCardModify.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una tarjeta ha modificar");
            }
        }
    }
}
