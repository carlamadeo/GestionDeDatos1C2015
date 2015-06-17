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

        public static Boolean isEnabled(Int32 idRol)
        {
            SqlCommand sp_rol_is_enabled = new SqlCommand();
            sp_rol_is_enabled.CommandText = "SQL_SERVANT.sp_rol_is_enabled";
            sp_rol_is_enabled.Parameters.Add(new SqlParameter("@p_rol_id", SqlDbType.Int));
            sp_rol_is_enabled.Parameters["@p_rol_id"].Value = idRol;

            var returnParametersIsEnabled = sp_rol_is_enabled.Parameters.Add(new SqlParameter("@p_isEnabled", SqlDbType.Int));
            returnParametersIsEnabled.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_rol_is_enabled, "chequear estado rol", false);

            if (Convert.ToInt16(returnParametersIsEnabled.Value) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void editRol(Rol rol)
        {
            int habilitado = 0;

            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_rol_create";

            command.Parameters.Add(new SqlParameter("@p_rol_description", SqlDbType.VarChar));
            command.Parameters["@p_rol_description"].Value = rol.description;

            if (rol.habilitado)
                habilitado = 1;
            
            command.Parameters.Add(new SqlParameter("@p_rol_habilitado", SqlDbType.Int));
            command.Parameters["@p_rol_habilitado"].Value = habilitado;

            command.Parameters.Add(new SqlParameter("@p_rol_funcionalidad", SqlDbType.VarChar));
            command.Parameters["@p_rol_funcionalidad"].Value = rol.primerFuncionalidad;

            var returnParameterValue = command.Parameters.Add(new SqlParameter("@p_id_rol", SqlDbType.Int));
            returnParameterValue.Direction = ParameterDirection.InputOutput;
            command.Parameters["@p_id_rol"].Value = rol.id;

            ProcedureHelper.execute(command, "agregar funcionalidad a rol", false);

            rol.id = Convert.ToInt32(returnParameterValue.Value);
        }
    }
}
