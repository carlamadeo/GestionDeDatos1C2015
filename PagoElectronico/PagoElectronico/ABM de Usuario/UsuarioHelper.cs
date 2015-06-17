using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PagoElectronico.DB;
using PagoElectronico.Seguridad;

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

        public static void search(string name, string rol, DataGridView dgvUser)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_user_search";

            command.Parameters.Add(new SqlParameter("@p_user_name", SqlDbType.VarChar, 255));
            if (name == string.Empty)
                command.Parameters["@p_user_name"].Value = null;
            else
                command.Parameters["@p_user_name"].Value = name;

            command.Parameters.Add(new SqlParameter("@p_id_rol", SqlDbType.Int));
            if (rol == string.Empty)
                command.Parameters["@p_id_rol"].Value = null;
            else
                command.Parameters["@p_id_rol"].Value = Convert.ToInt16(rol);

            DataGridViewHelper.fill(command, dgvUser);
        }

        public static void enable(string username, Int16 idRol, Boolean enable)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_user_enable_disable";

            command.Parameters.Add(new SqlParameter("@p_user_name", SqlDbType.VarChar, 255));
            command.Parameters["@p_user_name"].Value = username;

            command.Parameters.Add(new SqlParameter("@p_id_rol", SqlDbType.Int));
            command.Parameters["@p_id_rol"].Value = idRol;

            command.Parameters.Add(new SqlParameter("@p_enable_disable", SqlDbType.Int));
            if (enable)
            {
                command.Parameters["@p_enable_disable"].Value = 1;
            }
            else
            {
                command.Parameters["@p_enable_disable"].Value = 0;
            }

            command.Parameters.Add(new SqlParameter("@p_time", SqlDbType.DateTime));
            command.Parameters["@p_time"].Value = DateHelper.getToday();

            ProcedureHelper.execute(command, "Habilitar o deshabilitar usuario", false);
        }

        public static UsuarioDatos getUserData(string username)
        {
            UsuarioDatos userData = new UsuarioDatos();

            SqlConnection conn = Connection.getConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SQL_SERVANT.sp_user_get_by_user";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@p_user_name", username);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                userData.username = username;
                userData.password = Convert.ToString(reader["Password"]);
                userData.modifyDate = Convert.ToDateTime(reader["Ultima_Modificacion"]);
                userData.creationDate = Convert.ToDateTime(reader["Fecha_Creacion"]);

                var question = Convert.ToString(reader["Pregunta_Secreta"]);
                var answer = Convert.ToString(reader[7]);

                var questionAnswer = new PreguntaRespuesta(question, answer);

                userData.questionAnswer = questionAnswer;
                
                int enable = Convert.ToInt16(reader["Habilitado"]);

                if (enable == 1)
                    userData.enabled = true;
                else
                    userData.enabled = false;
            }

            conn.Close();

            return userData;
        }

        public static void save(UsuarioDatos userData)
        {
            SqlCommand sp_save_or_update_user = new SqlCommand();
            sp_save_or_update_user.CommandType = CommandType.StoredProcedure;
            sp_save_or_update_user.CommandText = "SQL_SERVANT.sp_user_save_update";

            sp_save_or_update_user.Parameters.AddWithValue("@p_user_name", userData.username);
            sp_save_or_update_user.Parameters.AddWithValue("@p_password", userData.password);
            sp_save_or_update_user.Parameters.AddWithValue("@p_user_question_secret", userData.questionAnswer.pregunta);
            sp_save_or_update_user.Parameters.AddWithValue("@p_user_answer_secret", userData.questionAnswer.respuesta);
            sp_save_or_update_user.Parameters.AddWithValue("@p_user_creation_date", userData.creationDate);
            sp_save_or_update_user.Parameters.AddWithValue("@p_user_modify_date", userData.modifyDate);
            
            if (userData.enabled){
                sp_save_or_update_user.Parameters.AddWithValue("@p_enabled", 1);
            }else{
                sp_save_or_update_user.Parameters.AddWithValue("@p_enabled", 0);
            }

            ProcedureHelper.execute(sp_save_or_update_user, "save or update user data", false);
        }
    }
}
