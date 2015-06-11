namespace PagoElectronico.ABM_Cliente
{
    partial class FormABMCliente
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
            this.labelNombre = new System.Windows.Forms.Label();
            this.labelApellido = new System.Windows.Forms.Label();
            this.labelTipoId = new System.Windows.Forms.Label();
            this.labelId = new System.Windows.Forms.Label();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxApellido = new System.Windows.Forms.TextBox();
            this.textBoxNumeroId = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxMail = new System.Windows.Forms.TextBox();
            this.comboBoxTipoId = new System.Windows.Forms.ComboBox();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.dgvClient = new System.Windows.Forms.DataGridView();
            this.buttonVolver = new System.Windows.Forms.Button();
            this.buttonDeshabilitar = new System.Windows.Forms.Button();
            this.buttonHabilitar = new System.Windows.Forms.Button();
            this.buttonModificar = new System.Windows.Forms.Button();
            this.buttonAlta = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(17, 29);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(44, 13);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "Nombre";
            // 
            // labelApellido
            // 
            this.labelApellido.AutoSize = true;
            this.labelApellido.Location = new System.Drawing.Point(317, 29);
            this.labelApellido.Name = "labelApellido";
            this.labelApellido.Size = new System.Drawing.Size(44, 13);
            this.labelApellido.TabIndex = 1;
            this.labelApellido.Text = "Apellido";
            // 
            // labelTipoId
            // 
            this.labelTipoId.AutoSize = true;
            this.labelTipoId.Location = new System.Drawing.Point(17, 59);
            this.labelTipoId.Name = "labelTipoId";
            this.labelTipoId.Size = new System.Drawing.Size(94, 13);
            this.labelTipoId.TabIndex = 2;
            this.labelTipoId.Text = "Tipo Identificacion";
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(317, 59);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(110, 13);
            this.labelId.TabIndex = 3;
            this.labelId.Text = "Numero Identificacion";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(133, 26);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(147, 20);
            this.textBoxNombre.TabIndex = 0;
            // 
            // textBoxApellido
            // 
            this.textBoxApellido.Location = new System.Drawing.Point(460, 26);
            this.textBoxApellido.Name = "textBoxApellido";
            this.textBoxApellido.Size = new System.Drawing.Size(147, 20);
            this.textBoxApellido.TabIndex = 1;
            // 
            // textBoxNumeroId
            // 
            this.textBoxNumeroId.Location = new System.Drawing.Point(460, 59);
            this.textBoxNumeroId.Name = "textBoxNumeroId";
            this.textBoxNumeroId.Size = new System.Drawing.Size(147, 20);
            this.textBoxNumeroId.TabIndex = 3;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(17, 90);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(32, 13);
            this.labelEmail.TabIndex = 7;
            this.labelEmail.Text = "Email";
            // 
            // textBoxMail
            // 
            this.textBoxMail.Location = new System.Drawing.Point(133, 87);
            this.textBoxMail.Name = "textBoxMail";
            this.textBoxMail.Size = new System.Drawing.Size(147, 20);
            this.textBoxMail.TabIndex = 4;
            // 
            // comboBoxTipoId
            // 
            this.comboBoxTipoId.FormattingEnabled = true;
            this.comboBoxTipoId.Location = new System.Drawing.Point(133, 56);
            this.comboBoxTipoId.Name = "comboBoxTipoId";
            this.comboBoxTipoId.Size = new System.Drawing.Size(147, 21);
            this.comboBoxTipoId.TabIndex = 2;
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Location = new System.Drawing.Point(45, 148);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(75, 23);
            this.buttonLimpiar.TabIndex = 7;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.Location = new System.Drawing.Point(599, 148);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(75, 23);
            this.buttonBuscar.TabIndex = 5;
            this.buttonBuscar.Text = "Buscar";
            this.buttonBuscar.UseVisualStyleBackColor = true;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // dgvClient
            // 
            this.dgvClient.AllowUserToAddRows = false;
            this.dgvClient.AllowUserToDeleteRows = false;
            this.dgvClient.AllowUserToResizeRows = false;
            this.dgvClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClient.Location = new System.Drawing.Point(45, 186);
            this.dgvClient.Name = "dgvClient";
            this.dgvClient.Size = new System.Drawing.Size(629, 198);
            this.dgvClient.TabIndex = 12;
            this.dgvClient.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvClient_CellMouseClick);
            // 
            // buttonVolver
            // 
            this.buttonVolver.Location = new System.Drawing.Point(45, 403);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(75, 23);
            this.buttonVolver.TabIndex = 11;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // buttonDeshabilitar
            // 
            this.buttonDeshabilitar.Location = new System.Drawing.Point(320, 402);
            this.buttonDeshabilitar.Name = "buttonDeshabilitar";
            this.buttonDeshabilitar.Size = new System.Drawing.Size(75, 23);
            this.buttonDeshabilitar.TabIndex = 9;
            this.buttonDeshabilitar.Text = "Deshabilitar";
            this.buttonDeshabilitar.UseVisualStyleBackColor = true;
            this.buttonDeshabilitar.Click += new System.EventHandler(this.buttonDeshabilitar_Click);
            // 
            // buttonHabilitar
            // 
            this.buttonHabilitar.Location = new System.Drawing.Point(467, 402);
            this.buttonHabilitar.Name = "buttonHabilitar";
            this.buttonHabilitar.Size = new System.Drawing.Size(75, 23);
            this.buttonHabilitar.TabIndex = 8;
            this.buttonHabilitar.Text = "Habilitar";
            this.buttonHabilitar.UseVisualStyleBackColor = true;
            this.buttonHabilitar.Click += new System.EventHandler(this.buttonHabilitar_Click);
            // 
            // buttonModificar
            // 
            this.buttonModificar.Location = new System.Drawing.Point(598, 402);
            this.buttonModificar.Name = "buttonModificar";
            this.buttonModificar.Size = new System.Drawing.Size(75, 23);
            this.buttonModificar.TabIndex = 6;
            this.buttonModificar.Text = "Modificar";
            this.buttonModificar.UseVisualStyleBackColor = true;
            this.buttonModificar.Click += new System.EventHandler(this.buttonModificar_Click);
            // 
            // buttonAlta
            // 
            this.buttonAlta.Location = new System.Drawing.Point(178, 402);
            this.buttonAlta.Name = "buttonAlta";
            this.buttonAlta.Size = new System.Drawing.Size(75, 23);
            this.buttonAlta.TabIndex = 10;
            this.buttonAlta.Text = "Alta";
            this.buttonAlta.UseVisualStyleBackColor = true;
            this.buttonAlta.Click += new System.EventHandler(this.buttonAlta_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelNombre);
            this.groupBox1.Controls.Add(this.labelTipoId);
            this.groupBox1.Controls.Add(this.labelEmail);
            this.groupBox1.Controls.Add(this.textBoxNombre);
            this.groupBox1.Controls.Add(this.comboBoxTipoId);
            this.groupBox1.Controls.Add(this.textBoxMail);
            this.groupBox1.Controls.Add(this.textBoxNumeroId);
            this.groupBox1.Controls.Add(this.labelApellido);
            this.groupBox1.Controls.Add(this.labelId);
            this.groupBox1.Controls.Add(this.textBoxApellido);
            this.groupBox1.Location = new System.Drawing.Point(45, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(628, 121);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Búsqueda";
            // 
            // FormABMCliente
            // 
            this.AcceptButton = this.buttonBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 448);
            this.Controls.Add(this.buttonAlta);
            this.Controls.Add(this.buttonModificar);
            this.Controls.Add(this.buttonHabilitar);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.buttonDeshabilitar);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.buttonVolver);
            this.Controls.Add(this.dgvClient);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormABMCliente";
            this.Text = "Cliente";
            this.Load += new System.EventHandler(this.FormABMCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.Label labelApellido;
        private System.Windows.Forms.Label labelTipoId;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxApellido;
        private System.Windows.Forms.TextBox textBoxNumeroId;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxMail;
        private System.Windows.Forms.ComboBox comboBoxTipoId;
        private System.Windows.Forms.Button buttonLimpiar;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.DataGridView dgvClient;
        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.Button buttonDeshabilitar;
        private System.Windows.Forms.Button buttonHabilitar;
        private System.Windows.Forms.Button buttonModificar;
        private System.Windows.Forms.Button buttonAlta;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}