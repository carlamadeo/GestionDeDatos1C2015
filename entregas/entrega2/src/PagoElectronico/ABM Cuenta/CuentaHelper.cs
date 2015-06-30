using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.DB;

namespace PagoElectronico.ABM_Cuenta
{
    class CuentaHelper
    {
        public static void fillCuentasHabilitadasByClient(ComboBox comboBoxCuenta, Int32 idClient)
        {
            ComboBoxHelper.fill(comboBoxCuenta, "SQL_SERVANT.Cliente_Cuenta cc INNER JOIN SQL_SERVANT.Cuenta c ON cc.Id_Cuenta = c.Id_Cuenta",
                "c.Id_Cuenta", "c.Id_Cuenta", "cc.Id_Cliente = '" + idClient + "' AND c.Id_Estado_Cuenta = 4", null);
        }

        public static void fillCuentasByClient(ComboBox comboBoxCuenta, Int32 idClient)
        {
            ComboBoxHelper.fill(comboBoxCuenta, "SQL_SERVANT.Cliente_Cuenta cc INNER JOIN SQL_SERVANT.Cuenta c ON cc.Id_Cuenta = c.Id_Cuenta",
                "c.Id_Cuenta", "c.Id_Cuenta", "cc.Id_Cliente = '" + idClient + "'", null);
        }

        public static void fillCuentasHabilitadasDeshabilitadasByClient(ComboBox comboBoxCuenta, Int32 idClient)
        {
            ComboBoxHelper.fill(comboBoxCuenta, "SQL_SERVANT.Cliente_Cuenta cc INNER JOIN SQL_SERVANT.Cuenta c ON cc.Id_Cuenta = c.Id_Cuenta",
                "c.Id_Cuenta", "c.Id_Cuenta", "cc.Id_Cliente = '" + idClient + "' AND c.Id_Estado_Cuenta = 4 or c.Id_Estado_Cuenta = 3", null);
        }

        public static void save(Cuenta accountData)
        {
            SqlCommand sp_save_or_update_account = new SqlCommand();
            sp_save_or_update_account.CommandType = CommandType.StoredProcedure;
            sp_save_or_update_account.CommandText = "SQL_SERVANT.sp_account_save_update";

            sp_save_or_update_account.Parameters.Add(new SqlParameter("@p_account_id", SqlDbType.VarChar, 255));
            if (accountData.id == "")
                sp_save_or_update_account.Parameters["@p_account_id"].Value = accountData.id;
            else
                sp_save_or_update_account.Parameters["@p_account_id"].Value = null;

            sp_save_or_update_account.Parameters.Add(new SqlParameter("@p_account_client_id", SqlDbType.Int));
            sp_save_or_update_account.Parameters["@p_account_client_id"].Value = accountData.idClient;

            sp_save_or_update_account.Parameters.Add(new SqlParameter("@p_account_country_id", SqlDbType.Int));
            sp_save_or_update_account.Parameters["@p_account_country_id"].Value = accountData.idCountry;

            sp_save_or_update_account.Parameters.Add(new SqlParameter("@p_account_currency_id", SqlDbType.Int));
            sp_save_or_update_account.Parameters["@p_account_currency_id"].Value = accountData.idCurrency;

            sp_save_or_update_account.Parameters.Add(new SqlParameter("@p_account_type_account_id", SqlDbType.Int));
            sp_save_or_update_account.Parameters["@p_account_type_account_id"].Value = accountData.idTypeAccount;

            sp_save_or_update_account.Parameters.Add(new SqlParameter("@p_account_date", SqlDbType.DateTime));
            sp_save_or_update_account.Parameters["@p_account_date"].Value = DateHelper.getToday();

            ProcedureHelper.execute(sp_save_or_update_account, "save or update account data", false);
        }

        public static Cuenta getAccountData(Int16 idClient, String idAccount)
        {
            Cuenta accountData = new Cuenta();

            SqlConnection conn = Connection.getConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SQL_SERVANT.sp_account_get_data";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@p_account_id", idAccount);
            command.Parameters.AddWithValue("@p_account_client_id", idClient);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                accountData.id = idAccount;
                accountData.idClient = idClient;
                accountData.idCountry = Convert.ToInt16(reader["Id_Pais"]);
                accountData.countryDescription = Convert.ToString(reader["Pais"]);
                accountData.idCurrency = Convert.ToInt16(reader["Id_Moneda"]);
                accountData.currencyDescription = Convert.ToString(reader["Moneda"]);
                accountData.idTypeAccount = Convert.ToInt16(reader["Id_Tipo_Cuenta"]);
                accountData.typeAccountDescription = Convert.ToString(reader["Tipo_Cuenta"]);
                accountData.creationDate = Convert.ToDateTime(reader["Fecha_Creacion"]);
                accountData.expirationDate = Convert.ToDateTime(reader["Fecha_Vencimiento"]);
            }

            conn.Close();

            return accountData;
        }

        public static Decimal getImporteMaximo(Decimal cuenta)
        {
            SqlCommand sp_get_importe_maximo_por_cuenta = new SqlCommand();
            sp_get_importe_maximo_por_cuenta.CommandText = "SQL_SERVANT.sp_get_importe_maximo_por_cuenta";
            sp_get_importe_maximo_por_cuenta.Parameters.Add(new SqlParameter("@p_cuenta_id", SqlDbType.BigInt));
            sp_get_importe_maximo_por_cuenta.Parameters["@p_cuenta_id"].Value = cuenta;

            SqlParameter returnParametersImporteMaximo = new SqlParameter("@p_importe_maximo", SqlDbType.Decimal);
            returnParametersImporteMaximo.Precision = 18;
            returnParametersImporteMaximo.Scale = 2;
            sp_get_importe_maximo_por_cuenta.Parameters.Add(returnParametersImporteMaximo);
            returnParametersImporteMaximo.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_get_importe_maximo_por_cuenta, "chequear monto maximo", false);

            return Convert.ToDecimal(returnParametersImporteMaximo.Value);
        }
    }
}
