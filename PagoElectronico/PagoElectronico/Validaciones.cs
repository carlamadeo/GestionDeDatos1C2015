using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PagoElectronico
{
    public class Validaciones
    {

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
        public static Boolean requiredString(string value, string error)
        {
            if (value != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static Boolean validAndRequiredMail(string value, string error)
        {
            Regex objValidMail = new Regex(@"(@)(\w)+(\.)([\w])+$");

            if (objValidMail.IsMatch(value))
                return true;
            else
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
