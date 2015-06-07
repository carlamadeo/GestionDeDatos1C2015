using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Login
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            user.id = textBoxUser.Text;

            VarGlobal.usuario = user;

            String pass = textBoxPassword.Text;

            if (!Login.isValidUser(user))
            {
                MessageBox.Show("No es un usuario valido");
            }
            else
            {
                int intentos = Login.login(user, pass);

                if (intentos == 0)
                {
                    FormSeleccionRol fSeleccionRol = new FormSeleccionRol();
                    this.Hide();
                    fSeleccionRol.ShowDialog();
                    this.Close();
                }
                else if (intentos < 3)
                {
                    MessageBox.Show("La contraseña es erronea. Lleva " + intentos + " intento(s)" );
                }
                else
                {
                    MessageBox.Show("Contactese con el administrador para limpiar su clave");
                }
            }
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
