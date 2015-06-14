﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Seguridad
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void Password_Load(object sender, EventArgs e)
        {
            var answerQuestion = PasswordHelper.getAnswerQuestionSecret();

            textBoxPregSec.Text = answerQuestion.pregunta;
            textBoxRespSec.Text = answerQuestion.respuesta;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                if (PasswordHelper.isCorrectPassword(textBoxOldPass.Text))
                {
                    PasswordHelper.change(textBoxNewPass.Text, textBoxPregSec.Text, textBoxRespSec.Text);
                }
                else
                {
                    MessageBox.Show("La contraseña anterior es invalida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Boolean validar()
        {
            return Validaciones.requiredString(textBoxNewPass.Text, "Debe ingresar una nueva contraseña") &&
                Validaciones.requiredString(textBoxOldPass.Text, "Debe ingresar la contraseña actual") &&
                Validaciones.requiredString(textBoxRespSec.Text, "Debe ingresar una respuesta secreta") &&
                Validaciones.requiredString(textBoxPregSec.Text, "Debe ingresar una pregunta secreta") &&
                Validaciones.differentValues(textBoxNewPass.Text, textBoxOldPass.Text, "Las contrseñas deben ser distintas");

        }
    }
}
