using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.DB;
using PagoElectronico.ABM_de_Usuario;

namespace PagoElectronico.Login
{
    class Login
    {
        public static Boolean isValidUser(Usuario user)
        {
            return UsuarioHelper.existUser(user.id);
        }

        public static int login(Usuario user, String password)
        {
            SqlCommand sp_check_password = new SqlCommand();
            sp_check_password.CommandText = "SQL_SERVANT.sp_login_check_password";
            sp_check_password.Parameters.Add(new SqlParameter("@p_id", SqlDbType.VarChar));
            sp_check_password.Parameters["@p_id"].Value = user.id;
            sp_check_password.Parameters.Add(new SqlParameter("@p_pass", SqlDbType.VarChar));
            sp_check_password.Parameters["@p_pass"].Value = Encrypt.Sha256(password);

            var returnParameterIsValid = sp_check_password.Parameters.Add(new SqlParameter("@p_intentos", SqlDbType.Int));
            returnParameterIsValid.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_check_password, "chequear password", false);

            return Convert.ToInt16(returnParameterIsValid.Value);
        }
    }
}
