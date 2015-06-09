using System;
using System.Windows.Forms;
using PagoElectronico.ABM_de_Usuario;
using PagoElectronico.Seguridad;
using PagoElectronico.Tarjetas;

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
            TypeIdentification.fillComboBox(comboBoxIdentificationType);
            PaisHelper.fillComboBox(comboBoxNacionalidad);
            PaisHelper.fillComboBox(comboBoxPais);

            if (edit)
            {
                Cliente clientData = ClienteHelper.getClientData(client);
                Tarjeta.fillTarjetasByClientWhithout4LastDigits(comboBoxTarjetas, clientData.id);
                Empresa.fillEmpresa(comboBoxEmpresa);

                this.textBoxUsername.ReadOnly = true;
                this.textBoxPassword.ReadOnly = true;
                this.textBoxPregSecreta.ReadOnly = true;
                this.textBoxRespSecreta.ReadOnly = true;

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
            }

            else
            {
                this.Tab.TabPages.Remove(tabTarjetas);
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
            UsuarioDatos userData = this.getUserDataFromForm();

            if (userData != null)
            {
                Cliente clientData = this.getClientDataFromForm();

                if (clientData != null)
                {

                    if (textBoxPassword.Text != "" || edit)
                    {
                        if (!edit && UsuarioHelper.existUser(textBoxUsername.Text))
                        {
                            MessageBox.Show("Ya existe un usuario con ese Username.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    Boolean existEqualTypeAndIdentificationNumber = ClienteHelper.checkTypeAndIdentificationNumber(clientData.id,
                        clientData.typeIdentification, clientData.identificationNumber);

                    if (!edit && existEqualTypeAndIdentificationNumber)
                    {
                        DialogResult dialogIdentification = MessageBox.Show("Ya existe un usuario con ese Tipo y Numero de Identificacion.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Boolean existAnEqualMail = ClienteHelper.checkMail(clientData.id, clientData.mail);
                    if (!edit && existAnEqualMail)
                    {
                        DialogResult dialogMail = MessageBox.Show("Ya existe un usuario con ese Mail.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    this.saveOrUpdateClient(clientData, userData);
                }
            }
        }

        private void saveOrUpdateClient(Cliente clientData, UsuarioDatos userData)
        {
            Int32 clientId = ClienteHelper.save(clientData, userData, edit);
            this.client = clientId.ToString();
            if (edit)
            {
                MessageBox.Show("Modificacion de cliente realizada con exito", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Creacion de cliente realizada con exito", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.closeWindow();
        }

        private UsuarioDatos getUserDataFromForm()
        {
            UsuarioDatos userData = new UsuarioDatos();
            Boolean isValid;

            if (!edit)
            {
                isValid = Validaciones.requiredString(textBoxUsername.Text, "El campo Username no puede ser vacio");
                if (isValid)
                    userData.username = textBoxUsername.Text;
                else
                    return null;

                isValid = Validaciones.requiredString(textBoxPassword.Text, "El campo Password no puede ser vacio");
                if (isValid)
                    userData.password = Encrypt.Sha256(textBoxPassword.Text);
                else
                    return null;

                isValid = Validaciones.requiredString(textBoxPregSecreta.Text, "El campo Pregunta Secreta no puede ser vacio")
                    && Validaciones.requiredString(textBoxRespSecreta.Text, "El campo Respuesta Secreta no puede ser vacio");
                if (isValid)
                {
                    var questionAnswer = new PreguntaRespuesta(textBoxPregSecreta.Text, Encrypt.Sha256(textBoxRespSecreta.Text));
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

            isValid = (Validaciones.requiredString(textBoxName.Text, "El campo Nombre no puede ser vacio") &&
                Validaciones.validAndRequiredString(textBoxName.Text, "El campo Nombre debe contener unicamente letras"));
            if (isValid)
                clientData.name = textBoxName.Text;
            else
                return null;

            isValid = (Validaciones.requiredString(textBoxLastname.Text, "El campo Apellido no puede ser vacio") &&
                Validaciones.validAndRequiredString(textBoxLastname.Text, "El campo Apellido debe contener unicamente letras"));
            if (isValid)
                clientData.lastname = textBoxLastname.Text;
            else
                return null;

            isValid = Validaciones.requiredString(comboBoxIdentificationType.Text.ToString(), "Por favor seleccione el tipo de indentificacion");
            if (isValid)
                clientData.typeIdentification = comboBoxIdentificationType.Text.ToString();
            else
                return null;

            isValid = (Validaciones.validInt32(textBoxIdentificationNumber.Text, "El Numero de identificacion no puede ser vacio") &&
                Validaciones.validAndRequiredInt32(textBoxIdentificationNumber.Text, "El Numero de identificacion debe contener unicamente numeros"));
            if (isValid)
                clientData.identificationNumber = Convert.ToInt32(textBoxIdentificationNumber.Text);
            else
                return null;

            isValid = Validaciones.validAndRequiredMail(textBoxMail.Text, "El mail debe ser valido. Ej: pago@electronico.com");
            if (isValid)
                clientData.mail = textBoxMail.Text;
            else
                return null;

            isValid = Validaciones.requiredString(this.comboBoxPais.Text.ToString(), "Por favor seleccione el pais");
            if (isValid)
                clientData.country = this.comboBoxPais.Text.ToString();
            else
                return null;

            isValid = Validaciones.requiredString(textBoxAddress.Text, "El campo Direccion no puede ser vacio");
            if (isValid)
                clientData.addressName = textBoxAddress.Text;
            else
                return null;

            isValid = (Validaciones.validInt32(textBoxAddressNumber.Text, "El campo Numero de Direccion no puede ser vacio") &&
                Validaciones.validAndRequiredInt32(textBoxAddressNumber.Text, "El campo Numero de Direccion debe contener unicamente numeros"));
            if (isValid)
                clientData.addressNum = Convert.ToInt32(textBoxAddressNumber.Text);
            else
                return null;

            if (textBoxAddressFloor.Text != "" && textBoxAddressFloor.Text != String.Empty)
            {
                isValid = Validaciones.validAndRequiredInt32(textBoxAddressFloor.Text, "El campo Piso debe contener unicamente numeros");
                if (isValid)
                    clientData.addressFloor = Convert.ToInt32(textBoxAddressFloor.Text);
                else
                    return null;
                clientData.adressDeptName = textBoxAddressDept.Text;
            }
            else
            {
                if (textBoxAddressDept.Text != "" && textBoxAddressFloor.Text != String.Empty)
                {
                    MessageBox.Show("Si completa el campo Departamento debe completar el campo Piso");
                    return null;
                }
                clientData.addressFloor = VarGlobal.NoAddressFloor;
                clientData.adressDeptName = null;
            }

            isValid = (Validaciones.requiredString(textBoxLocalidad.Text, "El campo Localidad no puede ser vacio") &&
                Validaciones.validAndRequiredString(textBoxLocalidad.Text, "El campo Localidad debe contener unicamente letras"));
            if (isValid)
                clientData.localidad = textBoxLocalidad.Text;
            else
                return null;

            isValid = Validaciones.requiredString(this.comboBoxNacionalidad.Text.ToString(), "El campo nacionalidad no puede ser vacio");
            if (isValid)
                clientData.nationality = this.comboBoxNacionalidad.Text.ToString();
            else
                return null;

            DateTime birthdate = dtBrithdate.Value;

            clientData.birthdate = DateHelper.truncate(birthdate);

            return clientData;
        }

        private Tarjeta getTarjetaDataFromForm()
        {
            Tarjeta tarjetaData = new Tarjeta();

            Boolean isValid;

            isValid = Validaciones.requiredString(this.comboBoxTarjetas.Text, "Debe seleccionar una tarjeta");
            if (isValid)
                tarjetaData.id = this.comboBoxTarjetas.SelectedValue.ToString();
            else
                return null;

            isValid = Validaciones.requiredString(this.comboBoxTarjetas.Text, "Debe seleccionar una empresa");
            if (isValid)
                tarjetaData.empresa = this.comboBoxEmpresa.Text.ToString();
            else
                return null;

            isValid = Validaciones.validAndRequiredInt32(textBoxCodSeguridad.Text, "El codigo de seguridad es obligatorio");
            if (isValid)
                tarjetaData.codSeguridad = Convert.ToInt32(textBoxCodSeguridad.Text);
            else
                return null;

            DateTime fechaEmision = dateTimeEmision.Value;

            tarjetaData.fechaEmision = DateHelper.truncate(fechaEmision);

            DateTime fechaVencimiento = dateTimeVencimiento.Value;

            tarjetaData.fechaVencimiento = DateHelper.truncate(fechaVencimiento);

            return tarjetaData;
        }

        private void buttonDesvincular_Click(object sender, EventArgs e)
        {
            ClienteHelper.desvincularTarjeta(Convert.ToInt32(client), Convert.ToDecimal(this.comboBoxTarjetas.SelectedValue.ToString()));
            MessageBox.Show("Tarjeta desvinculada correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);           

            Tarjeta.fillTarjetasByClientWhithout4LastDigits(this.comboBoxTarjetas, Convert.ToInt32(client));
        }

        private void comboBoxTarjetas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tarjeta tarjeta = new Tarjeta();
            if (comboBoxTarjetas.SelectedIndex != 0)
            {
                tarjeta = TarjetaHelper.getClientTarjetaData(Convert.ToInt32(client), this.comboBoxTarjetas.SelectedValue.ToString());
                this.textBoxCodSeguridad.Text = tarjeta.codSeguridad.ToString();
                this.dateTimeEmision.Value = tarjeta.fechaEmision;
                this.dateTimeVencimiento.Value = tarjeta.fechaVencimiento;
                this.comboBoxEmpresa.SelectedIndex = this.comboBoxEmpresa.FindStringExact(tarjeta.empresa);
                
            }
        }

        private void buttonGuardarT_Click(object sender, EventArgs e)
        {
            Tarjeta tarjeta = this.getTarjetaDataFromForm();
            this.updateTarjeta(tarjeta);
        }

        private void updateTarjeta(Tarjeta tarjeta)
        {
            TarjetaHelper.save(tarjeta);
            MessageBox.Show("Modificacion de tarjeta realizada con exito", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);           
        }

        private void buttonVolverT_Click(object sender, EventArgs e)
        {
            this.closeWindow();
        }
    }
}
