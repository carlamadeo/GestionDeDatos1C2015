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
    class Roles
    {
        public static int fillRolByUser(Usuario user)
        {
            SqlCommand sp_rol_exist_one_by_user = new SqlCommand();
            sp_rol_exist_one_by_user.CommandText = "SQL_SERVANT.sp_rol_exist_one_by_user";
            sp_rol_exist_one_by_user.Parameters.Add(new SqlParameter("@p_id", SqlDbType.VarChar));
            sp_rol_exist_one_by_user.Parameters["@p_id"].Value = user.id;

            var returnParameterCountRol = sp_rol_exist_one_by_user.Parameters.Add(new SqlParameter("@p_count_rol", SqlDbType.Int));
            returnParameterCountRol.Direction = ParameterDirection.InputOutput;
            var returnParameterIdRol = sp_rol_exist_one_by_user.Parameters.Add(new SqlParameter("@p_id_rol", SqlDbType.Int));
            returnParameterIdRol.Direction = ParameterDirection.InputOutput;
            var returnParameterRolDesc = sp_rol_exist_one_by_user.Parameters.Add(new SqlParameter("@p_rol_desc", SqlDbType.VarChar, 255));
            returnParameterRolDesc.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_rol_exist_one_by_user, "chequear si un usuario tiene un solo rol", false);

            int countRol = Convert.ToInt16(returnParameterCountRol.Value);

            if (countRol == 1)
            {
                int id = Convert.ToInt16(returnParameterIdRol.Value);
                string description = Convert.ToString(returnParameterRolDesc.Value);

                user.rol = new Rol(id, description);
            }

            return countRol;
        }

        public static void fillComboBoxByUser(ComboBox comboBox_Roles, Usuario user)
        {
            ComboBoxHelper.fill(comboBox_Roles, "SQL_SERVANT.Usuario_Rol ur INNER JOIN SQL_SERVANT.Rol r ON ur.Id_Rol = r.Id_Rol",
                "ur.Id_Rol", "Descripcion", "ur.Id_Usuario = '" + user.id + "' AND r.Habilitado = 1 AND ur.Habilitado = 1", null);
        }

        public static void fillComboBox(ComboBox comboBox)
        {
            ComboBoxHelper.fill(comboBox, "SQL_SERVANT.Rol r",
                "r.Id_Rol", "r.Descripcion", "r.Habilitado = 1 AND r.Descripcion != 'guest'", null);
        }
    }
}
