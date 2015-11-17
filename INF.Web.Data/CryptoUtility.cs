using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace INF.Web.Data
{
    public static class CryptoUtility
    {
        /// <summary>
        /// Generate the MD5(Message-Digest Algorithm 5) hash for given text  and returns
        /// the hased values in the Base64 format. MD5 is a one way hasing algorithm
        /// and could be used to generate hash for password storage in the database.
        /// Read more about MD5 at: http://www.faqs.org/rfcs/rfc1321.html
        /// An-other good article is: http://www.unixwiz.net/techtips/iguide-crypto-hashes.html
        /// </summary>
        /// <param name="sourceMessage">Source String for which to generate Hash</param>
        /// <returns>Hash Value</returns>
        public static string GenerateMD5Hash(string sourceMessage)
        {
            var encoder = new UnicodeEncoding();
            Byte[] hashedText = null;
            var md5Hasher = new MD5CryptoServiceProvider();

            //Get the Byte array for the given text and then generate the MD5 hash for it.
            hashedText = md5Hasher.ComputeHash(encoder.GetBytes(sourceMessage));

            // Return back the hashed string as Base64 string
            return Convert.ToBase64String(hashedText);
        }

        /// <summary>
        /// Generate the SH1(Secure Hash-1) Hash for given text and returns
        /// the hased values in the Base64 format. SH1 is a one way hasing algorithm
        /// and could be used to generate hash for password storage in the database.
        /// An-other good article is: http://www.unixwiz.net/techtips/iguide-crypto-hashes.html
        /// </summary>
        /// <param name="sourceMessage">Source String for which to generate Hash</param>
        /// <returns>Hash Value</returns>
        public static string GenerateSH1Hash(string sourceMessage)
        {
            var encoder = new UnicodeEncoding();
            Byte[] hashedText = null;
            var sh1Hasher = new SHA1CryptoServiceProvider();

            //Get the Byte array for the given text and then generate the SH1 hash for it.
            hashedText = sh1Hasher.ComputeHash(encoder.GetBytes(sourceMessage));

            // Return back the hashed string as Base64 string
            return Convert.ToBase64String(hashedText);
        }

        /// <summary>
        /// An overloaded version of the EncryptText(string sourceMessage, string Key) function.
        /// This is used to encrypte values with an internal/default key '@RoyalGrill#'.
        /// </summary>
        /// <param name="sourceMessage">Source String to encrypt</param>
        /// <returns>Encrypted Value</returns>
        public static string EncryptText(string sourceMessage)
        {
            String result = EncryptText(sourceMessage, "rOyalGriLl^");
            result = result.Replace("+", "!~");
            return result;
        }

        /// <summary>
        /// Encrypt the given text using the *rOyalGriLl^ as the key and then returns
        /// the encrypted value as in the Base64 message. DES (Data Encryption Standard) alogrithm is used to encrypt 
        /// the password.
        /// </summary>
        /// <param name="sourceMessage">Source String to encrypt</param>
        /// <param name="key">Key used to encrypt value</param>
        /// <returns>Encrypted Value</returns>
        public static string EncryptText(string sourceMessage, string key)
        {
            // Most of this code portion is taken from MSDN website:
            // http://msdn2.microsoft.com/en-us/library/system.security.cryptography.descryptoserviceprovider.aspx

            // Create the DES encryptor object
            var encryptor = new DESCryptoServiceProvider();

            int keysize = encryptor.KeySize;

            // Set the encryption key. This same key must be used to decrypt the value
            byte[] encryptionKey = new byte[8];

            for (int iCounter = 0; iCounter <= encryptionKey.Length - 1; iCounter++)
            {
                encryptionKey[iCounter] = (key.Length > iCounter) ? Convert.ToByte(key[iCounter]) : Convert.ToByte(0);
            }
            encryptor.Key = encryptionKey;
            encryptor.IV = (new UnicodeEncoding()).GetBytes("AXUY");

            // Create a memory stream.
            var memStream = new MemoryStream();

            // Create a CryptoStream using the memory stream and the CSP DES Encrytpro.  
            var encStream = new CryptoStream(memStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write);

            var streamWriter = new StreamWriter(encStream);
            // Create a StreamWriter to write a string to the stream.
            // Now writing the source message on the encryptor stream will actually encrypt the message
            streamWriter.WriteLine(sourceMessage);

            // Close the StreamWriter and CryptoStream. This will flush any results to the target objects.
            streamWriter.Close();
            encStream.Close();

            byte[] buffer = memStream.ToArray();
            // Get an array of bytes that represents the memory stream.
            memStream.Close();
            // Finally Close the memory stream.
            // Return back the hashed string as Base64 string
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// An overloaded version of the DecryptText(string CypherText, string Key) function.
        /// This is used to encrypte values with an internal/default key 'bENCHIT^'.
        /// </summary>
        /// <param name="cypherText">Encrypted or Cypher Text</param>
        /// <returns>Orignal Text</returns>
        public static string DecryptText(string cypherText)
        {
            cypherText = cypherText.Replace("!~", "+");
            return DecryptText(cypherText, "rOyalGriLl^");
        }

        /// <summary>
        /// This method decrypts the text orignally encrypted by the above method. This methods uses the DES
        /// algorithm and as "@bm~^" as the key. Please note that this method decrypt the strings
        /// orignally encrypted by the above method.
        /// </summary>
        /// <param name="cypherText">Encrypted or Cypher Text</param>
        /// <param name="Key">Key used to encrypt value</param>
        /// <returns>Orignal Text</returns>
        public static string DecryptText(string cypherText, string Key)
        {
            // Most of this code portion is taken from MSDN website:
            // http://msdn2.microsoft.com/en-us/library/system.security.cryptography.descryptoserviceprovider.aspx

            // Create the DES encryptor object
            var encryptor = new DESCryptoServiceProvider();

            // Set the encryption key. This same key must be used to decrypt the value
            //(New UnicodeEncoding).GetBytes(stringToConvert)

            // Set the encryption key. This same key must be used to decrypt the value
            var encryptionKey = new byte[8];

            for (int iCounter = 0; iCounter <= encryptionKey.Length - 1; iCounter++)
            {
                encryptionKey[iCounter] = (Key.Length > iCounter) ? Convert.ToByte(Key[iCounter]) : Convert.ToByte(0);
            }
            encryptor.Key = encryptionKey;
            encryptor.IV = (new UnicodeEncoding()).GetBytes("AXUY");

            // Create a memory stream and then add the cipher text as Byte array into it.
            var memStream = new MemoryStream(Convert.FromBase64String(cypherText));

            // Create a CryptoStream using the memory stream and the CSP DES key. 
            var encStream = new CryptoStream(memStream, encryptor.CreateDecryptor(), CryptoStreamMode.Read);

            // Create a StreamReader for reading the stream.
            var streamReader = new StreamReader(encStream);

            // Read the stream as a string. This will decrypt the text stored in the memory stream.
            string decryptedText = streamReader.ReadLine();

            // Close the streams. Steams are closed in the reverse order of creation process.
            streamReader.Close();
            encStream.Close();
            memStream.Close();

            return decryptedText;
        }
    }
}
