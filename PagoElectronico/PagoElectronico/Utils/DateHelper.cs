using System;
namespace PagoElectronico
{
    public class DateHelper
    {
        public static DateTime convertirFecha(string dia, string mes, string anio)
        {
            DateTime fecha = new DateTime(Convert.ToInt32(anio), Convert.ToInt32(mes), Convert.ToInt32(dia), 00, 00, 00, 000);
            return fecha;
        }

        public static void truncate(DateTime date)
        {
            date.AddHours(0 - date.Hour);
            date.AddMinutes(0 - date.Minute);
            date.AddSeconds(0 - date.Second);
            date.AddMilliseconds(0 - date.Millisecond);
        }

        public static DateTime getToday()
        {
            return DateTime.ParseExact(Properties.Settings.Default.fechaSistema, Properties.Settings.Default.formatoFecha, null);
        }
    }
}