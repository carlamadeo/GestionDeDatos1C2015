namespace PagoElectronico.Transferencias
{
    partial class FormTransferencias
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
            this.comboBoxCuentaOrigen = new System.Windows.Forms.ComboBox();
            this.buttonTransferencia = new System.Windows.Forms.Button();
            this.buttonVolver = new System.Windows.Forms.Button();
            this.labelCuentaOrigen = new System.Windows.Forms.Label();
            this.labelImporte = new System.Windows.Forms.Label();
            this.textBoxImporte = new System.Windows.Forms.TextBox();
            this.groupBoxDestino = new System.Windows.Forms.GroupBox();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.listBoxUsuario = new System.Windows.Forms.ListBox();
            this.comboBoxCuentaTercero = new System.Windows.Forms.ComboBox();
            this.labelCuentaTercera = new System.Windows.Forms.Label();
            this.comboBoxCuentaPropia = new System.Windows.Forms.ComboBox();
            this.labelCuentaPropia = new System.Windows.Forms.Label();
            this.groupBoxOrigen = new System.Windows.Forms.GroupBox();
            this.labelMax = new System.Windows.Forms.Label();
            this.groupBoxDestino.SuspendLayout();
            this.groupBoxOrigen.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxCuentaOrigen
            // 
            this.comboBoxCuentaOrigen.FormattingEnabled = true;
            this.comboBoxCuentaOrigen.Location = new System.Drawing.Point(139, 25);
            this.comboBoxCuentaOrigen.Name = "comboBoxCuentaOrigen";
            this.comboBoxCuentaOrigen.Size = new System.Drawing.Size(165, 21);
            this.comboBoxCuentaOrigen.TabIndex = 0;
            this.comboBoxCuentaOrigen.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCuentaOrigen_SelectionChangeCommitted);
            // 
            // buttonTransferencia
            // 
            this.buttonTransferencia.Location = new System.Drawing.Point(338, 431);
            this.buttonTransferencia.Name = "buttonTransferencia";
            this.buttonTransferencia.Size = new System.Drawing.Size(80, 23);
            this.buttonTransferencia.TabIndex = 1;
            this.buttonTransferencia.Text = "Transferencia";
            this.buttonTransferencia.UseVisualStyleBackColor = true;
            this.buttonTransferencia.Click += new System.EventHandler(this.buttonTransferencia_Click);
            // 
            // buttonVolver
            // 
            this.buttonVolver.Location = new System.Drawing.Point(44, 431);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(80, 23);
            this.buttonVolver.TabIndex = 2;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // labelCuentaOrigen
            // 
            this.labelCuentaOrigen.AutoSize = true;
            this.labelCuentaOrigen.Location = new System.Drawing.Point(29, 28);
            this.labelCuentaOrigen.Name = "labelCuentaOrigen";
            this.labelCuentaOrigen.Size = new System.Drawing.Size(75, 13);
            this.labelCuentaOrigen.TabIndex = 3;
            this.labelCuentaOrigen.Text = "Cuenta Origen";
            // 
            // labelImporte
            // 
            this.labelImporte.AutoSize = true;
            this.labelImporte.Location = new System.Drawing.Point(29, 67);
            this.labelImporte.Name = "labelImporte";
            this.labelImporte.Size = new System.Drawing.Size(42, 13);
            this.labelImporte.TabIndex = 4;
            this.labelImporte.Text = "Importe";
            // 
            // textBoxImporte
            // 
            this.textBoxImporte.Location = new System.Drawing.Point(139, 64);
            this.textBoxImporte.Name = "textBoxImporte";
            this.textBoxImporte.Size = new System.Drawing.Size(100, 20);
            this.textBoxImporte.TabIndex = 5;
            // 
            // groupBoxDestino
            // 
            this.groupBoxDestino.Controls.Add(this.labelUsuario);
            this.groupBoxDestino.Controls.Add(this.listBoxUsuario);
            this.groupBoxDestino.Controls.Add(this.comboBoxCuentaTercero);
            this.groupBoxDestino.Controls.Add(this.labelCuentaTercera);
            this.groupBoxDestino.Controls.Add(this.comboBoxCuentaPropia);
            this.groupBoxDestino.Controls.Add(this.labelCuentaPropia);
            this.groupBoxDestino.Location = new System.Drawing.Point(44, 129);
            this.groupBoxDestino.Name = "groupBoxDestino";
            this.groupBoxDestino.Size = new System.Drawing.Size(374, 279);
            this.groupBoxDestino.TabIndex = 6;
            this.groupBoxDestino.TabStop = false;
            this.groupBoxDestino.Text = "Destino";
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Location = new System.Drawing.Point(32, 96);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(43, 13);
            this.labelUsuario.TabIndex = 5;
            this.labelUsuario.Text = "Usuario";
            // 
            // listBoxUsuario
            // 
            this.listBoxUsuario.FormattingEnabled = true;
            this.listBoxUsuario.Location = new System.Drawing.Point(139, 96);
            this.listBoxUsuario.Name = "listBoxUsuario";
            this.listBoxUsuario.Size = new System.Drawing.Size(165, 95);
            this.listBoxUsuario.TabIndex = 4;
            this.listBoxUsuario.SelectedValueChanged += new System.EventHandler(this.listBoxUsuario_SelectedValueChanged);
            // 
            // comboBoxCuentaTercero
            // 
            this.comboBoxCuentaTercero.FormattingEnabled = true;
            this.comboBoxCuentaTercero.Location = new System.Drawing.Point(139, 229);
            this.comboBoxCuentaTercero.Name = "comboBoxCuentaTercero";
            this.comboBoxCuentaTercero.Size = new System.Drawing.Size(165, 21);
            this.comboBoxCuentaTercero.TabIndex = 3;
            // 
            // labelCuentaTercera
            // 
            this.labelCuentaTercera.AutoSize = true;
            this.labelCuentaTercera.Location = new System.Drawing.Point(32, 232);
            this.labelCuentaTercera.Name = "labelCuentaTercera";
            this.labelCuentaTercera.Size = new System.Drawing.Size(81, 13);
            this.labelCuentaTercera.TabIndex = 2;
            this.labelCuentaTercera.Text = "Cuenta Tercero";
            // 
            // comboBoxCuentaPropia
            // 
            this.comboBoxCuentaPropia.FormattingEnabled = true;
            this.comboBoxCuentaPropia.Location = new System.Drawing.Point(139, 33);
            this.comboBoxCuentaPropia.Name = "comboBoxCuentaPropia";
            this.comboBoxCuentaPropia.Size = new System.Drawing.Size(165, 21);
            this.comboBoxCuentaPropia.TabIndex = 1;
            // 
            // labelCuentaPropia
            // 
            this.labelCuentaPropia.AutoSize = true;
            this.labelCuentaPropia.Location = new System.Drawing.Point(29, 36);
            this.labelCuentaPropia.Name = "labelCuentaPropia";
            this.labelCuentaPropia.Size = new System.Drawing.Size(74, 13);
            this.labelCuentaPropia.TabIndex = 0;
            this.labelCuentaPropia.Text = "Cuenta Propia";
            // 
            // groupBoxOrigen
            // 
            this.groupBoxOrigen.Controls.Add(this.labelMax);
            this.groupBoxOrigen.Controls.Add(this.labelCuentaOrigen);
            this.groupBoxOrigen.Controls.Add(this.comboBoxCuentaOrigen);
            this.groupBoxOrigen.Controls.Add(this.textBoxImporte);
            this.groupBoxOrigen.Controls.Add(this.labelImporte);
            this.groupBoxOrigen.Location = new System.Drawing.Point(44, 12);
            this.groupBoxOrigen.Name = "groupBoxOrigen";
            this.groupBoxOrigen.Size = new System.Drawing.Size(374, 102);
            this.groupBoxOrigen.TabIndex = 7;
            this.groupBoxOrigen.TabStop = false;
            this.groupBoxOrigen.Text = "Origen";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(256, 67);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(45, 13);
            this.labelMax.TabIndex = 6;
            this.labelMax.Text = "Max. $0";
            // 
            // FormTransferencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 479);
            this.Controls.Add(this.groupBoxOrigen);
            this.Controls.Add(this.groupBoxDestino);
            this.Controls.Add(this.buttonVolver);
            this.Controls.Add(this.buttonTransferencia);
            this.Name = "FormTransferencias";
            this.Text = "Transferencias";
            this.Load += new System.EventHandler(this.FormTransferencias_Load);
            this.groupBoxDestino.ResumeLayout(false);
            this.groupBoxDestino.PerformLayout();
            this.groupBoxOrigen.ResumeLayout(false);
            this.groupBoxOrigen.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCuentaOrigen;
        private System.Windows.Forms.Button buttonTransferencia;
        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.Label labelCuentaOrigen;
        private System.Windows.Forms.Label labelImporte;
        private System.Windows.Forms.TextBox textBoxImporte;
        private System.Windows.Forms.GroupBox groupBoxDestino;
        private System.Windows.Forms.ComboBox comboBoxCuentaPropia;
        private System.Windows.Forms.Label labelCuentaPropia;
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.ListBox listBoxUsuario;
        private System.Windows.Forms.ComboBox comboBoxCuentaTercero;
        private System.Windows.Forms.Label labelCuentaTercera;
        private System.Windows.Forms.GroupBox groupBoxOrigen;
        private System.Windows.Forms.Label labelMax;
    }
}