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

        public static DateTime truncate(DateTime date)
        {
            date = date.AddHours(-date.Hour);
            date = date.AddMinutes(-date.Minute);
            date = date.AddSeconds(-date.Second);
            date = date.AddMilliseconds(-date.Millisecond);
            return date;
        }

        public static DateTime firstMonthDay(DateTime date)
        {
            date = date.AddDays(1 - date.Day);
            return truncate(date);
        }

        public static DateTime nextMonthFirstDay(DateTime date)
        {
            DateTime actual = date;
            date = date.AddMonths(1);
            date = date.AddDays(1 - date.Day);
            date = date.AddYears(date.Year - actual.Year);
            return truncate(date);
        }

        public static DateTime getToday()
        {
            return DateTime.ParseExact(Properties.Settings.Default.fechaSistema, Properties.Settings.Default.formatoFecha, null);
        }
    }
}