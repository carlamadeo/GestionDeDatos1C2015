using System;
using System.Windows.Forms;
using PagoElectronico.ABM_Cliente;
using PagoElectronico.ABM_de_Usuario;
using PagoElectronico.ABM_Rol;

namespace PagoElectronico.Login
{
    public partial class FormLogin : Form
    {
        private int countRol;

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
                if (Login.isAdmin(user.id))
                {
                    int correcto = 0;
                    int intentos = Login.login(user, pass, ref correcto);

                    if (correcto == 1)
                    {
                        MessageBox.Show("El Administrador se encuentra inhabilitado. Solo podrá modificar o dar de baja Usuarios", "Imposible Loguearse", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FormABMUsuario formABMUsuario = new FormABMUsuario(true);
                        this.Hide();
                        formABMUsuario.Show();
                    }

                    else
                    {
                        MessageBox.Show("La contraseña es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }

                else
                    MessageBox.Show("El usuario es invalido. Intentelo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (pass == "")
            {
                MessageBox.Show("El Password no puede ser vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                countRol = Roles.fillRolByUser(user);
                int correcto = 0;

                int intentos = Login.login(user, pass, ref correcto);

                if (intentos == 0)
                {
                    if (countRol == 0)
                        MessageBox.Show("El usuario no tiene ningun rol habilitado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    else
                    {
                        FormSeleccionRol fSeleccionRol = new FormSeleccionRol(countRol);
                        this.Hide();
                        fSeleccionRol.ShowDialog();
                        this.Close();
                    }
                }

                else if (intentos < 3)
                    MessageBox.Show("La contraseña es incorrecta. Lleva " + intentos + " intento(s)", "Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else
                    MessageBox.Show("Contactese con el administrador para limpiar su clave", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonRegistro_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAltaModificacionCliente formAltaModificacion = new FormAltaModificacionCliente(false, null, true);
            formAltaModificacion.Show();
        }

    }
}
