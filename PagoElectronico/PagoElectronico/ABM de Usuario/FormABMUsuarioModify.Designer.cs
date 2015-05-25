namespace PagoElectronico.ABM_de_Usuario
{
    partial class FormABMUsuarioModify
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
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxQuestion = new System.Windows.Forms.TextBox();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonClean = new System.Windows.Forms.Button();
            this.labelCreationDate = new System.Windows.Forms.Label();
            this.dateTimeCreation = new System.Windows.Forms.DateTimePicker();
            this.labelModificationDate = new System.Windows.Forms.Label();
            this.dateTimeModification = new System.Windows.Forms.DateTimePicker();
            this.dgvAvailableRol = new System.Windows.Forms.DataGridView();
            this.dgvOwnRol = new System.Windows.Forms.DataGridView();
            this.buttonAddRol = new System.Windows.Forms.Button();
            this.buttonRemoveRol = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableRol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOwnRol)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre Usuario";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(149, 86);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(205, 20);
            this.textBoxUsername.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(149, 127);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(205, 20);
            this.textBoxPassword.TabIndex = 3;
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Location = new System.Drawing.Point(40, 283);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(73, 17);
            this.checkBoxEnable.TabIndex = 5;
            this.checkBoxEnable.Text = "Habilitado";
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Pregunta Secreta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Respuesta Secreta";
            // 
            // textBoxQuestion
            // 
            this.textBoxQuestion.Location = new System.Drawing.Point(149, 170);
            this.textBoxQuestion.Name = "textBoxQuestion";
            this.textBoxQuestion.Size = new System.Drawing.Size(205, 20);
            this.textBoxQuestion.TabIndex = 8;
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Location = new System.Drawing.Point(149, 210);
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.Size = new System.Drawing.Size(205, 20);
            this.textBoxAnswer.TabIndex = 9;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(249, 411);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 10;
            this.buttonAccept.Text = "Guardar";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click_1);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(509, 411);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Volver";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click_1);
            // 
            // buttonClean
            // 
            this.buttonClean.Location = new System.Drawing.Point(541, 339);
            this.buttonClean.Name = "buttonClean";
            this.buttonClean.Size = new System.Drawing.Size(99, 23);
            this.buttonClean.TabIndex = 12;
            this.buttonClean.Text = "Limpiar Intentos";
            this.buttonClean.UseVisualStyleBackColor = true;
            this.buttonClean.Click += new System.EventHandler(this.buttonClean_Click);
            // 
            // labelCreationDate
            // 
            this.labelCreationDate.AutoSize = true;
            this.labelCreationDate.Location = new System.Drawing.Point(37, 249);
            this.labelCreationDate.Name = "labelCreationDate";
            this.labelCreationDate.Size = new System.Drawing.Size(82, 13);
            this.labelCreationDate.TabIndex = 13;
            this.labelCreationDate.Text = "Fecha Creacion";
            // 
            // dateTimeCreation
            // 
            this.dateTimeCreation.Location = new System.Drawing.Point(149, 245);
            this.dateTimeCreation.Name = "dateTimeCreation";
            this.dateTimeCreation.Size = new System.Drawing.Size(205, 20);
            this.dateTimeCreation.TabIndex = 14;
            // 
            // labelModificationDate
            // 
            this.labelModificationDate.AutoSize = true;
            this.labelModificationDate.Location = new System.Drawing.Point(428, 308);
            this.labelModificationDate.Name = "labelModificationDate";
            this.labelModificationDate.Size = new System.Drawing.Size(99, 13);
            this.labelModificationDate.TabIndex = 15;
            this.labelModificationDate.Text = "Ultima Modificacion";
            // 
            // dateTimeModification
            // 
            this.dateTimeModification.Location = new System.Drawing.Point(541, 304);
            this.dateTimeModification.Name = "dateTimeModification";
            this.dateTimeModification.Size = new System.Drawing.Size(200, 20);
            this.dateTimeModification.TabIndex = 16;
            // 
            // dgvAvailableRol
            // 
            this.dgvAvailableRol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableRol.Location = new System.Drawing.Point(376, 86);
            this.dgvAvailableRol.Name = "dgvAvailableRol";
            this.dgvAvailableRol.Size = new System.Drawing.Size(151, 150);
            this.dgvAvailableRol.TabIndex = 17;
            // 
            // dgvOwnRol
            // 
            this.dgvOwnRol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOwnRol.Location = new System.Drawing.Point(593, 86);
            this.dgvOwnRol.Name = "dgvOwnRol";
            this.dgvOwnRol.Size = new System.Drawing.Size(148, 150);
            this.dgvOwnRol.TabIndex = 18;
            // 
            // buttonAddRol
            // 
            this.buttonAddRol.Location = new System.Drawing.Point(541, 105);
            this.buttonAddRol.Name = "buttonAddRol";
            this.buttonAddRol.Size = new System.Drawing.Size(34, 23);
            this.buttonAddRol.TabIndex = 19;
            this.buttonAddRol.Text = "->";
            this.buttonAddRol.UseVisualStyleBackColor = true;
            this.buttonAddRol.Click += new System.EventHandler(this.buttonAddRol_Click);
            // 
            // buttonRemoveRol
            // 
            this.buttonRemoveRol.Location = new System.Drawing.Point(541, 189);
            this.buttonRemoveRol.Name = "buttonRemoveRol";
            this.buttonRemoveRol.Size = new System.Drawing.Size(34, 23);
            this.buttonRemoveRol.TabIndex = 20;
            this.buttonRemoveRol.Text = "<-";
            this.buttonRemoveRol.UseVisualStyleBackColor = true;
            this.buttonRemoveRol.Click += new System.EventHandler(this.buttonRemoveRol_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(625, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Roles Asignados";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(413, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Roles Disponibles";
            // 
            // FormABMUsuarioModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 463);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonRemoveRol);
            this.Controls.Add(this.buttonAddRol);
            this.Controls.Add(this.dgvOwnRol);
            this.Controls.Add(this.dgvAvailableRol);
            this.Controls.Add(this.dateTimeModification);
            this.Controls.Add(this.labelModificationDate);
            this.Controls.Add(this.dateTimeCreation);
            this.Controls.Add(this.labelCreationDate);
            this.Controls.Add(this.buttonClean);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.textBoxAnswer);
            this.Controls.Add(this.textBoxQuestion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxEnable);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.label1);
            this.Name = "FormABMUsuarioModify";
            this.Text = "FormABMUsuarioModify";
            this.Load += new System.EventHandler(this.FormABMUsuarioModify_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableRol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOwnRol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.CheckBox checkBoxEnable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxQuestion;
        private System.Windows.Forms.TextBox textBoxAnswer;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonClean;
        private System.Windows.Forms.Label labelCreationDate;
        private System.Windows.Forms.DateTimePicker dateTimeCreation;
        private System.Windows.Forms.Label labelModificationDate;
        private System.Windows.Forms.DateTimePicker dateTimeModification;
        private System.Windows.Forms.DataGridView dgvAvailableRol;
        private System.Windows.Forms.DataGridView dgvOwnRol;
        private System.Windows.Forms.Button buttonAddRol;
        private System.Windows.Forms.Button buttonRemoveRol;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}