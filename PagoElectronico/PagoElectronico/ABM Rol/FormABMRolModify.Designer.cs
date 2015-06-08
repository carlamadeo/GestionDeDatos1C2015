namespace PagoElectronico.ABM_Rol
{
    partial class FormABMRolModify
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
            this.txtRolDescription = new System.Windows.Forms.TextBox();
            this.buttonSaveName = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvToAdd = new System.Windows.Forms.DataGridView();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxCrearRol = new System.Windows.Forms.GroupBox();
            this.groupBoxFuncionalidades = new System.Windows.Forms.GroupBox();
            this.labelHabilitado = new System.Windows.Forms.Label();
            this.checkBoxHabilitado = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            this.groupBoxCrearRol.SuspendLayout();
            this.groupBoxFuncionalidades.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de Rol";
            // 
            // txtRolDescription
            // 
            this.txtRolDescription.Location = new System.Drawing.Point(135, 33);
            this.txtRolDescription.Name = "txtRolDescription";
            this.txtRolDescription.Size = new System.Drawing.Size(114, 20);
            this.txtRolDescription.TabIndex = 1;
            // 
            // buttonSaveName
            // 
            this.buttonSaveName.Location = new System.Drawing.Point(518, 133);
            this.buttonSaveName.Name = "buttonSaveName";
            this.buttonSaveName.Size = new System.Drawing.Size(72, 23);
            this.buttonSaveName.TabIndex = 2;
            this.buttonSaveName.Text = "Crear";
            this.buttonSaveName.UseVisualStyleBackColor = true;
            this.buttonSaveName.Click += new System.EventHandler(this.buttonSaveName_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Funcionalidades Disponibles";
            // 
            // dgvToAdd
            // 
            this.dgvToAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToAdd.Location = new System.Drawing.Point(21, 68);
            this.dgvToAdd.Name = "dgvToAdd";
            this.dgvToAdd.Size = new System.Drawing.Size(228, 150);
            this.dgvToAdd.TabIndex = 4;
            // 
            // dgvSelected
            // 
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Location = new System.Drawing.Point(314, 68);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.Size = new System.Drawing.Size(228, 150);
            this.dgvSelected.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Funcionalidades Asignadas";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(264, 106);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(33, 23);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "->";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(264, 145);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(33, 23);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.Text = "<-";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(30, 423);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Volver";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxCrearRol
            // 
            this.groupBoxCrearRol.Controls.Add(this.checkBoxHabilitado);
            this.groupBoxCrearRol.Controls.Add(this.labelHabilitado);
            this.groupBoxCrearRol.Controls.Add(this.txtRolDescription);
            this.groupBoxCrearRol.Controls.Add(this.label1);
            this.groupBoxCrearRol.Location = new System.Drawing.Point(30, 37);
            this.groupBoxCrearRol.Name = "groupBoxCrearRol";
            this.groupBoxCrearRol.Size = new System.Drawing.Size(560, 82);
            this.groupBoxCrearRol.TabIndex = 10;
            this.groupBoxCrearRol.TabStop = false;
            this.groupBoxCrearRol.Text = "Creación de Rol";
            // 
            // groupBoxFuncionalidades
            // 
            this.groupBoxFuncionalidades.Controls.Add(this.label2);
            this.groupBoxFuncionalidades.Controls.Add(this.buttonRemove);
            this.groupBoxFuncionalidades.Controls.Add(this.dgvToAdd);
            this.groupBoxFuncionalidades.Controls.Add(this.buttonAdd);
            this.groupBoxFuncionalidades.Controls.Add(this.label3);
            this.groupBoxFuncionalidades.Controls.Add(this.dgvSelected);
            this.groupBoxFuncionalidades.Location = new System.Drawing.Point(30, 162);
            this.groupBoxFuncionalidades.Name = "groupBoxFuncionalidades";
            this.groupBoxFuncionalidades.Size = new System.Drawing.Size(560, 240);
            this.groupBoxFuncionalidades.TabIndex = 11;
            this.groupBoxFuncionalidades.TabStop = false;
            this.groupBoxFuncionalidades.Text = "Editar Funcionalidades";
            // 
            // labelHabilitado
            // 
            this.labelHabilitado.AutoSize = true;
            this.labelHabilitado.Location = new System.Drawing.Point(311, 36);
            this.labelHabilitado.Name = "labelHabilitado";
            this.labelHabilitado.Size = new System.Drawing.Size(54, 13);
            this.labelHabilitado.TabIndex = 2;
            this.labelHabilitado.Text = "Habilitado";
            // 
            // checkBoxHabilitado
            // 
            this.checkBoxHabilitado.AutoSize = true;
            this.checkBoxHabilitado.Location = new System.Drawing.Point(396, 35);
            this.checkBoxHabilitado.MaximumSize = new System.Drawing.Size(20, 20);
            this.checkBoxHabilitado.Name = "checkBoxHabilitado";
            this.checkBoxHabilitado.Size = new System.Drawing.Size(15, 14);
            this.checkBoxHabilitado.TabIndex = 3;
            this.checkBoxHabilitado.UseVisualStyleBackColor = true;
            // 
            // FormABMRolModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 465);
            this.Controls.Add(this.buttonSaveName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxCrearRol);
            this.Controls.Add(this.groupBoxFuncionalidades);
            this.Name = "FormABMRolModify";
            this.Text = "ABM Rol";
            this.Load += new System.EventHandler(this.FormABMRolModify_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvToAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            this.groupBoxCrearRol.ResumeLayout(false);
            this.groupBoxCrearRol.PerformLayout();
            this.groupBoxFuncionalidades.ResumeLayout(false);
            this.groupBoxFuncionalidades.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRolDescription;
        private System.Windows.Forms.Button buttonSaveName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvToAdd;
        private System.Windows.Forms.DataGridView dgvSelected;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxCrearRol;
        private System.Windows.Forms.GroupBox groupBoxFuncionalidades;
        private System.Windows.Forms.CheckBox checkBoxHabilitado;
        private System.Windows.Forms.Label labelHabilitado;
    }
}