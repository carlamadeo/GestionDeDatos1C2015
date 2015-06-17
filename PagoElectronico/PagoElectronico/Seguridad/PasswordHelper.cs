using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.DB;

namespace PagoElectronico.Seguridad
{
    public class PasswordHelper
    {
        public static PreguntaRespuesta getAnswerQuestionSecret()
        {
            SqlCommand command = new SqlCommand();
            var user = VarGlobal.usuario;
            command.CommandText = "SQL_SERVANT.sp_login_get_answer_question_secret";
            command.Parameters.Add(new SqlParameter("@p_id", SqlDbType.VarChar));
            command.Parameters["@p_id"].Value = user.id;

            var returnParameterQuestion = command.Parameters.Add(new SqlParameter("@p_question", SqlDbType.VarChar, 255));
            returnParameterQuestion.Direction = ParameterDirection.InputOutput;

            var returnParameterAnswer = command.Parameters.Add(new SqlParameter("@p_answer", SqlDbType.VarChar, 255));
            returnParameterAnswer.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(command, "obtener pregunta respuesta para el usuario", false);

            var pregunta = Convert.ToString(returnParameterQuestion.Value);
            var respuesta = Convert.ToString(returnParameterAnswer.Value);

            var questionAnswer = new PreguntaRespuesta(pregunta, respuesta);
            
            return questionAnswer;
        }

        public static Boolean isCorrectPassword(String password)
        {
            SqlCommand sp_check_password = new SqlCommand();
            sp_check_password.CommandText = "SQL_SERVANT.sp_password_check_ok";
            sp_check_password.Parameters.Add(new SqlParameter("@p_id", SqlDbType.VarChar));
            sp_check_password.Parameters["@p_id"].Value = VarGlobal.usuario.id;
            sp_check_password.Parameters.Add(new SqlParameter("@p_pass", SqlDbType.VarChar));
            sp_check_password.Parameters["@p_pass"].Value = Encrypt.Sha256(password);

            var returnParameterIsOk = sp_check_password.Parameters.Add(new SqlParameter("@p_ok", SqlDbType.Int));
            returnParameterIsOk.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_check_password, "chequear password", false);

            Int16 isOk = Convert.ToInt16(returnParameterIsOk.Value);

            if (isOk != 0)
            {
                return true;
            }

            return false;
        }

        public static void change(string password, string question, string answer, Boolean change)
        {
            SqlCommand sp_check_password = new SqlCommand();
            sp_check_password.CommandText = "SQL_SERVANT.sp_password_change";
            sp_check_password.Parameters.Add(new SqlParameter("@p_id", SqlDbType.VarChar));
            sp_check_password.Parameters["@p_id"].Value = VarGlobal.usuario.id;
            if (change)
            {
                sp_check_password.Parameters.Add(new SqlParameter("@p_pass", SqlDbType.VarChar));
                sp_check_password.Parameters["@p_pass"].Value = Encrypt.Sha256(password);
            }
            sp_check_password.Parameters.Add(new SqlParameter("@p_question", SqlDbType.VarChar, 255));
            sp_check_password.Parameters["@p_question"].Value = question;
            sp_check_password.Parameters.Add(new SqlParameter("@p_answer", SqlDbType.VarChar, 255));
            sp_check_password.Parameters["@p_answer"].Value = answer;

            ProcedureHelper.execute(sp_check_password, "Se guardo correctamente el cambio", true);
        }
    }
}
