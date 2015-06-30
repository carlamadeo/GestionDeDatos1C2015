using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.DB;
using System.Windows.Forms;

namespace PagoElectronico.Transferencias
{
    class TransferenciaHelper
    {
        public static void saveTransferencia(Transferencia transferencia)
        {
            SqlCommand sp_save_transferencia = new SqlCommand();
            sp_save_transferencia.CommandType = CommandType.StoredProcedure;
            sp_save_transferencia.CommandText = "SQL_SERVANT.sp_save_transferencia";

            sp_save_transferencia.Parameters.AddWithValue("@p_transferencia_origen", transferencia.cuentaOrigen);
            sp_save_transferencia.Parameters.AddWithValue("@p_transferencia_destino", transferencia.cuentaDestino);
            sp_save_transferencia.Parameters.AddWithValue("@p_transferencia_monto", transferencia.monto);
            sp_save_transferencia.Parameters.AddWithValue("@p_transferencia_moneda", transferencia.moneda);
            sp_save_transferencia.Parameters.AddWithValue("@p_tranferencia_fecha", transferencia.fecha);
            sp_save_transferencia.Parameters.AddWithValue("@p_tranferencia_mismo_cliente", transferencia.mismoCliente);

            ProcedureHelper.execute(sp_save_transferencia, "save transferencia", false);
        }
    }
}
