using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.DB;

namespace PagoElectronico.ABM_de_Usuario
{
    public class UsuarioHelper
    {
        public static void cleanLogin(string username)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_user_clean_login";

            command.Parameters.Add(new SqlParameter("@p_user_name", SqlDbType.VarChar, 255));
            command.Parameters["@p_user_name"].Value = username;

            ProcedureHelper.execute(command, "Limpiar intentos de login", false);
        }

        public static Boolean existUser(string user)
        {
            SqlCommand sp_check_user = new SqlCommand();
            sp_check_user.CommandText = "SQL_SERVANT.sp_login_check_valid_user";
            sp_check_user.Parameters.Add(new SqlParameter("@p_id", SqlDbType.VarChar));
            sp_check_user.Parameters["@p_id"].Value = user;

            var returnParameterIsValid = sp_check_user.Parameters.Add(new SqlParameter("@p_is_valid", SqlDbType.Bit));
            returnParameterIsValid.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_check_user, "chequear usuario valido", false);

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
