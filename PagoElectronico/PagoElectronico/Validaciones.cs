﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using PagoElectronico.DB;

namespace PagoElectronico
{
    public class Validaciones
    {

        public static Boolean validAndRequiredDecimalMoreThan0(string value, string error)
        {
            Decimal aux;
            if (Decimal.TryParse(value, out aux) && value != "" && aux > 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static Boolean validAndRequiredInt32(string value, string error)
        {
            Int32 aux;
            if (Int32.TryParse(value, out aux) && value != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static Boolean validInt32(string value, string error)
        {
            Int32 aux;
            if (Int32.TryParse(value, out aux))
            {
                return true;
            }
            else
            {
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static Boolean validAndRequiredInt32MoreThan0(string value, string error)
        {
            Int32 aux;
            if (Int32.TryParse(value, out aux) && value != "" && aux > 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static Boolean validAndRequiredInt32LessThan13(string value, string error)
        {
            Int32 aux;
            if (Int32.TryParse(value, out aux) && value != "" && aux < 13)
            {
                return true;
            }
            else
            {
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static Boolean validAndRequiredDoubleMoreThan0(string value, string error)
        {
            Double aux;
            if (Double.TryParse(value, out aux) && value != "" && aux > 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static Boolean validAndRequiredInt32MoreThanEqual0(string value, string error)
        {
            Int32 aux;
            if (Int32.TryParse(value, out aux) && value != "" && aux >= 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static Boolean condition(Boolean value, string error)
        {
            if (!value)
            {
                MessageBox.Show(error, "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return value;
        }

        public static Boolean requiredString(string value, string error)
        {
            if (value != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(error, "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static Boolean validAndRequiredString(string value, string error)
        {
            Regex objValidString = new Regex(@"[^a-zA-Záéíóú\s]");

            if (!objValidString.IsMatch(value))
                return true;
            else
                MessageBox.Show(error, "Datos erroneos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static Boolean validAndRequiredMail(string value, string error)
        {
            Regex objValidMail = new Regex(@"(@)(\w)+(\.)([\w])+$");

            if (objValidMail.IsMatch(value))
                return true;
            else
                MessageBox.Show(error, "Mail invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static Boolean differentValues(string value1, string value2, string error)
        {
            if (value1.Equals(value2))
            {
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Boolean tarjetaNoVencida(String tarjeta)
        {
            SqlCommand sp_tarjeta_not_expired = new SqlCommand();
            sp_tarjeta_not_expired.CommandText = "SQL_SERVANT.sp_tarjeta_not_expired";
            sp_tarjeta_not_expired.Parameters.Add(new SqlParameter("@p_tarjeta_id", SqlDbType.VarChar));
            sp_tarjeta_not_expired.Parameters["@p_tarjeta_id"].Value = tarjeta;

            sp_tarjeta_not_expired.Parameters.Add(new SqlParameter("@p_today", SqlDbType.DateTime));
            sp_tarjeta_not_expired.Parameters["@p_today"].Value = DateHelper.getToday();

            var returnParametersIsEnabled = sp_tarjeta_not_expired.Parameters.Add(new SqlParameter("@p_notExpired", SqlDbType.Int));
            returnParametersIsEnabled.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_tarjeta_not_expired, "chequear tarjeta no vencida", false);

            if (Convert.ToInt16(returnParametersIsEnabled.Value) == 0)
            {
                MessageBox.Show("La tarjeta seleccionada se encuentra vencida o la fecha de emision es posterior a la actual", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Boolean fecha1EsPosteriorAFecha2(DateTime fecha1, DateTime fecha2, string error)
        {
            int esAnterior = DateTime.Compare(fecha1, fecha2);
            if (esAnterior < 1)
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }
    }
}
