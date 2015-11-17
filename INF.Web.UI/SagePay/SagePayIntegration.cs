using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.VisualBasic;

namespace INF.Web.UI.SagePay
{
    //public class SagePayIntegration
    //{
    //    //** Set to SIMULATOR for the Sage Pay Simulator expert system, TEST for the Test Server and LIVE in the live environment **
    //    //Public Shared StrConnectTo As String = "SIMULATOR"

    //    //** Change if you've created a Virtual Directory in IIS with a different name **
    //    //Public Shared StrVirtualDir As String = "SagePayFormKit"

    //    //** IMPORTANT.  Set the strYourSiteFQDN value to the Fully Qualified Domain Name of your server. **
    //    //** This should start http:// or https:// and should be the name by which our servers can call back to yours **
    //    //** i.e. it MUST be resolvable externally, and have access granted to the Sage Pay servers **
    //    //** examples would be https://www.mysite.com or http://212.111.32.22/ **
    //    //** NOTE: You should leave the final / in place. **
    //    //Public Shared StrYourSiteFQDN As String = "http://local.royalgrill.co.uk/"

    //    //** Set this value to the Vendor Name assigned to you by Sage Pay or chosen when you applied **
    //    //Public Shared StrVendorName As String = "infinityepos"

    //    // ** Set this value to the XOR Encryption password assigned to you by Sage Pay **
    //    //Public Shared StrEncryptionPassword As String = "JIqoENoDbrGP4kyM"

    //    // Test Encryption Password: GTkc35tGj7BALCMj
    //    //Public Shared StrEncryptionPassword As String = "GTkc35tGj7BALCMj"

    //    // Live Encryption Password: sd76QGrsteJAxBiD
    //    //Public Shared StrEncryptionPassword As String = "sd76QGrsteJAxBiD"

    //    //** Set this to indicate the currency in which you wish to trade. You will need a merchant number in this currency **

    //    public static string StrCurrency = "GBP";
    //    // ** Set this to the mail address which will receive order confirmations and failures **

    //    public static string StrVendorEMail = "lekongbien@gmail.com";
    //    //** This can be DEFERRED or AUTHENTICATE if your Sage Pay account supports those payment types **

    //    public static string StrTransactionType = "PAYMENT";
    //    //** Optional setting. If you are a Sage Pay Partner and wish to flag the transactions with your unique partner id set it here.

    //    public static string StrPartnerID = "";
    //    //** Optional setting. 0 = Do not send either customer or vendor e-mails, 1 = Send customer and vendor e-mails if address(es) are provided(DEFAULT). 
    //    //** 2 = Send Vendor Email but not Customer Email. If you do not supply this field, 1 is assumed and e-mails are sent if addresses are provided.

    //    public static int ISendEMail = 2;
    //    //** Encryption type should be left set to AES unless you are experiencing problems and have been told by SagePay support to change it - XOR is the only other acceptable value **

    //    public static string StrEncryptionType = "AES";

    //    //**************************************************************************************************
    //    // Global Definitions for this site
    //    //**************************************************************************************************
    //    public static string StrProtocol = "2.23";
    //    public static string[] ArrBase64EncMap = new string[65];
    //    public static int[] ArrBase64DecMap = new int[128];
    //    public static string[,] ArrProducts = new string[4, 3];
    //    public static string StrNewLine = "<P>" + Constants.vbCrLf;
    //    public const string BASE_64_MAP_INIT = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="vStrConnectTo"></param>
    //    /// <returns></returns>
    //    public static string SystemUrl(string vStrConnectTo)
    //    {
    //        switch (vStrConnectTo)
    //        {
    //            case "LIVE":
    //                return "https://live.sagepay.com/gateway/service/vspform-register.vsp";
    //            case "TEST":
    //                return "https://test.sagepay.com/gateway/service/vspform-register.vsp";
    //            case "SIMULATOR":
    //                return "https://test.sagepay.com/Simulator/VSPFormGateway.asp";
    //            default:
    //                return "https://test.sagepay.com/showpost/showpost.asp";
                    
    //        }
    //    }

    //    /// <summary>
    //    /// Filters unwanted characters out of an input string based on type.  Useful for tidying up FORM field inputs
    //    /// </summary>
    //    /// <param name="strRawText"></param>
    //    /// <param name="filterType"></param>
    //    /// <returns></returns>
    //    public static string CleanInput(string strRawText, CleanInputFilterType filterType = CleanInputFilterType.WidestAllowableCharacterRange)
    //    {
    //        string strAllowableChars = null;
    //        bool bAllowAccentedChars = false;
    //        string strCleaned = "";

    //        if (filterType == CleanInputFilterType.Alphabetic | filterType == CleanInputFilterType.AlphabeticAndAccented)
    //        {
    //            strAllowableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz";
    //            if (filterType == CleanInputFilterType.AlphabeticAndAccented)
    //                bAllowAccentedChars = true;
    //            strCleaned = CleanInput(strRawText, strAllowableChars, bAllowAccentedChars);
    //        }
    //        else if (filterType == CleanInputFilterType.AlphaNumeric | filterType == CleanInputFilterType.AlphaNumericAndAccented)
    //        {
    //            strAllowableChars = "0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    //            if (filterType == CleanInputFilterType.AlphaNumericAndAccented)
    //                bAllowAccentedChars = true;
    //            strCleaned = CleanInput(strRawText, strAllowableChars, bAllowAccentedChars);

    //        }
    //        else if (filterType == CleanInputFilterType.Numeric)
    //        {
    //            strAllowableChars = "0123456789 .,";
    //            strCleaned = CleanInput(strRawText, strAllowableChars, false);
    //            // WidestAllowableCharacterRange
    //        }
    //        else
    //        {
    //            strAllowableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 .,'/\\{}@():?-_&£$=%~*+\"" + Constants.vbCrLf;
    //            strCleaned = CleanInput(strRawText, strAllowableChars, true);
    //        }

    //        return strCleaned;
    //    }

    //    /// <summary>
    //    /// Filters unwanted characters out of an input string based on an allowable character set.  Useful for tidying up FORM field inputs
    //    /// </summary>
    //    /// <param name="strRawText"></param>
    //    /// <param name="strAllowableChars"></param>
    //    /// <param name="blnAllowAccentedChars"></param>
    //    /// <returns></returns>
    //    public static string CleanInput(string strRawText, string strAllowableChars, bool blnAllowAccentedChars)
    //    {
    //        int iCharPos = 1;
    //        string chrThisChar = "";
    //        string strCleanedText = "";

    //        //Compare each character based on list of acceptable characters
    //        while (iCharPos <= Strings.Len(strRawText))
    //        {
    //            //** Only include valid characters **
    //            chrThisChar = Strings.Mid(strRawText, iCharPos, 1);

    //            if (Strings.InStr(strAllowableChars, chrThisChar) != 0)
    //            {
    //                strCleanedText = strCleanedText + chrThisChar;
    //            }
    //            else if (blnAllowAccentedChars)
    //            {
    //                //** Allow accented characters and most high order bit chars which are harmless **
    //                if (Strings.Asc(chrThisChar) >= 191)
    //                    strCleanedText = strCleanedText + chrThisChar;
    //            }

    //            iCharPos = iCharPos + 1;
    //        }

    //        return strCleanedText;
    //    }

    //    //** Counts the number of : in a string.  Used to validate the basket fields
    //    public int CountColons(string strSource)
    //    {

    //        int iNumCol = 0;
    //        int iCharPos = 0;

    //        if (string.IsNullOrEmpty(strSource))
    //        {
    //            iNumCol = 0;
    //        }
    //        else
    //        {
    //            iCharPos = 1;
    //            iNumCol = 0;
    //            while (iCharPos != 0)
    //            {
    //                iCharPos = Strings.InStr(iCharPos + 1, strSource, ":");
    //                if (iCharPos != 0)
    //                    iNumCol = iNumCol + 1;
    //            }

    //        }

    //        return iNumCol;

    //    }

    //    //** Checks to ensure a Basket Field is correctly formatted **
    //    public bool ValidateBasket(string strThisBasket)
    //    {

    //        string iRows = null;
    //        bool bolValid = false;

    //        if (Strings.Len(strThisBasket) > 0 & (Strings.InStr(strThisBasket, ":") != 0))
    //        {
    //            iRows = Strings.Left(strThisBasket, Strings.InStr(strThisBasket, ":") - 1);
    //            if (Information.IsNumeric(iRows))
    //            {
    //                //iRows = Convert.ToInt32(iRows);
    //                int rowCount = Convert.ToInt32(iRows);
    //                if (CountColons(strThisBasket) == ((rowCount * 5) + rowCount))
    //                    bolValid = true;
    //            }
    //        }

    //        return bolValid;

    //    }

    //    //** ASP has no inbuild URLDecode function, so here's on in case we need it **
    //    public static string UrlDecode(string strString)
    //    {

    //        int lngPos = 0;
    //        string strUrlDecode = "";

    //        for (lngPos = 1; lngPos <= Strings.Len(strString); lngPos++)
    //        {
    //            if (Strings.Mid(strString, lngPos, 1) == "%")
    //            {
    //                strUrlDecode = strUrlDecode + Strings.Chr(Convert.ToInt32("&H" + Strings.Mid(strString, lngPos + 1, 2)));
    //                lngPos = lngPos + 2;
    //            }
    //            else if (Strings.Mid(strString, lngPos, 1) == "+")
    //            {
    //                strUrlDecode = strUrlDecode + " ";
    //            }
    //            else
    //            {
    //                strUrlDecode = strUrlDecode + Strings.Mid(strString, lngPos, 1);
    //            }
    //        }

    //        return strUrlDecode;

    //    }

    //    //** There is a URLEncode function, but wrap it up so keep the code clean **
    //    public static string UrlEncode(string strString)
    //    {

    //        return HttpUtility.UrlEncode(strString);

    //    }

    //    //** Base 64 encoding routine.  Used to ensure the encrypted "crypt" field **
    //    //** can be safely transmitted over HTTP **
    //    public static string Base64Encode(string strPlain)
    //    {
    //        string functionReturnValue = null;
    //        int iLoop = 0;
    //        int iBy3 = 0;
    //        string strReturn = null;
    //        int iIndex = 0;
    //        int iFirst = 0;
    //        int iSecond = 0;
    //        int iiThird = 0;
    //        if (strPlain.Length == 0)
    //        {
    //            functionReturnValue = "";
    //            return functionReturnValue;
    //        }

    //        //** Set up Base64 Encoding and Decoding Maps for when we need them ** 
    //        for (iLoop = 0; iLoop <= Strings.Len(BASE_64_MAP_INIT) - 1; iLoop++)
    //        {
    //            ArrBase64EncMap[iLoop] = Strings.Mid(BASE_64_MAP_INIT, iLoop + 1, 1);
    //        }
    //        for (iLoop = 0; iLoop <= Strings.Len(BASE_64_MAP_INIT) - 1; iLoop++)
    //        {
    //            ArrBase64DecMap[Strings.Asc(ArrBase64EncMap[iLoop])] = iLoop;
    //        }

    //        //** Work out rounded down multiple of 3 bytes length for the unencoded text **
    //        iBy3 = (strPlain.Length / 3) * 3;
    //        strReturn = "";

    //        //** For each 3x8 byte chars, covert them to 4x6 byte representations in the Base64 map **
    //        iIndex = 1;
    //        while (iIndex <= iBy3)
    //        {
    //            iFirst = Strings.Asc(Strings.Mid(strPlain, iIndex + 0, 1));
    //            iSecond = Strings.Asc(Strings.Mid(strPlain, iIndex + 1, 1));
    //            iiThird = Strings.Asc(Strings.Mid(strPlain, iIndex + 2, 1));
    //            strReturn = strReturn + ArrBase64EncMap[(iFirst / 4) & 63];
    //            strReturn = strReturn + ArrBase64EncMap[((iFirst * 16) & 48) + ((iSecond / 16) & 15)];
    //            strReturn = strReturn + ArrBase64EncMap[((iSecond * 4) & 60) + ((iiThird / 64) & 3)];
    //            strReturn = strReturn + ArrBase64EncMap[iiThird & 63];
    //            iIndex = iIndex + 3;
    //        }

    //        //** Handle any trailing characters not in groups of 3 **
    //        //** Extend to multiple of 3 characters using = signs as per RFC **
    //        if (iBy3 < strPlain.Length)
    //        {
    //            iFirst = Strings.Asc(Strings.Mid(strPlain, iIndex + 0, 1));
    //            strReturn = strReturn + ArrBase64EncMap[(iFirst / 4) & 63];
    //            if ((strPlain.Length % 3) == 2)
    //            {
    //                iSecond = Strings.Asc(Strings.Mid(strPlain, iIndex + 1, 1));
    //                strReturn = strReturn + ArrBase64EncMap[((iFirst * 16) & 48) + ((iSecond / 16) & 15)];
    //                strReturn = strReturn + ArrBase64EncMap[(iSecond * 4) & 60];
    //            }
    //            else
    //            {
    //                strReturn = strReturn + ArrBase64EncMap[(iFirst * 16) & 48];
    //                strReturn = strReturn + "=";
    //            }
    //            strReturn = strReturn + "=";
    //        }

    //        //** Return the encoded result string **
    //        functionReturnValue = strReturn;
    //        return functionReturnValue;
    //    }
    //    public static string Base64Decode(string strEncoded)
    //    {
    //        string functionReturnValue = null;
    //        int iRealLength = 0;
    //        string strReturn = null;
    //        int iBy4 = 0;
    //        int iIndex = 0;
    //        int iFirst = 0;
    //        int iSecond = 0;
    //        int iThird = 0;
    //        int iFourth = 0;
    //        if (Strings.Len(strEncoded) == 0)
    //        {
    //            functionReturnValue = "";
    //            return functionReturnValue;
    //        }

    //        //** Base 64 encoded strings are right padded to 3 character multiples using = signs **
    //        //** Work out the actual length of data without the padding here **
    //        iRealLength = Strings.Len(strEncoded);
    //        while (Strings.Mid(strEncoded, iRealLength, 1) == "=")
    //        {
    //            iRealLength = iRealLength - 1;
    //        }

    //        //** Non standard extension to Base 64 decode to allow for + sign to space character substitution by **
    //        //** some web servers. Base 64 expects a +, not a space, so convert vack to + if space is found **
    //        while (Strings.InStr(strEncoded, " ") != 0)
    //        {
    //            strEncoded = Strings.Left(strEncoded, Strings.InStr(strEncoded, " ") - 1) + "+" + Strings.Mid(strEncoded, Strings.InStr(strEncoded, " ") + 1);
    //        }

    //        strReturn = "";
    //        //** Convert the base 64 4x6 byte values into 3x8 byte real values by reading 4 chars at a time **
    //        iBy4 = (iRealLength / 4) * 4;
    //        iIndex = 1;
    //        while (iIndex <= iBy4)
    //        {
    //            iFirst = ArrBase64DecMap[Strings.Asc(Strings.Mid(strEncoded, iIndex + 0, 1))];
    //            iSecond = ArrBase64DecMap[Strings.Asc(Strings.Mid(strEncoded, iIndex + 1, 1))];
    //            iThird = ArrBase64DecMap[Strings.Asc(Strings.Mid(strEncoded, iIndex + 2, 1))];
    //            iFourth = ArrBase64DecMap[Strings.Asc(Strings.Mid(strEncoded, iIndex + 3, 1))];
    //            strReturn = strReturn + Strings.Chr(((iFirst * 4) & 255) + ((iSecond / 16) & 3));
    //            strReturn = strReturn + Strings.Chr(((iSecond * 16) & 255) + ((iThird / 4) & 15));
    //            strReturn = strReturn + Strings.Chr(((iThird * 64) & 255) + (iFourth & 63));
    //            iIndex = iIndex + 4;
    //        }

    //        //** For non multiples of 4 characters, handle the = padding **
    //        if (iIndex < iRealLength)
    //        {
    //            iFirst = ArrBase64DecMap[Strings.Asc(Strings.Mid(strEncoded, iIndex + 0, 1))];
    //            iSecond = ArrBase64DecMap[Strings.Asc(Strings.Mid(strEncoded, iIndex + 1, 1))];
    //            strReturn = strReturn + Strings.Chr(((iFirst * 4) & 255) + ((iSecond / 16) & 3));
    //            if (iRealLength % 4 == 3)
    //            {
    //                iThird = ArrBase64DecMap[Strings.Asc(Strings.Mid(strEncoded, iIndex + 2, 1))];
    //                strReturn = strReturn + Strings.Chr(((iSecond * 16) & 255) + ((iThird / 4) & 15));
    //            }
    //        }

    //        functionReturnValue = strReturn;
    //        return functionReturnValue;
    //    }

    //    // ** The SimpleXor encryption algorithm. **
    //    // ** NOTE: This is a placeholder really.  Future releases of Sage Pay Form will use AES **
    //    // ** This simple function and the Base64 will deter script kiddies and prevent the "View Source" type tampering **
    //    public static string SimpleXor(string strIn, string strKey)
    //    {
    //        string functionReturnValue = null;
    //        int iInIndex = 0;
    //        int iKeyIndex = 0;
    //        string strReturn = null;
    //        if (Strings.Len(strIn) == 0 | Strings.Len(strKey) == 0)
    //        {
    //            functionReturnValue = "";
    //            return functionReturnValue;
    //        }

    //        iInIndex = 1;
    //        iKeyIndex = 1;
    //        strReturn = "";

    //        //** Step through the plain text source XORing the character at each point with the next character in the key **
    //        //** Loop through the key characters as necessary **
    //        while (iInIndex <= Strings.Len(strIn))
    //        {
    //            strReturn = strReturn + Strings.Chr(Strings.Asc(Strings.Mid(strIn, iInIndex, 1)) ^ Strings.Asc(Strings.Mid(strKey, iKeyIndex, 1)));
    //            iInIndex = iInIndex + 1;
    //            if (iKeyIndex == Strings.Len(strKey))
    //                iKeyIndex = 0;
    //            iKeyIndex = iKeyIndex + 1;
    //        }

    //        functionReturnValue = strReturn;
    //        return functionReturnValue;
    //    }

    //    private static string GetEncryptPassword(string connecToStr)
    //    {
    //        if (Strings.UCase(connecToStr) == Strings.UCase("TEST"))
    //        {
    //            return "GTkc35tGj7BALCMj";
    //        }
    //        else if (Strings.UCase(connecToStr) == Strings.UCase("LIVE"))
    //        {
    //            return "sd76QGrsteJAxBiD";
    //        }
    //        else if (Strings.UCase(connecToStr) == Strings.UCase("SIMULATOR"))
    //        {
    //            return "JIqoENoDbrGP4kyM";
    //        }
    //        else
    //        {
    //            return "JIqoENoDbrGP4kyM";
    //        }
    //    }


    //    //** Wrapper function do encrypt an encode based on strEncryptionType setting **
    //    public static string EncryptAndEncode(string strIn)
    //    {
    //        string functionReturnValue = null;
    //        if (StrEncryptionType == "XOR")
    //        {
    //            //** XOR encryption with Base64 encoding **
    //            functionReturnValue = Base64Encode(SimpleXor(strIn, GetEncryptPassword(SagePayConfig.TransValue)));
    //        }
    //        else
    //        {
    //            //** AES encryption, CBC blocking with PKCS5 padding then HEX encoding - DEFAULT **
    //            functionReturnValue = "@" + ByteArrayToHexString(AesEncrypt(strIn));
    //        }
    //        return functionReturnValue;
    //    }

    //    //** Wrapper function do decode then decrypt based on header of the encrypted field **
    //    public static string DecodeAndDecrypt(string strIn)
    //    {
    //        string functionReturnValue = null;
    //        if (Strings.Left(strIn, 1) == "@")
    //        {
    //            //** HEX decoding then AES decryption, CBC blocking with PKCS5 padding - DEFAULT **
    //            functionReturnValue = aesDecrypt(HexStringToBytes(strIn.Substring(1)));
    //        }
    //        else
    //        {
    //            //** Base 64 decoding plus XOR decryption **
    //            functionReturnValue = SimpleXor(Base64Decode(strIn), GetEncryptPassword(SagePayConfig.TransValue));
    //        }
    //        return functionReturnValue;
    //    }

    //    /// <summary>
    //    /// Encrypts input string using Rijndael (AES) algorithm with CBC blocking and PKCS7 padding.
    //    /// </summary>
    //    /// <param name="inputText">text string to encrypt </param>
    //    /// <returns>Encrypted text in Byte array</returns>
    //    /// <remarks>The key and IV are the same, and use strEncryptionPassword.</remarks>
    //    private static byte[] AesEncrypt(string inputText)
    //    {

    //        var aes = new RijndaelManaged();
    //        byte[] outBytes = null;

    //        //set the mode, padding and block size for the key
    //        aes.Padding = PaddingMode.PKCS7;
    //        aes.Mode = CipherMode.CBC;
    //        aes.KeySize = 128;
    //        aes.BlockSize = 128;

    //        //convert key and plain text input into byte arrays
    //        byte[] keyAndIvBytes = Encoding.UTF8.GetBytes(GetEncryptPassword(SagePayConfig.TransValue));
    //        byte[] inputBytes = Encoding.UTF8.GetBytes(inputText);

    //        //create streams and encryptor object
    //        MemoryStream memoryStream = new MemoryStream();
    //        CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(keyAndIvBytes, keyAndIvBytes), CryptoStreamMode.Write);

    //        //perform encryption
    //        cryptoStream.Write(inputBytes, 0, inputBytes.Length);
    //        cryptoStream.FlushFinalBlock();

    //        //get encrypted stream into byte array
    //        outBytes = memoryStream.ToArray();

    //        //close streams
    //        memoryStream.Close();
    //        cryptoStream.Close();
    //        aes.Clear();

    //        return outBytes;

    //    }

    //    /// <summary>
    //    /// Decrypts input string from Rijndael (AES) algorithm with CBC blocking and PKCS7 padding.
    //    /// </summary>
    //    /// <param name="inputBytes">Encrypted binary array to decrypt</param>
    //    /// <returns>string of Decrypted data</returns>
    //    /// <remarks>The key and IV are the same, and use strEncryptionPassword.</remarks>
    //    private static string aesDecrypt(byte[] inputBytes)
    //    {

    //        var aes = new RijndaelManaged();
    //        byte[] keyAndIvBytes = Encoding.UTF8.GetBytes(GetEncryptPassword(SagePayConfig.TransValue));
    //        byte[] outputBytes = new byte[inputBytes.Length + 1];

    //        //set the mode, padding and block size
    //        aes.Padding = PaddingMode.PKCS7;
    //        aes.Mode = CipherMode.CBC;
    //        aes.KeySize = 128;
    //        aes.BlockSize = 128;

    //        //create streams and decryptor object
    //        var memoryStream = new MemoryStream(inputBytes);
    //        var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(keyAndIvBytes, keyAndIvBytes), CryptoStreamMode.Read);

    //        //perform decryption
    //        cryptoStream.Read(outputBytes, 0, outputBytes.Length);

    //        //close streams
    //        memoryStream.Close();
    //        cryptoStream.Close();
    //        aes.Clear();

    //        //convert decryted data into string, assuming original text was UTF-8 encoded
    //        return Encoding.UTF8.GetString(outputBytes);

    //    }

    //    /// <summary>
    //    /// Converts a string of characters representing hexadecimal values into an array of bytes
    //    /// </summary>
    //    /// <param name="strInput">A hexadecimal string of characters to convert to binary.</param>
    //    /// <returns>A byte array</returns>
    //    public static byte[] HexStringToBytes(string strInput)
    //    {

    //        int numBytes = (strInput.Length / 2);
    //        byte[] bytes = new byte[numBytes];

    //        for (int x = 0; x <= numBytes - 1; x++)
    //        {
    //            bytes[x] = System.Convert.ToByte(strInput.Substring(x * 2, 2), 16);
    //        }

    //        return bytes;

    //    }

    //    /// <summary>
    //    /// Converts an array of bytes into a hexadecimal string representation.
    //    /// </summary>
    //    /// <param name="ba">Array of bytes to convert</param>
    //    /// <returns>String of hexadecimal values.</returns>
    //    public static string ByteArrayToHexString(byte[] ba)
    //    {

    //        return BitConverter.ToString(ba).Replace("-", "");

    //    }

    //    //** The getToken function. **
    //    //** NOTE: A function of convenience that extracts the value from the "name=value&name2=value2..." Sage Pay Form reply string **
    //    //** Does not use the split function because returned values can contain & or =, so it looks for known names and **
    //    //** removes those, leaving only the field selected ** 
    //    public static string GetToken(string strList, string strRequired)
    //    {
    //        string functionReturnValue = null;
    //        string[] arrTokens = new string[17];
    //        string strTokenValue = null;
    //        int iIndex = 0;

    //        arrTokens.SetValue("Status", 0);
    //        arrTokens.SetValue("StatusDetail", 1);
    //        arrTokens.SetValue("VendorTxCode", 2);
    //        arrTokens.SetValue("VPSTxId", 3);
    //        arrTokens.SetValue("TxAuthNo", 4);
    //        arrTokens.SetValue("Amount", 5);
    //        arrTokens.SetValue("AVSCV2", 6);
    //        arrTokens.SetValue("AddressResult", 7);
    //        arrTokens.SetValue("PostCodeResult", 8);
    //        arrTokens.SetValue("CV2Result", 9);
    //        arrTokens.SetValue("GiftAid", 10);
    //        arrTokens.SetValue("3DSecureStatus", 11);
    //        arrTokens.SetValue("CAVV", 12);
    //        arrTokens.SetValue("CardType", 13);
    //        arrTokens.SetValue("Last4Digits", 14);
    //        arrTokens.SetValue("PayerStatus", 15);
    //        arrTokens.SetValue("AddressStatus", 16);

    //        //** If the toekn we're after isn't in the list, return nothing **
    //        if (Strings.InStr(strList, strRequired + "=") == 0)
    //        {
    //            functionReturnValue = "";
    //            return functionReturnValue;
    //        }
    //        else
    //        {
    //            //** The token is present, so ignore everything before it in the list **    
    //            strTokenValue = Strings.Mid(strList, Strings.InStr(strList, strRequired) + Strings.Len(strRequired) + 1);

    //            //** Strip off all remaining tokens if they are present **
    //            iIndex = Information.LBound(arrTokens);
    //            while (iIndex <= Information.UBound(arrTokens))
    //            {
    //                if (arrTokens[iIndex] != strRequired)
    //                {
    //                    if (Strings.InStr(strTokenValue, "&" + arrTokens[iIndex]) != 0)
    //                    {
    //                        strTokenValue = Strings.Left(strTokenValue, Strings.InStr(strTokenValue, "&" + arrTokens[iIndex]) - 1);
    //                    }
    //                }
    //                iIndex = iIndex + 1;
    //            }

    //            functionReturnValue = strTokenValue;
    //        }
    //        return functionReturnValue;

    //    }

    //    //** Inspects and validates user input for a name field. Returns TRUE if input value is valid as a name field.
    //    //   Parameter "strInputValue" is the field value to validate.
    //    //   Parameter "returnedResult" sets a result to a value from enum FieldValidationResult.
    //    public static bool IsValidNameField(string strInputValue, ref FieldValidationResult returnedResult)
    //    {

    //        string strAllowableChars = " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-.'&\\";
    //        strInputValue = Strings.Trim(strInputValue);
    //        returnedResult = ValidateString(strInputValue, strAllowableChars, true, true, 20);
    //        if (returnedResult == FieldValidationResult.Valid)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    //** Inspects and validates user input for an Address field.
    //    //   Parameter "isRequired" specifies whether "strInputValue" must have a non-null and non-empty value.
    //    public static bool IsValidAddressField(string strInputValue, bool isRequired, ref FieldValidationResult returnedResult)
    //    {

    //        string strAllowableChars = " 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-.',/\\()&:+" + Constants.vbCr + Constants.vbLf;
    //        strInputValue = Strings.Trim(strInputValue);
    //        returnedResult = ValidateString(strInputValue, strAllowableChars, true, isRequired, 100);
    //        if (returnedResult == FieldValidationResult.Valid)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    //** Inspects and validates user input for a City field.
    //    public static bool IsValidCityField(string strInputValue, ref FieldValidationResult returnedResult)
    //    {

    //        string strAllowableChars = " 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-.',/\\()&:+" + Constants.vbCr + Constants.vbLf;
    //        strInputValue = Strings.Trim(strInputValue);
    //        returnedResult = ValidateString(strInputValue, strAllowableChars, true, true, 40);
    //        if (returnedResult == FieldValidationResult.Valid)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    //** Inspects and validates user input for a Postcode/zip field. 
    //    public static bool IsValidPostcodeField(string strInputValue, ref FieldValidationResult returnedResult)
    //    {

    //        string strAllowableChars = " 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-";
    //        strInputValue = Strings.Trim(strInputValue);
    //        returnedResult = ValidateString(strInputValue, strAllowableChars, false, true, 10);
    //        if (returnedResult == FieldValidationResult.Valid)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    //** Inspects and validates user input for an email field. 
    //    public static bool IsValidEmailField(string strInputValue, ref FieldValidationResult returnedResult)
    //    {

    //        //The allowable e-mail address format accepted by the SagePay gateway must be RFC 5321/5322 compliant (see RFC 3696)  
    //        var sEmailRegXPattern = "^[a-z0-9\\xC0-\\xFF!#$%&amp;'*+/=?^_`{|}~\\p{L}\\p{M}*-]+(?:\\.[a-z0-9\\xC0-\\xFF!#$%&amp;'*+/=?^_`{|}~\\p{L}\\p{M}*-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+(?:[a-z]{2,3}|com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|at|coop|travel)$";
    //        strInputValue = Strings.Trim(strInputValue);
    //        returnedResult = ValidateString(strInputValue, sEmailRegXPattern, false);
    //        if (returnedResult == FieldValidationResult.Valid)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    //** Inspects and validates user input for a phone field. 
    //    public static bool IsValidPhoneField(string strInputValue, ref FieldValidationResult returnedResult)
    //    {

    //        string strAllowableChars = " 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-()+";
    //        strInputValue = Strings.Trim(strInputValue);
    //        returnedResult = ValidateString(strInputValue, strAllowableChars, false, false, 20);
    //        if (returnedResult == FieldValidationResult.Valid)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    //** A generic function used to inspect and validate a string from user input.
    //    //   Parameter "strInputValue" is the value to perform validation on.
    //    //   Parameter "strAllowableChars" is a string of characters allowable in "strInputValue" if its to be deemed valid.
    //    //   Parameter "allowAccentedChars" determines if "strInputValue" can contain Accented or High-order characters.
    //    //   Parameter "isRequired" specifies whether "strInputValue" must have a non-null and non-empty value.
    //    //   Parameter "intMaxLength" specifies the maximum allowable length of "strInputValue".
    //    //   Parameter "intMinLength" specifies the miniumum allowable length of "strInputValue".
    //    //   Returns a result from enum FieldValidationResult. 
    //    private static FieldValidationResult ValidateString(string strInputValue, string strAllowableChars, bool allowAccentedChars, bool isRequired, int intMaxLength = -1, int intMinLength = 0)
    //    {


    //        if (isRequired == true & string.IsNullOrEmpty(strInputValue) == true)
    //        {
    //            return FieldValidationResult.InvalidRequiredInputValueMissing;


    //        }
    //        else if ((intMaxLength != -1) & (strInputValue.Length > intMaxLength))
    //        {
    //            return FieldValidationResult.InvalidMaximumLengthExceeded;


    //        }
    //        else if (strInputValue != CleanInput(strInputValue, strAllowableChars, allowAccentedChars))
    //        {
    //            return FieldValidationResult.InvalidBadCharacters;


    //        }
    //        else if ((isRequired == true) & (strInputValue.Length < intMinLength))
    //        {
    //            return FieldValidationResult.InvalidMinimumLengthNotMet;


    //        }
    //        else if ((isRequired == false) & (string.IsNullOrEmpty(strInputValue) == false) & (strInputValue.Length < intMinLength))
    //        {
    //            return FieldValidationResult.InvalidMinimumLengthNotMet;
    //        }
    //        else
    //        {
    //            return FieldValidationResult.Valid;
    //        }
    //    }

    //    //** A generic function to inspect and validate a string from user input based on a Regular Expression pattern.
    //    //   Parameter "strInputValue" is the value to perform validation on.
    //    //   Parameter "strRegExPattern" is a Regular Expression string pattern used to validate against "strInputValue".
    //    //   Parameter "isRequired" specifies whether "strInputValue" must have a non-null and non-empty value.
    //    //   Returns a result from enum FieldValidationResult. 
    //    private static FieldValidationResult ValidateString(string strInputValue, string strRegExPattern, bool isRequired)
    //    {


    //        if (isRequired == true & string.IsNullOrEmpty(strInputValue) == true)
    //        {
    //            return FieldValidationResult.InvalidRequiredInputValueMissing;


    //        }
    //        else if ((string.IsNullOrEmpty(strInputValue) == false) & (Regex.IsMatch(strInputValue.ToLower(), strRegExPattern) == false))
    //        {
    //            return FieldValidationResult.InvalidBadFormat;
    //        }
    //        else
    //        {
    //            return FieldValidationResult.Valid;
    //        }
    //    }

    //    //** Maps a FieldValidationResult value to a string representing a user friendly validation error message.
    //    //   Parameter "strFieldLabelName" is the display name of the form field to use in the returned message.
    //    public static string GetValidationMessage(FieldValidationResult validationCode, string strFieldLabelName)
    //    {
    //        string strReturn = "";

    //        switch (validationCode)
    //        {

    //            case FieldValidationResult.InvalidBadCharacters:
    //                strReturn = "Please correct " + strFieldLabelName + " as it contains disallowed characters.";

    //                break;
    //            case FieldValidationResult.InvalidBadFormat:
    //                strReturn = "Please correct " + strFieldLabelName + " as the format is invalid.";

    //                break;
    //            case FieldValidationResult.InvalidMinimumLengthNotMet:
    //                strReturn = "Please correct " + strFieldLabelName + " as the value is not long enough.";

    //                break;
    //            case FieldValidationResult.InvalidMaximumLengthExceeded:
    //                strReturn = "Please correct " + strFieldLabelName + " as the value is too long.";

    //                break;
    //            case FieldValidationResult.InvalidRequiredInputValueMissing:
    //                strReturn = "Please enter a value for " + strFieldLabelName + " where requested below.";

    //                break;
    //            case FieldValidationResult.InvalidRequiredInputValueNotSelected:
    //                strReturn = "Please select a value for " + strFieldLabelName + " where requested below.";
    //                break;
    //        }

    //        return strReturn;
    //    }

    //    //** Defines filter types used for a parameter in the cleanInput() function.
    //    public enum CleanInputFilterType
    //    {
    //        Alphabetic,
    //        AlphabeticAndAccented,
    //        AlphaNumeric,
    //        AlphaNumericAndAccented,
    //        Numeric,
    //        WidestAllowableCharacterRange
    //    }

    //    //** Defines a set of values used as outcomes in field validation functions such as isValidAddressField.
    //    public enum FieldValidationResult
    //    {
    //        Valid,
    //        Invalid,
    //        InvalidBadCharacters,
    //        InvalidBadFormat,
    //        InvalidMaximumLengthExceeded,
    //        InvalidMinimumLengthNotMet,
    //        InvalidRequiredInputValueMissing,
    //        InvalidRequiredInputValueNotSelected
    //    }
    //}
}