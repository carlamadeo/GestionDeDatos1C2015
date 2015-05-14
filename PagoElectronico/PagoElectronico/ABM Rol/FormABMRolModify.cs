using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Rol
{
    public partial class FormABMRolModify : Form
    {
        private Rol rol;
        private Boolean edit;

        public FormABMRolModify(Boolean edit, Rol rol)
        {
            InitializeComponent();
            this.edit = edit;
            this.rol = rol;
        }

        private void FormABMRolModify_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            /*reloadGrid();

            if (edit)
            {
                this.txtRolDescription.Text = this.rol.description;
                this.buttonSaveName.Text = "Editar nombre";
            }*/
        }
    }
}
