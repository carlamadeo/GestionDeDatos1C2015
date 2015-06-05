namespace PagoElectronico.Tarjetas
{
    partial class FormAssociateCardModify
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxSecurityCod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCompany = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.dtpCreation = new System.Windows.Forms.DateTimePicker();
            this.dtpExpiration = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro °";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(139, 28);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(143, 20);
            this.textBoxID.TabIndex = 1;
            // 
            // textBoxSecurityCod
            // 
            this.textBoxSecurityCod.Location = new System.Drawing.Point(139, 125);
            this.textBoxSecurityCod.Name = "textBoxSecurityCod";
            this.textBoxSecurityCod.Size = new System.Drawing.Size(143, 20);
            this.textBoxSecurityCod.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Empresa";
            // 
            // comboBoxCompany
            // 
            this.comboBoxCompany.FormattingEnabled = true;
            this.comboBoxCompany.Location = new System.Drawing.Point(139, 75);
            this.comboBoxCompany.Name = "comboBoxCompany";
            this.comboBoxCompany.Size = new System.Drawing.Size(143, 21);
            this.comboBoxCompany.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cod. Seguridad";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(190, 177);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Guardar";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(360, 177);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 7;
            this.buttonBack.Text = "Volver";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // dtpCreation
            // 
            this.dtpCreation.Location = new System.Drawing.Point(419, 27);
            this.dtpCreation.Name = "dtpCreation";
            this.dtpCreation.Size = new System.Drawing.Size(200, 20);
            this.dtpCreation.TabIndex = 8;
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Location = new System.Drawing.Point(419, 74);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(200, 20);
            this.dtpExpiration.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(304, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Fecha Emision";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(304, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Fecha Vencimiento";
            // 
            // FormAssociateCardModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 258);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpExpiration);
            this.Controls.Add(this.dtpCreation);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxCompany);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSecurityCod);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.label1);
            this.Name = "FormAssociateCardModify";
            this.Text = "FormAssociateCardModify";
            this.Load += new System.EventHandler(this.FormAssociateCardModify_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.TextBox textBoxSecurityCod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxCompany;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.DateTimePicker dtpCreation;
        private System.Windows.Forms.DateTimePicker dtpExpiration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}