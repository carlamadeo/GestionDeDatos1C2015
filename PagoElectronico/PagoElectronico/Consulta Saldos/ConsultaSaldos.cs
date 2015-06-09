using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.DB;

namespace PagoElectronico.Consulta_Saldos
{
    class ConsultaSaldos
    {
        public static Boolean isValidAccount(String id_cuenta)
        {

            SqlCommand sp_check_account = new SqlCommand();
            sp_check_account.CommandText = "SQL_SERVANT.sp_consulta_check_account";
            sp_check_account.Parameters.Add(new SqlParameter("@p_id_cuenta", SqlDbType.VarChar));
            sp_check_account.Parameters["@p_id_cuenta"].Value = id_cuenta;

            var returnParameterIsValid = sp_check_account.Parameters.Add(new SqlParameter("@p_is_valid", SqlDbType.Bit));
            returnParameterIsValid.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_check_account, "chequear cuenta valido", false);

            int isValid = Convert.ToInt16(returnParameterIsValid.Value);

            if (isValid == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
