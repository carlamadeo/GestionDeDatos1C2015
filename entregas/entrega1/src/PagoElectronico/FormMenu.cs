using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Menu;
using PagoElectronico.Login;

namespace PagoElectronico
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void opcionMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            MenuHelper.functionality func;

            func = MenuHelper.getFunctionality(menuItem.Tag.ToString());

            Form formAux;

            formAux = (Form)Activator.CreateInstance(null, "PagoElectronico." + func.folder + "." + func.form).Unwrap();

            formAux.MdiParent = this;

            formAux.StartPosition = FormStartPosition.CenterScreen;

            Form formActivo = (Form)this.ActiveMdiChild;

            if (formActivo != null)
            {
                MenuValidateHelper.setValidation(formActivo, false);
                formActivo.Close();
            }
            Size tamanio = new Size(30, 30);
            this.Size = formAux.Size + tamanio;
            formAux.Show();
        }

        private void opcionMenuSalir_Click(object sender, EventArgs e)
        {

            Form formActivo = (Form)this.ActiveMdiChild;

            if (formActivo != null)
            {
                MenuValidateHelper.setValidation(formActivo, false);
                formActivo.Close();
            }

            this.Close();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            Usuario user = VarGlobal.usuario;

            MdiClient ctlMDI;
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    ctlMDI = (MdiClient)ctl; ctlMDI.BackColor = this.BackColor;
                }
                catch (InvalidCastException exc)
                {
                    // Catch and ignore the error if casting failed.
                }
            }

            ToolStripMenuItem menuItem = new ToolStripMenuItem("&Menu");

            SortedList<int, OpcionMenu> optionsMenu = MenuHelper.getOptionMenu(user.rol.id);

            foreach (KeyValuePair<int, OpcionMenu> kvp in optionsMenu)
            {
                ToolStripMenuItem subItem = new ToolStripMenuItem(kvp.Value.descripcion, null, opcionMenu_Click);
                subItem.Tag = kvp.Value.descripcion;
                menuItem.DropDownItems.Add(subItem);
            }

            ToolStripMenuItem subItemLogin = new ToolStripMenuItem("Cambiar Usuario", null, irALogin);
            subItemLogin.Tag = "Cambiar Usuario";

            menuItem.DropDownItems.Add(subItemLogin);

            ToolStripMenuItem subItemSalir = new ToolStripMenuItem("Salir", null, opcionMenuSalir_Click);
            subItemSalir.Tag = "Salir";

            menuItem.DropDownItems.Add(subItemSalir);

            menuStrip1.Items.Add(menuItem);
            this.Controls.Add(menuStrip1);
        }

        private void irALogin(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            this.Hide();
            formLogin.ShowDialog();
            this.Close();
            
        }
    }
}
