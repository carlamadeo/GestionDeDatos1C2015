namespace PagoElectronico.Listados
{
    partial class FormListadoEstadistico
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
            this.comboBoxEstadistic = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxQuater = new System.Windows.Forms.ComboBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.dgvEstadistic = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadistic)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estadistica";
            // 
            // comboBoxEstadistic
            // 
            this.comboBoxEstadistic.FormattingEnabled = true;
            this.comboBoxEstadistic.Location = new System.Drawing.Point(123, 39);
            this.comboBoxEstadistic.Name = "comboBoxEstadistic";
            this.comboBoxEstadistic.Size = new System.Drawing.Size(412, 21);
            this.comboBoxEstadistic.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trimestre";
            // 
            // comboBoxQuater
            // 
            this.comboBoxQuater.FormattingEnabled = true;
            this.comboBoxQuater.Location = new System.Drawing.Point(123, 85);
            this.comboBoxQuater.Name = "comboBoxQuater";
            this.comboBoxQuater.Size = new System.Drawing.Size(121, 21);
            this.comboBoxQuater.TabIndex = 3;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(56, 368);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 4;
            this.buttonBack.Text = "Volver";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(414, 85);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(121, 21);
            this.comboBoxYear.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(354, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Año";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(460, 126);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 7;
            this.buttonSearch.Text = "Buscar";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // dgvEstadistic
            // 
            this.dgvEstadistic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstadistic.Location = new System.Drawing.Point(56, 171);
            this.dgvEstadistic.Name = "dgvEstadistic";
            this.dgvEstadistic.Size = new System.Drawing.Size(479, 178);
            this.dgvEstadistic.TabIndex = 8;
            // 
            // FormListadoEstadistico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 417);
            this.Controls.Add(this.dgvEstadistic);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxYear);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.comboBoxQuater);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxEstadistic);
            this.Controls.Add(this.label1);
            this.Name = "FormListadoEstadistico";
            this.Text = "FormListadoEstadistico";
            this.Load += new System.EventHandler(this.FormListadoEstadistico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadistic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxEstadistic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxQuater;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DataGridView dgvEstadistic;
    }
}