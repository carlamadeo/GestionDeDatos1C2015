using System;
using System.Data;
using System.Data.SqlClient;
using PagoElectronico.ABM_de_Usuario;
using PagoElectronico.DB;

namespace PagoElectronico.Login
{
    class Login
    {
        public static Boolean isValidUser(Usuario user)
        {
            return UsuarioHelper.existUser(user.id);
        }

        public static int login(Usuario user, String password, ref int correcto)
        {
            SqlCommand sp_check_password = new SqlCommand();
            sp_check_password.CommandText = "SQL_SERVANT.sp_login_check_password";
            sp_check_password.Parameters.Add(new SqlParameter("@p_id", SqlDbType.VarChar));
            sp_check_password.Parameters["@p_id"].Value = user.id;
            sp_check_password.Parameters.Add(new SqlParameter("@p_pass", SqlDbType.VarChar));
            sp_check_password.Parameters["@p_pass"].Value = Encrypt.Sha256(password);

            var returnParameterIsValid = sp_check_password.Parameters.Add(new SqlParameter("@p_intentos", SqlDbType.Int));
            returnParameterIsValid.Direction = ParameterDirection.InputOutput;

            var returnParameterIsCorrect = sp_check_password.Parameters.Add(new SqlParameter("@p_correcto", SqlDbType.Int));
            returnParameterIsCorrect.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_check_password, "chequear password", false);

            correcto = Convert.ToInt16(returnParameterIsCorrect.Value);

            auditoria(user.id, correcto);

            return Convert.ToInt16(returnParameterIsValid.Value);
        }

        public static void auditoria (String userId, int isCorrecto)
        {
            SqlCommand sp_save_auditoria_login = new SqlCommand();
            sp_save_auditoria_login.CommandType = CommandType.StoredProcedure;
            sp_save_auditoria_login.CommandText = "SQL_SERVANT.sp_save_auditoria_login";

            sp_save_auditoria_login.Parameters.AddWithValue("@p_id", userId);
            sp_save_auditoria_login.Parameters.AddWithValue("@p_is_correcto", isCorrecto);

            ProcedureHelper.execute(sp_save_auditoria_login, "Guardar auditoria", false);
        }

        public static Boolean isAdmin(String userId)
        {
            SqlCommand sp_check_user_is_admin = new SqlCommand();
            sp_check_user_is_admin.CommandText = "SQL_SERVANT.sp_check_user_is_admin";
            sp_check_user_is_admin.Parameters.Add(new SqlParameter("@p_id_usuario", SqlDbType.VarChar));
            sp_check_user_is_admin.Parameters["@p_id_usuario"].Value = userId;

            var returnParameterIsAdmin = sp_check_user_is_admin.Parameters.Add(new SqlParameter("@p_is_admin", SqlDbType.Int));
            returnParameterIsAdmin.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_check_user_is_admin, "chequear si usuario es admin", false);

            if (Convert.ToInt16(returnParameterIsAdmin.Value) == 1)
                return true;
            else
                return false;
        }
    }
}
