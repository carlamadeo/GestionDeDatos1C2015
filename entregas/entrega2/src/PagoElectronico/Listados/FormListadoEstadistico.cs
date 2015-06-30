using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Listados
{
    public partial class FormListadoEstadistico : Form
    {
        public FormListadoEstadistico()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void FormListadoEstadistico_Load(object sender, EventArgs e)
        {
            EstadisticHelper.fillQuater(comboBoxQuater);
            EstadisticHelper.fillYear(comboBoxYear);
            EstadisticHelper.fillEstadistic(comboBoxEstadistic);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (comboBoxEstadistic.Text == String.Empty)
            {
                MessageBox.Show("Debe seleccionar una estadistica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxQuater.Text == String.Empty)
            {
                MessageBox.Show("Debe seleccionar un trimestre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxYear.Text == String.Empty)
            {
                MessageBox.Show("Debe seleccionar un año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            String sp = comboBoxEstadistic.SelectedValue.ToString();
            String[] quater = comboBoxQuater.SelectedValue.ToString().Split(';');
            Int16 dayFrom = Convert.ToInt16(quater[0].Split(',')[0]);
            Int16 monthFrom = Convert.ToInt16(quater[0].Split(',')[1]);
            Int16 dayTo = Convert.ToInt16(quater[1].Split(',')[0]);
            Int16 monthTo = Convert.ToInt16(quater[1].Split(',')[1]);

            Int32 year = Convert.ToInt32(comboBoxYear.SelectedValue.ToString());

            DateTime from = new DateTime(year, monthFrom, dayFrom);
            DateTime to = new DateTime(year, monthTo, dayTo);

            EstadisticHelper.loadEstadistic(sp, from, to, dgvEstadistic);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
