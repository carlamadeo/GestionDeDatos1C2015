using System.Windows.Forms;
namespace PagoElectronico
{
    public class CheckBoxHelper
    {
        public static void clean(Control parent)
        {
            TextBox t;
            foreach (Control c in parent.Controls)
            {
                t = c as TextBox;
                if (t != null)
                {
                    t.Clear();
                }
                if (c.Controls.Count > 0)
                {
                    clean(c);
                }
            }
        }
    }
}