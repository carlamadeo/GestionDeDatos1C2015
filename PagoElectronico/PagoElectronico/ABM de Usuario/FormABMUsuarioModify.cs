using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.ABM_Rol;
using PagoElectronico.Seguridad;

namespace PagoElectronico.ABM_de_Usuario
{
    public partial class FormABMUsuarioModify : Form
    {
        private Boolean edit;
        private String user;

        public FormABMUsuarioModify(Boolean edit, String user)
        {
            InitializeComponent();
            this.edit = edit;
            this.user = user;
        }

        private void FormABMUsuarioModify_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //Roles.fillComboBox(comboBoxRol);
            this.labelCreationDate.Visible = false;
            this.labelModificationDate.Visible = false;
            this.dateTimeCreation.Visible = false;
            this.dateTimeModification.Visible = false;

            UsuarioHelper.fill_dgv_user_rol(user, dgvOwnRol);
            UsuarioHelper.fill_dgv_not_user_rol(user, dgvAvailableRol);

            if (edit)
            {
                UsuarioDatos userData = UsuarioHelper.getUserData(user);
                this.textBoxUsername.Text = userData.username;
                this.textBoxUsername.Enabled = false;
                this.textBoxPassword.Text = userData.password;
                this.textBoxPassword.Enabled = false;
                this.textBoxAnswer.Text = userData.questionAnswer.respuesta;
                this.textBoxQuestion.Text = userData.questionAnswer.pregunta;
                //this.comboBoxDocumentType.SelectedIndex = this.comboBoxDocumentType.FindStringExact(userData.typeDocumentDescription);
                this.checkBoxEnable.Checked = userData.enabled;
                this.dateTimeCreation.Value = userData.creationDate;
                this.dateTimeModification.Value = userData.modifyDate;
                this.labelModificationDate.Visible = true;
                this.labelCreationDate.Visible = true;
                this.dateTimeModification.Enabled = false;
                this.dateTimeCreation.Enabled = false;
                this.dateTimeCreation.Visible = true;
                this.dateTimeModification.Visible = true;

                //this.dtBirthDate.Value = userData.birthDate;

                //Rol rolUsuario = UsuarioHelper.getRolByUserHotel(user, VarGlobal.usuario.hotel);
                //this.comboBoxRol.SelectedIndex = this.comboBoxRol.FindStringExact(rolUsuario.description);
            }
        }

        private UsuarioDatos getDataFromForm()
        {
            UsuarioDatos userData = new UsuarioDatos();
            Boolean isValid;
            isValid = Validaciones.requiredString(textBoxUsername.Text, "El nombre de usuario es necesario");
            if (isValid)
                userData.username = textBoxUsername.Text;
            else
                return null;

            isValid = Validaciones.requiredString(textBoxPassword.Text, "El password es necesario");
            if (isValid){
                if (edit)
                    userData.password = null;
                    
                else
                    userData.password = Encrypt.Sha256(textBoxPassword.Text);
            }else{
                return null;
            }

            isValid = Validaciones.requiredString(textBoxQuestion.Text, "La pregunta secreta es necesaria")
                && Validaciones.requiredString(textBoxAnswer.Text, "La respuesta secreta es necesaria");
            if (isValid)
            {
                var questionAnswer = new PreguntaRespuesta(textBoxQuestion.Text, textBoxAnswer.Text);
                userData.questionAnswer = questionAnswer;
            }
            else
                return null;

            userData.enabled = this.checkBoxEnable.Checked;
            if (edit)
            {
                userData.creationDate = this.dateTimeCreation.Value;
                userData.modifyDate = DateHelper.getToday();
            }
            else
            {
                userData.creationDate = DateHelper.getToday();
                userData.modifyDate = DateHelper.getToday();
            }
            
            return userData;
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            UsuarioHelper.cleanLogin(textBoxUsername.Text.ToString());
            MessageBox.Show("Se reinicio contador de login");
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            this.closeWindow();
        }

        private void closeWindow()
        {
            FormABMUsuario formABMUsuario = new FormABMUsuario();
            formABMUsuario.MdiParent = this.MdiParent;
            MdiParent.Size = formABMUsuario.Size;
            this.Close();
            formABMUsuario.Show();
        }

        private void buttonAccept_Click_1(object sender, EventArgs e)
        {
            UsuarioDatos userData = this.getDataFromForm();
            if (userData != null)
            {
                if (textBoxPassword.Text != "" || edit)
                {
                    if (!edit && UsuarioHelper.existUser(textBoxUsername.Text))
                    {
                        MessageBox.Show("Ya existe un usuario con ese nombre", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    UsuarioHelper.save(userData);
                    if (edit)
                    {
                        MessageBox.Show("Modificacion de usuario realizada con exito");
                    }
                    else
                    {
                        MessageBox.Show("Creacion de usuario realizada con exito");
                    }
                }
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
