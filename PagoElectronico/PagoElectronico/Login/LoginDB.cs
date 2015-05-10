using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace PagoElectronico.Login
{
    class LoginDB
    {
        internal static bool isSuccessLogin(string user, string password)
        {
            bool ret = false;

            using (SqlConnection con = Connection.getConnection())
            {
                using (SqlCommand command = new SqlCommand("SQL_SERVANT.proc_isSuccessLogin", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", user);
                    command.Parameters.AddWithValue("@Password", password);

                    var returnParameter = command.Parameters.Add("@Correct", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    command.ExecuteNonQuery();
                    ret = Convert.ToBoolean(returnParameter.Value);   
                }
            }

            return ret;
        }

        public static void WrongLogin(string user)
        {
            using (SqlConnection con = Connection.getConnection())
            {
                using (SqlCommand command = new SqlCommand("SQL_SERVANT.proc_wrongLogin", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", user);

                    command.ExecuteNonQuery();
                }
            }

        }

        internal static int CountRol(string user)
        {
            int ret;

            using (SqlConnection con = Connection.getConnection())
            {
                using (SqlCommand command = new SqlCommand("SQL_SERVANT.proc_countRol", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", user);

                    var returnParameter = command.Parameters.Add("@CountRol", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    command.ExecuteNonQuery();
                    ret = Convert.ToInt32(returnParameter.Value);
                }
            }

            return ret;
        }

        internal static void ResetLogin(string user)
        {
            using (SqlConnection con = Connection.getConnection())
            {
                using (SqlCommand command = new SqlCommand("SQL_SERVANT.proc_resetLogin", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", user);

                    command.ExecuteNonQuery();
                }
            }
        }

        internal static int CountWrongLogin(string user)
        {
            int ret;

            using (SqlConnection con = Connection.getConnection())
            {
                using (SqlCommand command = new SqlCommand("SQL_SERVANT.proc_countWrongLogin", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", user);

                    var returnParameter = command.Parameters.Add("@CountWrongLogin", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    command.ExecuteNonQuery();
                    ret = Convert.ToInt32(returnParameter.Value);
                }
            }

            return ret;
        }

        internal static void BlockUser(string user)
        {
            using (SqlConnection con = Connection.getConnection())
            {
                using (SqlCommand command = new SqlCommand("SQL_SERVANT.proc_blockUser", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", user);

                    command.ExecuteNonQuery();
                }
            }
        }

        internal static bool notBlockedUser(string user)
        {
            bool ret = false;

            using (SqlConnection con = Connection.getConnection())
            {
                using (SqlCommand command = new SqlCommand("SQL_SERVANT.proc_notBlockedUser", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", user);

                    var returnParameter = command.Parameters.Add("@notBlocked", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    command.ExecuteNonQuery();
                    ret = Convert.ToBoolean(returnParameter.Value);
                }
            }

            return ret;
        }

        internal static bool userExist(string user)
        {
            bool ret = false;

            using (SqlConnection con = Connection.getConnection())
            {
                using (SqlCommand command = new SqlCommand("SQL_SERVANT.proc_isUser", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", user);

                    var returnParameter = command.Parameters.Add("@exists", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    command.ExecuteNonQuery();
                    ret = Convert.ToBoolean(returnParameter.Value);
                }
            }

            return ret;
        }
    }
}
