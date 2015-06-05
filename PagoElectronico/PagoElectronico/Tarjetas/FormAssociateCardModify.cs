using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.ABM_Cliente;

namespace PagoElectronico.Tarjetas
{
    public partial class FormAssociateCardModify : Form
    {
        Boolean isEdition;
        String idCard;

        public FormAssociateCardModify(Boolean isEdition, String idCard)
        {
            InitializeComponent();
            this.isEdition = isEdition;
            this.idCard = idCard;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.closeWindow();
        }

        private void closeWindow()
        {
            FormAssociateCard formAssociateCard = new FormAssociateCard();
            formAssociateCard.MdiParent = this.MdiParent;
            MdiParent.Size = formAssociateCard.Size;
            this.Close();
            formAssociateCard.Show();
        }

        private void FormAssociateCardModify_Load(object sender, EventArgs e)
        {
            Empresa.fillEmpresa(this.comboBoxCompany);
            this.dtpCreation.Format = DateTimePickerFormat.Custom;
            this.dtpCreation.CustomFormat = "MM yyyy";
            this.dtpExpiration.Format = DateTimePickerFormat.Custom;
            this.dtpExpiration.CustomFormat = "MM yyyy";

            if (isEdition)
            {
                textBoxID.Text = idCard;
                Tarjeta card = TarjetaHelper.getData(idCard);
                textBoxSecurityCod.Text = card.codSeguridad.ToString();
                this.comboBoxCompany.SelectedIndex = this.comboBoxCompany.FindStringExact(card.empresa);
                this.dtpCreation.Value = card.fechaEmision;
                this.dtpExpiration.Value = card.fechaVencimiento;
                if (idCard.Length == 16)
                {
                    textBoxID.Enabled = false;
                    comboBoxCompany.Enabled = false;
                    dtpCreation.Enabled = false;
                    dtpExpiration.Enabled = false;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                Tarjeta card = new Tarjeta();
                card.id = textBoxID.Text;
                card.fechaEmision = DateHelper.firstMonthDay(dtpCreation.Value);
                card.fechaVencimiento = DateHelper.nextMonthFirstDay(dtpExpiration.Value);
                card.codSeguridad = Convert.ToInt16(textBoxSecurityCod.Text);
                card.idEmpresa = Convert.ToInt16(this.comboBoxCompany.SelectedValue.ToString());

                TarjetaHelper.saveWithAssociation(card);

                MessageBox.Show("Se guardo correctamente la tarjeta");

                this.closeWindow();
            }
        }

        private Boolean validate()
        {
            return Validaciones.validInt32(this.textBoxSecurityCod.Text, "El codigo de seguridad debe ser numerico") &&
                Validaciones.requiredString(this.comboBoxCompany.Text, "Debe seleccionar una empresa") &&
                Validaciones.condition(this.textBoxID.Text.Length == 16, "La cantidad de numeros de la tarjeta deben ser 16") &&
                Validaciones.condition(this.textBoxSecurityCod.Text.Length == 3, "El codigo de seguridad debe tener un largo de 3") &&
                Validaciones.condition(!TarjetaHelper.existCard(this.textBoxID.Text), "Ya existe una tarjeta con ese numero");
        }
    }
}
