namespace PagoElectronico.Depositos
{
    partial class FormDepositos
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
            this.labelMonto = new System.Windows.Forms.Label();
            this.textBoxImporte = new System.Windows.Forms.TextBox();
            this.labelMoneda = new System.Windows.Forms.Label();
            this.listBoxMoneda = new System.Windows.Forms.ListBox();
            this.labelTarjetaCredito = new System.Windows.Forms.Label();
            this.comboBoxTarjetas = new System.Windows.Forms.ComboBox();
            this.buttonVolver = new System.Windows.Forms.Button();
            this.buttonDepositar = new System.Windows.Forms.Button();
            this.groupBoxOrigen = new System.Windows.Forms.GroupBox();
            this.groupBoxDestino = new System.Windows.Forms.GroupBox();
            this.groupBoxOrigen.SuspendLayout();
            this.groupBoxDestino.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCuenta
            // 
            this.labelCuenta.AutoSize = true;
            this.labelCuenta.Location = new System.Drawing.Point(27, 40);
            this.labelCuenta.Name = "labelCuenta";
            this.labelCuenta.Size = new System.Drawing.Size(41, 13);
            this.labelCuenta.TabIndex = 0;
            this.labelCuenta.Text = "Cuenta";
            // 
            // comboBoxCuentas
            // 
            this.comboBoxCuentas.FormattingEnabled = true;
            this.comboBoxCuentas.Location = new System.Drawing.Point(134, 37);
            this.comboBoxCuentas.Name = "comboBoxCuentas";
            this.comboBoxCuentas.Size = new System.Drawing.Size(167, 21);
            this.comboBoxCuentas.TabIndex = 1;
            // 
            // labelMonto
            // 
            this.labelMonto.AutoSize = true;
            this.labelMonto.Location = new System.Drawing.Point(27, 69);
            this.labelMonto.Name = "labelMonto";
            this.labelMonto.Size = new System.Drawing.Size(37, 13);
            this.labelMonto.TabIndex = 2;
            this.labelMonto.Text = "Monto";
            // 
            // textBoxImporte
            // 
            this.textBoxImporte.Location = new System.Drawing.Point(134, 66);
            this.textBoxImporte.Name = "textBoxImporte";
            this.textBoxImporte.Size = new System.Drawing.Size(104, 20);
            this.textBoxImporte.TabIndex = 3;
            // 
            // labelMoneda
            // 
            this.labelMoneda.AutoSize = true;
            this.labelMoneda.Location = new System.Drawing.Point(27, 105);
            this.labelMoneda.Name = "labelMoneda";
            this.labelMoneda.Size = new System.Drawing.Size(46, 13);
            this.labelMoneda.TabIndex = 4;
            this.labelMoneda.Text = "Moneda";
            // 
            // listBoxMoneda
            // 
            this.listBoxMoneda.FormattingEnabled = true;
            this.listBoxMoneda.Location = new System.Drawing.Point(134, 105);
            this.listBoxMoneda.Name = "listBoxMoneda";
            this.listBoxMoneda.Size = new System.Drawing.Size(167, 95);
            this.listBoxMoneda.TabIndex = 5;
            // 
            // labelTarjetaCredito
            // 
            this.labelTarjetaCredito.AutoSize = true;
            this.labelTarjetaCredito.Location = new System.Drawing.Point(27, 29);
            this.labelTarjetaCredito.Name = "labelTarjetaCredito";
            this.labelTarjetaCredito.Size = new System.Drawing.Size(91, 13);
            this.labelTarjetaCredito.TabIndex = 6;
            this.labelTarjetaCredito.Text = "Tarjeta de Credito";
            // 
            // comboBoxTarjetas
            // 
            this.comboBoxTarjetas.FormattingEnabled = true;
            this.comboBoxTarjetas.Location = new System.Drawing.Point(134, 26);
            this.comboBoxTarjetas.Name = "comboBoxTarjetas";
            this.comboBoxTarjetas.Size = new System.Drawing.Size(167, 21);
            this.comboBoxTarjetas.TabIndex = 7;
            // 
            // buttonVolver
            // 
            this.buttonVolver.Location = new System.Drawing.Point(44, 389);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(75, 23);
            this.buttonVolver.TabIndex = 8;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // buttonDepositar
            // 
            this.buttonDepositar.Location = new System.Drawing.Point(341, 389);
            this.buttonDepositar.Name = "buttonDepositar";
            this.buttonDepositar.Size = new System.Drawing.Size(75, 23);
            this.buttonDepositar.TabIndex = 9;
            this.buttonDepositar.Text = "Depositar";
            this.buttonDepositar.UseVisualStyleBackColor = true;
            this.buttonDepositar.Click += new System.EventHandler(this.buttonDepositar_Click);
            // 
            // groupBoxOrigen
            // 
            this.groupBoxOrigen.Controls.Add(this.labelTarjetaCredito);
            this.groupBoxOrigen.Controls.Add(this.textBoxImporte);
            this.groupBoxOrigen.Controls.Add(this.comboBoxTarjetas);
            this.groupBoxOrigen.Controls.Add(this.labelMonto);
            this.groupBoxOrigen.Location = new System.Drawing.Point(44, 12);
            this.groupBoxOrigen.Name = "groupBoxOrigen";
            this.groupBoxOrigen.Size = new System.Drawing.Size(372, 103);
            this.groupBoxOrigen.TabIndex = 10;
            this.groupBoxOrigen.TabStop = false;
            this.groupBoxOrigen.Text = "Origen";
            // 
            // groupBoxDestino
            // 
            this.groupBoxDestino.Controls.Add(this.labelCuenta);
            this.groupBoxDestino.Controls.Add(this.comboBoxCuentas);
            this.groupBoxDestino.Controls.Add(this.labelMoneda);
            this.groupBoxDestino.Controls.Add(this.listBoxMoneda);
            this.groupBoxDestino.Location = new System.Drawing.Point(44, 131);
            this.groupBoxDestino.Name = "groupBoxDestino";
            this.groupBoxDestino.Size = new System.Drawing.Size(372, 235);
            this.groupBoxDestino.TabIndex = 11;
            this.groupBoxDestino.TabStop = false;
            this.groupBoxDestino.Text = "Destino";
            // 
            // FormDepositos
            // 
            this.AcceptButton = this.buttonDepositar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 442);
            this.Controls.Add(this.buttonDepositar);
            this.Controls.Add(this.buttonVolver);
            this.Controls.Add(this.groupBoxOrigen);
            this.Controls.Add(this.groupBoxDestino);
            this.Name = "FormDepositos";
            this.Text = "Depositos";
            this.Load += new System.EventHandler(this.Depositos_Load);
            this.groupBoxOrigen.ResumeLayout(false);
            this.groupBoxOrigen.PerformLayout();
            this.groupBoxDestino.ResumeLayout(false);
            this.groupBoxDestino.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelCuenta;
        private System.Windows.Forms.ComboBox comboBoxCuentas;
        private System.Windows.Forms.Label labelMonto;
        private System.Windows.Forms.TextBox textBoxImporte;
        private System.Windows.Forms.Label labelMoneda;
        private System.Windows.Forms.ListBox listBoxMoneda;
        private System.Windows.Forms.Label labelTarjetaCredito;
        private System.Windows.Forms.ComboBox comboBoxTarjetas;
        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.Button buttonDepositar;
        private System.Windows.Forms.GroupBox groupBoxOrigen;
        private System.Windows.Forms.GroupBox groupBoxDestino;
    }
}