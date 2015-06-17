using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Menu
{
    public class MenuValidateHelper
    {
        static Dictionary<Control, ErrorProvider> dictionaryErrorProviders = new Dictionary<Control, ErrorProvider>();

        static public Boolean validate(Control cntr, Form parentForm)
        {
            Boolean isValid = true;

            foreach (Control c in cntr.Controls)
            {
                if (c is TabControl)
                    validate((TabControl)c, parentForm);
                else if (c is TabPage)
                    validate((TabPage)c, parentForm);
                else if (c is Panel)
                    validate((Panel)c, parentForm);
                else if (c is GroupBox)
                    validate((GroupBox)c, parentForm);

                c.Focus();

                if (!parentForm.Validate())
                    isValid = false;
            }
            return isValid;
        }

        static public void setValidation(Control cntr, Boolean valido)
        {
            foreach (Control c in cntr.Controls)
            {
                if (c is TabControl)
                    setValidation((TabControl)c, valido);
                else if (c is TabPage)
                    setValidation((TabPage)c, valido);
                else if (c is Panel)
                    setValidation((Panel)c, valido);
                else if (c is GroupBox)
                    setValidation((GroupBox)c, valido);

                c.CausesValidation = valido;
            }
        }
    }
}
