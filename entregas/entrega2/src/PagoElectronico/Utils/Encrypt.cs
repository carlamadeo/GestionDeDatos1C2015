using System.Text;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Collections.Generic;
namespace PagoElectronico
{
    public class Encrypt
    {
        public static string Sha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}