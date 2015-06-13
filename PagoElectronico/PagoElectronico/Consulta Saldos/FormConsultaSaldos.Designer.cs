namespace PagoElectronico.Consulta_Saldos
{
    partial class FormConsultaSaldos
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
            this.buttonMovimientos = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.dgvClient = new System.Windows.Forms.DataGridView();
            this.txtBoxSaldo = new System.Windows.Forms.TextBox();
            this.labelSaldo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero de cuenta";
            // 
            // buttonMovimientos
            // 
            this.buttonMovimientos.Location = new System.Drawing.Point(459, 236);
            this.buttonMovimientos.Name = "buttonMovimientos";
            this.buttonMovimientos.Size = new System.Drawing.Size(86, 23);
            this.buttonMovimientos.TabIndex = 3;
            this.buttonMovimientos.Text = "Movimientos";
            this.buttonMovimientos.UseVisualStyleBackColor = true;
            this.buttonMovimientos.Click += new System.EventHandler(this.buttonMovimientos_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(33, 236);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 13;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // comboBoxAccount
            // 
            this.comboBoxAccount.FormattingEnabled = true;
            this.comboBoxAccount.Location = new System.Drawing.Point(146, 146);
            this.comboBoxAccount.Name = "comboBoxAccount";
            this.comboBoxAccount.Size = new System.Drawing.Size(172, 21);
            this.comboBoxAccount.TabIndex = 14;
            this.comboBoxAccount.SelectedValueChanged += new System.EventHandler(this.comboBoxAccount_SelectedValueChanged);
            // 
            // dgvClient
            // 
            this.dgvClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClient.Location = new System.Drawing.Point(33, 30);
            this.dgvClient.Name = "dgvClient";
            this.dgvClient.Size = new System.Drawing.Size(510, 91);
            this.dgvClient.TabIndex = 15;
            this.dgvClient.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClient_SelectionChanged);
            // 
            // txtBoxSaldo
            // 
            this.txtBoxSaldo.Location = new System.Drawing.Point(146, 190);
            this.txtBoxSaldo.Name = "txtBoxSaldo";
            this.txtBoxSaldo.ReadOnly = true;
            this.txtBoxSaldo.Size = new System.Drawing.Size(172, 20);
            this.txtBoxSaldo.TabIndex = 17;
            // 
            // labelSaldo
            // 
            this.labelSaldo.AutoSize = true;
            this.labelSaldo.Location = new System.Drawing.Point(30, 193);
            this.labelSaldo.Name = "labelSaldo";
            this.labelSaldo.Size = new System.Drawing.Size(96, 13);
            this.labelSaldo.TabIndex = 16;
            this.labelSaldo.Text = "Saldo de la cuenta";
            // 
            // FormConsultaSaldos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 303);
            this.Controls.Add(this.txtBoxSaldo);
            this.Controls.Add(this.labelSaldo);
            this.Controls.Add(this.dgvClient);
            this.Controls.Add(this.comboBoxAccount);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.buttonMovimientos);
            this.Controls.Add(this.label1);
            this.Name = "FormConsultaSaldos";
            this.Text = "Consulta de saldos";
            this.Load += new System.EventHandler(this.FormConsultaSaldos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonMovimientos;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.DataGridView dgvClient;
        private System.Windows.Forms.TextBox txtBoxSaldo;
        private System.Windows.Forms.Label labelSaldo;
    }
}