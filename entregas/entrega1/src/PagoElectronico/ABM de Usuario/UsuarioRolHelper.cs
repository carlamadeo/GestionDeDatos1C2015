using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using PagoElectronico.DB;

namespace PagoElectronico.ABM_de_Usuario
{
    public class UsuarioRolHelper
    {
        public static void addRolToUser(string user, int idRol)
        {
            SqlCommand sp_add_rol_to_user = new SqlCommand();
            sp_add_rol_to_user.CommandText = "SQL_SERVANT.sp_user_rol_add";
            sp_add_rol_to_user.Parameters.AddWithValue("@p_user_name", user);
            sp_add_rol_to_user.Parameters.AddWithValue("@p_rol_id", idRol);
            sp_add_rol_to_user.Parameters.AddWithValue("@p_date", DateHelper.getToday());
            ProcedureHelper.execute(sp_add_rol_to_user, "add rol to user", false);
        }

        public static void removeRolToUser(string user, int idRol)
        {
            SqlCommand sp_remove_rol_to_user = new SqlCommand();
            sp_remove_rol_to_user.CommandText = "SQL_SERVANT.sp_user_rol_remove";
            sp_remove_rol_to_user.Parameters.AddWithValue("@p_user_name", user);
            sp_remove_rol_to_user.Parameters.AddWithValue("@p_rol_id", idRol);
            sp_remove_rol_to_user.Parameters.AddWithValue("@p_date", DateHelper.getToday());
            ProcedureHelper.execute(sp_remove_rol_to_user, "remove rol to user", false);
        }

        public static void fill_dgv_user_rol(String user, DataGridView dgv)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_user_get_user_rol";
            command.Parameters.AddWithValue("@p_user_name", user);
            DataGridViewHelper.fill(command, dgv);
        }

        public static void fill_dgv_not_user_rol(String user, DataGridView dgv)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_user_get_not_user_rol";
            command.Parameters.AddWithValue("@p_user_name", user);
            DataGridViewHelper.fill(command, dgv);
        }
    }
}
