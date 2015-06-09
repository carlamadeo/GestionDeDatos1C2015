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
            this.txtBoxCuenta = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxSaldo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvDepositos = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvRetiros = new System.Windows.Forms.DataGridView();
            this.dgvTransferencias = new System.Windows.Forms.DataGridView();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepositos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetiros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero de cuenta";
            // 
            // txtBoxCuenta
            // 
            this.txtBoxCuenta.Location = new System.Drawing.Point(120, 52);
            this.txtBoxCuenta.Name = "txtBoxCuenta";
            this.txtBoxCuenta.Size = new System.Drawing.Size(324, 20);
            this.txtBoxCuenta.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(22, 78);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Limpiar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(358, 78);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(86, 23);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.Text = "Buscar";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Saldo de la cuenta";
            // 
            // txtBoxSaldo
            // 
            this.txtBoxSaldo.Location = new System.Drawing.Point(141, 122);
            this.txtBoxSaldo.Name = "txtBoxSaldo";
            this.txtBoxSaldo.ReadOnly = true;
            this.txtBoxSaldo.Size = new System.Drawing.Size(303, 20);
            this.txtBoxSaldo.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ultimos 5 depositos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ultimos 5 retiros";
            // 
            // dgvDepositos
            // 
            this.dgvDepositos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepositos.Location = new System.Drawing.Point(25, 166);
            this.dgvDepositos.Name = "dgvDepositos";
            this.dgvDepositos.Size = new System.Drawing.Size(419, 72);
            this.dgvDepositos.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(148, 330);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Ultimas 10 transferencias de fondos";
            // 
            // dgvRetiros
            // 
            this.dgvRetiros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRetiros.Location = new System.Drawing.Point(22, 257);
            this.dgvRetiros.Name = "dgvRetiros";
            this.dgvRetiros.Size = new System.Drawing.Size(419, 70);
            this.dgvRetiros.TabIndex = 11;
            // 
            // dgvTransferencias
            // 
            this.dgvTransferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransferencias.Location = new System.Drawing.Point(25, 346);
            this.dgvTransferencias.Name = "dgvTransferencias";
            this.dgvTransferencias.Size = new System.Drawing.Size(419, 85);
            this.dgvTransferencias.TabIndex = 12;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(192, 78);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 13;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FormConsultaSaldos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 445);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.dgvTransferencias);
            this.Controls.Add(this.dgvRetiros);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvDepositos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBoxSaldo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtBoxCuenta);
            this.Controls.Add(this.label1);
            this.Name = "FormConsultaSaldos";
            this.Text = "Consulta de saldos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepositos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetiros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxCuenta;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxSaldo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvDepositos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvRetiros;
        private System.Windows.Forms.DataGridView dgvTransferencias;
        private System.Windows.Forms.Button btnVolver;
    }
}