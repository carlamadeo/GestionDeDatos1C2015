namespace PagoElectronico.Facturacion
{
    partial class FormFacturacion
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
            this.labelUsuario = new System.Windows.Forms.Label();
            this.comboBoxUsuario = new System.Windows.Forms.ComboBox();
            this.dgvPendientes = new System.Windows.Forms.DataGridView();
            this.dgvAPagar = new System.Windows.Forms.DataGridView();
            this.labelPendientes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonVolver = new System.Windows.Forms.Button();
            this.buttonContinuar = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAPagar)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Location = new System.Drawing.Point(24, 30);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(43, 13);
            this.labelUsuario.TabIndex = 0;
            this.labelUsuario.Text = "Usuario";
            // 
            // comboBoxUsuario
            // 
            this.comboBoxUsuario.FormattingEnabled = true;
            this.comboBoxUsuario.Location = new System.Drawing.Point(105, 27);
            this.comboBoxUsuario.Name = "comboBoxUsuario";
            this.comboBoxUsuario.Size = new System.Drawing.Size(139, 21);
            this.comboBoxUsuario.TabIndex = 0;
            this.comboBoxUsuario.SelectionChangeCommitted += new System.EventHandler(this.comboBoxUsuario_SelectionChangeCommitted);
            // 
            // dgvPendientes
            // 
            this.dgvPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendientes.Location = new System.Drawing.Point(27, 92);
            this.dgvPendientes.Name = "dgvPendientes";
            this.dgvPendientes.Size = new System.Drawing.Size(689, 163);
            this.dgvPendientes.TabIndex = 2;
            // 
            // dgvAPagar
            // 
            this.dgvAPagar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAPagar.Location = new System.Drawing.Point(27, 293);
            this.dgvAPagar.Name = "dgvAPagar";
            this.dgvAPagar.Size = new System.Drawing.Size(689, 163);
            this.dgvAPagar.TabIndex = 3;
            // 
            // labelPendientes
            // 
            this.labelPendientes.AutoSize = true;
            this.labelPendientes.Location = new System.Drawing.Point(24, 76);
            this.labelPendientes.Name = "labelPendientes";
            this.labelPendientes.Size = new System.Drawing.Size(133, 13);
            this.labelPendientes.TabIndex = 4;
            this.labelPendientes.Text = "Transacciones Pendientes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 277);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Transacciones a Pagar";
            // 
            // buttonVolver
            // 
            this.buttonVolver.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonVolver.Location = new System.Drawing.Point(27, 477);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(75, 23);
            this.buttonVolver.TabIndex = 4;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // buttonContinuar
            // 
            this.buttonContinuar.Location = new System.Drawing.Point(641, 477);
            this.buttonContinuar.Name = "buttonContinuar";
            this.buttonContinuar.Size = new System.Drawing.Size(75, 23);
            this.buttonContinuar.TabIndex = 3;
            this.buttonContinuar.Text = "Continuar";
            this.buttonContinuar.UseVisualStyleBackColor = true;
            this.buttonContinuar.Click += new System.EventHandler(this.buttonContinuar_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDel.Location = new System.Drawing.Point(423, 264);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(34, 23);
            this.buttonDel.TabIndex = 2;
            this.buttonDel.Text = "▲";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.Location = new System.Drawing.Point(285, 264);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(34, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "▼";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // FormFacturacion
            // 
            this.AcceptButton = this.buttonContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonVolver;
            this.ClientSize = new System.Drawing.Size(733, 546);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonContinuar);
            this.Controls.Add(this.buttonVolver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelPendientes);
            this.Controls.Add(this.dgvAPagar);
            this.Controls.Add(this.dgvPendientes);
            this.Controls.Add(this.comboBoxUsuario);
            this.Controls.Add(this.labelUsuario);
            this.Name = "FormFacturacion";
            this.Text = "Facturacion de Costos";
            this.Load += new System.EventHandler(this.FormFacturacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAPagar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.ComboBox comboBoxUsuario;
        private System.Windows.Forms.DataGridView dgvPendientes;
        private System.Windows.Forms.DataGridView dgvAPagar;
        private System.Windows.Forms.Label labelPendientes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.Button buttonContinuar;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonAdd;
    }
}