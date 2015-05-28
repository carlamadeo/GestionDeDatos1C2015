namespace PagoElectronico.Depositos
{
    partial class Depositos
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
            this.labelCuenta = new System.Windows.Forms.Label();
            this.comboBoxCuentas = new System.Windows.Forms.ComboBox();
            this.labelImporte = new System.Windows.Forms.Label();
            this.textBoxImporte = new System.Windows.Forms.TextBox();
            this.labelMoneda = new System.Windows.Forms.Label();
            this.listBoxMoneda = new System.Windows.Forms.ListBox();
            this.labelTarjetaCredito = new System.Windows.Forms.Label();
            this.comboBoxTarjetas = new System.Windows.Forms.ComboBox();
            this.buttonVolver = new System.Windows.Forms.Button();
            this.buttonDepositar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelCuenta
            // 
            this.labelCuenta.AutoSize = true;
            this.labelCuenta.Location = new System.Drawing.Point(28, 54);
            this.labelCuenta.Name = "labelCuenta";
            this.labelCuenta.Size = new System.Drawing.Size(41, 13);
            this.labelCuenta.TabIndex = 0;
            this.labelCuenta.Text = "Cuenta";
            // 
            // comboBoxCuentas
            // 
            this.comboBoxCuentas.FormattingEnabled = true;
            this.comboBoxCuentas.Location = new System.Drawing.Point(105, 51);
            this.comboBoxCuentas.Name = "comboBoxCuentas";
            this.comboBoxCuentas.Size = new System.Drawing.Size(152, 21);
            this.comboBoxCuentas.TabIndex = 1;
            // 
            // labelImporte
            // 
            this.labelImporte.AutoSize = true;
            this.labelImporte.Location = new System.Drawing.Point(309, 54);
            this.labelImporte.Name = "labelImporte";
            this.labelImporte.Size = new System.Drawing.Size(42, 13);
            this.labelImporte.TabIndex = 2;
            this.labelImporte.Text = "Importe";
            // 
            // textBoxImporte
            // 
            this.textBoxImporte.Location = new System.Drawing.Point(418, 51);
            this.textBoxImporte.Name = "textBoxImporte";
            this.textBoxImporte.Size = new System.Drawing.Size(73, 20);
            this.textBoxImporte.TabIndex = 3;
            // 
            // labelMoneda
            // 
            this.labelMoneda.AutoSize = true;
            this.labelMoneda.Location = new System.Drawing.Point(28, 112);
            this.labelMoneda.Name = "labelMoneda";
            this.labelMoneda.Size = new System.Drawing.Size(46, 13);
            this.labelMoneda.TabIndex = 4;
            this.labelMoneda.Text = "Moneda";
            // 
            // listBoxMoneda
            // 
            this.listBoxMoneda.FormattingEnabled = true;
            this.listBoxMoneda.Location = new System.Drawing.Point(105, 112);
            this.listBoxMoneda.Name = "listBoxMoneda";
            this.listBoxMoneda.Size = new System.Drawing.Size(152, 95);
            this.listBoxMoneda.TabIndex = 5;
            // 
            // labelTarjetaCredito
            // 
            this.labelTarjetaCredito.AutoSize = true;
            this.labelTarjetaCredito.Location = new System.Drawing.Point(309, 112);
            this.labelTarjetaCredito.Name = "labelTarjetaCredito";
            this.labelTarjetaCredito.Size = new System.Drawing.Size(91, 13);
            this.labelTarjetaCredito.TabIndex = 6;
            this.labelTarjetaCredito.Text = "Tarjeta de Credito";
            // 
            // comboBoxTarjetas
            // 
            this.comboBoxTarjetas.FormattingEnabled = true;
            this.comboBoxTarjetas.Location = new System.Drawing.Point(418, 104);
            this.comboBoxTarjetas.Name = "comboBoxTarjetas";
            this.comboBoxTarjetas.Size = new System.Drawing.Size(152, 21);
            this.comboBoxTarjetas.TabIndex = 7;
            // 
            // buttonVolver
            // 
            this.buttonVolver.Location = new System.Drawing.Point(31, 260);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(75, 23);
            this.buttonVolver.TabIndex = 8;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // buttonDepositar
            // 
            this.buttonDepositar.Location = new System.Drawing.Point(494, 259);
            this.buttonDepositar.Name = "buttonDepositar";
            this.buttonDepositar.Size = new System.Drawing.Size(75, 23);
            this.buttonDepositar.TabIndex = 9;
            this.buttonDepositar.Text = "Depositar";
            this.buttonDepositar.UseVisualStyleBackColor = true;
            this.buttonDepositar.Click += new System.EventHandler(this.buttonDepositar_Click);
            // 
            // Depositos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 317);
            this.Controls.Add(this.buttonDepositar);
            this.Controls.Add(this.buttonVolver);
            this.Controls.Add(this.comboBoxTarjetas);
            this.Controls.Add(this.labelTarjetaCredito);
            this.Controls.Add(this.listBoxMoneda);
            this.Controls.Add(this.labelMoneda);
            this.Controls.Add(this.textBoxImporte);
            this.Controls.Add(this.labelImporte);
            this.Controls.Add(this.comboBoxCuentas);
            this.Controls.Add(this.labelCuenta);
            this.Name = "Depositos";
            this.Text = "Depositos";
            this.Load += new System.EventHandler(this.Depositos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCuenta;
        private System.Windows.Forms.ComboBox comboBoxCuentas;
        private System.Windows.Forms.Label labelImporte;
        private System.Windows.Forms.TextBox textBoxImporte;
        private System.Windows.Forms.Label labelMoneda;
        private System.Windows.Forms.ListBox listBoxMoneda;
        private System.Windows.Forms.Label labelTarjetaCredito;
        private System.Windows.Forms.ComboBox comboBoxTarjetas;
        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.Button buttonDepositar;
    }
}