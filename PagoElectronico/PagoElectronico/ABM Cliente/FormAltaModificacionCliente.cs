using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.ABM_Rol;
using PagoElectronico.ABM_de_Usuario;
using PagoElectronico.Seguridad;

namespace PagoElectronico.ABM_Cliente
{
    public partial class FormAltaModificacionCliente : Form
    {
        private String client { get; set; }
        private Boolean edit { get; set; }
        private Form from;

        public FormAltaModificacionCliente(Boolean edit, String client, Form from)
        {
            this.edit = edit;
            this.client = client;
            this.from = from;
            InitializeComponent();
        }

        private void FormAltaModificacionCliente_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            Usuario user = VarGlobal.usuario;
            Roles.fillComboBoxByUser(comboBoxRol, user);
            TypeIdentification.fillComboBox(comboBoxIdentificationType);
            Paises.fillComboBox(comboBoxNacionalidad);
            Paises.fillComboBox(comboBoxPais);

            if (edit)
            {
                Cliente clientData = ClienteHelper.getClientData(client);

                this.textBoxUsername.ReadOnly = true;
                this.textBoxPassword.ReadOnly = true;
                this.textBoxPregSecreta.ReadOnly = true;
                this.textBoxRespSecreta.ReadOnly = true;
                this.comboBoxRol.Enabled = false;

                this.textBoxName.Text = clientData.name;
                this.textBoxLastname.Text = clientData.lastname;
                this.comboBoxIdentificationType.SelectedIndex = this.comboBoxIdentificationType.FindStringExact(clientData.typeIdentification);
                this.textBoxIdentificationNumber.Text = clientData.identificationNumber.ToString();
                this.textBoxMail.Text = clientData.mail;
                this.textBoxAddress.Text = clientData.addressName;
                this.textBoxAddressNumber.Text = clientData.addressNum.ToString();
                this.textBoxAddressFloor.Text = clientData.addressFloor.ToString();
                this.textBoxAddressDept.Text = clientData.adressDeptName;
                this.comboBoxPais.SelectedIndex = this.comboBoxPais.FindStringExact(clientData.country);
                this.textBoxLocalidad.Text = clientData.localidad;
                this.comboBoxNacionalidad.SelectedIndex = this.comboBoxNacionalidad.FindStringExact(clientData.nationality);
                this.dtBrithdate.Value = clientData.birthdate;
                this.comboBoxRol.SelectedIndex = this.comboBoxRol.FindStringExact(clientData.rol);
            }

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.closeWindow();
        }

        private void closeWindow()
        {
            if (from != null)
            {
                from.MdiParent = this.MdiParent;
                MdiParent.Size = from.Size;
                this.Close();
                from.Show();
            }
            else
            {
                FormABMCliente formABMCliente = new FormABMCliente();
                formABMCliente.MdiParent = this.MdiParent;
                MdiParent.Size = formABMCliente.Size;
                this.Close();
                formABMCliente.Show();
            }
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            Cliente clientData = this.getClientDataFromForm();
            UsuarioDatos userData = this.getUserDataFromForm();

            if (clientData != null && userData != null)
            {

                if (textBoxPassword.Text != "" || edit)
                {
                    if (!edit && UsuarioHelper.existUser(textBoxUsername.Text))
                    {
                        MessageBox.Show("Ya existe un usuario con ese username", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                Boolean existEqualTypeAndIdentificationNumber = ClienteHelper.checkTypeAndIdentificationNumber(clientData.id, 
                    clientData.typeIdentification, clientData.identificationNumber);

                if (existEqualTypeAndIdentificationNumber)
                {
                    DialogResult dialogIdentification = MessageBox.Show("Ya existe un usuario con ese tipo y numero de identificacion.",
                        "Mensaje importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Boolean existAnEqualMail = ClienteHelper.checkMail(clientData.id, clientData.mail);
                if (existAnEqualMail)
                {
                    DialogResult dialogMail = MessageBox.Show("Ya existe un usuario con ese mail.",
                        "Mensaje importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.saveOrUpdateClient(clientData, userData);
            }
        }

        private void saveOrUpdateClient(Cliente clientData, UsuarioDatos userData)
        {
            Int32 clientId = ClienteHelper.save(clientData, userData, edit);
            this.client = clientId.ToString();
            if (edit)
            {
                MessageBox.Show("Modificacion de cliente realizada con exito");
            }
            else
            {
                MessageBox.Show("Creacion de cliente realizada con exito");
            }
            this.closeWindow();
        }

        private UsuarioDatos getUserDataFromForm()
        {
            UsuarioDatos userData = new UsuarioDatos();
            Boolean isValid;

            if (!edit)
            {
                isValid = Validaciones.requiredString(textBoxUsername.Text, "El username es necesario");
                if (isValid)
                    userData.username = textBoxUsername.Text;
                else
                    return null;

                isValid = Validaciones.requiredString(textBoxPassword.Text, "El password es necesario");
                if (isValid)
                    userData.password = Encrypt.Sha256(textBoxPassword.Text);
                else
                    return null;

                isValid = Validaciones.requiredString(textBoxPregSecreta.Text, "La pregunta secreta es necesaria")
                    && Validaciones.requiredString(textBoxRespSecreta.Text, "La respuesta secreta es necesaria");
                if (isValid)
                {
                    var questionAnswer = new PreguntaRespuesta(textBoxPregSecreta.Text, textBoxRespSecreta.Text);
                    userData.questionAnswer = questionAnswer;
                }
                else
                    return null;
            }

            return userData;
        }

        private Cliente getClientDataFromForm()
        {
            Cliente clientData = new Cliente();

            if (client != null || client != "")
            {
                clientData.id = Convert.ToInt32(client);
            }
            else
            {
                clientData.id = 0;
            }

            Boolean isValid;

            isValid = Validaciones.requiredString(textBoxLocalidad.Text, "La localidad es necesaria");
            if (isValid)
                clientData.localidad = textBoxLocalidad.Text;
            else
                return null;

            isValid = Validaciones.requiredString(textBoxLocalidad.Text, "La localidad es necesaria");
            if (isValid)
                clientData.localidad = textBoxLocalidad.Text;
            else
                return null;

            isValid = Validaciones.requiredString(textBoxName.Text, "El nombre es necesario");
            if (isValid)
                clientData.name = textBoxName.Text;
            else
                return null;

            isValid = Validaciones.requiredString(textBoxLastname.Text, "El apellido es necesario");
            if (isValid)
                clientData.lastname = textBoxLastname.Text;
            else
                return null;

            isValid = Validaciones.requiredString(comboBoxIdentificationType.Text.ToString(), "El tipo de identificacion es necesario");
            if (isValid)
                clientData.typeIdentification = comboBoxIdentificationType.Text.ToString();
            else
                return null;

            if (isValid)
                clientData.identificationNumber = Convert.ToInt32(textBoxIdentificationNumber.Text);
            else
                return null;

            isValid = Validaciones.validAndRequiredMail(textBoxMail.Text, "El mail debe ser valido");
            if (isValid)
                clientData.mail = textBoxMail.Text;
            else
                return null;

            isValid = Validaciones.requiredString(textBoxAddress.Text, "La direccion es obligatoria");
            if (isValid)
                clientData.addressName = textBoxAddress.Text;
            else
                return null;

            isValid = Validaciones.validAndRequiredInt32(textBoxAddressNumber.Text, "El numero de la direccion es obligatorio");
            if (isValid)
                clientData.addressNum = Convert.ToInt32(textBoxAddressNumber.Text);
            else
                return null;

            if (textBoxAddressFloor.Text != "" && textBoxAddressFloor.Text != String.Empty)
            {
                isValid = Validaciones.validAndRequiredInt32(textBoxAddressFloor.Text, "El piso debe ser numerico");
                clientData.addressFloor = Convert.ToInt32(textBoxAddressFloor.Text);
                clientData.adressDeptName = textBoxAddressDept.Text;
            }
            else
            {
                if (textBoxAddressDept.Text != "" && textBoxAddressFloor.Text != String.Empty)
                {
                    MessageBox.Show("Si completa el nombre de departamento debe colocar un piso");
                    return null;
                }
                clientData.addressFloor = VarGlobal.NoAddressFloor;
                clientData.adressDeptName = null;
            }

            isValid = Validaciones.requiredString(this.comboBoxNacionalidad.Text.ToString(), "Debe seleccionar una nacionalidad");
            if (isValid)
                clientData.nationality = this.comboBoxNacionalidad.Text.ToString();
            else
                return null;

            isValid = Validaciones.requiredString(this.comboBoxPais.Text.ToString(), "Debe seleccionar un pais");
            if (isValid)
                clientData.country = this.comboBoxPais.Text.ToString();
            else
                return null;

            DateTime birthdate = dtBrithdate.Value;

            DateHelper.truncate(birthdate);
            clientData.birthdate = birthdate;

            return clientData;
        }

        private void textBoxPregSecreta_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
