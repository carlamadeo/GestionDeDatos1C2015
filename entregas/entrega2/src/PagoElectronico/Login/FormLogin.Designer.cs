namespace PagoElectronico.Login
{
    partial class FormLogin
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
            this.buttonSalir = new System.Windows.Forms.Button();
            this.buttonIngresar = new System.Windows.Forms.Button();
            this.textUsername = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonRegistro = new System.Windows.Forms.Button();
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // buttonSalir
            // 
            this.buttonSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSalir.Location = new System.Drawing.Point(38, 205);
            this.buttonSalir.Name = "buttonSalir";
            this.buttonSalir.Size = new System.Drawing.Size(75, 23);
            this.buttonSalir.TabIndex = 4;
            this.buttonSalir.Text = "Salir";
            this.buttonSalir.UseVisualStyleBackColor = true;
            this.buttonSalir.Click += new System.EventHandler(this.buttonSalir_Click);
            // 
            // buttonIngresar
            // 
            this.buttonIngresar.Location = new System.Drawing.Point(267, 205);
            this.buttonIngresar.Name = "buttonIngresar";
            this.buttonIngresar.Size = new System.Drawing.Size(75, 23);
            this.buttonIngresar.TabIndex = 2;
            this.buttonIngresar.Text = "Ingresar";
            this.buttonIngresar.UseVisualStyleBackColor = true;
            this.buttonIngresar.Click += new System.EventHandler(this.buttonIngresar_Click);
            // 
            // textUsername
            // 
            this.textUsername.AutoSize = true;
            this.textUsername.Location = new System.Drawing.Point(69, 94);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(55, 13);
            this.textUsername.TabIndex = 2;
            this.textUsername.Text = "Username";
            // 
            // textPassword
            // 
            this.textPassword.AutoSize = true;
            this.textPassword.Location = new System.Drawing.Point(69, 135);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(53, 13);
            this.textPassword.TabIndex = 3;
            this.textPassword.Text = "Password";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(164, 91);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(133, 20);
            this.textBoxUser.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(164, 132);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(133, 20);
            this.textBoxPassword.TabIndex = 1;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // buttonRegistro
            // 
            this.buttonRegistro.Location = new System.Drawing.Point(152, 205);
            this.buttonRegistro.Name = "buttonRegistro";
            this.buttonRegistro.Size = new System.Drawing.Size(75, 23);
            this.buttonRegistro.TabIndex = 3;
            this.buttonRegistro.Text = "Registrarse";
            this.buttonRegistro.UseVisualStyleBackColor = true;
            this.buttonRegistro.Click += new System.EventHandler(this.buttonRegistro_Click);
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxLogin.Location = new System.Drawing.Point(38, 29);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(304, 157);
            this.groupBoxLogin.TabIndex = 8;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "  Sistema de Pago Electronico ";
            // 
            // FormLogin
            // 
            this.AcceptButton = this.buttonIngresar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonSalir;
            this.ClientSize = new System.Drawing.Size(377, 240);
            this.ControlBox = false;
            this.Controls.Add(this.buttonRegistro);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textUsername);
            this.Controls.Add(this.buttonIngresar);
            this.Controls.Add(this.buttonSalir);
            this.Controls.Add(this.groupBoxLogin);
            this.Name = "FormLogin";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSalir;
        private System.Windows.Forms.Button buttonIngresar;
        private System.Windows.Forms.Label textUsername;
        private System.Windows.Forms.Label textPassword;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonRegistro;
        private System.Windows.Forms.GroupBox groupBoxLogin;
    }
}