using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Properties;
using System.Data.SqlClient;

namespace PagoElectronico.DB
{
    public class Connection
    {
        private static string getStringConnection()
        {
            return Settings.Default.connectionString;
        }

        private static SqlConnection create()
        {
            return new SqlConnection(getStringConnection());
        }

        private static SqlConnection open(SqlConnection conn)
        {
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return conn;
        }

        public static void close(SqlConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static SqlConnection getConnection()
        {
            return open(create());
        }
    }
}
