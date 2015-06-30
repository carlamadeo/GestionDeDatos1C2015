namespace PagoElectronico.Facturacion
{
    partial class FormSeleccionSuscripciones
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
            this.textBoxSuscripciones = new System.Windows.Forms.TextBox();
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Suscripciones";
            // 
            // textBoxSuscripciones
            // 
            this.textBoxSuscripciones.Location = new System.Drawing.Point(132, 62);
            this.textBoxSuscripciones.Name = "textBoxSuscripciones";
            this.textBoxSuscripciones.Size = new System.Drawing.Size(134, 20);
            this.textBoxSuscripciones.TabIndex = 0;
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Location = new System.Drawing.Point(190, 131);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(75, 23);
            this.buttonAceptar.TabIndex = 1;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // FormSeleccionSuscripciones
            // 
            this.AcceptButton = this.buttonAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 189);
            this.ControlBox = false;
            this.Controls.Add(this.buttonAceptar);
            this.Controls.Add(this.textBoxSuscripciones);
            this.Controls.Add(this.label1);
            this.Name = "FormSeleccionSuscripciones";
            this.Text = "Cantidad de Suscripciones A Pagar";
            this.Load += new System.EventHandler(this.FormSeleccionSuscripciones_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSuscripciones;
        private System.Windows.Forms.Button buttonAceptar;
    }
}