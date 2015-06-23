using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PagoElectronico.ABM_Cuenta;
using PagoElectronico.DB;
using PagoElectronico.Tarjetas;
using PagoElectronico.Utils;

namespace PagoElectronico.Depositos
{
    public partial class FormDepositos : Form
    {
        public FormDepositos()
        {
            InitializeComponent();
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Depositos_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            Tarjeta.fillTarjetasByClient(comboBoxTarjetas, VarGlobal.usuario.clientId);
            CuentaHelper.fillCuentasHabilitadasByClient(comboBoxCuentas, VarGlobal.usuario.clientId);
            this.fillListBoxMonedas();
        }

        public void fillListBoxMonedas()
        {
            ListBoxHelper.fill(this.listBoxMoneda, "SQL_SERVANT.Moneda m",
                "m.Id_Moneda", "m.Descripcion", "", null);
        }

        private Deposito getDataFromForm()
        {
            Deposito deposito = new Deposito();

            Boolean isValid;

            isValid = Validaciones.requiredString(this.comboBoxCuentas.Text.ToString(), "Es necesario que seleccione una cuenta");
            if (isValid)
                deposito.cuenta = Convert.ToDecimal(this.comboBoxCuentas.Text.ToString());
            else
                return null;

            isValid = Validaciones.validAndRequiredDecimalMoreThan0(this.textBoxImporte.Text.ToString().Replace(".", ","), "El monto a depositar debe ser mayor a 0");
            if (isValid)
                deposito.importe = Convert.ToDecimal(this.textBoxImporte.Text.ToString().Replace(".", ","));
            else
                return null;

            isValid = Validaciones.requiredString(this.comboBoxTarjetas.SelectedValue.ToString(), "Es necesario que seleccione una tarjeta");
            if (isValid)
                deposito.tarjeta = this.comboBoxTarjetas.SelectedValue.ToString();
            else
                return null;

            deposito.moneda = Convert.ToInt32(listBoxMoneda.SelectedValue.ToString());

            return deposito;
        }

        private void buttonDepositar_Click(object sender, EventArgs e)
        {
            Deposito deposito = this.getDataFromForm();
            if (deposito != null)
            {
                Boolean tarjetaValida = Validaciones.tarjetaNoVencida(deposito.tarjeta);
                if (tarjetaValida)
                {
                    deposito.fecha = DateHelper.getToday();
                    this.saveDeposito(deposito);
                    MessageBox.Show("Se ha realizado correctamente el deposito por un monto de " + String.Format("{0:C}", deposito.importe), "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void saveDeposito(Deposito deposito)
        {
            SqlCommand sp_save_deposito = new SqlCommand();
            sp_save_deposito.CommandType = CommandType.StoredProcedure;
            sp_save_deposito.CommandText = "SQL_SERVANT.sp_save_deposito";

            sp_save_deposito.Parameters.AddWithValue("@p_deposito_cuenta", deposito.cuenta);
            sp_save_deposito.Parameters.AddWithValue("@p_deposito_importe", deposito.importe);
            sp_save_deposito.Parameters.AddWithValue("@p_deposito_moneda", deposito.moneda);
            sp_save_deposito.Parameters.AddWithValue("@p_deposito_tarjeta", deposito.tarjeta);
            sp_save_deposito.Parameters.AddWithValue("@p_deposito_fecha", deposito.fecha);

            ProcedureHelper.execute(sp_save_deposito, "save deposito", false);
        }
    }
}
