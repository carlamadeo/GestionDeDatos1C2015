using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.ABM_Cliente;

namespace PagoElectronico.Menu
{
    public class MenuHelper
    {
        public static SortedList<int, OpcionMenu> getOptionMenu(int idRol)
        {
            SortedList<int, OpcionMenu> menuOptions = new SortedList<int, OpcionMenu>();

            SqlConnection conn = Connection.getConnection();

            string storedProcedureName = "SQL_SERVANT.sp_menu_list_functionality_by_user";
            SqlCommand command = new SqlCommand(storedProcedureName);
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@p_id_rol", idRol);

            if (idRol == 2)
                ClienteHelper.getClientIdByUserId(VarGlobal.usuario.id);

            SqlDataReader reader = command.ExecuteReader() as SqlDataReader;

            int position = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    OpcionMenu menuOption = new OpcionMenu();
                    menuOption.descripcion = reader["Descripcion"].ToString();
                    menuOption.idFuncionalidad = Convert.ToInt32(reader["Id_Funcionalidad"]);
                    menuOptions.Add(position, menuOption);
                    position++;
                }
            }

            Connection.close(conn);

            return menuOptions;
        }

        public struct functionality
        {
            public String folder;
            public String form;
        }

        public static functionality getFunctionality(string id)
        {
            functionality func = new functionality();

            switch (id)
            {
                case "ABM de Rol":
                    func.folder = "ABM_Rol";
                    func.form = "FormABMRol";
                    break;
                case "Login y Seguridad":
                    func.folder = "Seguridad";
                    func.form = "Password";
                    break;
                case "ABM de Usuario":
                    func.folder = "ABM_de_Usuario";
                    func.form = "FormABMUsuario";
                    break;
                case "ABM de Cuenta":
                    func.folder = "ABM_Cuenta";
                    func.form = "FormABMCuenta";
                    break;
                case "Asociar Tarjeta":
                    func.folder = "Tarjetas";
                    func.form = "FormAssociateCard";
                    break;
                case "Retiro de Efectivo":
                    func.folder = "Retiros";
                    func.form = "FormRetiros";
                    break;
                case "Depositos":
                    func.folder = "Depositos";
                    func.form = "FormDepositos";
                    break;
                case "Listado Estadistico":
                    func.folder = "Listados";
                    func.form = "FormListadoEstadistico";
                    break;
                case "ABM de Cliente":
                    func.folder = "ABM_Cliente";
                    func.form = "FormABMCliente";
                    break;
                case "Transferencias entre cuentas":
                    func.folder = "Transferencias";
                    func.form = "FormTransferencias";
                    break;
                case "Facturacion de Costos":
                    func.folder = "Facturacion";
                    func.form = "FormFacturacion";
                    break;
                case "Consulta de saldos":
                    func.folder = "Consulta_Saldos";
                    func.form = "FormConsultaSaldos";
                    break;

            }
            return func;
        }
    }
}
