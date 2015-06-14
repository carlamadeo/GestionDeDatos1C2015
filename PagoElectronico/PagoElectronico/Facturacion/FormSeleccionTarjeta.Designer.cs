namespace PagoElectronico.Facturacion
{
    partial class FormSeleccionTarjeta
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
            this.labelTarjeta = new System.Windows.Forms.Label();
            this.comboBoxTarjetas = new System.Windows.Forms.ComboBox();
            this.buttonPagar = new System.Windows.Forms.Button();
            this.buttonVolver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTarjeta
            // 
            this.labelTarjeta.AutoSize = true;
            this.labelTarjeta.Location = new System.Drawing.Point(34, 60);
            this.labelTarjeta.Name = "labelTarjeta";
            this.labelTarjeta.Size = new System.Drawing.Size(40, 13);
            this.labelTarjeta.TabIndex = 0;
            this.labelTarjeta.Text = "Tarjeta";
            // 
            // comboBoxTarjetas
            // 
            this.comboBoxTarjetas.FormattingEnabled = true;
            this.comboBoxTarjetas.Location = new System.Drawing.Point(114, 57);
            this.comboBoxTarjetas.Name = "comboBoxTarjetas";
            this.comboBoxTarjetas.Size = new System.Drawing.Size(154, 21);
            this.comboBoxTarjetas.TabIndex = 1;
            // 
            // buttonPagar
            // 
            this.buttonPagar.Location = new System.Drawing.Point(193, 129);
            this.buttonPagar.Name = "buttonPagar";
            this.buttonPagar.Size = new System.Drawing.Size(75, 23);
            this.buttonPagar.TabIndex = 2;
            this.buttonPagar.Text = "Pagar";
            this.buttonPagar.UseVisualStyleBackColor = true;
            this.buttonPagar.Click += new System.EventHandler(this.buttonPagar_Click);
            // 
            // buttonVolver
            // 
            this.buttonVolver.Location = new System.Drawing.Point(37, 129);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(75, 23);
            this.buttonVolver.TabIndex = 3;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // FormSeleccionTarjeta
            // 
            this.AcceptButton = this.buttonPagar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 218);
            this.Controls.Add(this.buttonVolver);
            this.Controls.Add(this.buttonPagar);
            this.Controls.Add(this.comboBoxTarjetas);
            this.Controls.Add(this.labelTarjeta);
            this.Name = "FormSeleccionTarjeta";
            this.Text = "Tarjeta para Facturacion de Costos";
            this.Load += new System.EventHandler(this.FormSeleccionTarjeta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTarjeta;
        private System.Windows.Forms.ComboBox comboBoxTarjetas;
        private System.Windows.Forms.Button buttonPagar;
        private System.Windows.Forms.Button buttonVolver;
    }
}