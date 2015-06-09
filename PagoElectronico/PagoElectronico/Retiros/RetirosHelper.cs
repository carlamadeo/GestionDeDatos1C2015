using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.DB;

namespace PagoElectronico.Retiros
{
    public class RetirosHelper
    {
        public static String generateExtraction(String accountId, Double amount, String currency, Int32 bankId)
        {
            SqlCommand sp_retirement_generate_extraction = new SqlCommand();
            sp_retirement_generate_extraction.CommandText = "SQL_SERVANT.sp_retirement_generate_extraction";
            sp_retirement_generate_extraction.Parameters.Add(new SqlParameter("@p_retirement_client_id", SqlDbType.Int));
            sp_retirement_generate_extraction.Parameters["@p_retirement_client_id"].Value = VarGlobal.usuario.clientId;

            sp_retirement_generate_extraction.Parameters.Add(new SqlParameter("@p_retirement_account_id", SqlDbType.BigInt));
            sp_retirement_generate_extraction.Parameters["@p_retirement_account_id"].Value = accountId;

            sp_retirement_generate_extraction.Parameters.Add(new SqlParameter("@p_retirement_amount", SqlDbType.Decimal));
            sp_retirement_generate_extraction.Parameters["@p_retirement_amount"].Value = amount;

            sp_retirement_generate_extraction.Parameters.Add(new SqlParameter("@p_retirement_bank_id", SqlDbType.Int));
            sp_retirement_generate_extraction.Parameters["@p_retirement_bank_id"].Value = bankId;

            sp_retirement_generate_extraction.Parameters.Add(new SqlParameter("@p_retirement_today", SqlDbType.DateTime));
            sp_retirement_generate_extraction.Parameters["@p_retirement_today"].Value = DateHelper.getToday();

            sp_retirement_generate_extraction.Parameters.Add(new SqlParameter("@p_retirement_currency", SqlDbType.VarChar, 3));
            sp_retirement_generate_extraction.Parameters["@p_retirement_currency"].Value = currency;

            var returnParametersIsValid = sp_retirement_generate_extraction.Parameters.Add(new SqlParameter("@p_retirement_check_number", SqlDbType.Int));
            returnParametersIsValid.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_retirement_generate_extraction, "Se genero retiro", false);

            String checkNumber = Convert.ToString(returnParametersIsValid.Value);
            
            return checkNumber;
        }
    }
}
