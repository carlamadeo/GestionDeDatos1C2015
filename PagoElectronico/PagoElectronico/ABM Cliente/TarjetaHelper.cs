using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.DB;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    public class TarjetaHelper
    {
        public static Tarjeta getClientTarjetaData(Int32 clientId, Decimal tarjetaId)
        {
            Tarjeta tarjetaData = new Tarjeta();

            SqlConnection conn = Connection.getConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SQL_SERVANT.sp_client_tarjeta_get_by_id_client";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@p_id_client", clientId);
            command.Parameters.AddWithValue("@p_id_tarjeta", tarjetaId);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                tarjetaData.id = Convert.ToDecimal(tarjetaId);
                tarjetaData.empresa = Convert.ToString(reader["Empresa"]);
                tarjetaData.fechaEmision = Convert.ToDateTime(reader["Fecha_Emision"]);
                tarjetaData.fechaVencimiento = Convert.ToDateTime(reader["Fecha_Vencimiento"]);
                tarjetaData.codSeguridad = Convert.ToInt32(reader["Codigo_Seguridad"]);
            }

            conn.Close();

            return tarjetaData;
        }

        internal static void save(Tarjeta tarjeta)
        {
            SqlCommand sp_save_tarjeta = new SqlCommand();
            sp_save_tarjeta.CommandType = CommandType.StoredProcedure;
            sp_save_tarjeta.CommandText = "SQL_SERVANT.sp_tarjeta_save";

            var returnParameterTarjetaId = sp_save_tarjeta.Parameters.Add(new SqlParameter("@p_tarjeta_id", SqlDbType.Decimal));
            returnParameterTarjetaId.Direction = ParameterDirection.InputOutput;

            sp_save_tarjeta.Parameters["@p_tarjeta_id"].Value = tarjeta.id;

            sp_save_tarjeta.Parameters.AddWithValue("@p_tarjeta_empresa", tarjeta.empresa);
            sp_save_tarjeta.Parameters.AddWithValue("@p_tarjeta_fecha_vencimiento", tarjeta.fechaVencimiento);
            sp_save_tarjeta.Parameters.AddWithValue("@p_tarjeta_fecha_emision", tarjeta.fechaEmision);
            sp_save_tarjeta.Parameters.AddWithValue("@p_tarjeta_codigo_seguridad", tarjeta.codSeguridad);

            ProcedureHelper.execute(sp_save_tarjeta, "save tarjeta data", false);
        }

        public static void getClientCard(Int16 idClient, DataGridView dgv)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_card_by_client_id";

            command.Parameters.Add(new SqlParameter("@p_card_client_id", SqlDbType.Int));
            command.Parameters["@p_card_client_id"].Value = idClient;

            DataGridViewHelper.fill(command, dgv);
        }

        public static void associate(String id, Int16 clientId, Boolean associate)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SQL_SERVANT.sp_card_associate";

            command.Parameters.Add(new SqlParameter("@p_card_id", SqlDbType.VarChar, 16));
            command.Parameters["@p_card_id"].Value = id;

            command.Parameters.Add(new SqlParameter("@p_card_client_id", SqlDbType.Int));
            command.Parameters["@p_card_client_id"].Value = clientId;

            command.Parameters.Add(new SqlParameter("@p_card_associate", SqlDbType.Bit));
            if (associate)
                command.Parameters["@p_card_associate"].Value = 1;
            else
                command.Parameters["@p_card_associate"].Value = 0;

            ProcedureHelper.execute(command, "Se asocio/desasocio la tarjeta", false);
        }

        public static Tarjeta getData(String id)
        {
            Tarjeta card = new Tarjeta();

            SqlConnection conn = Connection.getConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SQL_SERVANT.sp_card_get_data";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@p_card_id", id);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                card.id = Convert.ToDecimal(reader["Id_Tarjeta"]);
                card.empresa = Convert.ToString(reader["Tarjeta_Descripcion"]);
                card.idEmpresa = Convert.ToInt16(reader["Id_Tarjeta_Empresa"]);
                card.fechaEmision = Convert.ToDateTime(reader["Fecha_Emision"]);
                card.fechaVencimiento = Convert.ToDateTime(reader["Fecha_Vencimiento"]);
                card.codSeguridad = Convert.ToInt32(reader["Codigo_Seguridad"]);
            }

            conn.Close();

            return card;
        }
    }
}
