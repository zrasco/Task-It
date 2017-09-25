using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TodoAdminSite.Utilities
{
    public static class SHA256
    {
        // Obatained from: https://stackoverflow.com/questions/12416249/hashing-a-string-with-sha256
        // Author: Nico Dumdum & Quuxplusone
        static string MakeIt(string randomString)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString), 0, Encoding.UTF8.GetByteCount(randomString));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}