namespace PagoElectronico.Login
{
    partial class FormBlockedUser
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
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.labelAtencion = new System.Windows.Forms.Label();
            this.labelMensajeBlock = new System.Windows.Forms.Label();
            this.labelContactarAdmin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Location = new System.Drawing.Point(227, 99);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(75, 23);
            this.buttonAceptar.TabIndex = 0;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // labelAtencion
            // 
            this.labelAtencion.AutoSize = true;
            this.labelAtencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAtencion.Location = new System.Drawing.Point(12, 27);
            this.labelAtencion.Name = "labelAtencion";
            this.labelAtencion.Size = new System.Drawing.Size(61, 13);
            this.labelAtencion.TabIndex = 1;
            this.labelAtencion.Text = "Atencion:";
            // 
            // labelMensajeBlock
            // 
            this.labelMensajeBlock.AutoSize = true;
            this.labelMensajeBlock.Location = new System.Drawing.Point(79, 27);
            this.labelMensajeBlock.Name = "labelMensajeBlock";
            this.labelMensajeBlock.Size = new System.Drawing.Size(223, 13);
            this.labelMensajeBlock.TabIndex = 2;
            this.labelMensajeBlock.Text = "El usuario ingresado se encuentra bloqueado.";
            this.labelMensajeBlock.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelContactarAdmin
            // 
            this.labelContactarAdmin.AutoSize = true;
            this.labelContactarAdmin.Location = new System.Drawing.Point(79, 56);
            this.labelContactarAdmin.Name = "labelContactarAdmin";
            this.labelContactarAdmin.Size = new System.Drawing.Size(174, 13);
            this.labelContactarAdmin.TabIndex = 3;
            this.labelContactarAdmin.Text = "Por favor contacte al administrador.";
            // 
            // FormBlockedUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 137);
            this.Controls.Add(this.labelContactarAdmin);
            this.Controls.Add(this.labelMensajeBlock);
            this.Controls.Add(this.labelAtencion);
            this.Controls.Add(this.buttonAceptar);
            this.Name = "FormBlockedUser";
            this.Text = "Usuario Bloqueado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.Label labelAtencion;
        private System.Windows.Forms.Label labelMensajeBlock;
        private System.Windows.Forms.Label labelContactarAdmin;
    }
}