using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INF.Web.Data
{
    public static class Extensions
    {
        public static string WithEncrypt(this string source)
        {
            return CryptoUtility.EncryptText(source);
        }

        public static string WithDecrypt(this string source)
        {
            return CryptoUtility.DecryptText(source);
        }
    }
}
