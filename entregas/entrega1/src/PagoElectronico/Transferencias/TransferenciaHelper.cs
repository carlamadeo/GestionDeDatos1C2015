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
        public static Decimal getImporteMaximo(Decimal cuenta, int tipoCuenta)
        {
            SqlCommand sp_get_importe_maximo_por_cuenta = new SqlCommand();
            sp_get_importe_maximo_por_cuenta.CommandText = "SQL_SERVANT.sp_get_importe_maximo_por_cuenta";
            sp_get_importe_maximo_por_cuenta.Parameters.Add(new SqlParameter("@p_cuenta_id", SqlDbType.BigInt));
            sp_get_importe_maximo_por_cuenta.Parameters["@p_cuenta_id"].Value = cuenta;
            sp_get_importe_maximo_por_cuenta.Parameters.Add(new SqlParameter("@p_cuenta_propia", SqlDbType.Int));
            sp_get_importe_maximo_por_cuenta.Parameters["@p_cuenta_propia"].Value = tipoCuenta;

            SqlParameter returnParametersImporteMaximo = new SqlParameter("@p_importe_maximo", SqlDbType.Decimal);
            returnParametersImporteMaximo.Precision = 18;
            returnParametersImporteMaximo.Scale = 2;
            sp_get_importe_maximo_por_cuenta.Parameters.Add(returnParametersImporteMaximo);
            returnParametersImporteMaximo.Direction = ParameterDirection.InputOutput;
            

            ProcedureHelper.execute(sp_get_importe_maximo_por_cuenta, "chequear monto maximo", false);

            return Convert.ToDecimal(returnParametersImporteMaximo.Value);

        }

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
