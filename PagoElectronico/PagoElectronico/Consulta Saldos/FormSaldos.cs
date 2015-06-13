using System;
using System.Windows.Forms;

namespace PagoElectronico.Consulta_Saldos
{
    public partial class FormSaldos : Form
    {
        private Int16 idClient;
        private String idCuenta;

        public FormSaldos(Int16 idClient, String idCuenta)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.idClient = idClient;
            this.idCuenta = idCuenta;

        }

        private void FormSaldos_Load(object sender, EventArgs e)
        {
            ConsultaSaldosHelper.getLastDeposits(this.dgvDepositos, this.idCuenta);

            ConsultaSaldosHelper.getLastWithrawals(this.dgvRetiros, this.idCuenta);

            ConsultaSaldosHelper.getLastTransfers(this.dgvTransferencias, this.idCuenta);
            
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            FormConsultaSaldos formABMConsultaSaldos = new FormConsultaSaldos();
            formABMConsultaSaldos.MdiParent = this.MdiParent;
            MdiParent.Size = formABMConsultaSaldos.Size;
            this.Close();
            formABMConsultaSaldos.Show();
        }
    }
}
