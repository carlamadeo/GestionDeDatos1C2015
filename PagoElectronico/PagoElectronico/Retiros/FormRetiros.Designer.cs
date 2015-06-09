namespace PagoElectronico.Retiros
{
    partial class FormRetiros
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
            this.buttonBack = new System.Windows.Forms.Button();
            this.dgvAccount = new System.Windows.Forms.DataGridView();
            this.buttonRetire = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNroDoc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxBank = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(236, 319);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = "Volver";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // dgvAccount
            // 
            this.dgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccount.Location = new System.Drawing.Point(41, 29);
            this.dgvAccount.Name = "dgvAccount";
            this.dgvAccount.Size = new System.Drawing.Size(665, 150);
            this.dgvAccount.TabIndex = 1;
            // 
            // buttonRetire
            // 
            this.buttonRetire.Location = new System.Drawing.Point(459, 319);
            this.buttonRetire.Name = "buttonRetire";
            this.buttonRetire.Size = new System.Drawing.Size(75, 23);
            this.buttonRetire.TabIndex = 3;
            this.buttonRetire.Text = "Retirar";
            this.buttonRetire.UseVisualStyleBackColor = true;
            this.buttonRetire.Click += new System.EventHandler(this.buttonRetire_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nro° Documento";
            // 
            // textBoxNroDoc
            // 
            this.textBoxNroDoc.Location = new System.Drawing.Point(156, 215);
            this.textBoxNroDoc.Name = "textBoxNroDoc";
            this.textBoxNroDoc.Size = new System.Drawing.Size(161, 20);
            this.textBoxNroDoc.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(429, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Importe";
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(545, 215);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(161, 20);
            this.textBoxCount.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Banco";
            // 
            // comboBoxBank
            // 
            this.comboBoxBank.FormattingEnabled = true;
            this.comboBoxBank.Location = new System.Drawing.Point(156, 257);
            this.comboBoxBank.Name = "comboBoxBank";
            this.comboBoxBank.Size = new System.Drawing.Size(155, 21);
            this.comboBoxBank.TabIndex = 9;
            // 
            // FormRetiros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 373);
            this.Controls.Add(this.comboBoxBank);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNroDoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRetire);
            this.Controls.Add(this.dgvAccount);
            this.Controls.Add(this.buttonBack);
            this.Name = "FormRetiros";
            this.Text = "FormRetiros";
            this.Load += new System.EventHandler(this.FormRetiros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.DataGridView dgvAccount;
        private System.Windows.Forms.Button buttonRetire;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNroDoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxBank;
    }
}