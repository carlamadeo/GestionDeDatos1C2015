using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.DB;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Rol
{
    public class RolHelper
    {
        public static void search(String rolName, DataGridView dvgRol)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_rol_search";

            command.Parameters.Add(new SqlParameter("@p_rol_name", SqlDbType.VarChar, 255));
            if (rolName == string.Empty)
                command.Parameters["@p_rol_name"].Value = null;
            else
                command.Parameters["@p_rol_name"].Value = rolName;

            DataGridViewHelper.fill(command, dvgRol);
        }

        public static void enable(Int32 idRol, Boolean enable)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_rol_enable_disable";

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

            ProcedureHelper.execute(command, "Habilitar o deshabilitar un rol", false);
        }

        public static void editRol(Rol rol)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_rol_create";

            var returnParameterValue = command.Parameters.Add(new SqlParameter("@p_id_rol", SqlDbType.Int));
            returnParameterValue.Direction = ParameterDirection.InputOutput;
            command.Parameters["@p_id_rol"].Value = rol.id;

            command.Parameters.Add(new SqlParameter("@p_rol_description", SqlDbType.VarChar));
            command.Parameters["@p_rol_description"].Value = rol.description;

            ProcedureHelper.execute(command, "agregar funcionalidad a rol", false);

            rol.id = Convert.ToInt32(returnParameterValue.Value);
        }
    }
}
