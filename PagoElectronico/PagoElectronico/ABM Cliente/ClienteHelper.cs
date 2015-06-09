using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using PagoElectronico.DB;
using PagoElectronico.ABM_de_Usuario;
using PagoElectronico.Seguridad;

namespace PagoElectronico.ABM_Cliente
{
    public class ClienteHelper
    {
        public static void searchAllClient(string lastname, DataGridView dgv)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_client_search_by_lastname";

            command.Parameters.Add(new SqlParameter("@p_client_lastname", SqlDbType.VarChar, 255));
            if (lastname == string.Empty)
                command.Parameters["@p_client_lastname"].Value = null;
            else
                command.Parameters["@p_client_lastname"].Value = lastname;

            DataGridViewHelper.fill(command, dgv);
        }

        public static void search(Cliente client, DataGridView dgvClient)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_client_search";

            command.Parameters.Add(new SqlParameter("@p_client_name", SqlDbType.VarChar, 255));
            if (client.name == string.Empty)
                command.Parameters["@p_client_name"].Value = null;
            else
                command.Parameters["@p_client_name"].Value = client.name;

            command.Parameters.Add(new SqlParameter("@p_client_lastname", SqlDbType.VarChar, 255));
            if (client.lastname == string.Empty)
                command.Parameters["@p_client_lastname"].Value = null;
            else
                command.Parameters["@p_client_lastname"].Value = client.lastname;

            command.Parameters.Add(new SqlParameter("@p_id_type_identification", SqlDbType.Int));
            if (client.idTypeIdentification == 0)
                command.Parameters["@p_id_type_identification"].Value = null;
            else
                command.Parameters["@p_id_type_identification"].Value = client.idTypeIdentification;

            command.Parameters.Add(new SqlParameter("@p_client_identification_number", SqlDbType.VarChar, 255));
            if (client.identificationNumber == 0)
                command.Parameters["@p_client_identification_number"].Value = null;
            else
                command.Parameters["@p_client_identification_number"].Value = client.identificationNumber.ToString();

            command.Parameters.Add(new SqlParameter("@p_client_mail", SqlDbType.VarChar, 255));
            if (client.mail == string.Empty)
                command.Parameters["@p_client_mail"].Value = null;
            else
                command.Parameters["@p_client_mail"].Value = client.mail;

            DataGridViewHelper.fill(command, dgvClient);
        }

        public static void enable(Int32 id, Boolean enable)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_client_enable_disable";

            command.Parameters.Add(new SqlParameter("@p_client_id", SqlDbType.Int));
            command.Parameters["@p_client_id"].Value = id;

            command.Parameters.Add(new SqlParameter("@p_enable_disable", SqlDbType.Int));
            if (enable)
            {
                command.Parameters["@p_enable_disable"].Value = 1;
            }
            else
            {
                command.Parameters["@p_enable_disable"].Value = 0;
            }

            ProcedureHelper.execute(command, "Habilitar o deshabilitar client", false);
        }

        public static Cliente getClientData(String clientId)
        {
            Cliente clientData = new Cliente();
            UsuarioDatos userData = new UsuarioDatos();

            SqlConnection conn = Connection.getConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SQL_SERVANT.sp_client_data_get_by_id_client";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@p_id_client", clientId);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                clientData.id = Convert.ToInt32(clientId);
                userData.username = Convert.ToString(reader["Username"]);
                clientData.name = Convert.ToString(reader["Nombre"]);
                clientData.lastname = Convert.ToString(reader["Apellido"]);
                clientData.idTypeIdentification = Convert.ToInt32(reader["Tipo_Identificacion"]);
                clientData.typeIdentification = Convert.ToString(reader["Identificacion_Descripcion"]);
                if (reader["Nro_Identificacion"] != DBNull.Value)
                    clientData.identificationNumber = Convert.ToInt32(reader["Nro_Identificacion"]);
                clientData.mail = Convert.ToString(reader["Mail"]);
                clientData.addressName = Convert.ToString(reader["Direccion"]);
                clientData.addressNum = Convert.ToInt32(reader["Calle_Nro"]);
                if (reader["Piso"] != DBNull.Value)
                    clientData.addressFloor = Convert.ToInt32(reader["Piso"]);
                if (reader["Depto"] != DBNull.Value)
                    clientData.adressDeptName = Convert.ToString(reader["Depto"]);
                clientData.idNacionality = Convert.ToInt32(reader["Id_Nacionalidad"]);
                clientData.nationality = Convert.ToString(reader["Nacionalidad_Descripcion"]);
                clientData.birthdate = Convert.ToDateTime(reader["Fecha_Nacimiento"]);
                if (reader["Localidad"] != DBNull.Value)
                    clientData.localidad = Convert.ToString(reader["Localidad"]);
                clientData.idCountry = Convert.ToInt32(reader["Id_Pais"]);
                clientData.country = Convert.ToString(reader["Pais"]);
                int enable = Convert.ToInt16(reader["Habilitado"]);

                if (enable == 1)
                    clientData.enable = true;
                else
                    clientData.enable = false;
            }

            conn.Close();

            return clientData;
        }

        public static Boolean checkTypeAndIdentificationNumber(Int32 clientId, String typeIdentification, Int32 identificationNumber)
        {
            SqlCommand sp_client_check_exist_identification = new SqlCommand();
            sp_client_check_exist_identification.CommandText = "SQL_SERVANT.sp_client_check_exist_identification";
            sp_client_check_exist_identification.Parameters.Add(new SqlParameter("@p_client_id", SqlDbType.Int));
            sp_client_check_exist_identification.Parameters["@p_client_id"].Value = clientId;

            sp_client_check_exist_identification.Parameters.Add(new SqlParameter("@p_client_type_identification", SqlDbType.VarChar, 255));
            sp_client_check_exist_identification.Parameters["@p_client_type_identification"].Value = typeIdentification;

            sp_client_check_exist_identification.Parameters.Add(new SqlParameter("@p_client_identification_number", SqlDbType.Int));
            sp_client_check_exist_identification.Parameters["@p_client_identification_number"].Value = identificationNumber;

            var returnParametersIsValid = sp_client_check_exist_identification.Parameters.Add(new SqlParameter("@p_isValid", SqlDbType.Int));
            returnParametersIsValid.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_client_check_exist_identification, "Chequear tipo y numero de identificacion", false);

            if (Convert.ToInt16(returnParametersIsValid.Value) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Boolean checkMail(Int32 clientId, String mail)
        {
            SqlCommand sp_client_check_exist_mail = new SqlCommand();
            sp_client_check_exist_mail.CommandText = "SQL_SERVANT.sp_client_check_exist_mail";
            sp_client_check_exist_mail.Parameters.Add(new SqlParameter("@p_client_id", SqlDbType.Int));
            sp_client_check_exist_mail.Parameters["@p_client_id"].Value = clientId;

            sp_client_check_exist_mail.Parameters.Add(new SqlParameter("@p_client_mail", SqlDbType.VarChar));
            sp_client_check_exist_mail.Parameters["@p_client_mail"].Value = mail;

            var returnParametersIsValid = sp_client_check_exist_mail.Parameters.Add(new SqlParameter("@p_isValid", SqlDbType.Int));
            returnParametersIsValid.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_client_check_exist_mail, "chequear mail", false);

            if (Convert.ToInt16(returnParametersIsValid.Value) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Int32 save(Cliente clientData, UsuarioDatos userData, Boolean edit)
        {
            SqlCommand sp_save_or_update_client = new SqlCommand();
            sp_save_or_update_client.CommandType = CommandType.StoredProcedure;
            sp_save_or_update_client.CommandText = "SQL_SERVANT.sp_client_save_update";

            var returnParameterClientId = sp_save_or_update_client.Parameters.Add(new SqlParameter("@p_client_id", SqlDbType.Int));
            returnParameterClientId.Direction = ParameterDirection.InputOutput;
            sp_save_or_update_client.Parameters["@p_client_id"].Value = clientData.id;

            sp_save_or_update_client.Parameters.AddWithValue("@p_client_name", clientData.name);
            sp_save_or_update_client.Parameters.AddWithValue("@p_client_lastname", clientData.lastname);
            sp_save_or_update_client.Parameters.AddWithValue("@p_client_type_identification", clientData.typeIdentification);
            sp_save_or_update_client.Parameters.AddWithValue("@p_client_identification_number", clientData.identificationNumber);
            sp_save_or_update_client.Parameters.AddWithValue("@p_client_country", clientData.country);
            sp_save_or_update_client.Parameters.AddWithValue("@p_client_mail", clientData.mail);
            sp_save_or_update_client.Parameters.AddWithValue("@p_client_address_name", clientData.addressName);
            sp_save_or_update_client.Parameters.AddWithValue("@p_client_address_number", clientData.addressNum);

            if (clientData.addressFloor != VarGlobal.NoAddressFloor)
            {
                sp_save_or_update_client.Parameters.AddWithValue("@p_client_address_floor", clientData.addressFloor);
                sp_save_or_update_client.Parameters.AddWithValue("@p_client_address_dept", clientData.adressDeptName);
            }

            sp_save_or_update_client.Parameters.AddWithValue("@p_client_localidad", clientData.localidad);
            sp_save_or_update_client.Parameters.AddWithValue("@p_client_nationality", clientData.nationality);
            sp_save_or_update_client.Parameters.AddWithValue("@p_client_birthdate", clientData.birthdate);

            if (edit)
            {
                userData.username = "";
                userData.password = "";
                var questionAnswer = new PreguntaRespuesta("", "");
                userData.questionAnswer = questionAnswer;
            }
            sp_save_or_update_client.Parameters.AddWithValue("@p_user_username", userData.username);
            sp_save_or_update_client.Parameters.AddWithValue("@p_user_password", userData.password);
            sp_save_or_update_client.Parameters.AddWithValue("@p_user_secret_question", userData.questionAnswer.pregunta);
            sp_save_or_update_client.Parameters.AddWithValue("@p_user_secret_answer", userData.questionAnswer.respuesta);

            ProcedureHelper.execute(sp_save_or_update_client, "save or update client data", false);

            return Convert.ToInt32(returnParameterClientId.Value);
        }

        public static void desvincularTarjeta(Int32 idCliente, Decimal idTarjeta)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_client_tarjeta_disable";

            command.Parameters.Add(new SqlParameter("@p_client_id", SqlDbType.Int));
            command.Parameters["@p_client_id"].Value = idCliente;

            command.Parameters.Add(new SqlParameter("@p_tarjeta_id", SqlDbType.Decimal));
            command.Parameters["@p_tarjeta_id"].Value = idTarjeta;

            ProcedureHelper.execute(command, "Desvincular Tarjeta", false);
        }

        public static Boolean isEnabled(Int32 idCliente)
        {
            SqlCommand sp_client_is_enabled = new SqlCommand();
            sp_client_is_enabled.CommandText = "SQL_SERVANT.sp_client_is_enabled";
            sp_client_is_enabled.Parameters.Add(new SqlParameter("@p_client_id", SqlDbType.Int));
            sp_client_is_enabled.Parameters["@p_client_id"].Value = idCliente;

            var returnParametersIsEnabled = sp_client_is_enabled.Parameters.Add(new SqlParameter("@p_isEnabled", SqlDbType.Int));
            returnParametersIsEnabled.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_client_is_enabled, "chequear estado cliente", false);

            if (Convert.ToInt16(returnParametersIsEnabled.Value) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void getClientIdByUserId()
        {
            SqlCommand sp_client_get_by_user = new SqlCommand();
            sp_client_get_by_user.CommandText = "SQL_SERVANT.sp_client_get_by_user";
            sp_client_get_by_user.Parameters.Add(new SqlParameter("@p_user_id", SqlDbType.VarChar));
            sp_client_get_by_user.Parameters["@p_user_id"].Value = VarGlobal.usuario.id;

            var returnParameter = sp_client_get_by_user.Parameters.Add(new SqlParameter("@p_client_id", SqlDbType.Int));
            returnParameter.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_client_get_by_user, "conocer id cliente", false);

            VarGlobal.usuario.clientId = Convert.ToInt16(returnParameter.Value);

        }

        public static Boolean checkIdentificationIsCorrect(Int32 identificationId)
        {
            SqlCommand sp_client_check_valid_identification = new SqlCommand();
            sp_client_check_valid_identification.CommandText = "SQL_SERVANT.sp_client_nro_identity_is_valid";
            sp_client_check_valid_identification.Parameters.Add(new SqlParameter("@p_client_id", SqlDbType.Int));
            sp_client_check_valid_identification.Parameters["@p_client_id"].Value = VarGlobal.usuario.clientId;

            sp_client_check_valid_identification.Parameters.Add(new SqlParameter("@p_client_identity_id", SqlDbType.Int));
            sp_client_check_valid_identification.Parameters["@p_client_identity_id"].Value = identificationId;

            var returnParametersIsValid = sp_client_check_valid_identification.Parameters.Add(new SqlParameter("@p_is_valid", SqlDbType.Int));
            returnParametersIsValid.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_client_check_valid_identification, "Chequear numero de identificacion", false);

            if (Convert.ToInt16(returnParametersIsValid.Value) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}