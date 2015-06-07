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
        public static Decimal getImporteMaximo(Decimal cuenta)
        {
            SqlCommand sp_get_importe_maximo_por_cuenta = new SqlCommand();
            sp_get_importe_maximo_por_cuenta.CommandText = "SQL_SERVANT.sp_get_importe_maximo_por_cuenta";
            sp_get_importe_maximo_por_cuenta.Parameters.Add(new SqlParameter("@p_cuenta_id", SqlDbType.BigInt));
            sp_get_importe_maximo_por_cuenta.Parameters["@p_cuenta_id"].Value = cuenta;

            SqlParameter returnParametersImporteMaximo = new SqlParameter("@p_importe_maximo", SqlDbType.Decimal);
            returnParametersImporteMaximo.Precision = 18;
            returnParametersImporteMaximo.Scale = 2;
            sp_get_importe_maximo_por_cuenta.Parameters.Add(returnParametersImporteMaximo);
            returnParametersImporteMaximo.Direction = ParameterDirection.InputOutput;
            

            ProcedureHelper.execute(sp_get_importe_maximo_por_cuenta, "chequear importe maximo", false);

            return Convert.ToDecimal(returnParametersImporteMaximo.Value);

        }
    }
}
