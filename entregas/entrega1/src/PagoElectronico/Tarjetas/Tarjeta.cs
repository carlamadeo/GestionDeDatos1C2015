﻿using System;
using System.Windows.Forms;

namespace PagoElectronico.Tarjetas
{
    public class Tarjeta
    {
        public String id { get; set; }
        public string empresa { get; set; }
        public int idEmpresa { get; set; }
        public DateTime fechaEmision { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int codSeguridad { get; set; }

        public static void fillTarjetasByClient(ComboBox comboBoxTarjetas, Int32 idClient)
        {
            ComboBoxHelper.fill(comboBoxTarjetas, "SQL_SERVANT.Cliente_Tarjeta ct",
                "CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', ct.Id_Tarjeta))", "CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', ct.Id_Tarjeta))", "ct.Id_Cliente = '" + idClient + "' AND ct.Habilitada = 1", null);
        }

        public static void fillTarjetasByClientWhithout4LastDigits(ComboBox comboBoxTarjetas, Int32 idClient)
        {
            ComboBoxHelper.fillWithoutLast4Digits(comboBoxTarjetas, "SQL_SERVANT.Cliente_Tarjeta ct",
                "CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', ct.Id_Tarjeta))", "CONVERT(varchar(50), DecryptByPassphrase ('SQL SERVANT', ct.Id_Tarjeta))", "ct.Id_Cliente = '" + idClient + "' AND ct.Habilitada = 1", null);
        }

        public static string tc4UltimosDigitos(string unString)
        {
            char[] splitString;
            string cenpw = null;
            int NtoShow;

            splitString = new char[unString.Length];
            splitString = unString.ToCharArray();
            NtoShow = 4;
            for (int i = 0; i < unString.Length; i++)
            {
                if (i < unString.Length - NtoShow)
                    cenpw += "*";
                else
                    cenpw += splitString[i];
            }

            return cenpw;
        }

    }
}
