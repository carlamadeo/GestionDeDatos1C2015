﻿using System;
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
    }
}
